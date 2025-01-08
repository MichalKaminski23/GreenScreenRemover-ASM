;--------------------------------------
;Project:     Green screen remover
;
;Description: The algorithm takes the address of the pixel array, 
;             then iterates over each pixel byte by byte and compares whether green is green (100), 
;             red is less than green and tolerance (50) and so is blue. 
;             If the conditions are met the pixel is changed to white (255), 
;             otherwise the pixel is skipped and the next pixel is checked, until the end of the array.
;
;Author/Studies: Micha³ Kamiñski, INF AEI Gliwice, year 3 semester 5
;
;Versions:   0.1: Checking if function parameters are passed correctly
;            0.2: First steps with calculating stride and index
;            0.3: Index works but not properly because sometimes there is a problem with a stack
;            0.4: Added tolerance and minGreen to check values of pixels and later to change or not them
;            0.5: Removed problems with stack and changed a bit the way to calculate index (for the better)
;            0.6: added a 5th parameter realStride
;            0.7: removed a realStirde parameter and changed index calculate and changed INC R12 to ADD R12, 3
;            1.0: Everything works
;--------------------------------------

;--------------------------------------
;removeGreenScreenASM - removes green screen from image loaded by the user
;Inputs:
;   RCX = (unsigned char*) pixels
;   RDX = (int) width
;   R8 = (int) startRow
;   R9 = (int) numRows
;Outputs:
;   RCX + R13 = (unsigned char*) pixels with removed green screen
;Registers used:
;   R10 - realStride
;   R11 - endRow
;   R12 - x
;   R13 - index
;   R14B - for comparing bytes with green value
;   RAX, RBX - for calculations
;   AL, BL, SIL - for loading pixel values
;--------------------------------------
.code
removeGreenScreenASM proc

; const values
tolerance EQU 50 ; tolerance for green color
minGreen EQU 100 ; minimum green value

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
    ADD R12, 3 ; add 3 to x - next pixel (3 bytes)
    JMP columnLoop ; jump to columnLoop

nextRow:
    INC R8 ; increment y
    JMP rowLoop ; jump to rowLoop

endLoop:
    RET ; return

removeGreenScreenASM endp
END