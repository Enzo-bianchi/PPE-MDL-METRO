using BaseDeDonnees;
using System;
using System.Windows.Forms;

namespace MDLMetro
{
    public partial class FrmLogin : MetroFramework.Forms.MetroForm
    {
        internal BaseDeDonnees.Bdd UneConnexion;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnConnexion_Click(object sender, EventArgs e)
        {
            try
            {
                UneConnexion = new Bdd(TxtLogin.Text, TxtMdp.Text);
                (new FrmPrincipale()).Show(this);
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
           this.ControleValide(sender, e);
        }

        /// <summary>
        /// gestion de l'activation/désactivation du bouton connexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControleValide(object sender, EventArgs e)
        {
            if (TxtLogin.Text.Length == 0 || TxtMdp.Text.Length == 0)
            {
                BtnConnexion.Enabled = false;
            }
            else
            {
                BtnConnexion.Enabled = true;
            }
        }
    }
}
