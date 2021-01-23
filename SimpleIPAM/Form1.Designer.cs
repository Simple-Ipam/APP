namespace SimpleIPAM
{
    partial class LoginSIPAM
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginSIPAM));
            this.renderWeb = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // renderWeb
            // 
            this.renderWeb.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            resources.ApplyResources(this.renderWeb, "renderWeb");
            this.renderWeb.IsWebBrowserContextMenuEnabled = false;
            this.renderWeb.Name = "renderWeb";
            this.renderWeb.ScriptErrorsSuppressed = true;
            // 
            // LoginSIPAM
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.renderWeb);
            this.Name = "LoginSIPAM";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.WebBrowser renderWeb;
    }
}

