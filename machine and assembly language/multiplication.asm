TITLE Multiplication of 3 32-Bit Numbers
; Author: Olorunfemi-Ojo Tomiwa

.386
.MODEL FLAT, STDCALL
.STACK
ExitProcess PROTO, dwExitCode:DWORD

.DATA
	val1 DWORD 3
	val2 DWORD 4
	val3 DWORD 5
.CODE
main PROC
	MOV EAX, val1	; eax = val1
	MUL val2    	; eax = eax * val2
	MUL val3    	; eax = eax * val3

	INVOKE ExitProcess, 0

main ENDP

END main
