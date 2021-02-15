using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;
using RestSharp;

namespace LaunchApp
{
    public partial class loadingScr : Form
    {
        public static Label informationLabel;
        const string VERSION = "/version.dt";
        const string APN = "/AppNet-";
        public static bool isChecking;
        static string versionFile,localExecutable;

        public loadingScr()
        {
            //Definir les requis (methodes,fonctions,variables,..).
            InitializeComponent();
            SetPathVariable("206");
            informationLabel = infoBar;
            //CheckRoutine();
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
                //Verifier si la machine est connectee à internet:
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
                barLoading.Value += 10;

                //Recuperer les informations relative a la nouvelle version de l'app.(API)
                var client = new RestClient("server1.alelix.net:47651");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("function", "checkUpdate");
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);



                //Afficher un message de lancement.
                barLoading.Value = 100;
                informationLabel.Text = "Lancement de SimpleIPAM...";

                //Lancer la version installee sur cette machine.
                SetPathVariable(versionApp);
                System.Diagnostics.Process.Start(localExecutable + "/app/SimpleIPAM.exe");

                //Fermer cette application.
                await System.Threading.Tasks.Task.Delay(20);
                informationLabel.Text = "sortie...";
                Environment.Exit(0);

            }
        }

        #region FunctionPlus
        public static void InstallPackage(Uri URI,string version)
        {
            //Telecharger le paquet de version V et dans l'espace logiciel.
            informationLabel.Text = "Téléchargement...";
            var url = localExecutable;
            Directory.CreateDirectory(url);
            new WebClient().DownloadFile(URI, url + "/NetApp-"+version+".zip"); //Attends la fin pour ecrire 'Terminé! ...'.

            //Extraire le paquet.
            if (!Directory.Exists(url + "/app")) Directory.CreateDirectory(url + "/app");
            ZipFile.ExtractToDirectory(url + "/NetApp-" + version +".zip", url + "/app");
            informationLabel.Text = "Terminé! ...";

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

        public static void SetPathVariable(string version)
        {
            versionFile = Application.LocalUserAppDataPath + VERSION;
            localExecutable = Application.LocalUserAppDataPath + APN + version;
        }

        #endregion

    }


    /*
     var ur = Application.LocalUserAppDataPath + APN + versionMachine;
                System.Diagnostics.Process.Start(ur + "/dl/SimpleIPAM.exe");
     */

}
