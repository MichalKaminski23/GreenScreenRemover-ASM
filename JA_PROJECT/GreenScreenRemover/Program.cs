using System;
using System.Windows.Forms;

namespace GreenScreenRemover
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.DoEvents();
            Application.Run(new Menu());
        }
    }
}
