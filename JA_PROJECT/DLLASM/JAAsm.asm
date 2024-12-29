.data
tolerance DB 50 ; tolerance for green color
minGreen DB 100 ; minimum green value
strideMultiplier DW 3 ; 3 bytes per pixel

.code
removeGreenScreenASM proc
; Arguments:
; RCX = pixels
; RDX = width
; R8 = startRow
; R9 = numRows

MOV RAX, RDX ; move width (RDX) to RAX
MOVZX RBX, strideMultiplier ; move strideMultiplier to RBX
MUL RBX ; RAX = RDX * strideMultiplier
MOV R10, RAX ; move stride (RAX) to R10 

ADD R9, R8 ; add startRow (R8) to numRows (R9) 
MOV R11, R9 ; move numRows (R9) to R11

rowLoop:
    CMP R8, R11 ; check if R8 == R11
    JE endLoop ; if R8 == R11, jump to endLoop
    INC R8 ; increment R8
    MOV R12, 0 ; R12 = 0 - column counter
    JMP columnLoop ; jump to columnLoop


columnLoop:
    CMP R12, RDX ; check if R12 == RDX
    JE rowLoop ; if R12 == RDX, jump to rowLoop

    ; int index = y * stride + x * 3 ;
    MOV RAX, R10 ; RAX = R10 = stride
;    MOVZX RBX, R8 ; RBX = R8 = y


    

endLoop:
        RET
removeGreenScreenASM endp
END