#include "pch.h"

extern "C" __declspec(dllexport) void removeGreenScreenC(unsigned char* pixels, int width, int startRow, int numRows, int stride)
{
    // Define the tolerance for red and blue values
    const int tolerance = 50;

    // Define the minimum value for green to be considered as green screen
    const int minGreen = 100;

    // Loop through the rows of the image, starting from startRow and processing numRows rows
    for (int y = startRow; y < startRow + numRows; ++y)
    {
        // Loop through the columns of the image
        for (int x = 0; x < width; ++x)
        {
            // Calculate the index of the current pixel in the pixel array
            int index = y * stride + x * 3;

            // Retrieve the color values (B, G, R) of the current pixel
            unsigned char blue = pixels[index];
            unsigned char green = pixels[index + 1];
            unsigned char red = pixels[index + 2];

            // Check if the pixel is green (green > minGreen, red < green - tolerance, blue < green - tolerance)
            if (green >= minGreen && red <= green - tolerance && blue <= green - tolerance)
            {
                // Set the pixel to white (B, G, R = 255)
                pixels[index] = 255;       // B
                pixels[index + 1] = 255;   // G
                pixels[index + 2] = 255;   // R
            }
        }
    }
}