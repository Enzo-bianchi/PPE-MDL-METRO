using BaseDeDonnees;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using System.Collections.ObjectModel;
using ComposantNuite;
using ComposantVacation;
using System.IO;
using System.Text.RegularExpressions;

namespace MDLMetro
{
    public partial class FrmPrincipale : MetroFramework.Forms.MetroForm
    {
        private String IdStatutSelectionne = "";
        private Bdd UneConnexion;
        Collection<MetroPanel> PanelsVacations = new Collection<MetroPanel>();
        int NombreLigne = 0;
        int NombreVacationCreerAtelier = 0;
        int x = 14;
        int y = 0;
        int x2 = 630;
        byte[] photoByte;
        private string ChainePropre;


        public FrmPrincipale()
        {
            InitializeComponent();
        }

        private void FrmPrincipale_Load(object sender, EventArgs e)
        {
            UneConnexion = ((FrmLogin)Owner).UneConnexion;
        }

        private void BtnQuitter_Click(object sender, EventArgs e)
        {
            if (MetroMessageBox.Show(this, "Voulez-vous quitter l'application ?", " Maison des ligues", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void RadTypeParticipant_CheckedChanged(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "RadBenevole":
                    this.GererInscriptionBenevole();
                    break;
                case "RadLicencie":
                    //this.GererInscriptionLicencie();
                    break;
                case "RadIntervenant":
                    this.GererInscriptionIntervenant();
                    break;
                default:
                    throw new Exception("Erreur interne à l'application");
            }
        }
        /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie des disponibilités des bénévoles.
        /// </summary>
        private void GererInscriptionBenevole()
        {

            PanelBenevole.Visible = true;
            PanelIntervenant.Visible = false;

            Utilitaire.CreerDesControles(this, UneConnexion, "VDATEBENEVOLAT01", "ChkDateB_", PanelDispoBenevole, "MetroCheckBox", this.rdbStatutIntervenant_StateChanged);
            // on va tester si le controle à placer est de type CheckBox afin de lui placer un événement checked_changed
            // Ceci afin de désactiver les boutons si aucune case à cocher du container n'est cochée
            foreach (Control UnControle in PanelDispoBenevole.Controls)
            {
                if (UnControle.GetType().Name == "MetroCheckBox")
                {
                    MetroCheckBox UneCheckBox = (MetroCheckBox)UnControle;
                    UneCheckBox.CheckedChanged += new System.EventHandler(this.ChkDateBenevole_CheckedChanged);
                }
            }


        }
        /// <summary>
        /// Cetet méthode teste les données saisies afin d'activer ou désactiver le bouton d'enregistrement d'un bénévole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkDateBenevole_CheckedChanged(object sender, EventArgs e)
        {
            BtnEnregistrerBenevole.Enabled = (TxtLicenceBenevole.Text == "" || TxtLicenceBenevole.MaskCompleted) && TxtDateNaissance.MaskCompleted && Utilitaire.CompteChecked(PanelDispoBenevole) > 0;
        }

        /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie du complément d'inscription d'un intervenant.
        /// </summary>
        private void GererInscriptionIntervenant()
        {

            PanelBenevole.Visible = false;
            PanelIntervenant.Visible = true;
            PanelFonctionIntervenant.Visible = true;
            Utilitaire.CreerDesControles(this, UneConnexion, "VSTATUT01", "Rad_", PanelFonctionIntervenant, "MetroRadioButton", this.rdbStatutIntervenant_StateChanged);
            Utilitaire.RemplirComboBox(UneConnexion, CmbAtelierIntervenant, "VATELIER01");

            CmbAtelierIntervenant.Text = "Choisir";

        }
        /// <summary>
        /// permet d'appeler la méthode VerifBtnEnregistreIntervenant qui déterminera le statu du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbStatutIntervenant_StateChanged(object sender, EventArgs e)
        {
            // stocke dans un membre de niveau form l'identifiant du statut sélectionné (voir règle de nommage des noms des controles : prefixe_Id)
            this.IdStatutSelectionne = ((RadioButton)sender).Name.Split('_')[1];
            BtnEnregistrerIntervenant.Enabled = VerifBtnEnregistreIntervenant();
        }
        /// <summary>
        /// Méthode privée testant le contrôle combo et la variable IdStatutSelectionne qui contient une valeur
        /// Cette méthode permetra ensuite de définir l'état du bouton BtnEnregistrerIntervenant
        /// </summary>
        /// <returns></returns>
        private Boolean VerifBtnEnregistreIntervenant()
        {
            return CmbAtelierIntervenant.Text != "Choisir" && this.IdStatutSelectionne.Length > 0;
        }

        private void RadGestionAtelier_CheckedChanged(object sender, EventArgs e)
        {
            PanelCreerAtelier.Visible = true;
            PanelCreerAtelierTheme.Visible = true;
            PanelCreerAtelierVacation.Visible = true;
            PanelCreerAtelier.Enabled = true;
            PanelCreerAtelierTheme.Enabled = true;
            PanelCreerAtelierVacation.Enabled = true;
            BtnCreerAtelierEnregistrer.Visible = true;
            BtnCreerAtelierEnregistrer.Enabled = true;

            PanelCreerTheme.Visible = false;
            PanelCreerTheme.Enabled = false;

            PanelCreerVacation.Visible = false;
            PanelCreerVacation.Enabled = false;
            PanelCreerVacationSuite.Visible = false;
            PanelCreerVacationSuite.Enabled = false;
        }

        private void BtnCreerAtelierCreerThemeAjout_Click(object sender, EventArgs e)
        {
            if (this.TxtCreerAtelierCreerTheme.Text != string.Empty)
            {
                this.ListeCreerAtelierCreerTheme.Items.Add(ChainePropre);
                this.TxtCreerAtelierCreerTheme.Text = string.Empty;
            }
        }

        private void TxtCreerAtelierCreerTheme_TextChanged(object sender, EventArgs e)
        {
            char[] CharsToTrim = { ',', '.', ';', ' ' };
            string ChaineANettoyer = TxtCreerAtelierCreerTheme.Text;
            Regex RegexDebutChaine = new Regex(@"^[^\s]");

            if (RegexDebutChaine.IsMatch(ChaineANettoyer))
            {
                ChainePropre = ChaineANettoyer.TrimEnd(CharsToTrim);
                if (ChainePropre == "")
                    this.BtnCreerAtelierCreerThemeAjout.Enabled = false;

                else 
                    this.BtnCreerAtelierCreerThemeAjout.Enabled = true;

            }
            else
            {
                this.BtnCreerAtelierCreerThemeAjout.Enabled = false;
                this.TxtCreerAtelierCreerTheme.Text = string.Empty;
            }
        }

        private void ListeCreerAtelierCreerTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListeCreerAtelierCreerTheme.SelectedItems.Count > 0)
            {
                this.BtnCreerAtelierCreerThemeRetirer.Enabled = true;
            }
            else
            {
                this.BtnCreerAtelierCreerThemeRetirer.Enabled = false;
            }
        }

