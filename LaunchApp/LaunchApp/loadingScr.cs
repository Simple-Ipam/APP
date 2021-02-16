using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;

namespace LaunchApp
{
    public partial class loadingScr : Form
    {
        public static Label informationLabel;
        public static bool isChecking = false;

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
                //Créer une instance de RestSharp.
                var client = new RestClient("server1.alelix.net:47651");
                client.Timeout = -1;

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
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("function", "ping");
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                if (response.StatusCode == 0)
                {
                    Console.WriteLine("-STATUT API OFFLINE-");
                    informationLabel.ForeColor = System.Drawing.Color.Red;
                    informationLabel.Text = "Service indisponible(API)...";
                    isChecking = false;
                    notifyIcon1.ShowBalloonTip(4000, "Problème d'API", "Nos services internes à Simple-IPAM ne sont pas disponible pour le moment.",ToolTipIcon.Error);
                    return;
                }

                //Recuperer les informations relative a la nouvelle version de l'app.(API)
                var requestURL = new RestRequest(Method.POST);
                requestURL.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                requestURL.AddParameter("function", "ping");
                IRestResponse responseURL = client.Execute(requestURL);
                var table = JsonConvert.DeserializeObject<List<dynamic>>(responseURL.Content);
                Console.WriteLine(table[0].url);
                Console.WriteLine(table[0].version);

                //Verifier si une aucune version est installee *OU QUE* la version installee est differente de celle sur GitHub:
                //  [OUI] -> installer la derniere version
                if (!File.Exists(Application.LocalUserAppDataPath+"/app.ve"))
                {
                    UpdatePackage(table[0].url, int.Parse(table[0].version.Replace('v', ' ')));
                }
                else if (File.ReadAllText(Application.LocalUserAppDataPath + "/app.ve") != table[0].version)
                {
                    UpdatePackage(table[0].url, int.Parse(table[0].version.Replace('v', ' ')));
                }

                //Ouvrir l AppNet correspondant
                System.Diagnostics.Process.Start(Application.LocalUserAppDataPath + "/app/SimpleIPAM.exe");


                //Fermer cette application.
                await System.Threading.Tasks.Task.Delay(20);
                informationLabel.Text = "sortie...";
                Environment.Exit(0);

            }
        }

        #region FunctionPlus
        
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