using BaseDeDonnees;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using System.Collections.ObjectModel;
using ComposantNuite;
using System.IO;
using System.Text.RegularExpressions;

namespace MDLMetro
{
    public partial class FrmPrincipale : MetroFramework.Forms.MetroForm
    {
        private String IdStatutSelectionne = "";
        private Bdd UneConnexion;

        
        //Variables correspondant a la création dynamique des composants pour la gestion des vacations.
        //Nombre de ligne remplies par les composants.
        private int NombreLigne = 0;
        private int NombreVacationCreerAtelier = 0;
        private int NombreVacationModifier = 0;
        private int NombreVacationModifierAjouter = 0;
        //Colone des composants de gauche.
        private int XVacationCreerAtelier = 14;
        //Ligne des composants.
        private int YVacationCreerAtelier = 0;
        //Colone des composants de droite.
        private int X2VacationCreerAtelier = 630;

        byte[] photoByte;
        private string ChainePropre;
        private float totalnuite1 = 0;
        private float totalnuite2 = 0;
        private int totalRepasSupp = 0;

        Dictionary<Int16, String> LesNuites;

        /// <summary>
        /// Initialise la fenetre principale.
        /// </summary>
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
                    this.GererInscriptionLicencie();
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
            PanelLicencie.Visible = false;

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
            PanelLicencie.Visible = false;
            PanelFonctionIntervenant.Visible = true;
            Utilitaire.CreerDesControles(this, UneConnexion, "VSTATUT01", "Rad_", PanelFonctionIntervenant, "MetroRadioButton", this.rdbStatutIntervenant_StateChanged);
            Utilitaire.RemplirComboBox(UneConnexion, CmbAtelierIntervenant, "VATELIER01");
        }



        /// <summary>     
        /// procédure permettant d'afficher l'interface de saisie du complément d'inscription d'un licencié.
        /// </summary>
        private void GererInscriptionLicencie()
        {
            PanelBenevole.Visible = false;
            PanelIntervenant.Visible = false;
            PanelLicencie.Visible = true;
            PanelFonctionIntervenant.Visible = true;
            Utilitaire.RemplirComboBox(UneConnexion, CmbAtelierLicencie, "VATELIER01");
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
            return CmbAtelierIntervenant.Items.Count > 0 && this.IdStatutSelectionne.Length > 0;
        }

        /// <summary>
        /// Événement permettant d'activer et de rendre visibles tous les éléments graphiques correspondant à la gestion des ateliers lorsque le bouton radio correspondant est coché.
        /// Et de cacher les éléments ne correspondants pas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadGestionAtelier_CheckedChanged(object sender, EventArgs e)
        {
            if (RadGestionAtelier.Checked)
            {
                ViderChampsGestionAtelierThemeVacation();

                NombreLigne = 0;
                NombreVacationCreerAtelier = 0;
                NombreVacationModifier = 0;
                XVacationCreerAtelier = 14;
                YVacationCreerAtelier = 0;
                X2VacationCreerAtelier = 630;

                PanelCreerAtelier.Visible = true;
                PanelCreerAtelierTheme.Visible = true;
                PanelCreerAtelierVacation.Visible = true;
                PanelCreerAtelier.Enabled = true;
                PanelCreerAtelierTheme.Enabled = true;
                PanelCreerAtelierVacation.Enabled = true;
                BtnCreerAtelierEnregistrer.Visible = true;
                BtnCreerAtelierEnregistrer.Enabled = false;

                PanelCreerTheme.Visible = false;
                PanelCreerTheme.Enabled = false;

                PanelCreerVacation.Visible = false;
                PanelCreerVacation.Enabled = false;
                PanelCreerVacationSuite.Visible = false;
                PanelCreerVacationSuite.Enabled = false;

            }
        }

        /// <summary>
        /// Ajoute un thème dans la liste des thèmes a la création d'un atelier après vérification du thème par l'événement TxtCreerAtelierCreerTheme_TextChanged.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreerAtelierCreerThemeAjout_Click(object sender, EventArgs e)
        {
            if (this.TxtCreerAtelierCreerTheme.Text != string.Empty)
            {
                this.ListeCreerAtelierCreerTheme.Items.Add(ChainePropre);
                this.TxtCreerAtelierCreerTheme.Text = string.Empty;
            }
            CheckCreerAtelier();
        }

