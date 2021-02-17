using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

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
            Console.WriteLine("-----[PROGRAM SHOULD GO]----");

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
                Console.WriteLine(pingResp);
                if(pingResp == "ERROR")
                {
                    informationLabel.Text = "Service indisponible(API)...";
                    if(apiDesactivedFirstTime) notifyIcon1.ShowBalloonTip(4000, "Service indisponible",
                        "Les services internes à Simple-IPAM sont indisponible pour le moment.. ici, c'est le service principale (API) qui est désactivé.", ToolTipIcon.Error);
                    apiDesactivedFirstTime = false;
                    isChecking = false;
                    return;
                }

                //Recuperer les informations relative a la nouvelle version de l'app.(API)
                string updateResp = APIRest.Post("http://server1.alelix.net:47651", "checkUpdate");
                var table = JsonConvert.DeserializeObject<List<dynamic>>(updateResp);
                Console.WriteLine(table[0].url);
                Console.WriteLine(table[0].version);

                //Verifier si une aucune version est installee *OU QUE* la version installee est differente de celle sur GitHub:
                //  [OUI] -> installer la derniere version
                int vers = int.Parse(table[0].version.Replace('v', ' '));
                if (!File.Exists(Application.LocalUserAppDataPath+"/app.ve"))
                {
                    UpdatePackage(table[0].url, vers);
                }
                else if (File.ReadAllText(Application.LocalUserAppDataPath + "/app.ve") != table[0].version)
                {
                    UpdatePackage(table[0].url, vers);
                }
                else if (!Directory.Exists(Application.LocalUserAppDataPath + "/app_" + vers))
                {
                    UpdatePackage(table[0].url, vers);
                }
                else if (!File.Exists(Application.LocalUserAppDataPath + "/app_"+vers+ "/SimpleIPAM.exe"))
                {
                    UpdatePackage(table[0].url, vers);
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
            string urlLocal = Application.LocalUserAppDataPath+"/Release.zip";
            new WebClient().DownloadFile(urlDownload, urlLocal);

            //Verifier si le dossier 'app' existe pas:
            //  [OUI]-> Creer le dossier 'app'
            if (!Directory.Exists(Application.LocalUserAppDataPath + "/app_"+ version))
            {
                Directory.CreateDirectory(Application.LocalUserAppDataPath + "/app_"+ version);
            }

            //Extraire les paquets au bon emplacement.
            ZipFile.ExtractToDirectory(urlDownload, Application.LocalUserAppDataPath + "/app_"+ version);

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
