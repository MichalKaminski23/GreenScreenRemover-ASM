#include "pch.h"
#include <stdio.h>
#include <windows.h> 

extern "C" __declspec(dllexport) void removeGreenScreenC(int threads)
{
    char message[256];
    sprintf_s(message, "Message from function in C: Threads: %d, DLL Option: C", threads);

    MessageBoxA(NULL, message, "Info", MB_OK | MB_ICONINFORMATION);
}