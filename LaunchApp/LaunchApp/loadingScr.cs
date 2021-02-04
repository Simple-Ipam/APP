using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace LaunchApp
{
    public partial class loadingScr : Form
    {
        public static Label informationLabel;
        const string VERSION = "/version.dt";
        const string APN = "/AppNet-";

        static string versionFile,localExecutable;

        public loadingScr()
        {
            //Definir les requis (methodes,fonctions,variables,..).
            InitializeComponent();
            SetPathVariable("206");
            informationLabel = infoBar;
            barLoading.Value += 10;
            CheckRoutine();
        }

        async void CheckRoutine()
        {
            await System.Threading.Tasks.Task.Delay(2000);
            //Verifier si la machine est connectee à internet:
            // <OUI> -> Proceder a la suite.
            // <NON> -> Attendre 10s puis reessayer.
            var xTry = 0;
            informationLabel.Text = "Connexion...";
            while (!IsConnectedToInternet())
            {
                xTry++;
                informationLabel.Text = "Nouvelle tentative(" + xTry + ")...";
                await System.Threading.Tasks.Task.Delay(10000);
            }
            barLoading.Value += 10;

            //Recuperer la version serveur.
            string serverVersion = "206";

            //Parametrer les chemins d'access selon la version.
            SetPathVariable(serverVersion);

            //Afficher un message d'attente.
            informationLabel.Text = "Vérification des fichiers...";

            //Verifier si une version machine existe :
            //<NON> -> Créer le fichier 'version.dt' avec la version serveur, comme version.
            if (!File.Exists(versionFile))
            {
                File.WriteAllText(versionFile, serverVersion);
            }

            //Verifier si le dossier de la version machine existe :
            //<NON> -> Afficher un message d'attente.
            //<NON> -> Installer la version machine sur celle-ci.
            if (!Directory.Exists(localExecutable))
            {
                informationLabel.Text = "Installation...";
                InstallPackage(new Uri("https://alelix.net/AppNet-" + serverVersion), serverVersion);
                barLoading.Value += 20;
            }

            //Verifier si la version machine est différente à celle sur la machine :
            //<OUI> -> Afficher un message d'attente.
            //<OUI> -> Installer la version serveur.
            //<OUI> -> Modifier la version machine pour celle du serveur.
            if (File.ReadAllText(versionFile) != serverVersion)
            {
                informationLabel.Text = "Mise à jour...";
                InstallPackage(new Uri("https://alelix.net/AppNet-" + serverVersion), serverVersion);
                File.WriteAllText(versionFile, serverVersion);
            }
            barLoading.Value += 20;

            //Recuperer la nouvelle version machine.
            var homeVersion = File.ReadAllText(versionFile);

            //Afficher un message de lancement.
            barLoading.Value = 100;
            informationLabel.Text = "Lancement de SimpleIPAM...";

            //Lancer la version installee sur cette machine.
            System.Diagnostics.Process.Start(localExecutable + "/app/SimpleIPAM.exe");

            //Fermer cette application.
            await System.Threading.Tasks.Task.Delay(20);
            informationLabel.Text = "sortie...";
            Environment.Exit(0);
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
