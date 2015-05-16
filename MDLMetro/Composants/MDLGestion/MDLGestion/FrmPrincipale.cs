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
using System.Configuration;
using System.Net.Mail;
using System.Net;

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
            

            //Definition des colonnes en readonly True sauf la premiere afin qu'on puisse cocher
            for (int i = 1; i < GridArrivants.Columns.Count; i++)
            {
                GridArrivants.Columns[i].ReadOnly = true;
            }
            //Definition d'un TAG "D" pour "desactiver" afin de repérer lors de l'inscription / désincription plus bas
            for (int i = 0; i < GridArrivants.Rows.Count; i++)
            {
                GridArrivants.Rows[i].Cells[0].Tag = "D";
            }

        }
        private void GridArrivants_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (GridArrivants[0, e.RowIndex].Tag.ToString() == "D") //Alors le participant n'est pas inscrit
                {
                    string clewifi = GenererWPA(12);

                    UneConnexion.ValiderInscription(int.Parse(GridArrivants[1, e.RowIndex].Value.ToString()), clewifi);
                    GridArrivants[0, e.RowIndex].Style.BackColor = Color.Green;
                    string prenom = GridArrivants[3, e.RowIndex].Value.ToString();
                    string nom = GridArrivants[2, e.RowIndex].Value.ToString();
                    string mail = GridArrivants[4, e.RowIndex].Value.ToString();
                    GridArrivants[0, e.RowIndex].Tag = "I"; // On le passe en inscrit
                    EnvoieMail(mail, nom, prenom);
                    (new FrmDetail(clewifi, nom, prenom, mail)).Show();
                }
                else
                    MessageBox.Show("Déjà inscrit");

            }
        }
        public static string GenererWPA(int length)
        {
            string chars = "ABCDEFabcdef0123456789";
            var randomString = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
                randomString.Append(chars[random.Next(chars.Length)]);

            return randomString.ToString();
        }

        /// <summary>
        /// La fonction EnvoieMail permet d'envoyer un mail de confirmation à l'adresse email entrée dans TxtMail.
        /// </summary>
        /// 
        //TxtNom.Text,TxtPrenom.Text,TxtTel.Text,TxtVille.Text
        public static void EnvoieMail(string LeMail, string LeNom, string LePrenom)
        {
            MailMessage Mail;
            SmtpClient SmtpServer;
            string Expediteur = ConfigurationManager.AppSettings["Expediteur"];
            string Motdepasse = ConfigurationManager.AppSettings["Motdepasse"];
            string Host = ConfigurationManager.AppSettings["Host"];
            string Port = ConfigurationManager.AppSettings["Port"];
            
            try
            {
                Mail = new MailMessage();
                SmtpServer = new SmtpClient();

                Mail.From = new MailAddress(LeMail);
                Mail.To.Add(LeMail);
                Mail.Subject = "Comfirmation arrivée Maison des ligues";
                Mail.IsBodyHtml = true;
                Mail.Body = "Bonjour " + LePrenom + "" + LeNom + "," + "<p>Nous avons le plaisir de vous confirmer votre arrivée aux Assises de l'Escrime 2015. </p>";

                NetworkCredential basicCredential = new NetworkCredential(Expediteur, Motdepasse);

                SmtpServer.Host = Host;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = basicCredential;
                SmtpServer.Port = Convert.ToInt16(Port);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(Mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de l'envoi de votre email de confirmation. Veuillez réessayer ultérieurement.");
            }
        }
    }
}
