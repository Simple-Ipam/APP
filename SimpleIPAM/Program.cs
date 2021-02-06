using System;
using System.Windows.Forms;

namespace SimpleIPAM
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string s = "https://alelix.net/simple-ipam";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainIpam("https://google.com"));
        }

    }
}