        /// <summary>
        /// Vérifie que le nom du thème soit valide lorsque l'utilisateur entre les données dans le champ permettant la création d'un thème à la création d'un atelier.
        /// Entre le texte valide dans une variable (pas d'espaces au debut et a la fin, pas de symboles).
        /// Si le texte est invalide l'utilisateur ne peut pas l'ajouter à la liste des thèmes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Permets lors de la sélection d'un thème existant dans la liste des thèmes d'activer le bouton permettant de supprimer le thème.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Permet de supprimer un ou plusieurs thème de la liste des thèmes a la création d'un atelier.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            CheckCreerAtelier();
        }

        /// <summary>
        /// Permet d'enregistrer un atelier.
        /// Crée une collection des dates de debut des vacations, idem pour les dates de fin et crée une collection des thèmes.
        /// Utilise les collections (de type texte) pour crée l'atelier.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreerAtelierEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                Collection<String> VacationsDebut = new Collection<String>();
                Collection<String> VacationsFin = new Collection<String>();
                Collection<String> Themes = new Collection<String>();

                foreach (Control UnControle in PanelCreerAtelierVacation.Controls)
                {
                    if (UnControle is ComposantVacation.ComposantVacation)
                    {
                        if (UnControle.Controls[3].BackColor == Color.Green)
                        {
                            string UnDebut = UnControle.Controls[5].Text + " " + UnControle.Controls[3].Text;
                            string UneFin = UnControle.Controls[5].Text + " " + UnControle.Controls[1].Text;
                            VacationsDebut.Add(UnDebut);
                            VacationsFin.Add(UneFin);
                        }
                        else
                        {
                            throw new Exception("Une des heures ne respecte pas le bon format.");
                        }
                    }
                }

                foreach (ListViewItem UnTheme in ListeCreerAtelierCreerTheme.Items)
                {
                    Themes.Add(UnTheme.Text);
                }

                UneConnexion.AjoutAtelier(TxtCreerAtelierNom.Text, Convert.ToInt32(NumCreerAtelierNbPlaces.Text), Themes, VacationsDebut, VacationsFin);
                ViderChampsGestionAtelierThemeVacation();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this,ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        /// <summary>
        /// Événement permettant d'activer et de rendre visibles tous les éléments graphiques correspondant à la gestion des thèmes lorsque le bouton radio correspondant est coché.
        /// Et de cacher les éléments ne correspondants pas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadGestionTheme_CheckedChanged(object sender, EventArgs e)
        {
            DataTable UneDataTable = new DataTable();
            if (RadGestionTheme.Checked)
            {
                ViderChampsGestionAtelierThemeVacation();

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

        /// <summary>
        /// Événement permettant d'activer et de rendre visibles tous les éléments graphiques correspondant à la gestion des vacations lorsque le bouton radio correspondant est coché.
        /// Et de cacher les éléments ne correspondants pas.
        /// Permet aussi d'initialisé la liste des ateliers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadGestionVacation_CheckedChanged(object sender, EventArgs e)
        {
            DataTable UneDataTable = new DataTable();
            if (RadGestionVacation.Checked)
            {
                ViderChampsGestionAtelierThemeVacation();

                NombreLigne = 0;
                NombreVacationCreerAtelier = 0;
                NombreVacationModifier = 0;
                XVacationCreerAtelier = 14;
                YVacationCreerAtelier = 0;
                X2VacationCreerAtelier = 630;

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

                CbbCreerVacationAtelier.SelectedIndexChanged -= new System.EventHandler(this.CbbCreerVacationAtelier_SelectedIndexChanged);
                UneDataTable = UneConnexion.ObtenirDonnesOracle("VATELIER01");
                CbbCreerVacationAtelier.DataSource = UneDataTable;
                CbbCreerVacationAtelier.DisplayMember = "LIBELLE";
                CbbCreerVacationAtelier.ValueMember = "ID";
                CbbCreerVacationAtelier.Refresh();
                CbbCreerVacationAtelier.SelectedIndexChanged += new System.EventHandler(this.CbbCreerVacationAtelier_SelectedIndexChanged);
                CbbCreerVacationAtelier_SelectedIndexChanged(sender, e);
                //Activer/désactiver l'événement SelectedIndexChanged permet de contourner le problème suivant :
                //l'événement SelectedIndexChanged s'active lors d'un changement dans la combo-box mais aussi lors du REMPLISSAGE de la combo-box.
                //Ce qui pose problème car le premier objet est un type datarowview et il faut donc le traiter autrement que les autres.
                //Donc en désactivant l'événement lors du remplissage, il ne se passera rien. Une fois la combo-box remplie, on réactive l'événement.
                //ViderChampsGestionAtelierThemeVacation();
            }
        }

        /// <summary>
        /// Permet de supprimer les composants dynamiques correspondants a la modification des vacations lorsqu'un atelier est choisi.
        /// Permet par la suite de crée des composants dynamiques correspondants a la modification des vacations pour l'atelier choisi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbbCreerVacationAtelier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CbbCreerVacationAtelier.Items.Count > 0)
            {
                BtnCreerVacationEnregistrer.Enabled = true;
            }
            else
            {
                BtnCreerVacationEnregistrer.Enabled = false;
            }

            DataTable UneDataTable = new DataTable();
            //tant qu'il y a des composant dynamique pour la modification de vacation, on supprime.
            while (PanelCreerVacationSuite.Controls.Count != 5) // 5 correspond au nombre de controls avec aucun composant.
            {
                PanelCreerVacationSuite.Controls.RemoveAt(PanelCreerVacationSuite.Controls.Count - 1);//4 pour ne pas supprimer le label et les boutons + et -
            }

            NombreLigne = 0;
            NombreVacationCreerAtelier = 0;
            NombreVacationModifier = 0;
            NombreVacationModifierAjouter = 0;
            XVacationCreerAtelier = 14;
            YVacationCreerAtelier = 0;
            X2VacationCreerAtelier = 630;
            //On récupere les vacations éxistantes pour l'atelier.
            UneDataTable = UneConnexion.ObtenirVacationAtelier(Convert.ToInt32(CbbCreerVacationAtelier.SelectedValue));
            //On compte le nombre de vacations.
            NombreLigne = UneDataTable.Rows.Count;
            //On crée un composant vacation PAR vacation correspondant à l'atelier et on les complète avec les données récupérées de la base de données.
            for (int i = 0; i < NombreLigne; i++)
            {
                string[] datedebut = UneDataTable.Rows[i]["heuredebut"].ToString().Split(' '); // Ici on separe la date et l'heure pour les differents controls
                string[] datefin = UneDataTable.Rows[i]["heurefin"].ToString().Split(' ');
                 //Si le nombre de vacation est pair, il s'agit d'une vacation qui sera a gauche.
                if (NombreVacationModifier % 2 == 0)
                {
                    YVacationCreerAtelier += 64;
                    ComposantVacation.ComposantVacation ModifierUneVacation = new ComposantVacation.ComposantVacation();
                    ModifierUneVacation.Location = new Point(XVacationCreerAtelier, YVacationCreerAtelier);
                    ModifierUneVacation.Controls[5].Text = datedebut[0];
                    ModifierUneVacation.Controls[3].Text = datedebut[1];
                    ModifierUneVacation.Controls[1].Text = datefin[1];
                    ModifierUneVacation.Controls[1].BackColor = Color.Green;
                    ModifierUneVacation.Controls[3].BackColor = Color.Green;
                    PanelCreerVacationSuite.Controls.Add(ModifierUneVacation);
                    NombreVacationModifier++;
                }
                //Sinon a droite.
                else
                {
                    ComposantVacation.ComposantVacation ModifierUneVacation = new ComposantVacation.ComposantVacation();
                    ModifierUneVacation.Location = new Point(X2VacationCreerAtelier, YVacationCreerAtelier);
                    ModifierUneVacation.Controls[5].Text = datedebut[0];
                    ModifierUneVacation.Controls[3].Text = datedebut[1];
                    ModifierUneVacation.Controls[1].Text = datefin[1];
                    ModifierUneVacation.Controls[1].BackColor = Color.Green;
                    ModifierUneVacation.Controls[3].BackColor = Color.Green;
                    PanelCreerVacationSuite.Controls.Add(ModifierUneVacation);
                    NombreVacationModifier++;
                }
            }
        }

        /// <summary>
        /// Permet de sauvegarder les vacations après modification.
        /// Crée des dates en type texte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreerVacationEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                int NombreModifier = NombreVacationModifier - NombreVacationModifierAjouter;//permet de compter le nombre de vacation modifiée et non ajoutée
                Collection<String> VacationsDebut = new Collection<String>();
                Collection<String> VacationsFin = new Collection<String>();
                Collection<String> VacationsDebutAjouter = new Collection<String>();
                Collection<String> VacationsFinAjouter = new Collection<String>();
                foreach (Control UnControle in PanelCreerVacationSuite.Controls)
                {
                    if (UnControle is ComposantVacation.ComposantVacation && NombreModifier > 0)
                    {
                        if (UnControle.Controls[3].BackColor == Color.Green)
                        {
                            string UnDebut = UnControle.Controls[5].Text + " " + UnControle.Controls[3].Text;
                            string UneFin = UnControle.Controls[5].Text + " " + UnControle.Controls[1].Text;
                            VacationsDebut.Add(UnDebut);
                            VacationsFin.Add(UneFin);
                            NombreModifier--;
                        }
                        else
                        {
                            throw new Exception("Une des heures ne respecte pas le bon format.");
                        }
                    }
                    else if (UnControle is ComposantVacation.ComposantVacation && NombreModifier == 0)
                    {
                        if (UnControle.Controls[3].BackColor == Color.Green)
                        {
                            string UnDebut = UnControle.Controls[5].Text + " " + UnControle.Controls[3].Text;
                            string UneFin = UnControle.Controls[5].Text + " " + UnControle.Controls[1].Text;
                            VacationsDebutAjouter.Add(UnDebut);
                            VacationsFinAjouter.Add(UneFin);
                        }
                        else
                        {
                            throw new Exception("Une des heures ne respecte pas le bon format.");
                        }
                    }
                }
                UneConnexion.ModificationVacation(VacationsDebut, VacationsFin, Convert.ToInt32(CbbCreerVacationAtelier.SelectedValue));
                UneConnexion.AjoutVacations(VacationsDebutAjouter, VacationsFinAjouter, Convert.ToInt32(CbbCreerVacationAtelier.SelectedValue));
                ViderChampsGestionAtelierThemeVacation();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        /// <summary>
        /// Permet de crée un thème.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreerTheme_Click(object sender, EventArgs e)
        {
            UneConnexion.AjoutTheme(Convert.ToInt32(CbbCreerThemeAtelier.SelectedValue), TxtCreerThemeNom.Text);
            ViderChampsGestionAtelierThemeVacation();
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
            ViderChampsBenevole();
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
                    LesNuites = UneConnexion.ObtenirDatesNuites();
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
                ViderChampsIntervenant();
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

        /// <summary>
        /// Permet d'ajouter un composant vacation dynamique lors de la création d'un atelier et de la pression du bouton "+".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreerAtelierAjoutVacation_Click(object sender, EventArgs e)
        {
            //5 vacations maximum.
            if (NombreVacationCreerAtelier < 5)
            {
                //Si le nombre de vacation est pair la prochaine sera du coté gauche.
                if (NombreVacationCreerAtelier % 2 == 0)
                {
                    YVacationCreerAtelier += 64;
                    ComposantVacation.ComposantVacation CreerAtelierUneVacation = new ComposantVacation.ComposantVacation();
                    CreerAtelierUneVacation.Location = new Point(XVacationCreerAtelier, YVacationCreerAtelier);
                    PanelCreerAtelierVacation.Controls.Add(CreerAtelierUneVacation);
                    NombreVacationCreerAtelier++;
                }
                //Sinon du coté droit.
                else
                {
                    ComposantVacation.ComposantVacation CreerAtelierUneVacation = new ComposantVacation.ComposantVacation();
                    CreerAtelierUneVacation.Location = new Point(X2VacationCreerAtelier, YVacationCreerAtelier);
                    PanelCreerAtelierVacation.Controls.Add(CreerAtelierUneVacation);
                    NombreVacationCreerAtelier++;
                }
            }
            CheckCreerAtelier();
        }

        /// <summary>
        /// Supprimer un composant vacation dynamique de la page de création d'un atelier.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreerAtelierRetirerVacation_Click(object sender, EventArgs e)
        {
            if (PanelCreerAtelierVacation.Controls.Count > 5)
            {
                NombreVacationCreerAtelier--;
                if (NombreVacationCreerAtelier % 2 == 0)
                {
                    YVacationCreerAtelier -= 64;
                }
                PanelCreerAtelierVacation.Controls.RemoveAt(PanelCreerAtelierVacation.Controls.Count - 1);
            }
            CheckCreerAtelier();
        }

        private void RdbNuiteIntervenantNon_CheckedChanged(object sender, EventArgs e)
        {
            PanelNuiteIntervenant.Visible = false;
        }

        /// <summary>
        /// Contrôle activant ou désactivant la possibilité d'enregistrer un atelier selon le résultat.
        /// </summary>
        private void CheckCreerAtelier()
        {
            if (NumCreerAtelierNbPlaces.Value > 0 && TxtCreerAtelierNom.Text != "" && ListeCreerAtelierCreerTheme.Items.Count > 0 && NombreVacationCreerAtelier > 0)
            {
                BtnCreerAtelierEnregistrer.Enabled = true;
            }
            else
            {
                BtnCreerAtelierEnregistrer.Enabled = false;
            }
        }

        /// <summary>
        /// Contrôle activant ou désactivant la possibilité d'enregistrer un thème selon le résultat.
        /// </summary>
        private void CheckCreerTheme()
        {
            if (TxtCreerThemeNom.Text != "" && CbbCreerThemeAtelier.Text != "")
            {
                BtnCreerTheme.Enabled = true;
            }
            else
            {
                BtnCreerTheme.Enabled = false;
            }
        }

        private void NumCreerAtelierNbPlaces_ValueChanged(object sender, EventArgs e)
        {
            CheckCreerAtelier();
        }

        private void TxtCreerThemeNom_TextChanged(object sender, EventArgs e)
        {
            CheckCreerTheme();
        }

        private void ViderChampsIntervenant()
        {
            
            TxtNom.Text = null;
            TxtPrenom.Text = null;
            TxtAdr1.Text = null;
            TxtAdr2.Text = null;
            TxtCp.Text = null;
            TxtVille.Text = null;
            TxtTel.Text = null;
            TxtMail.Text = null;
            IdStatutSelectionne = null;
            photoByte = null;
            PanelIntervenant.Visible = false;
            RadIntervenant.Checked = false;
            RdbNuiteIntervenantOui.Checked = false;
            RdbNuiteIntervenantNon.Checked = true;
            if (LesNuites != null)
            {
                foreach (KeyValuePair<Int16, String> UneNuite in LesNuites)
                {
                    PanelNuiteIntervenant.Controls.RemoveAt(PanelNuiteIntervenant.Controls.Count - 1);
                }
            }
            while (PanelFonctionIntervenant.Controls.Count > 0)
            {
                PanelFonctionIntervenant.Controls.RemoveAt(PanelFonctionIntervenant.Controls.Count - 1);
            }
            BtnEnregistrerIntervenant.Enabled = false;
            PanelIntervenant.Visible = false;
        }

        private void ViderChampsBenevole()
        {
            TxtNom.Text = null;
            TxtPrenom.Text = null;
            TxtAdr1.Text = null;
            TxtAdr2.Text = null;
            TxtCp.Text = null;
            TxtVille.Text = null;
            TxtTel.Text = null;
            TxtMail.Text = null;
            TxtDateNaissance.Text = null;
            TxtLicenceBenevole.Text = null;
            photoByte = null;
            RadBenevole.Checked = false;
            while (PanelDispoBenevole.Controls.Count > 0)
            {
                PanelDispoBenevole.Controls.RemoveAt(PanelDispoBenevole.Controls.Count - 1);
            }
            BtnEnregistrerBenevole.Enabled = false;
            PanelBenevole.Visible = false;
        }

        private void TxtLicenceBenevole_TextChanged(object sender, EventArgs e)
        {
            BtnEnregistrerBenevole.Enabled = (TxtLicenceBenevole.Text == "" || TxtLicenceBenevole.MaskCompleted) && TxtDateNaissance.MaskCompleted && Utilitaire.CompteChecked(PanelDispoBenevole) > 0;
        }

        private void TxtDateNaissance_TextChanged(object sender, EventArgs e)
        {
            BtnEnregistrerBenevole.Enabled = (TxtLicenceBenevole.Text == "" || TxtLicenceBenevole.MaskCompleted) && TxtDateNaissance.MaskCompleted && Utilitaire.CompteChecked(PanelDispoBenevole) > 0;
        }

        private void ViderChampsGestionAtelierThemeVacation()
        {
            TxtCreerAtelierNom.Text = null;
            NumCreerAtelierNbPlaces.Text = null;
            TxtCreerAtelierCreerTheme.Text = null;
            BtnCreerAtelierEnregistrer.Enabled = false;
            foreach (Control UnControle in PanelCreerAtelierVacation.Controls)
            {
                if (PanelCreerAtelierVacation.Controls.Count > 5)
                {
                    NombreVacationCreerAtelier--;
                    if (NombreVacationCreerAtelier % 2 == 0)
                    {
                        YVacationCreerAtelier -= 64;
                    }
                    PanelCreerAtelierVacation.Controls.RemoveAt(PanelCreerAtelierVacation.Controls.Count - 1);
                }
            }

            for (int i = this.ListeCreerAtelierCreerTheme.Items.Count - 1; this.ListeCreerAtelierCreerTheme.Items.Count > 0; i--)
            {
                this.ListeCreerAtelierCreerTheme.Items.RemoveAt(i);
            }
            TxtCreerThemeNom.Text = null;
        }

        private void ChkCheque1_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCheque1.Checked)
            {
                TxtMontantCheque1.Enabled = true;
                ChkCheque2.Enabled = true;
            }
            else
            {
                TxtMontantCheque1.Enabled = false;
                ChkCheque2.Enabled = false;
                ChkCheque2.Checked = false;
                TxtMontantCheque2.Enabled = false;
                TxtMontantCheque2.Text = "";
                TxtMontantCheque1.Text = "";
            }

        }

        private void ChkCheque2_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCheque2.Checked)
                TxtMontantCheque2.Enabled = true;
            else {
                TxtMontantCheque2.Text = "";
                TxtMontantCheque2.Enabled = false;
            }
        }

        private void RdbNuiteLicencieNon_CheckedChanged(object sender, EventArgs e)
        {
            PanelNuiteLicencie.Visible = false;
        }

        private void RdbNuiteLicencieOui_CheckedChanged(object sender, EventArgs e)
        {
            if (((MetroRadioButton)sender).Name == "RdbNuiteLicencieOui")
            {
                PanelNuiteLicencie.Visible = true;
                if (PanelNuiteLicencie.Controls.Count == 2) // on charge les nuites possibles possibles et on les affiche
                {
                    LesNuites = UneConnexion.ObtenirDatesNuites();
                    int i = 0;
                    foreach (KeyValuePair<Int16, String> UneNuite in LesNuites)
                    {
                        ComposantNuite.ResaNuite unResaNuit = new ResaNuite(UneConnexion.ObtenirDonnesOracle("VHOTEL01"), (UneConnexion.ObtenirDonnesOracle("VCATEGORIECHAMBRE01")), UneNuite.Value, UneNuite.Key);
                        unResaNuit.Left = 5;
                        unResaNuit.Top = 15 + (34 * i++);
                        unResaNuit.Visible = true;
                        unResaNuit.Name = "controleNuite" + i;
                        ((ComboBox)(unResaNuit.Controls[1])).SelectedValueChanged += hotel_SelectedValueChanged;
                        ((ComboBox)(unResaNuit.Controls[0])).SelectedValueChanged += hotel_SelectedValueChanged;
                        ((MetroCheckBox)(unResaNuit.Controls[2])).CheckedChanged += hotel_SelectedValueChanged;
                        PanelNuiteLicencie.Controls.Add(unResaNuit);

                        
                    }

                }
            }
        }
        /// <summary>
        /// Evenement appartenant aux controles du composant nuite afin de  pouvoir mettre à jour le montant total avant l'inscription
        /// </summary>
        /// <param name="sender">La combobox contenant le nom de l'hotel</param>
        /// <param name="e"></param>
       
        private void hotel_SelectedValueChanged(object sender, EventArgs e)
        {
            string nomResaNuite = ((Control)(sender)).Parent.Name;
            if (nomResaNuite == "controleNuite1" || nomResaNuite == "controleNuite2")
            {
                string HotelCourantnuite1 = "IBIS";
                string HotelCourantnuite2 = "IBIS";
                object ObjetNuite = ((Control)(sender)).Parent;
                string TypeChambreSelectionne = ((ResaNuite)ObjetNuite).GetLibelleTypeChambreSelectionnee();
                string HotelSelectionnee = ((ResaNuite)ObjetNuite).GetHotelSelectionne();
                bool cocher = ((MetroCheckBox)((ResaNuite)ObjetNuite).Controls[2]).Checked; // On verfie si la Chekcbox est coché pour la nuité

                if (nomResaNuite == "controleNuite1" && cocher) // ControleNuite1
                {

                    if (HotelCourantnuite1 == HotelSelectionnee)
                    {
                        switch (TypeChambreSelectionne) // IBIS
                        {
                            case "Simple":
                                totalnuite1 = 61.20F; // F pour float
                                break;
                            case "Double":
                                totalnuite1 = 62.20F; // F pour float
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (TypeChambreSelectionne) // NOVO
                        {
                            case "Simple":
                                totalnuite1 = 112;
                                break;
                            case "Double":
                                totalnuite1 = 122;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if (nomResaNuite == "controleNuite2" && cocher) // ControleNuite2
                {
                    if (HotelCourantnuite2 == HotelSelectionnee)
                    {
                        switch (TypeChambreSelectionne) // IBIS
                        {
                            case "Simple":
                                totalnuite2 = 61.20F; // F pour float
                                break;
                            case "Double":
                                totalnuite2 = 62.20F; // F pour float
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (TypeChambreSelectionne) // NOVO
                        {
                            case "Simple":
                                totalnuite2 = 112;
                                break;
                            case "Double":
                                totalnuite2 = 122;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if (nomResaNuite == "controleNuite2" && !cocher) // Si on décoche la nuit du controleNuite1 alors on enleve le prix
                {
                    totalnuite2 = 0;
                }
                else if (nomResaNuite == "controleNuite1" && !cocher) // Si on décoche la nuit du controleNuite1 alors on enleve le prix
                {
                    totalnuite1 = 0;
                }
                else // Aucune nuit selectionnée
                {
                    LblMontantTotal.Text = Convert.ToString(100) + "€";
                }
            }
            LblMontantTotal.Text = Convert.ToString(100 + totalnuite1 + totalnuite2 + totalRepasSupp) + "€";
            LblRecapNuiteLicencie.Text = (totalnuite1 + totalnuite2) + "€ pour les nuités," + totalRepasSupp + "€ pour les repas" + Environment.NewLine +
                "et 100€ de frais d'inscription";

            string message = "Montant du chèque 1 = " + (100 + totalnuite1 + totalnuite2) +"€" + Environment.NewLine +
                             "Montant du chèque 2 = " + totalRepasSupp + "€" + Environment.NewLine +
                             "Vous pouvez payer avec un seul chèque = " + LblMontantTotal.Text;
            AideModalitePaiement.SetToolTip(TileModalitePaiement, message); //Message d'aide
            TileModalitePaiement.Visible = true;
        }

        private void TrackBNbRepasSuppLicencie_Scroll(object sender, EventArgs e)
        {
            LblTradckBValue.Text = TrackBNbRepasSuppLicencie.Value.ToString() + " Repas supplémentaires";
            totalRepasSupp = TrackBNbRepasSuppLicencie.Value * 35; // 35 est le prix pour un repas supplémentaire
            hotel_SelectedValueChanged(sender, e);
        }
        /// <summary>
        /// Inscription d'un Licencié
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnregistrerLicencie_Click(object sender, EventArgs e)
        {
             try
            {
                if (RdbNuiteLicencieOui.Checked)
                {
                    // inscription avec les nuitées
                    Collection<Int16> NuitsSelectionnes = new Collection<Int16>();
                    Collection<String> HotelsSelectionnes = new Collection<String>();
                    Collection<String> CategoriesSelectionnees = new Collection<string>();
                    foreach (Control UnControle in PanelNuiteLicencie.Controls)
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
                        MetroMessageBox.Show(this, "Si vous avez sélectionné que le licencié avait des nuités, il faut qu'au moins une nuit soit sélectionnée", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        //UneConnexion.InscrireIntervenant(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, TxtTel.MaskCompleted ? TxtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbAtelierIntervenant.SelectedValue), this.IdStatutSelectionne, CategoriesSelectionnees, HotelsSelectionnes, NuitsSelectionnes, photoByte);
                        MetroMessageBox.Show(this, "Inscription licencié effectuée", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                { // inscription sans les nuitées
                    //UneConnexion.InscrireIntervenant(TxtNom.Text, TxtPrenom.Text, TxtAdr1.Text, TxtAdr2.Text != "" ? TxtAdr2.Text : null, TxtCp.Text, TxtVille.Text, TxtTel.MaskCompleted ? TxtTel.Text : null, TxtMail.Text != "" ? TxtMail.Text : null, System.Convert.ToInt16(CmbAtelierIntervenant.SelectedValue), this.IdStatutSelectionne, photoByte);
                    MetroMessageBox.Show(this,"Inscription licencié effectuée", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
             catch (Exception Ex)
             {
                 MetroMessageBox.Show(this, Ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
        }

        private void BtnCreerVacationAjouter_Click(object sender, EventArgs e)
        {
            //5 vacations maximum.
            if (NombreVacationModifier < 5)
            {
                //Si le nombre de vacation est pair la prochaine sera du coté gauche.
                if (NombreVacationModifier % 2 == 0)
                {
                    YVacationCreerAtelier += 64;
                    ComposantVacation.ComposantVacation ModifierUneVacation = new ComposantVacation.ComposantVacation();
                    ModifierUneVacation.Location = new Point(XVacationCreerAtelier, YVacationCreerAtelier);
                    PanelCreerVacationSuite.Controls.Add(ModifierUneVacation);
                    NombreVacationModifier++;
                    NombreVacationModifierAjouter++;
                }
                //Sinon du coté droit.
                else
                {
                    ComposantVacation.ComposantVacation ModifierUneVacation = new ComposantVacation.ComposantVacation();
                    ModifierUneVacation.Location = new Point(X2VacationCreerAtelier, YVacationCreerAtelier);
                    PanelCreerVacationSuite.Controls.Add(ModifierUneVacation);
                    NombreVacationModifier++;
                    NombreVacationModifierAjouter++;
                }
            }

        }

        private void BtnCreerVacationRetirer_Click(object sender, EventArgs e)
        {
            if (NombreVacationModifierAjouter > 0)
            {
                NombreVacationModifier--;
                NombreVacationModifierAjouter--;
                if (NombreVacationModifier % 2 == 0)
                {
                    YVacationCreerAtelier -= 64;
                }
                PanelCreerVacationSuite.Controls.RemoveAt(PanelCreerVacationSuite.Controls.Count - 1);
            }

        }
    }
}
