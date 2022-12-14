TITLE Detecting the highest of 2 numbers
; Author: Olorunfemi-Ojo Daniel Tomiwa 190805503

.386
.MODEL FLAT, STDCALL
.STACK
ExitProcess PROTO, dwExitCode:DWORD

.DATA
	val1 DWORD 74
	val2 DWORD 10
.CODE
main PROC
		MOV EAX, val1	
		MOV EBX, val2
		CMP EAX, EBX	
		JA firstnum
		MOV ECX, EBX	;  copy EBX to ECX if higher
		JMP finish
	firstnum:
		MOV ECX, EAX	; copy EAX to ECX if higher
	finish:
		INVOKE ExitProcess, 0

main ENDP

END main
