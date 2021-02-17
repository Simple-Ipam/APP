using System;
using System.Windows.Forms;

namespace LaunchApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //Definir les requis pour l'app.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            var scn = new loadingScr();
            Application.Run(scn);
        }

    }
}
