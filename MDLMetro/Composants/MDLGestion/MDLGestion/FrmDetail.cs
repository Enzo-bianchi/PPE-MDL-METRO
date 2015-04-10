using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MDLGestion
{
    public partial class FrmDetail : MetroFramework.Forms.MetroForm
    {
        private string clewifi;
        private string nom;
        private string prenom;
        private string mail;
        public FrmDetail(string Pwifi, string Pnom, string Pprenom, string Pmail)
        {
            InitializeComponent();
            clewifi = Pwifi;
            nom = Pnom;
            prenom = Pprenom;
            mail = Pmail;


        }

        private void FrmDetail_Load(object sender, EventArgs e)
        {
            LblNom.Text = nom;
            LblWifi.Text = clewifi;
            renderQRCode();
        }
        /// <summary>
        /// Procedure qui génere le QR CODE, avec les informations(nom, prenom, mail) de l'arrivant
        /// </summary>
        private void renderQRCode()
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode("Nom : " + nom +  Environment.NewLine + "prenom : " + prenom + Environment.NewLine + "Mail : " + mail,
                                                                     QRCodeGenerator.ECCLevel.Q);
            pictureBoxQRCode.BackgroundImage = qrCode.GetGraphic(20);
        }
    }
}
