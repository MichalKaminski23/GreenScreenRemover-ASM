using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreenScreenRemover
{
    public partial class Menu : Form
    {
        private Bitmap bitmap;

        [DllImport(@"D:\OneDrive\STUDIA\ROK_3\JA\PROJEKT\JA_PROJECT\GreenScreenRemover\x64\Release\DLLASM.dll")]
        static extern int MyProc1(int a, int b);

        [DllImport(@"D:\OneDrive\STUDIA\ROK_3\JA\PROJEKT\JA_PROJECT\GreenScreenRemover\x64\Release\DLLC.dll")]
        static extern void removeGreenScreenC(byte[] pixels, int width, int height, int startRow, int numRows);

        public Menu()
        {
            InitializeComponent();
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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

        private byte getDllOption()
        {
            if (cButton.Checked)
            {
                return 1;
            }
            else if (asmButton.Checked)
            {
                return 2;
            }
            else
            {
                return 255;
            }
        }

        private byte getThreadOption()
        {
            if (thread1.Checked)
            {
                return 1;
            }
            else if (thread2.Checked)
            {
                return 2;
            }
            else if (thread4.Checked)
            {
                return 4;
            }
            else if (thread8.Checked)
            {
                return 8;
            }
            else if (thread16.Checked)
            {
                return 16;
            }
            else if (thread32.Checked)
            {
                return 32;
            }
            else if (thread64.Checked)
            {
                return 64;
            }
            else
            {
                return 255;
            }
        }

        private void removeGreenScreenButton_Click(object sender, EventArgs e)
        {
            byte threadSelected = getThreadOption();
            byte dllOption = getDllOption();

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

            bitmap = new Bitmap(beforePicture.Image);

            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
            {
                bitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppArgb);
            }

            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(ptr, rgbValues, 0, bytes);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            if (dllOption == 1)
            {
                List<Thread> threads = new List<Thread>();
                int maxThreads = Math.Min(threadSelected, Environment.ProcessorCount);

                for (int i = 0; i < maxThreads; i++)
                {
                    int startRow = i * (bitmap.Height / maxThreads);
                    int numRows = (i == maxThreads - 1) ? bitmap.Height - startRow : (bitmap.Height / maxThreads);

                    Thread thread = new Thread(() =>
                    {
                        removeGreenScreenC(rgbValues, bitmap.Width, bitmap.Height, startRow, numRows);
                        MessageBox.Show($"Thread {i} finished processing rows {startRow} to {startRow + numRows}");
                    });
                    threads.Add(thread);
                    thread.Start();
                }

                foreach (var thread in threads)
                {
                    thread.Join();
                }
            }
            else if (dllOption == 2)
            {
                // Implementacja dla removeGreenScreenASM
            }

            sw.Stop();

            Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(bmpData);

            beforePicture.Visible = false;
            afterPicture.Visible = true;
            afterPicture.Image = bitmap;
            afterPicture.SizeMode = PictureBoxSizeMode.StretchImage;

            timeTextLabel.Visible = true;
            timeResultLabel.Visible = true;
            timeResultLabel.Text = sw.ElapsedMilliseconds.ToString() + " ms";

            saveButton.Visible = true;
            saveButton.Click -= new EventHandler(saveButton_Click);
            saveButton.Click += new EventHandler(saveButton_Click);
        }

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


//private void processImage(byte dllOption, byte threadsSelected)
//{
//    Stopwatch sw = new Stopwatch();
//    sw.Start();

//    bitmap = new Bitmap(beforePicture.Image);

//    if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
//    {
//        bitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppArgb);
//    }

//    Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
//    BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

//    IntPtr ptr = bmpData.Scan0;
//    int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
//    byte[] rgbValues = new byte[bytes];

//    Marshal.Copy(ptr, rgbValues, 0, bytes);

//    try
//    {
//        Parallel.For(0, threadsSelected, i =>
//        {
//            int rowsPerThread = bitmap.Height / threadsSelected;
//            int startRow = i * rowsPerThread;
//            int numRows = (i == threadsSelected - 1) ? bitmap.Height - startRow : rowsPerThread;

//            if (dllOption == 1)
//            {
//                removeGreenScreenC(rgbValues, bitmap.Width, bitmap.Height, startRow, numRows);
//            }
//            else if (dllOption == 2)
//            {
//                MyProc1(startRow, numRows);
//            }
//        });
//    }
//    catch (Exception ex)
//    {
//        MessageBox.Show("Error: " + ex.Message);
//    }

//    Marshal.Copy(rgbValues, 0, ptr, bytes);
//    bitmap.UnlockBits(bmpData);

//    beforePicture.Visible = false;
//    afterPicture.Visible = true;
//    afterPicture.Image = bitmap;
//    afterPicture.SizeMode = PictureBoxSizeMode.StretchImage;

//    timeTextLabel.Visible = true;
//    timeResultLabel.Visible = true;
//    timeResultLabel.Text = sw.ElapsedMilliseconds.ToString() + " ms";

//    saveButton.Visible = true;
//    saveButton.Click -= new EventHandler(saveButton_Click);
//    saveButton.Click += new EventHandler(saveButton_Click);
//    sw.Stop();
//}
//private void removeGreenScreenButton_Click(object sender, EventArgs e)
//{
//    byte threadsSelected = getThreadOption();
//    byte dllOption = getDllOption();

//    if (threadsSelected == 255)
//    {
//        MessageBox.Show("Choose threads!");
//        return;
//    }
//    if (threadsSelected == 255)
//    {
//        MessageBox.Show("Choose DLL!");
//        return;
//    }
//    if (beforePicture.Image == null)
//    {
//        MessageBox.Show("Choose a picture!");
//        return;
//    }
//    processImage(dllOption, threadsSelected);
//}