using System;
using System.Windows.Forms;

namespace LaunchApp
{
    public partial class loadingScr : Form
    {
        public static Label lblInfo;
        public static ProgressBar barProgress;
        public loadingScr()
        {
            //Definir les requis (methodes,fonctions,variables,..).
            InitializeComponent();
            lblInfo = infoBar;
            barProgress = barLoading;
        }

    }
}
