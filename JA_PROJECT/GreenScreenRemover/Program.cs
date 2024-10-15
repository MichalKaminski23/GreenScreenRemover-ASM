using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreenScreenRemover
{
    internal static class Program
    {
        [DllImport(@"C:\Users\placu\GreenScreenRemover-ASM\JA_PROJECT\GreenScreenRemover\x64\Release\DLLASM.dll")]
        static extern int MyProc1(int a, int b);

        [DllImport(@"C:\Users\placu\GreenScreenRemover-ASM\JA_PROJECT\GreenScreenRemover\x64\Release\DLLC.dll")]
        static extern int adding(int a, int b);

        [STAThread]
        static void Main()
        {
            int x = 5, y = 3;
            int retVal = MyProc1(x, y);
            Console.Write("DLL ASM wynik dodawania: ");
            Console.WriteLine(retVal);

            Console.Write("DLL C wynik dodawania: ");
            Console.WriteLine(adding(x, y));

            Application.EnableVisualStyles();
            Application.DoEvents();
            Application.Run(new Form());
        }
    }
}
