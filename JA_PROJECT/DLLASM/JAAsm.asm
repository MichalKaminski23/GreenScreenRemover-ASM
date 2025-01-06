tolerance EQU 50 ; tolerance for green color
minGreen EQU 100 ; minimum green value

.data
whiteVector db 16 dup(255) ; white vector
minGreenVector db 16 dup(100) ; minimum green value vector
vector128 db 16 dup(128) ; 128 vector
testData db 101, 101, 101, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 200, ; test data
            16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31,
            32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47

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

MOVDQU XMM1, XMMWORD PTR [minGreenVector]
MOVDQU XMM2, XMMWORD PTR [vector128]

;PSUBB XMM1, XMM2 ; subtract 128 from all values

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

    MOVDQU XMM0, [RCX + R13] ; pobieranie 16 bajtów z pamiêci do rejestru XMM0 
    PSUBSB  XMM0, XMM2 ; jeœli dobrze kumam to trzeba to przekszta³ciæ na wartoœci ze znakiem bo na takich wartoœciach dzia³a porównywanie 

    PCMPGTB XMM0, XMM1 ; zamys³ mam taki ¿eby porównaæ wszystkie 16 bajtów z zielonym kolorem i potem coœ na tym robiæ 

    ;LEA RCX, testData ; tutaj testy ¿eby dobrze zrozumieæ jak to przeskakuje i tak dalej
   ; MOVDQU XMM0, [RCX+R13]
   ; PADDUSB XMM0, XMM2 zamieniæ na inne wartoœci bo jest przek³amane
   ; PCMPGTB XMM0, XMM1 ; tests

    ;MOVDQU XMM0, XMMWORD PTR[whiteVector] ; for test load white vector

    MOVDQU [RCX + R13], XMM0 ; store 16 bytes to memory

    ADD R12, 16; add 16 to x 
    JMP columnLoop ; jump to columnLoop

nextRow:
    INC R8 ; increment y
    JMP rowLoop ; jump to rowLoop

endLoop:
    RET ; return

removeGreenScreenASM endp
END

;Tutaj asm prosty
;tolerance EQU 50 ; tolerance for green color
;minGreen EQU 100 ; minimum green value

;.code
;Arguments (Windows x64 calling convention):
;RCX = (unsigned char*) pixels
;RDX = (int) width
;R8 = (int) startRow
;R9 = (int) numRows
;removeGreenScreenASM proc

; calculate realStride
;MOV R10, RDX ; move width (RDX) to realStride (R10)
;IMUL R10, 3 ; realStride = width * 3

; calculate endRow
;ADD R9, R8 ; add startRow (R8) to numRows (R9) 
;MOV R11, R9 ; move numRows (R9) to R11

;rowLoop:
;    CMP R8, R11 ; check if R8 (y) == R11 (startRow + numRows)
;    JGE endLoop ; if R8 >= R11, jump to endLoop
;    MOV R12, 0 ; clear R12 (x)

;columnLoop:
;    CMP R12, R10 ; check if R12 >= R10
;    JGE nextRow ; if R12 >= R10, jump to nextRow

    ; y * realStride
;    MOV RAX, R10 ; move realStride (R10) to RAX
;    MOV RBX, R8 ; move y (R8) to RBX
;    IMUL RAX, RBX ; RAX = RBX * R10 = y * realStride

    ; y * realStride + x
;    MOV RBX, R12 ; move x (R12) to RBX
;    ADD RAX, RBX ; RAX = RAX + RBX = y * realStride + x
;    MOV R13, RAX ; move index (RAX) to R13

;    MOV R14, 0 ; clear R14

;    MOV AL, [RCX + R13]     ; load blue value
;    MOV BL, [RCX + R13 + 1] ; load green value
;    MOV SIL, [RCX + R13 + 2] ; load red value

    ; check if green value is >= than minGreen
;    CMP BL, minGreen
;    JB skipPixel ; JB = jump if below (unsigned compare) - if green value < minGreen, jump to skipPixel

    ; check if red value is <= than green value + tolerance
;    MOVZX R14, BL ; move green value to R14
;    SUB R14B, tolerance ; subtract tolerance from green value
;    CMP SIL, R14B ; compare red value with green value - tolerance
;    JA skipPixel ; JA = jump if above (unsigned) - if red value > green value - tolerance, jump to skipPixel

    ; check if blue value is <= than green value + tolerance
;    CMP AL, R14B ; compare blue value with green value - tolerance
;    JA skipPixel ; if blue value > green value - tolerance, jump to skipPixel

;    MOV BYTE PTR [RCX + R13], 255      ; B = 255
;    MOV BYTE PTR [RCX + R13 + 1], 255  ; G = 255
;    MOV BYTE PTR [RCX + R13 + 2], 255  ; R = 255

;skipPixel:
;    ADD R12, 3 ; add 3 to x 
;    JMP columnLoop ; jump to columnLoop

;nextRow:
;    INC R8 ; increment y
;    JMP rowLoop ; jump to rowLoop

;endLoop:
;    RET ; return

;removeGreenScreenASM endp
;END