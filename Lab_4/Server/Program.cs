using System;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Server
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Server());
        }
    }
}