        private void BtnCreerAtelierCreerThemeRetirer_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (this.ListeCreerAtelierCreerTheme.SelectedItems.Count > 0)
            {
                foreach (int UnIndice in ListeCreerAtelierCreerTheme.SelectedIndices)
                {
                    this.ListeCreerAtelierCreerTheme.Items.RemoveAt(UnIndice - i);
                    i++;
                }
            }
        }

        private void BtnCreerAtelierEnregistrer_Click(object sender, EventArgs e)
        {
            Collection<String> VacationsDebut = new Collection<String>();
            Collection<String> VacationsFin = new Collection<String>();
            Collection<String> Themes = new Collection<String>();

            foreach (Control UnControle in PanelCreerAtelierVacation.Controls)
            {
                if (UnControle is ComposantVacation.ComposantVacation)
                {
                   MessageBox.Show(UnControle.Controls[5].Text);
                }
            }

            /*foreach (ListViewItem UnTheme in ListeCreerAtelierCreerTheme.Items)
            {
                Themes.Add(UnTheme.Text);
            }

            UneConnexion.AjoutAtelier(TxtCreerAtelierNom.Text, Convert.ToInt32(NumCreerAtelierNbPlaces.Text), Themes, VacationsDebut, VacationsFin);*/
        }
        /// <summary>
        /// Procedure permettant de vider les champs des Textbox contenue dans une collection de controles d'un panel
        /// </summary>
        /// <param name="UneCollectionDeControle">Collection contenant un ensemble de controles</param>
        private void ViderLesChamps(MetroPanel.ControlCollection UneCollectionDeControle)
        {
            foreach (Control UnControle in UneCollectionDeControle)
            {
                if (UnControle is MetroTextBox)
                    ((MetroTextBox)UnControle).Clear();
                else if (UnControle is MaskedTextBox)
                    ((MaskedTextBox)UnControle).Clear();
            }
        }

