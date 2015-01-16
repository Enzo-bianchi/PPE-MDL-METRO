namespace MDLGestion
{
    partial class FrmLogin
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.BtnConnexion = new MetroFramework.Controls.MetroButton();
            this.TxtLogin = new MetroFramework.Controls.MetroTextBox();
            this.TxtMdp = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(113, 49);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(129, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Gestion des arrivées ";
            // 
            // BtnConnexion
            // 
            this.BtnConnexion.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BtnConnexion.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.BtnConnexion.Highlight = true;
            this.BtnConnexion.Location = new System.Drawing.Point(219, 97);
            this.BtnConnexion.Name = "BtnConnexion";
            this.BtnConnexion.Size = new System.Drawing.Size(94, 23);
            this.BtnConnexion.Style = MetroFramework.MetroColorStyle.Red;
            this.BtnConnexion.TabIndex = 1;
            this.BtnConnexion.Text = "Se connecter";
            this.BtnConnexion.UseSelectable = true;
            this.BtnConnexion.Click += new System.EventHandler(this.BtnConnexion_Click);
            // 
            // TxtLogin
            // 
            this.TxtLogin.Lines = new string[] {
        "employemdl"};
            this.TxtLogin.Location = new System.Drawing.Point(65, 83);
            this.TxtLogin.MaxLength = 32767;
            this.TxtLogin.Name = "TxtLogin";
            this.TxtLogin.PasswordChar = '\0';
            this.TxtLogin.PromptText = "Login";
            this.TxtLogin.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtLogin.SelectedText = "";
            this.TxtLogin.Size = new System.Drawing.Size(123, 23);
            this.TxtLogin.TabIndex = 2;
            this.TxtLogin.Text = "employemdl";
            this.TxtLogin.UseSelectable = true;
            // 
            // TxtMdp
            // 
            this.TxtMdp.Lines = new string[] {
        "employemdl"};
            this.TxtMdp.Location = new System.Drawing.Point(65, 115);
            this.TxtMdp.MaxLength = 32767;
            this.TxtMdp.Name = "TxtMdp";
            this.TxtMdp.PasswordChar = '●';
            this.TxtMdp.PromptText = "Mot de passe";
            this.TxtMdp.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtMdp.SelectedText = "";
            this.TxtMdp.Size = new System.Drawing.Size(123, 23);
            this.TxtMdp.TabIndex = 3;
            this.TxtMdp.Text = "employemdl";
            this.TxtMdp.UseSelectable = true;
            this.TxtMdp.UseSystemPasswordChar = true;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 161);
            this.Controls.Add(this.TxtMdp);
            this.Controls.Add(this.TxtLogin);
            this.Controls.Add(this.BtnConnexion);
            this.Controls.Add(this.metroLabel1);
            this.Name = "FrmLogin";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "Maison des ligues";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton BtnConnexion;
        private MetroFramework.Controls.MetroTextBox TxtLogin;
        private MetroFramework.Controls.MetroTextBox TxtMdp;
    }
}

