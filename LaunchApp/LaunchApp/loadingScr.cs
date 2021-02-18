using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;
using System.Text;
using System.Net.Http;

namespace LaunchApp
{
    public partial class loadingScr : Form
    {
        public static Label informationLabel;
        public static bool isChecking = false, apiDesactivedFirstTime = true;

        public loadingScr()
        {
            //Definir les requis (methodes,fonctions,variables,..).
            InitializeComponent();
            informationLabel = infoBar;
            timer1.Interval = 10000;
            timer1.Start();
            timer1.Tick += CheckUpdate;

        }

        static int xTry = 0;
        private async void CheckUpdate(object sender, EventArgs e)
        {
            informationLabel.ForeColor = System.Drawing.Color.FromName("AppWorkspace");
            if (!isChecking)
            {
                isChecking = true;
                //Verifier si la machine est connectee a internet:
                // <OUI> -> Proceder a la suite.
                // <NON> -> Attendre 10s puis reessayer.
                informationLabel.Text = "Connexion...";
                await System.Threading.Tasks.Task.Delay(1000);
                if (!IsConnectedToInternet())
                {
                    xTry++;
                    informationLabel.ForeColor = System.Drawing.Color.Red;
                    informationLabel.Text = "Nouvelle tentative(" + xTry + ")...";
                    isChecking = false;
                    return;
                }

                //Verifier si l API de Simple-Ipam est inactive:
                // <OUI> -> Afficher 'Service indisponible(API)..'.
                // <NON> -> Proceder a la suite.
                informationLabel.Text = "Récupération des données serveurs...";
                await System.Threading.Tasks.Task.Delay(2000);
                string pingResp = APIRest.Post("http://server1.alelix.net:47651", "ping");
                if(pingResp == "ERROR")
                {
                    informationLabel.Text = "Service indisponible(API)...";
                    informationLabel.ForeColor = System.Drawing.Color.Red;
                    if(apiDesactivedFirstTime) notifyIcon1.ShowBalloonTip(4000, "Service indisponible",
                        "L'API de Simple-IPAM est indisponible pour le moment.", ToolTipIcon.Error);
                    apiDesactivedFirstTime = false;
                    isChecking = false;
                    return;
                }

                //Recuperer les informations relative a la nouvelle version de l'app.(API)
                string updateResp = APIRest.Post("http://server1.alelix.net:47651", "checkUpdate");
                var table = Newtonsoft.Json.Linq.JObject.Parse(updateResp);

                //Verifier si une aucune version est installee *OU QUE* la version installee est differente de celle sur GitHub:
                //  [OUI] -> installer la derniere version
                string versionS = (string)table["version"];
                int vers = int.Parse(versionS.Replace('v', ' '));
                if (!File.Exists(Application.LocalUserAppDataPath+"/app.ve"))
                {
                    UpdatePackage((string)table["url"], vers);
                }
                else if (File.ReadAllText(Application.LocalUserAppDataPath + "/app.ve") != (string)table["version"])
                {
                    UpdatePackage((string)table["url"], vers);
                }
                else if (!Directory.Exists(Application.LocalUserAppDataPath + "/app_" + vers))
                {
                    UpdatePackage((string)table["url"], vers);
                }
                else if (!File.Exists(Application.LocalUserAppDataPath + "/app_"+vers+ "/SimpleIPAM.exe"))
                {
                    UpdatePackage((string)table["url"], vers);
                }

                //Ouvrir l AppNet correspondant
                try
                {
                    System.Diagnostics.Process.Start(Application.LocalUserAppDataPath + "/app_"+vers+"/SimpleIPAM.exe");
                    informationLabel.Text = "sortie...";
                    //Fermer cette application.
                    Environment.Exit(0);
                }
                catch
                {
                    // Dans le cas où l AppNet nest pas bien installe:
                    // - afficher un message communiquant le probleme
                    // - recommencer la procédure
                }


            }
        }

        #region otherFunctions/Method
        
        public static void UpdatePackage(string urlDownload,int version)
        {
            //Telecharger la nouvelle application sur la machine.
            informationLabel.Text = "Mise à jour...";
            string urlLocal = Application.LocalUserAppDataPath+"/Release.zip";
            new WebClient().DownloadFile(urlDownload, urlLocal);

            //Verifier si le dossier 'app' existe pas:
            //  [OUI]-> Creer le dossier 'app'
            if (!Directory.Exists(Application.LocalUserAppDataPath + "/app_"+ version))
            {
                Directory.CreateDirectory(Application.LocalUserAppDataPath + "/app_"+ version);
            }

            //Extraire les paquets au bon emplacement.
            ZipFile.ExtractToDirectory(urlLocal, Application.LocalUserAppDataPath + "/app_"+ version);

            //Supprimer le ZIP.
            File.Delete(urlLocal);

            //Modifier le fichier de version 'app.ve'.
            File.WriteAllText(Application.LocalUserAppDataPath + "/app.ve", "v"+version);

            //Suprimer l ancienne version.
            if (Directory.Exists(Application.LocalUserAppDataPath + "/app_" + (version-1)))
            {
                Directory.Delete(Application.LocalUserAppDataPath + "/app_" + (version-1));
            }

        }

        private void CookieForYou(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode);
            if(e.KeyCode == Keys.Menu && !isChecking)
            {
                notifyIcon1.ShowBalloonTip(100);
                LogoIcon.Load("./../../Resources/android-chrome-192x192.png");
            }
        }


        public static bool IsConnectedToInternet()
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

        #endregion

    }
}

public static class APIRest
{

    public static string Post(string url, string valueFunction)
    {
        using (var client = new HttpClient())
        {
            var objJSON = new Function { function = valueFunction };
            var content = new StringContent(
                Newtonsoft.Json.JsonConvert.SerializeObject(objJSON), Encoding.UTF8, "application/json"
            );
            Console.WriteLine(">[WEB-PRELOAD]>: " + content);
            var res = client.PostAsync(url, content);
            try
            {
                var resp = res.Result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(">[WEB]>: " + resp);
                return resp;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "ERROR";
            }
        }

    }

}

public class Function
{
    public string function
    {
        get; set;
    }
}
