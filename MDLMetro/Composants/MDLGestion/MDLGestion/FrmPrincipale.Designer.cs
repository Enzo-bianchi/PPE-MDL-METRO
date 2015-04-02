namespace MDLGestion
{
    partial class FrmPrincipale
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.BtnQuitter = new MetroFramework.Controls.MetroButton();
            this.GridArrivants = new MetroFramework.Controls.MetroGrid();
            this.Arrive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridArrivants)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnQuitter
            // 
            this.BtnQuitter.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BtnQuitter.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.BtnQuitter.Highlight = true;
            this.BtnQuitter.Location = new System.Drawing.Point(773, 22);
            this.BtnQuitter.Name = "BtnQuitter";
            this.BtnQuitter.Size = new System.Drawing.Size(136, 23);
            this.BtnQuitter.Style = MetroFramework.MetroColorStyle.Red;
            this.BtnQuitter.TabIndex = 0;
            this.BtnQuitter.Text = "Quitter l\'application";
            this.BtnQuitter.UseSelectable = true;
            this.BtnQuitter.Click += new System.EventHandler(this.BtnQuitter_Click);
            // 
            // GridArrivants
            // 
            this.GridArrivants.AllowUserToAddRows = false;
            this.GridArrivants.AllowUserToDeleteRows = false;
            this.GridArrivants.AllowUserToResizeRows = false;
            this.GridArrivants.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GridArrivants.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridArrivants.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GridArrivants.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridArrivants.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GridArrivants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridArrivants.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Arrive});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridArrivants.DefaultCellStyle = dataGridViewCellStyle2;
            this.GridArrivants.EnableHeadersVisualStyles = false;
            this.GridArrivants.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.GridArrivants.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GridArrivants.Location = new System.Drawing.Point(23, 65);
            this.GridArrivants.Name = "GridArrivants";
            this.GridArrivants.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(170)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridArrivants.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GridArrivants.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GridArrivants.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridArrivants.Size = new System.Drawing.Size(886, 457);
            this.GridArrivants.Style = MetroFramework.MetroColorStyle.Teal;
            this.GridArrivants.TabIndex = 1;
            this.GridArrivants.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridArrivants_CellContentClick);
            // 
            // Arrive
            // 
            this.Arrive.HeaderText = "ARRIVEE";
            this.Arrive.Name = "Arrive";
            // 
            // FrmPrincipale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 545);
            this.ControlBox = false;
            this.Controls.Add(this.GridArrivants);
            this.Controls.Add(this.BtnQuitter);
            this.Name = "FrmPrincipale";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Text = "Gestion des arrivants";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Load += new System.EventHandler(this.FrmPrincipale_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridArrivants)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton BtnQuitter;
        private MetroFramework.Controls.MetroGrid GridArrivants;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Arrive;
    }
}