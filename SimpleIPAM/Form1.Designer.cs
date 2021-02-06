namespace SimpleIPAM
{
    partial class MainIpam
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainIpam));
            this.notifBar = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuBar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quitSimpleIPAMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.contextMenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifBar
            // 
            resources.ApplyResources(this.notifBar, "notifBar");
            this.notifBar.ContextMenuStrip = this.contextMenuBar;
            this.notifBar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifBar_MouseClick);
            // 
            // contextMenuBar
            // 
            this.contextMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitSimpleIPAMToolStripMenuItem});
            this.contextMenuBar.Name = "contextMenuBar";
            this.contextMenuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            resources.ApplyResources(this.contextMenuBar, "contextMenuBar");
            this.contextMenuBar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuBar_ItemClicked);
            // 
            // quitSimpleIPAMToolStripMenuItem
            // 
            this.quitSimpleIPAMToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLight;
            resources.ApplyResources(this.quitSimpleIPAMToolStripMenuItem, "quitSimpleIPAMToolStripMenuItem");
            this.quitSimpleIPAMToolStripMenuItem.Name = "quitSimpleIPAMToolStripMenuItem";
            // 
            // MainIpam
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.DoubleBuffered = true;
            this.Name = "MainIpam";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.contextMenuBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifBar;
        private System.Windows.Forms.ContextMenuStrip contextMenuBar;
        private System.Windows.Forms.ToolStripMenuItem quitSimpleIPAMToolStripMenuItem;
        private System.Windows.Forms.Timer timerUpdate;
    }
}

