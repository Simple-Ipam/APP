using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace SimpleIPAM
{
    public partial class MainIpam : Form
    {
        public ChromiumWebBrowser chromiumWeb;
        public MainIpam()
        {
            InitializeComponent();
            chromiumWeb = new ChromiumWebBrowser("https://alelix.net");
            Controls.Add(chromiumWeb);
            chromiumWeb.Dock = DockStyle.Fill;
            Notify("Ipam - Notification ☻", "Ceci est un teste du système de notification (Lazrack Ipsum).", ToolTipIcon.None);
        }

        public void Notify(string title,string descript, ToolTipIcon icon)
        {
            notifBar.ShowBalloonTip(5000, title, descript, icon);
        }

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
                Application.Exit();
            }
        }
    }
}
