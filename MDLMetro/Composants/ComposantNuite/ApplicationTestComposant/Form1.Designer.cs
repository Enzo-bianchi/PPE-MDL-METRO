namespace ApplicationTestComposant
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resaNuite1
            // 
            this.resaNuite1.BackColor = System.Drawing.Color.White;
            this.resaNuite1.Location = new System.Drawing.Point(21, 38);
            this.resaNuite1.Name = "resaNuite1";
            this.resaNuite1.Size = new System.Drawing.Size(496, 46);
            this.resaNuite1.TabIndex = 0;
            this.resaNuite1.OnNuitChanged += new System.EventHandler(this.resaNuite1_OnNuitChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(144, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 20);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 112);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.resaNuite1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ComposantNuite.ResaNuite resaNuite1;
        private System.Windows.Forms.Button button1;

    }
}

