tolerance EQU 50 ; tolerance for green color
minGreen EQU 100 ; minimum green value

.code
;Arguments (Windows x64 calling convention):
;RCX = (unsigned char*) pixels
;RDX = (int) width
;R8 = (int) startRow
;R9 = (int) numRows
removeGreenScreenASM proc

; calculate realStride
MOV R10, RDX ; move width (RDX) to realStride (R10)
IMUL R10, 3 ; realStride = width * 3

; calculate endRow
ADD R9, R8 ; add startRow (R8) to numRows (R9) 
MOV R11, R9 ; move numRows (R9) to R11

rowLoop:
    CMP R8, R11 ; check if R8 (y) == R11 (startRow + numRows)
    JGE endLoop ; if R8 >= R11, jump to endLoop
    MOV R12, 0 ; clear R12 (x)

columnLoop:
    CMP R12, R10 ; check if R12 >= R10
    JGE nextRow ; if R12 >= R10, jump to nextRow

    ; y * realStride
    MOV RAX, R10 ; move realStride (R10) to RAX
    MOV RBX, R8 ; move y (R8) to RBX
    IMUL RAX, RBX ; RAX = RBX * R10 = y * realStride

    ; y * realStride + x
    MOV RBX, R12 ; move x (R12) to RBX
    ADD RAX, RBX ; RAX = RAX + RBX = y * realStride + x
    MOV R13, RAX ; move index (RAX) to R13

    MOV R14, 0 ; clear R14

    MOV AL, [RCX + R13]     ; load blue value
    MOV BL, [RCX + R13 + 1] ; load green value
    MOV SIL, [RCX + R13 + 2] ; load red value

    ; check if green value is >= than minGreen
    CMP BL, minGreen
    JB skipPixel ; JB = jump if below (unsigned compare) - if green value < minGreen, jump to skipPixel

    ; check if red value is <= than green value + tolerance
    MOVZX R14, BL ; move green value to R14
    SUB R14B, tolerance ; subtract tolerance from green value
    CMP SIL, R14B ; compare red value with green value - tolerance
    JA skipPixel ; JA = jump if above (unsigned) - if red value > green value - tolerance, jump to skipPixel

    ; check if blue value is <= than green value + tolerance
    CMP AL, R14B ; compare blue value with green value - tolerance
    JA skipPixel ; if blue value > green value - tolerance, jump to skipPixel

    MOV BYTE PTR [RCX + R13], 255      ; B = 255
    MOV BYTE PTR [RCX + R13 + 1], 255  ; G = 255
    MOV BYTE PTR [RCX + R13 + 2], 255  ; R = 255

skipPixel:
    ADD R12, 3 ; add 3 to x 
    JMP columnLoop ; jump to columnLoop

nextRow:
    INC R8 ; increment y
    JMP rowLoop ; jump to rowLoop

endLoop:
    RET ; return

removeGreenScreenASM endp
END