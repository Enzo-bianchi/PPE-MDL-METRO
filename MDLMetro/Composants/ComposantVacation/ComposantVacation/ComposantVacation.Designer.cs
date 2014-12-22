namespace ComposantVacation
{
    partial class ComposantVacation
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
            this.ComposantVacationDateTime = new MetroFramework.Controls.MetroDateTime();
            this.ComposantVacationLabelDate = new MetroFramework.Controls.MetroLabel();
            this.ComposantVacationMaskedTBHeureDebut = new System.Windows.Forms.MaskedTextBox();
            this.ComposantVacationLabelHeureDebut = new System.Windows.Forms.Label();
            this.ComposantVacationMaskedTBHeureFin = new System.Windows.Forms.MaskedTextBox();
            this.ComposantVacationLabelHeureFin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComposantVacationDateTime
            // 
            this.ComposantVacationDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ComposantVacationDateTime.Location = new System.Drawing.Point(3, 22);
            this.ComposantVacationDateTime.MaxDate = new System.DateTime(2015, 9, 13, 0, 0, 0, 0);
            this.ComposantVacationDateTime.MinDate = new System.DateTime(2015, 9, 12, 0, 0, 0, 0);
            this.ComposantVacationDateTime.MinimumSize = new System.Drawing.Size(0, 29);
            this.ComposantVacationDateTime.Name = "ComposantVacationDateTime";
            this.ComposantVacationDateTime.Size = new System.Drawing.Size(200, 29);
            this.ComposantVacationDateTime.TabIndex = 0;
            this.ComposantVacationDateTime.Value = new System.DateTime(2015, 9, 12, 0, 0, 0, 0);
            // 
            // ComposantVacationLabelDate
            // 
            this.ComposantVacationLabelDate.AutoSize = true;
            this.ComposantVacationLabelDate.Location = new System.Drawing.Point(78, 0);
            this.ComposantVacationLabelDate.Name = "ComposantVacationLabelDate";
            this.ComposantVacationLabelDate.Size = new System.Drawing.Size(36, 19);
            this.ComposantVacationLabelDate.TabIndex = 1;
            this.ComposantVacationLabelDate.Text = "Date";
            // 
            // ComposantVacationMaskedTBHeureDebut
            // 
            this.ComposantVacationMaskedTBHeureDebut.BackColor = System.Drawing.Color.Red;
            this.ComposantVacationMaskedTBHeureDebut.Location = new System.Drawing.Point(228, 29);
            this.ComposantVacationMaskedTBHeureDebut.Mask = "00:00";
            this.ComposantVacationMaskedTBHeureDebut.Name = "ComposantVacationMaskedTBHeureDebut";
            this.ComposantVacationMaskedTBHeureDebut.Size = new System.Drawing.Size(100, 20);
            this.ComposantVacationMaskedTBHeureDebut.TabIndex = 2;
            this.ComposantVacationMaskedTBHeureDebut.Text = "0000";
            this.ComposantVacationMaskedTBHeureDebut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ComposantVacationMaskedTBHeureDebut.Enter += new System.EventHandler(this.ComposantVacationMaskedTBHeureDebut_Enter);
            this.ComposantVacationMaskedTBHeureDebut.Leave += new System.EventHandler(this.ComposantVacationMaskedTBHeureDebut_Leave);
            // 
            // ComposantVacationLabelHeureDebut
            // 
            this.ComposantVacationLabelHeureDebut.AutoSize = true;
            this.ComposantVacationLabelHeureDebut.BackColor = System.Drawing.Color.White;
            this.ComposantVacationLabelHeureDebut.Location = new System.Drawing.Point(238, 6);
            this.ComposantVacationLabelHeureDebut.Name = "ComposantVacationLabelHeureDebut";
            this.ComposantVacationLabelHeureDebut.Size = new System.Drawing.Size(81, 13);
            this.ComposantVacationLabelHeureDebut.TabIndex = 3;
            this.ComposantVacationLabelHeureDebut.Text = "Heure de debut";
            // 
            // ComposantVacationMaskedTBHeureFin
            // 
            this.ComposantVacationMaskedTBHeureFin.BackColor = System.Drawing.Color.Red;
            this.ComposantVacationMaskedTBHeureFin.Location = new System.Drawing.Point(354, 29);
            this.ComposantVacationMaskedTBHeureFin.Mask = "00:00";
            this.ComposantVacationMaskedTBHeureFin.Name = "ComposantVacationMaskedTBHeureFin";
            this.ComposantVacationMaskedTBHeureFin.Size = new System.Drawing.Size(100, 20);
            this.ComposantVacationMaskedTBHeureFin.TabIndex = 4;
            this.ComposantVacationMaskedTBHeureFin.Text = "0000";
            this.ComposantVacationMaskedTBHeureFin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ComposantVacationMaskedTBHeureFin.Enter += new System.EventHandler(this.ComposantVacationMaskedTBHeureFin_Enter);
            this.ComposantVacationMaskedTBHeureFin.Leave += new System.EventHandler(this.ComposantVacationMaskedTBHeureFin_Leave);
            // 
            // ComposantVacationLabelHeureFin
            // 
            this.ComposantVacationLabelHeureFin.AutoSize = true;
            this.ComposantVacationLabelHeureFin.BackColor = System.Drawing.Color.White;
            this.ComposantVacationLabelHeureFin.Location = new System.Drawing.Point(373, 6);
            this.ComposantVacationLabelHeureFin.Name = "ComposantVacationLabelHeureFin";
            this.ComposantVacationLabelHeureFin.Size = new System.Drawing.Size(65, 13);
            this.ComposantVacationLabelHeureFin.TabIndex = 5;
            this.ComposantVacationLabelHeureFin.Text = "Heure de fin";
            // 
            // ComposantVacation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ComposantVacationLabelHeureFin);
            this.Controls.Add(this.ComposantVacationMaskedTBHeureFin);
            this.Controls.Add(this.ComposantVacationLabelHeureDebut);
            this.Controls.Add(this.ComposantVacationMaskedTBHeureDebut);
            this.Controls.Add(this.ComposantVacationLabelDate);
            this.Controls.Add(this.ComposantVacationDateTime);
            this.Name = "ComposantVacation";
            this.Size = new System.Drawing.Size(460, 55);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel ComposantVacationLabelDate;
        private System.Windows.Forms.Label ComposantVacationLabelHeureDebut;
        private System.Windows.Forms.Label ComposantVacationLabelHeureFin;
        internal MetroFramework.Controls.MetroDateTime ComposantVacationDateTime;
        internal System.Windows.Forms.MaskedTextBox ComposantVacationMaskedTBHeureDebut;
        internal System.Windows.Forms.MaskedTextBox ComposantVacationMaskedTBHeureFin;
    }
}