        private void RadGestionTheme_CheckedChanged(object sender, EventArgs e)
        {
            DataTable UneDataTable = new DataTable();
            if (RadGestionTheme.Checked)
            {
                PanelCreerAtelier.Enabled = false;
                PanelCreerAtelier.Visible = false;
                PanelCreerAtelierTheme.Visible = false;
                PanelCreerAtelierTheme.Enabled = false;
                PanelCreerAtelierVacation.Visible = false;
                PanelCreerAtelierVacation.Enabled = false;
                BtnCreerAtelierEnregistrer.Visible = false;

                PanelCreerTheme.Enabled = true;
                PanelCreerTheme.Visible = true;

                PanelCreerVacation.Visible = false;
                PanelCreerVacation.Enabled = false;
                PanelCreerVacationSuite.Visible = false;
                PanelCreerVacationSuite.Enabled = false;

                UneDataTable = UneConnexion.ObtenirDonnesOracle("VATELIER01");
                CbbCreerThemeAtelier.DataSource = UneDataTable;
                CbbCreerThemeAtelier.DisplayMember = "LIBELLE";
                CbbCreerThemeAtelier.ValueMember = "ID";
            }
        }

        private void RadGestionVacation_CheckedChanged(object sender, EventArgs e)
        {
            DataTable UneDataTable = new DataTable();
            PanelsVacations.Add(PanelCreerVacation1);
            PanelsVacations.Add(PanelCreerVacation2);
            PanelsVacations.Add(PanelCreerVacation3);
            PanelsVacations.Add(PanelCreerVacation4);
            PanelsVacations.Add(PanelCreerVacation5);
            if (RadGestionVacation.Checked)
            {
                PanelCreerAtelier.Enabled = false;
                PanelCreerAtelier.Visible = false;
                PanelCreerAtelierTheme.Visible = false;
                PanelCreerAtelierTheme.Enabled = false;
                PanelCreerAtelierVacation.Visible = false;
                PanelCreerAtelierVacation.Enabled = false;
                BtnCreerAtelierEnregistrer.Visible = false;

                PanelCreerTheme.Enabled = false;
                PanelCreerTheme.Visible = false;

                PanelCreerVacation.Visible = true;
                PanelCreerVacation.Enabled = true;
                PanelCreerVacationSuite.Visible = true;
                PanelCreerVacationSuite.Enabled = true;

                UneDataTable = UneConnexion.ObtenirDonnesOracle("VATELIER01");
                CbbCreerVacationAtelier.DataSource = UneDataTable;
                CbbCreerVacationAtelier.DisplayMember = "LIBELLE";
                CbbCreerVacationAtelier.ValueMember = "ID";
                CbbCreerVacationAtelier.Refresh();
            }
        }

