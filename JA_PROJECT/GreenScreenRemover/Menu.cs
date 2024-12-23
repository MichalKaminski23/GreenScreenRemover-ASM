﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GreenScreenRemover
{
    public partial class Menu : Form
    {
        private Bitmap bitmap;

        //Importing functions from external DLLs
        [DllImport(@"D:\OneDrive\STUDIA\ROK_3\JA\PROJEKT\JA_PROJECT\GreenScreenRemover\x64\Release\DLLASM.dll")]
        static extern void removeGreenScreenASM(byte[] pixels, int width, int startRow, int numRows);

        [DllImport(@"D:\OneDrive\STUDIA\ROK_3\JA\PROJEKT\JA_PROJECT\GreenScreenRemover\x64\Release\DLLC.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void removeGreenScreenC(byte[] pixels, int width, int startRow, int numRows);

        // Initialaze the menu
        public Menu()
        {
            InitializeComponent();
        }

        // Event handler for the exit button
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Event handler for the open file button
        private void openFileButton_Click(object sender, EventArgs e)
        {
            beforePicture.Visible = true;
            afterPicture.Visible = false;
            saveButton.Visible = false;
            timeTextLabel.Visible = false;
            timeResultLabel.Visible = false;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Choose a bmp or jpg picture";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg|Bitmap Image|*.bmp|JPEG Image|*.jpg;*.jpeg";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string fileName = Path.GetFileName(filePath);

                    if (Path.GetExtension(fileName).ToLower() != ".bmp" && Path.GetExtension(fileName).ToLower() != ".jpg" && Path.GetExtension(fileName).ToLower() != ".jpeg")
                    {
                        MessageBox.Show("Please choose a .bmp or .jpg picture!");
                    }
                    else
                    {
                        try
                        {
                            beforePicture.Image = new Bitmap(filePath);
                            beforePicture.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error loading image: " + ex.Message);
                        }
                    }
                }
            }
        }

        // Get the selected DLL option
        private byte getDllOption()
        {
            //if (cButton.Checked)
            //{
            //    return 1;
            //}
            //else if (asmButton.Checked)
            //{
            //    return 2;
            //}
            //else
            //{
            //    return 255;
            //}
            return 2;
        }

        // Get the selected thread option
        private byte getThreadOption()
        {
            //if (thread1.Checked)
            //{
            //    return 1;
            //}
            //else if (thread2.Checked)
            //{
            //    return 2;
            //}
            //else if (thread4.Checked)
            //{
            //    return 4;
            //}
            //else if (thread8.Checked)
            //{
            //    return 8;
            //}
            //else if (thread16.Checked)
            //{
            //    return 16;
            //}
            //else if (thread32.Checked)
            //{
            //    return 32;
            //}
            //else if (thread64.Checked)
            //{
            //    return 64;
            //}
            //else
            //{
            //    return 255;
            //}
            return 1;
        }

        // Event handler for the remove green screen button
        private void removeGreenScreenButton_Click(object sender, EventArgs e)
        {
            byte threadSelected = getThreadOption();
            byte dllOption = getDllOption();

            string testImagePath = @"D:\OneDrive\STUDIA\ROK_3\JA\PROJEKT\JA_PROJECT\GreenScreenRemover\2JPG.jpg";
            beforePicture.Image = new Bitmap(testImagePath);
            beforePicture.SizeMode = PictureBoxSizeMode.StretchImage;

            if (threadSelected == 255)
            {
                MessageBox.Show("Choose threads!");
                return;
            }
            if (dllOption == 255)
            {
                MessageBox.Show("Choose DLL!");
                return;
            }
            if (beforePicture.Image == null)
            {
                MessageBox.Show("Choose a picture!");
                return;
            }
            processImage(dllOption, threadSelected);
        }

        // Process the image to remove the green screen
        private void processImage(byte dllOption, byte threadsSelected)
        {
            try
            {
                // Load the image from the PictureBox into a Bitmap object
                bitmap = new Bitmap(beforePicture.Image);

                // Ensure the bitmap is in 24bpp RGB format
                if (bitmap.PixelFormat != PixelFormat.Format24bppRgb)
                {
                    // Clone the bitmap to convert it to 24bpp RGB format
                    bitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format24bppRgb);
                }

                // Lock the bitmap's bits for read/write access
                Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

                // Get the address of the first pixel data
                IntPtr ptr = bmpData.Scan0;

                // Declare an array to hold the bytes of the bitmap
                int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
                byte[] rgbValues = new byte[bytes];

                // Copy the RGB values into the array
                Marshal.Copy(ptr, rgbValues, 0, bytes);

                removeGreenScreenASM(rgbValues, bitmap.Width, 0, 0);

                // Start the stopwatch to measure processing time
                Stopwatch sw = new Stopwatch();
                sw.Start();

                // Create a list to hold the threads
                List<Thread> threads = new List<Thread>();
                int maxThreads = Math.Min(threadsSelected, Environment.ProcessorCount);
                
                // Create and start threads to process the image
                for (int i = 0; i < maxThreads; i++)
                {
                    // Calculate the starting row and number of rows for each thread
                    int startRow = i * (bitmap.Height / maxThreads);
                    int numRows = (i == maxThreads - 1) ? bitmap.Height - startRow : (bitmap.Height / maxThreads);

                    // var to store the index of the thread for debugging purposes
                    int threadIndex = i;
                    Thread thread = new Thread(() =>
                    {
                        try
                        {
                            // Call the appropriate DLL function based on the selected option
                            if (dllOption == 1)
                            {
                                removeGreenScreenC(rgbValues, bitmap.Width, startRow, numRows);
                            }
                            else if (dllOption == 2)
                            {
                                //removeGreenScreenASM(rgbValues, bitmap.Width, startRow, numRows);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Display an error message if an exception occurs in the thread
                            MessageBox.Show($"Error in thread {threadIndex}: {ex.Message}");
                        }
                    });
                    thread.Start();
                    threads.Add(thread);
                }

                // Wait for all threads to complete
                threads.ForEach(thread => thread.Join());

                // Stop the stopwatch
                sw.Stop();

                // Copy the modified RGB values back to the bitmap
                Marshal.Copy(rgbValues, 0, ptr, bytes);
                bitmap.UnlockBits(bmpData);

                // Update the UI to show the processed image
                beforePicture.Visible = false;
                afterPicture.Visible = true;
                afterPicture.Image = bitmap;
                afterPicture.SizeMode = PictureBoxSizeMode.StretchImage;

                // Display the processing time
                timeTextLabel.Visible = true;
                timeResultLabel.Visible = true;
                timeResultLabel.Text = sw.ElapsedMilliseconds.ToString() + " ms";

                // Enable the save button and attach the click event handler
                saveButton.Visible = true;
                saveButton.Click -= new EventHandler(saveButton_Click);
                saveButton.Click += new EventHandler(saveButton_Click);
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Event handler for the save button
        private void saveButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg|Bitmap Image|*.bmp|JPEG Image|*.jpg;*.jpeg";
                saveFileDialog.Title = "Save the modified image";
                saveFileDialog.FileName = "modified_image";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    string fileName = Path.GetFileName(filePath);

                    if (Path.GetExtension(fileName).ToLower() == ".bmp")
                    {
                        bitmap.Save(saveFileDialog.FileName, ImageFormat.Bmp);
                        MessageBox.Show("Image saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (Path.GetExtension(fileName).ToLower() == ".jpg" || Path.GetExtension(fileName).ToLower() == ".jpeg")
                    {
                        bitmap.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                        MessageBox.Show("Image saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Please choose a .bmp or .jpg picture!");
                    }
                }
            }
        }
    }
}