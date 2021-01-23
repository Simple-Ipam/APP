using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace LaunchApp
{
    public partial class loadingScr : Form
    {
        public static Label label;
        const string VERSION = "/version.dt";
        const string APN = "/AppNet-";

        public loadingScr()
        {
            //Definir les requis (methodes,fonctions,variables,..).
            InitializeComponent();
            label = infoBar;
            //label.Text = Application.LocalUserAppDataPath + VERSION;
            barLoading.Value = 10;
            CheckRoutine();
        }

        async void CheckRoutine()
        {

            await System.Threading.Tasks.Task.Delay(500);
            //Verifier si la connection est existante.
            if (CheckForInternetConnection())
            {
                //Recuper la version mis en ligne (version serveur)
                var versionServeur = "101";
                var versionMachine = "";
                barLoading.Value = 20;
                label.Text = "Verification des fichiers...";
                // Verifier si le fichier existe pas.
                if (!File.Exists(Application.LocalUserAppDataPath + VERSION))
                {
                    //Installer NetApp + creer le fichier version.
                    label.Text = "Installation des paquets...";
                    InstallPackage(versionServeur);
                    File.WriteAllText(Application.LocalUserAppDataPath + VERSION, versionServeur);
                }

                //Recuperer la version machine de AppNet.
                barLoading.Value = 50;
                versionMachine = File.ReadAllText(Application.LocalUserAppDataPath + VERSION);
                var x = new WebClient();
                //Verifier si NetApp de la version machine existe pas.
                if (!Directory.Exists(Application.LocalUserAppDataPath + APN + versionMachine))
                {
                    //Installer AppNet de la version machine.
                    label.Text = "Installation des paquets...";
                    InstallPackage(versionMachine);
                }

                barLoading.Value = 75;
                //Verifier si la version machine est != de celle du serveur.
                if (versionMachine != versionServeur)
                {
                    //Installer la nouvelle version de AppNet.
                    label.Text = "Mise à jour...";
                    InstallPackage(versionServeur);
                    //Modifier le fichier version (machine).
                    File.WriteAllText(Application.LocalUserAppDataPath + VERSION, versionServeur);
                }
                //Lancer la bonne version de AppNet.
                label.Text = "Lancement de Simple Ipam...";
                barLoading.Value = 95;
                var ur = Application.LocalUserAppDataPath + APN + versionMachine;
                
                System.Diagnostics.Process.Start(ur + "/img.exe");

                //Fermer LaunchApp.
                barLoading.Value = 100;
                Application.Exit();

            }
            else
            {
                label.Text = "Hors ligne...";
                barLoading.Value = 0;
            }
        }

        public async static void InstallPackage(string version)
        {
            //Telecharger le paquet de version V et dans l'espace logiciel.
            var url = Application.LocalUserAppDataPath + APN + version;

            if (!Directory.Exists(url + "/dl")) Directory.CreateDirectory(url + "/dl");

            new WebClient().DownloadFile("https://alelix.net/img.zip", url + "/dl/img.zip");

            //Extraire le paquet.
            ZipFile.ExtractToDirectory(url + "/dl/img.zip", Application.LocalUserAppDataPath + APN + version);

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
