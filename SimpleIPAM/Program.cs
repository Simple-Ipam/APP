using System;
using System.Net;
using System.Windows.Forms;

namespace SimpleIPAM
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (CheckForInternetConnection())
            {
                Application.Run(new LoginSIPAM());
            }
            else
            {
                Application.Run();
            }
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
