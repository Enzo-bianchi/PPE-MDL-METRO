namespace ProjetTest
{
    partial class FrmComposantNuite
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
            this.resaNuite1 = new ComposantNuite.ResaNuite();
            this.SuspendLayout();
            // 
            // resaNuite1
            // 
            this.resaNuite1.Location = new System.Drawing.Point(90, 33);
            this.resaNuite1.Name = "resaNuite1";
            this.resaNuite1.Size = new System.Drawing.Size(523, 35);
            this.resaNuite1.TabIndex = 0;
            // 
            // FrmComposantNuite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 125);
            this.Controls.Add(this.resaNuite1);
            this.Name = "FrmComposantNuite";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ComposantNuite.ResaNuite resaNuite1;
    }
}

