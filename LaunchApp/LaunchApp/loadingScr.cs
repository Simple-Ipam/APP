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

        }

        static int xTry = 0;
        private async void CheckUpdate(object sender, EventArgs e)
        {
            if (!isChecking)
            {
                isChecking = true;
                await System.Threading.Tasks.Task.Delay(1000);
                //Verifier si la machine est connectee a internet:
                // <OUI> -> Proceder a la suite.
                // <NON> -> Attendre 10s puis reessayer.
                informationLabel.Text = "Connexion...";
                if (!IsConnectedToInternet())
                {
                    xTry++;
                    informationLabel.Text = "Nouvelle tentative(" + xTry + ")...";
                    isChecking = false;
                    return;
                }

                //Verifier si l API de Simple-Ipam est inactive:
                // <OUI> -> Afficher la page de 'problème avec l app, non disponible pour le moment'.
                // <NON> -> Proceder a la suite.
                var client = new RestClient("server1.alelix.net:47651");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("function", "ping");
                IRestResponse response = client.Execute(request);
                Console.WriteLine("statut code : "+response.StatusCode);
                if(response.StatusCode == 0)
                {
                    Console.WriteLine("-STATUT API OFFLINE-");
                    return;
                }

                //Recuperer les informations relative a la nouvelle version de l'app.(API)
                request = new RestRequest(Method.POST);
                request.AddParameter("function", "checkUpdate");
                response = client.Execute(request);
                Console.WriteLine("Response API>>: \n"+response.Content);
                var table = JsonConvert.DeserializeObject<List<dynamic>>(response.Content);

                //Verifier si une aucune version est installee *OU QUE* la version installee est differente de celle sur GitHub:
                //  [OUI] -> installer la derniere version
                if (!File.Exists(Application.LocalUserAppDataPath+"/app.ve"))
                {
                    UpdatePackage(table[0].url);
                }
                else if (File.ReadAllText(Application.LocalUserAppDataPath + "/app.ve") != table[0].version)
                {
                    UpdatePackage(table[0].url);
                }

                //Ouvrir l AppNet correspondant
                //System.Diagnostics.Process.Start(localExecutable + "/app/SimpleIPAM.exe");


                //Fermer cette application.
                await System.Threading.Tasks.Task.Delay(20);
                informationLabel.Text = "sortie...";
                Environment.Exit(0);

            }
        }

        #region FunctionPlus
        
        public static void UpdatePackage(string urlDownload)
        {
            //Telecharger la nouvelle application sur la machine.
            

            //Verifier si le dossier 'app' existe pas:
            //  [OUI]-> Creer le dossier 'app'


            //Extraire les paquets au bon emplacement.
            

            //Suprimer l ancienne version.
            

            //Modifier le fichier de version 'app.ve'.


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


    /*
     var ur = Application.LocalUserAppDataPath + APN + versionMachine;
                System.Diagnostics.Process.Start(ur + "/dl/SimpleIPAM.exe");
     */

}
