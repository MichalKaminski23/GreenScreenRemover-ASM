using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace GreenScreenRemover
{
    public partial class Menu : Form
    {
        [DllImport(@"D:\OneDrive\STUDIA\ROK_3\JA\PROJEKT\JA_PROJECT\GreenScreenRemover\x64\Release\DLLASM.dll")]
        static extern int MyProc1(int a, int b);

        [DllImport(@"D:\OneDrive\STUDIA\ROK_3\JA\PROJEKT\JA_PROJECT\GreenScreenRemover\x64\Release\DLLC.dll")]
        static extern void removeGreenScreenC(byte threads);

        public Menu()
        {
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "Pictures (*.bmp)|*.bmp";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string fileName = Path.GetFileName(filePath);

                    if (Path.GetExtension(fileName).ToLower() != ".bmp")
                    {
                        MessageBox.Show("Please choose a .bmp file!");
                    }
                    else
                    {
                        MessageBox.Show("Chosen file: " + fileName);
                        beforePicture.Image = new Bitmap(filePath);
                        beforePicture.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            if (dllOption == 1)
            {
                //MessageBox.Show($"Threads: {threadSelected}, DLL: C");
                removeGreenScreenC(threadSelected);
            }
            else if (dllOption == 2)
            {
                MessageBox.Show($"Threads: {threadSelected}, DLL: ASM");
            }
        }
    }
}