        private void CbbCreerVacationAtelier_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable UneDataTable = new DataTable();
            for (int i = 0; i < 5; i++)
            {
                PanelsVacations[i].Enabled = false;
                PanelsVacations[i].Visible = false;
            }
            int TestParse = 0;
            int.TryParse(CbbCreerVacationAtelier.SelectedValue.ToString(), out TestParse);
            if (TestParse == 0)
            {
                UneDataTable = UneConnexion.ObtenirVacationAtelier(Convert.ToInt32(((DataRowView)CbbCreerVacationAtelier.SelectedValue)["id"]));
                NombreLigne = UneDataTable.Rows.Count;
                for (int i = 0; i < NombreLigne; i++)
                {
                    PanelsVacations[i].Enabled = true;
                    PanelsVacations[i].Visible = true;
                    PanelsVacations[i].Controls[0].Text = UneDataTable.Rows[i]["heurefin"].ToString();
                    PanelsVacations[i].Controls[4].Text = UneDataTable.Rows[i]["heuredebut"].ToString();
                    PanelsVacations[i].Refresh();

                }
            }
            if (TestParse != 0)
            {
                UneDataTable = UneConnexion.ObtenirVacationAtelier(Convert.ToInt32(CbbCreerVacationAtelier.SelectedValue));
                NombreLigne = UneDataTable.Rows.Count;
                for (int i = 0; i < NombreLigne; i++)
                {
                    PanelsVacations[i].Enabled = true;
                    PanelsVacations[i].Visible = true;
                    PanelsVacations[i].Controls[0].Text = UneDataTable.Rows[i]["heurefin"].ToString();
                    PanelsVacations[i].Controls[4].Text = UneDataTable.Rows[i]["heuredebut"].ToString();
                    PanelsVacations[i].Refresh();
                }
            }

        }

        private void BtnCreerVacationEnregistrer_Click(object sender, EventArgs e)
        {
            Collection<String> VacationsDebut = new Collection<String>();
            Collection<String> VacationsFin = new Collection<String>();
            for (int i = 0; i < NombreLigne; i++)
            {
                VacationsDebut.Add(PanelsVacations[i].Controls[0].Text);
                VacationsFin.Add(PanelsVacations[i].Controls[2].Text);
            }
            UneConnexion.ModificationVacation(VacationsDebut, VacationsFin, Convert.ToInt32(CbbCreerVacationAtelier.SelectedValue));
        }

        private void BtnCreerTheme_Click(object sender, EventArgs e)
        {
            UneConnexion.AjoutTheme(Convert.ToInt32(CbbCreerThemeAtelier.SelectedValue), TxtCreerThemeNom.Text);
        }

        private void BtnEnregistrerBenevole_Click(object sender, EventArgs e)
        {
            Collection<Int16> IdDatesSelectionnees = new Collection<Int16>();
            Int64? NumeroLicence;
            if (TxtLicenceBenevole.MaskCompleted)
            {
                NumeroLicence = System.Convert.ToInt64(TxtLicenceBenevole.Text);
            }
            else
            {
                NumeroLicence = null;
            }


            foreach (Control UnControle in PanelDispoBenevole.Controls)
            {
                if (UnControle.GetType().Name == "MetroCheckBox" && ((MetroCheckBox)UnControle).Checked)
                {
                    /* Un name de controle est toujours formé come ceci : xxx_Id où id représente l'id dans la table
                     * Donc on splite la chaine et on récupére le deuxième élément qui correspond à l'id de l'élément sélectionné.
                     * on rajoute cet id dans la collection des id des dates sélectionnées
                        
                    */
                    IdDatesSelectionnees.Add(System.Convert.ToInt16((UnControle.Name.Split('_'))[1]));
                }
            }
            UneConnexion.InscrireBenevole(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, TxtTel.MaskCompleted ? TxtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToDateTime(TxtDateNaissance.Text), NumeroLicence, IdDatesSelectionnees, photoByte);
        }

        private void FrmPrincipale_FormClosing(object sender, FormClosingEventArgs e)
        {
            UneConnexion.FermerConnexion();
        }

        private void RdbNuiteIntervenantOui_CheckedChanged(object sender, EventArgs e)
        {
            if (((MetroRadioButton)sender).Name == "RdbNuiteIntervenantOui")
            {
                PanelNuiteIntervenant.Visible = true;
                if (PanelNuiteIntervenant.Controls.Count == 2) // on charge les nuites possibles possibles et on les affiche
                {
                    //DataTable LesDateNuites = UneConnexion.ObtenirDonnesOracle("VDATENUITE01");
                    //foreach(Dat
                    Dictionary<Int16, String> LesNuites = UneConnexion.ObtenirDatesNuites();
                    int i = 0;
                    foreach (KeyValuePair<Int16, String> UneNuite in LesNuites)
                    {
                        ComposantNuite.ResaNuite unResaNuit = new ResaNuite(UneConnexion.ObtenirDonnesOracle("VHOTEL01"), (UneConnexion.ObtenirDonnesOracle("VCATEGORIECHAMBRE01")), UneNuite.Value, UneNuite.Key);
                        unResaNuit.Left = 5;
                        unResaNuit.Top = 15 + (34 * i++);
                        unResaNuit.Visible = true;
                        //unResaNuit.click += new System.EventHandler(ComposantNuite_StateChanged);
                        PanelNuiteIntervenant.Controls.Add(unResaNuit);
                    }

                }
            }
        }

        private void BtnEnregistrerIntervenant_Click(object sender, EventArgs e)
        {
            try
            {
                if (RdbNuiteIntervenantOui.Checked)
                {
                    // inscription avec les nuitées
                    Collection<Int16> NuitsSelectionnes = new Collection<Int16>();
                    Collection<String> HotelsSelectionnes = new Collection<String>();
                    Collection<String> CategoriesSelectionnees = new Collection<string>();
                    foreach (Control UnControle in PanelNuiteIntervenant.Controls)
                    {
                        if (UnControle.GetType().Name == "ResaNuite" && ((ResaNuite)UnControle).GetNuitSelectionnee())
                        {
                            // la nuité a été cochée, il faut donc envoyer l'hotel et la type de chambre à la procédure de la base qui va enregistrer le contenu hébergement 
                            //ContenuUnHebergement UnContenuUnHebergement= new ContenuUnHebergement();
                            CategoriesSelectionnees.Add(((ResaNuite)UnControle).GetTypeChambreSelectionnee());
                            HotelsSelectionnes.Add(((ResaNuite)UnControle).GetHotelSelectionne());
                            NuitsSelectionnes.Add(((ResaNuite)UnControle).IdNuite);
                        }

                    }
                    if (NuitsSelectionnes.Count == 0)
                    {
                        MetroMessageBox.Show(this, "Si vous avez sélectionné que l'intervenant avait des nuités, il faut qu'au moins une nuit soit sélectionnée", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        UneConnexion.InscrireIntervenant(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, TxtTel.MaskCompleted ? TxtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbAtelierIntervenant.SelectedValue), this.IdStatutSelectionne, CategoriesSelectionnees, HotelsSelectionnes, NuitsSelectionnes, photoByte);
                        MetroMessageBox.Show(this, "Inscription intervenant effectuée", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                { // inscription sans les nuitées
                    UneConnexion.InscrireIntervenant(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, TxtTel.MaskCompleted ? TxtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbAtelierIntervenant.SelectedValue), this.IdStatutSelectionne, photoByte);
                    MetroMessageBox.Show(this,"Inscription intervenant effectuée", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
            catch (Exception Ex)
            {
                MetroMessageBox.Show(this,Ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Control Openfiledialog où on va récupérer une image dont la taille en pixel ne doit pas
        /// depasser 150x150 et ne doit pas depasser 1mo en poids
        /// Une fois la validation faite, la photo est transformer en un tableau de byte (photobyte)
        /// et va servir en parametre oracle Blob
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnParcourirPhotoParticipant_Click(object sender, EventArgs e)
        {
            try
            {
                OfpPhotoParticipant.Title = "Selectionner une photo";
                OfpPhotoParticipant.Filter = "Images (*.jpg)|*.jpg";
                OfpPhotoParticipant.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                DialogResult res = OfpPhotoParticipant.ShowDialog();

                if (res == DialogResult.OK)
                {
                    System.IO.FileInfo DetailPhoto = new System.IO.FileInfo(OfpPhotoParticipant.FileName);
                    Bitmap img = new Bitmap(OfpPhotoParticipant.FileName);
                    if (img.Width > 150 || img.Height > 150 || DetailPhoto.Length > 1000000)
                        throw new Exception();
                    else PboxApercuParticipant.Image = img;
                    LblDesignPhotoParticipant.Visible = true;
                    LblNomPhotoParticipant.Text = OfpPhotoParticipant.SafeFileName.ToString();

                    FileStream fs = new System.IO.FileStream(OfpPhotoParticipant.FileName.ToString(), FileMode.Open, FileAccess.Read);
                    photoByte = new byte[fs.Length];
                    fs.Read(photoByte, 0, photoByte.Length);
                    fs.Close();
                }
            }
            catch (Exception Ex)
            {
                string messageComplet = "Verifier la taille de votre photo (Max. 150x150)" + Environment.NewLine + "Verifier le poid de la photo (Max. 1Mo)";
                MetroMessageBox.Show(this, messageComplet, "Une Erreur est survenue", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void BtnCreerAtelierAjoutVacation_Click(object sender, EventArgs e)
        {

            if (NombreVacationCreerAtelier % 2 == 0)
            {
                y += 64;
                ComposantVacation.ComposantVacation CreerAtelierUneVacation = new ComposantVacation.ComposantVacation();
                CreerAtelierUneVacation.Location = new Point(x, y);
                PanelCreerAtelierVacation.Controls.Add(CreerAtelierUneVacation);
                NombreVacationCreerAtelier++;
            }
            else
            {
                ComposantVacation.ComposantVacation CreerAtelierUneVacation = new ComposantVacation.ComposantVacation();
                CreerAtelierUneVacation.Location = new Point(x2, y);
                PanelCreerAtelierVacation.Controls.Add(CreerAtelierUneVacation);
                NombreVacationCreerAtelier++;
            }
        }

        private void BtnCreerAtelierRetirerVacation_Click(object sender, EventArgs e)
        {
            if (PanelCreerAtelierVacation.Controls.Count > 5)
            {
                NombreVacationCreerAtelier--;
                if (NombreVacationCreerAtelier % 2 == 0)
                {
                    y -= 64;
                }
                PanelCreerAtelierVacation.Controls.RemoveAt(PanelCreerAtelierVacation.Controls.Count - 1);
            }
        }
    }
}
