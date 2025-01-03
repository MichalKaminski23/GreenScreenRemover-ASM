tolerance EQU 50 ; tolerance value
minGreen EQU 100 ; min value of green color

.data
    minGreenVector db 16 dup(minGreen) ; minGreenVector with 16 elements of minGreen
    greenPlusToleranceVector db 16 dup(minGreen + tolerance) ; greenPlusToleranceVector with 16 elements of minGreen + tolerance
    whiteVector db 16 dup(255) ; whiteVector with 16 elements of 255

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

    ; x * 4
    MOV RBX, R12 ; move x (R12) to RBX
    IMUL RBX, 4 ; RBX = R12 * 4 = x * 4
    ADD RAX, RBX ; RAX = y * realStride + x * 4
    MOV R13, RAX ; move index (RAX) to R13

    LEA R14, [RCX + R13] ; load address of pixels + index to R14
        
    MOVDQU XMM0, [R14] ; load 16 bytes to xmm0   

    MOVDQU XMM1, [R14 + 1] ; load next 16 bytes to xmm1
    PSHUFB XMM1, [minGreenVector] ; apply minGreenVector mask

    MOVDQU [R14], XMM1 ; store result in pixels

skipPixel:
    ADD R12, 4 ; add 4 to x (R12) 
    JMP columnLoop ; jump to columnLoop

nextRow:
    INC R8 ; increment y
    JMP rowLoop ; jump to rowLoop

endLoop:
    RET ; return

removeGreenScreenASM endp
END