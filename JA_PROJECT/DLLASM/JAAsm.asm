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

; save width in R13
MOV R13, RDX ; move width (RDX) to R13

; calculate stride
MOV RAX, RDX ; move width (RDX) to RAX
MOVZX RBX, strideMultiplier ; move strideMultiplier to RBX
MUL RBX ; RAX = RDX * strideMultiplier
MOV R10, RAX ; move stride (RAX) to R10 

; calculate endRow
ADD R9, R8 ; add startRow (R8) to numRows (R9) 
MOV R11, R9 ; move numRows (R9) to R11

rowLoop:
    CMP R8, R11 ; check if R8 (y) == R11 (startRow + numRows))
    JE endLoop ; if R8 == R11, jump to endLoop
    XOR R12, R12 ; clear R12 (x)

columnLoop:
    CMP R12, R13 ; check if R12 == R13
    JE nextRow ; if R12 == R13, jump to nextRow
    ; else calculate index -> int index = y * stride + x * 3 ;
    MOV RAX, R10 ; move stride (R10) to RAX
    MOV RBX, R8 ; move y (R8) to RBX
    MUL RBX ; RAX = R10 * RBX
    PUSH RAX ; save RAX to stack -> y * stride

    MOV RAX, R12 ; move x (R12) to RAX
    MOVZX RBX, strideMultiplier ; move strideMultiplier to RBX
    MUL RBX ; RAX = R12 * strideMultiplier
    MOV R14, RAX ; move x * 3 (RAX) to R14
    POP RAX ; pop y * stride (RAX) from stack
    ADD RAX, R14 ; RAX = y * stride + x * 3
    MOV R14, RAX ; move index (RAX) to R14

    ; get pixel



    INC R12 ; increment x
    JMP columnLoop ; jump to columnLoop

nextRow:
    INC R8 ; increment y
    JMP rowLoop ; jump to rowLoop

endLoop:
        RET
removeGreenScreenASM endp
END