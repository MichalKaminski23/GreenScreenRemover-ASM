.code

removeGreenScreenASM proc
    ; Arguments
    ; RCX = array of pixels
    ; RDX = width of the image
    ; R8 = start row to process
    ; R9 = num rows to process

    MOV RCX, 0
    MOV RDX, 0
    MOV R8, 0
    MOV R9, 0

    ; Load the address of the first pixel
    MOV RAX, RCX    

    ret
removeGreenScreenASM endp

END

;.code

;removeGreenScreenASM proc
    ; Parametry funkcji:
    ; RCX - wskaŸnik do tablicy bajtów (pixels)
    ; RDX - szerokoœæ obrazu (width)
    ; R8  - pocz¹tkowy wiersz (startRow)
    ; R9  - liczba wierszy (numRows)

    ; Przyk³ad: iteracja po tablicy bajtów
    ; Zak³adamy, ¿e ka¿dy piksel to 3 bajty (RGB)
    ; i przetwarzamy ka¿dy piksel w podanym zakresie wierszy

    ; Oblicz offset pocz¹tkowego wiersza
 ;   mov rax, r8          ; rax = startRow
 ;   imul rax, rdx        ; rax = startRow * width
 ;   imul rax, 3          ; rax = startRow * width * 3 (rozmiar piksela w bajtach)
  ;  add rcx, rax         ; rcx = pixels + offset

    ; Oblicz liczbê bajtów do przetworzenia
  ;  mov rax, r9          ; rax = numRows
  ;  imul rax, rdx        ; rax = numRows * width
  ;  imul rax, 3          ; rax = numRows * width * 3 (rozmiar piksela w bajtach)

    ; Przetwarzaj piksele
;process_pixels:
    ; SprawdŸ, czy przetworzyliœmy wszystkie bajty
 ;   test rax, rax
 ;   jz done

    ; Przyk³ad: ustawienie wartoœci piksela na czarny (0, 0, 0)
   ; mov byte ptr [rcx], 0    ; Ustaw R
   ; mov byte ptr [rcx+1], 0  ; Ustaw G
   ; mov byte ptr [rcx+2], 0  ; Ustaw B

    ; PrzejdŸ do nastêpnego piksela
   ; add rcx, 3
   ; sub rax, 3
  ;  jmp process_pixels

;done:
 ;   ret  



;removeGreenScreenASM endp

;END