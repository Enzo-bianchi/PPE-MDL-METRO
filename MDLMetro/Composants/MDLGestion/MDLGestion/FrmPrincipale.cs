using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseDeDonnees;
using System.Security.Cryptography;

namespace MDLGestion
{
    public partial class FrmPrincipale : MetroFramework.Forms.MetroForm
    {
        private Bdd UneConnexion;
        public FrmPrincipale()
        {
            InitializeComponent();
        }

        private void BtnQuitter_Click(object sender, EventArgs e)
        {
            if (MetroMessageBox.Show(this, "Voulez-vous quitter l'application ?", " Maison des ligues", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FrmPrincipale_Load(object sender, EventArgs e)
        {
            UneConnexion = ((FrmLogin)Owner).UneConnexion;
            DataTable Participants = UneConnexion.ObtenirDonnesOracle("vparticipant");
            GridArrivants.DataSource = Participants;
            GridArrivants.Columns[0].DefaultCellStyle.BackColor = Color.Red;
        }
        private void GridArrivants_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                byte[] wpa2 = new byte[12];
                RandomNumberGenerator.Create().GetNonZeroBytes(wpa2);
                String clefinal = Convert.ToBase64String(wpa2).Substring(1);

                UneConnexion.ValiderInscription(int.Parse(GridArrivants[1, e.RowIndex].Value.ToString()), clefinal);
                GridArrivants[0,e.RowIndex].Style.BackColor = Color.Green;
            }
        }
    }
}
