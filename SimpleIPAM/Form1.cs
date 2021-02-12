using System;
using System.IO;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace SimpleIPAM
{
    public partial class MainIpam : Form
    {
        public ChromiumWebBrowser chromiumWeb;
        public MainIpam(string url)
        {
            var pathCache = Application.LocalUserAppDataPath + "/cache";
            Console.WriteLine(pathCache);
            if (!Directory.Exists(pathCache)) Directory.CreateDirectory(pathCache);

            //Initialisation du naviguateur integre.
            InitializeComponent();
            CefSettings cefS = new CefSettings() {
                PersistSessionCookies = true,
                CachePath = pathCache
            };
            Cef.Initialize(cefS);
            chromiumWeb = new ChromiumWebBrowser(url);
            chromiumWeb.BrowserSettings.ApplicationCache = CefState.Enabled; 
            Controls.Add(chromiumWeb);
            chromiumWeb.Dock = DockStyle.Fill;

            //Parametrage de l'evenement 'Notification'.
            timerUpdate.Interval = 5000;
            timerUpdate.Start();
            timerUpdate.Tick += Timer_Tick;

        }

        //Code de rafraichissement des notification (devoir et message).
        private void Timer_Tick(object sender, EventArgs e)
        {

        }


        #region Notify();
        public void Notify(string title, string descript, ToolTipIcon icon)
        {
            notifBar.ShowBalloonTip(5000, title, descript, icon);
        }
        public void Notify(string title, string descript)
        {
            notifBar.ShowBalloonTip(5000, title, descript, ToolTipIcon.None);
        }
        public void Notify(string description)
        {
            notifBar.ShowBalloonTip(5000, "Ipam", description, ToolTipIcon.None);
        }
        public void Notify(string description, ToolTipIcon icon)
        {
            notifBar.ShowBalloonTip(5000, "Ipam", description, icon);
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void notifBar_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Show();
            }
        }

        private void contextMenuBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(e.ClickedItem.Name == "quitSimpleIPAMToolStripMenuItem")
            {
                Environment.Exit(0);
            }
        }
    }
}
