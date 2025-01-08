#include "pch.h"
//---------------------------------------
//Project:     Green screen remover
//
//Description: The algorithm takes the address of the pixel array,
//             then iterates over each pixel byte by byte and compares whether green is green(100),
//             red is less than green and tolerance(50) and so is blue.
//             If the conditions are met the pixel is changed to white(255),
//             otherwise the pixel is skipped and the next pixel is checked, until the end of the array.
//
//Author / Studies: Micha³ Kamiñski, INF AEI Gliwice, year 3 semester 5
//
//Versions:   0.1: Initial version (without multithreading) - rest works
//            0.2: Added multithreading
//            0.3: Changed the way the index is calculated
//            1.0: Everything works
//---------------------------------------

//--------------------------------------
//removeGreenScreenC - removes green screen from image loaded by the user
//Inputs:
//  (unsigned char*) pixels
//  (int) width
//  (int) startRow
//  (int) numRows
//Outputs:
// (unsigned char*) pixels with removed green screen
// --------------------------------------
extern "C" __declspec(dllexport) void removeGreenScreenC(unsigned char* pixels, int width, int startRow, int numRows)
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
            int index = (y * width + x) * 3;

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
