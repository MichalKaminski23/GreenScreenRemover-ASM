.data
minGreen db 100

tolerance db 50

white db 255

adjustMask db 128

.code
;Arguments (Windows x64 calling convention):
;RCX = (unsigned char*) pixels
;RDX = (int) width
;R8 = (int) startRow
;R9 = (int) numRows
;[RSP+40] = (int) realStride
removeGreenScreenASM proc

MOV R10, [RSP + 40] ; move realStride (RSP + 40) to R10

; calculate endRow
ADD R9, R8 ; add startRow (R8) to numRows (R9) 
MOV R11, R9 ; move numRows (R9) to R11

    VPBROADCASTB YMM0, BYTE PTR [adjustMask]  ; Load adjust mask (128)
    VPBROADCASTB YMM1, BYTE PTR [minGreen]    ; Broadcast minGreen to all elements
    VPXOR YMM1, YMM1, YMM0                    ; Convert minGreen to signed
    VPBROADCASTB YMM2, BYTE PTR [tolerance]   ; Broadcast tolerance
    VPBROADCASTB YMM3, BYTE PTR [white]       ; Broadcast white color

rowLoop:
    CMP R8, R11 ; check if R8 (y) == R11 (startRow + numRows)
    JGE endLoop ; if R8 >= R11, jump to endLoop
    MOV R12, 0 ; clear R12 (x)

columnLoop:
    CMP R12, RDX ; check if R12 >= RDX (width)
    JGE nextRow ; if R12 >= R13, jump to nextRow

    ; y * realStride
    MOV RAX, R10 ; move realStride (R10) to RAX
    MOV RBX, R8 ; move y (R8) to RBX
    IMUL RAX, RBX ; RAX = RBX * R10 = y * realStride

    ; x * 3
    MOV RBX, R12 ; move x (R12) to RBX
    IMUL RBX, 3 ; RBX = R12 * 3 = x * 3
    ADD RAX, RBX ; RAX = y * realStride + x * 3
    MOV R13, RAX ; move index (RAX) to R13

    VMOVDQU YMM4, YMMWORD PTR [RCX + R13] ; Load 32 bytes (10 pixels) from memory into YMM4

    ; Extract green channel directly from BGRBGR...
    VPSRLDQ   YMM5, YMM4, 1                    ; Shift right by 1 byte (green channel starts at 1st byte)
    VPAND YMM5, YMM5, YMMWORD PTR [RCX + R13] ; Mask only green (if needed)
    ;VPSHUFB YMM5, YMM4, YMMWORD PTR [RCX + R13 + 1]
    ;VPXOR YMM5, YMM5, YMM0                   ; Convert green channel to signed
    VPCMPGTB YMM6, YMM5, YMM1                ; Compare green with minGreen

    VPSUBB YMM7, YMM5, YMM2                  ; G - tolerance

    ; Extract red channel directly from BGRBGR...
    VPSRLDQ  YMM8, YMM4, 2                    ; Shift right by 2 bytes (red channel starts at 2nd byte)
    VPAND YMM8, YMM8, YMMWORD PTR [RCX + R13] ; Mask only red (if needed)
    ;VPSHUFB YMM8, YMM4, YMMWORD PTR [RCX + R13 + 2] ; Red channel
    ;VPXOR YMM8, YMM8, YMM0                   ; Convert red channel to signed

    VPCMPGTB YMM9, YMM8, YMM7               ; Compare red with (G - tolerance)

     ; Extract blue channel directly from BGRBGR...
    VPSRLDQ  YMM10, YMM4, 0                    ; Shift right by 0 bytes (blue channel starts at 0th byte)
    VPAND YMM10, YMM10, YMMWORD PTR [RCX + R13] ; Mask only blue (if needed)
    ;VPSHUFB YMM10, YMM4, YMMWORD PTR [RCX + R13] ; Blue channel

    ;VPXOR YMM10, YMM10, YMM0                   ; Convert blue channel to signed

    VPCMPGTB YMM11, YMM10, YMM7              ; Compare blue with (G - tolerance)

    ; Combine conditions
    VPAND YMM12, YMM6, YMM9                 ; G >= minGreen AND R <= (G - tolerance)
    VPAND YMM12, YMM12, YMM11                ; AND B <= (G - tolerance)

    ; Set pixel to white where condition is true
    VPAND YMM14, YMM12, YMM3                 ; Extract white where mask is true
    VPANDN YMM15, YMM12, YMM4                ; Extract original pixels where mask is false
    VPOR YMM4, YMM14, YMM15                  ; Combine results

    ; Store the processed pixel back to memory
    VMOVDQU YMMWORD PTR [RCX + R13], YMM4


    
    ; Increment the column index by 10 (process next 10 pixels)
    ADD r12, 10
    JMP columnLoop

nextRow:
    INC R8 ; increment y
    JMP rowLoop ; jump to rowLoop

endLoop:
    RET ; return

removeGreenScreenASM endp
END