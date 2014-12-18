namespace MDLMetro
{
    partial class FrmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.TxtLogin = new MetroFramework.Controls.MetroTextBox();
            this.TxtMdp = new MetroFramework.Controls.MetroTextBox();
            this.BtnConnexion = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(147, 50);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(71, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Connexion";
            // 
            // TxtLogin
            // 
            this.TxtLogin.Lines = new string[] {
        "employemdl"};
            this.TxtLogin.Location = new System.Drawing.Point(58, 81);
            this.TxtLogin.MaxLength = 32767;
            this.TxtLogin.Name = "TxtLogin";
            this.TxtLogin.PasswordChar = '\0';
            this.TxtLogin.PromptText = "Login";
            this.TxtLogin.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtLogin.SelectedText = "";
            this.TxtLogin.Size = new System.Drawing.Size(113, 23);
            this.TxtLogin.TabIndex = 1;
            this.TxtLogin.Text = "employemdl";
            this.TxtLogin.UseSelectable = true;
            // 
            // TxtMdp
            // 
            this.TxtMdp.Lines = new string[] {
        "employemdl"};
            this.TxtMdp.Location = new System.Drawing.Point(58, 110);
            this.TxtMdp.MaxLength = 32767;
            this.TxtMdp.Name = "TxtMdp";
            this.TxtMdp.PasswordChar = '●';
            this.TxtMdp.PromptText = "Mot de passe";
            this.TxtMdp.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtMdp.SelectedText = "";
            this.TxtMdp.Size = new System.Drawing.Size(113, 23);
            this.TxtMdp.TabIndex = 2;
            this.TxtMdp.Text = "employemdl";
            this.TxtMdp.UseSelectable = true;
            this.TxtMdp.UseSystemPasswordChar = true;
            // 
            // BtnConnexion
            // 
            this.BtnConnexion.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BtnConnexion.FontWeight = MetroFramework.MetroButtonWeight.Light;
            this.BtnConnexion.Highlight = true;
            this.BtnConnexion.Location = new System.Drawing.Point(191, 94);
            this.BtnConnexion.Name = "BtnConnexion";
            this.BtnConnexion.Size = new System.Drawing.Size(89, 23);
            this.BtnConnexion.Style = MetroFramework.MetroColorStyle.Red;
            this.BtnConnexion.TabIndex = 3;
            this.BtnConnexion.Text = "Se connecter";
            this.BtnConnexion.UseSelectable = true;
            this.BtnConnexion.Click += new System.EventHandler(this.BtnConnexion_Click);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 153);
            this.Controls.Add(this.BtnConnexion);
            this.Controls.Add(this.TxtMdp);
            this.Controls.Add(this.TxtLogin);
            this.Controls.Add(this.metroLabel1);
            this.Name = "FrmLogin";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Application assises de l\'escrime";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox TxtLogin;
        private MetroFramework.Controls.MetroTextBox TxtMdp;
        private MetroFramework.Controls.MetroButton BtnConnexion;
    }
}