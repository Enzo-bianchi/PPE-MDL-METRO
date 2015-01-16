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
using QRCoder;

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
            DataTable Participants = UneConnexion.ObtenirDonnesOracle("vparticipant01");
            GridArrivants.DataSource = Participants;
            GridArrivants.Columns[0].DefaultCellStyle.BackColor = Color.Red;
        }
        private void GridArrivants_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                string clewifi = GenererWPA("ABCDEFabcdef0123456789", 12);

                UneConnexion.ValiderInscription(int.Parse(GridArrivants[1, e.RowIndex].Value.ToString()), clewifi);
                GridArrivants[0, e.RowIndex].Style.BackColor = Color.Green;
                string prenom = GridArrivants[3, e.RowIndex].Value.ToString();
                string nom = GridArrivants[2, e.RowIndex].Value.ToString();
                string mail = GridArrivants[4, e.RowIndex].Value.ToString();
                (new FrmDetail(clewifi, nom, prenom, mail)).Show();
            }
        }
        public static string GenererWPA(string chars, int length)
        {
            var randomString = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
                randomString.Append(chars[random.Next(chars.Length)]);

            return randomString.ToString();
        }
    }
}
