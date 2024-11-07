#include "pch.h"

extern "C" __declspec(dllexport) void removeGreenScreenC(unsigned char* pixels, int width, int height, int startRow, int numRows)
{
    /*char message[256];
    sprintf_s(message, "Hi from function in C");
    MessageBoxA(NULL, message, "Info", MB_OK | MB_ICONINFORMATION);*/

    int stride = width * 4;

    for (int y = startRow; y < startRow + numRows; ++y)
    {
        for (int x = 0; x < width; ++x)
        {
            int index = y * stride + x * 4;

            unsigned char blue = pixels[index];
            unsigned char green = pixels[index + 1];
            unsigned char red = pixels[index + 2];
            unsigned char alpha = pixels[index + 3];

            if (green > 100 && red < 100 && blue < 100)
            {
                pixels[index] = 255;      // B
                pixels[index + 1] = 255;  // G
                pixels[index + 2] = 255;  // R
                pixels[index + 3] = 0; // A
            }
        }
    }
}

