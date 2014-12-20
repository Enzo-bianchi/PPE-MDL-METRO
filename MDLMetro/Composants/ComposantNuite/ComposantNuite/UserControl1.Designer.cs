namespace ComposantNuite
{
    partial class ResaNuite
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

        #region Code généré par le Concepteur de composants

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChkChoisir = new MetroFramework.Controls.MetroCheckBox();
            this.CmbHotel = new MetroFramework.Controls.MetroComboBox();
            this.CmbTypeChambre = new MetroFramework.Controls.MetroComboBox();
            this.SuspendLayout();
            // 
            // ChkChoisir
            // 
            this.ChkChoisir.AutoSize = true;
            this.ChkChoisir.Location = new System.Drawing.Point(3, 8);
            this.ChkChoisir.Name = "ChkChoisir";
            this.ChkChoisir.Size = new System.Drawing.Size(62, 15);
            this.ChkChoisir.TabIndex = 3;
            this.ChkChoisir.Text = "Nuit de";
            this.ChkChoisir.UseSelectable = true;
            // 
            // CmbHotel
            // 
            this.CmbHotel.FormattingEnabled = true;
            this.CmbHotel.ItemHeight = 23;
            this.CmbHotel.Location = new System.Drawing.Point(215, 1);
            this.CmbHotel.Name = "CmbHotel";
            this.CmbHotel.Size = new System.Drawing.Size(231, 29);
            this.CmbHotel.TabIndex = 4;
            this.CmbHotel.UseSelectable = true;
            // 
            // CmbTypeChambre
            // 
            this.CmbTypeChambre.FormattingEnabled = true;
            this.CmbTypeChambre.ItemHeight = 23;
            this.CmbTypeChambre.Location = new System.Drawing.Point(452, 1);
            this.CmbTypeChambre.Name = "CmbTypeChambre";
            this.CmbTypeChambre.Size = new System.Drawing.Size(74, 29);
            this.CmbTypeChambre.TabIndex = 5;
            this.CmbTypeChambre.UseSelectable = true;
            // 
            // ResaNuite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.CmbTypeChambre);
            this.Controls.Add(this.CmbHotel);
            this.Controls.Add(this.ChkChoisir);
            this.Name = "ResaNuite";
            this.Size = new System.Drawing.Size(529, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroCheckBox ChkChoisir;
        private MetroFramework.Controls.MetroComboBox CmbHotel;
        private MetroFramework.Controls.MetroComboBox CmbTypeChambre;

    }
}
