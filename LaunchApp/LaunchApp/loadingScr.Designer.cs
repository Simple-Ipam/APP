namespace LaunchApp
{
    partial class loadingScr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loadingScr));
            this.infoBar = new System.Windows.Forms.Label();
            this.LogoIcon = new System.Windows.Forms.PictureBox();
            this.barLoading = new System.Windows.Forms.ProgressBar();
            this.underLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LogoIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // infoBar
            // 
            this.infoBar.AutoSize = true;
            this.infoBar.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoBar.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.infoBar.Location = new System.Drawing.Point(12, 325);
            this.infoBar.Name = "infoBar";
            this.infoBar.Size = new System.Drawing.Size(218, 16);
            this.infoBar.TabIndex = 0;
            this.infoBar.Text = "Chargement de Simple IPAM. . .";
            this.infoBar.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LogoIcon
            // 
            this.LogoIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.LogoIcon.Image = ((System.Drawing.Image)(resources.GetObject("LogoIcon.Image")));
            this.LogoIcon.Location = new System.Drawing.Point(150, 15);
            this.LogoIcon.Name = "LogoIcon";
            this.LogoIcon.Size = new System.Drawing.Size(200, 200);
            this.LogoIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LogoIcon.TabIndex = 1;
            this.LogoIcon.TabStop = false;
            // 
            // barLoading
            // 
            this.barLoading.Location = new System.Drawing.Point(150, 225);
            this.barLoading.Name = "barLoading";
            this.barLoading.Size = new System.Drawing.Size(200, 10);
            this.barLoading.TabIndex = 2;
            // 
            // underLabel
            // 
            this.underLabel.AutoSize = true;
            this.underLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.underLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.underLabel.Location = new System.Drawing.Point(186, 238);
            this.underLabel.Name = "underLabel";
            this.underLabel.Size = new System.Drawing.Size(164, 16);
            this.underLabel.TabIndex = 0;
            this.underLabel.Text = "par Do Evrything Better";
            this.underLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // loadingScr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this.barLoading);
            this.Controls.Add(this.LogoIcon);
            this.Controls.Add(this.underLabel);
            this.Controls.Add(this.infoBar);
            this.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "loadingScr";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LaunchApp-LOADING";
            ((System.ComponentModel.ISupportInitialize)(this.LogoIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label infoBar;
        private System.Windows.Forms.PictureBox LogoIcon;
        private System.Windows.Forms.ProgressBar barLoading;
        private System.Windows.Forms.Label underLabel;
    }
}

