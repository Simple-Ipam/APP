using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaunchApp
{
    static class Program
    {
        const string VERSION = "/version.dt";
        const string APN = "/AppNet-";

        [STAThread]
        static void Main()
        {
            //Definir les requis pour l'app.
            var scn = new loadingScr();
            Application.EnableVisualStyles();
            Application.Run(scn);
            
            //Verifier si la connection est existante.
            if (CheckForInternetConnection())
            {
                //Recuper la version mis en ligne (version serveur)
                var versionServeur = new WebClient().DownloadString("");
                var versionMachine = "";
                // Verifier si le fichier existe pas.
                if (!File.Exists(Application.UserAppDataPath+VERSION))
                {
                    //Installer NetApp + creer le fichier version.
                }

                //Recuperer la version machine de AppNet.
                versionMachine = File.ReadAllText(Application.UserAppDataPath + VERSION);

                //Verifier si NetApp de la version machine existe pas.
                if(!Directory.Exists(Application.UserAppDataPath+APN+versionMachine))
                {
                    //Installer AppNet de la version machine
                    InstallPackage(versionMachine);
                }

                //Verifier si la version machine est != de celle du serveur.
                if(versionMachine != versionServeur)
                {
                    //Installer la nouvelle version de AppNet
                    //Modifier le fichier version (machine).
                    File.WriteAllText(Application.UserAppDataPath + VERSION, versionServeur);
                }

            }
            else
            {
                
            }
        }

        public static void InstallPackage(string version)
        {
            //Telecharger le paquet de version V et dans l'espace logiciel.
            //Extraire le paquet.
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
