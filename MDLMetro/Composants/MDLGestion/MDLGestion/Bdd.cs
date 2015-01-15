using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;  // bibliothèque pour les expressions régulières
using MDLGestion;



namespace BaseDeDonnees
{
    class Bdd
    {
        //
        // propriétés membres
        //
        private OracleConnection CnOracle;
        private OracleCommand UneOracleCommand;
        private OracleDataAdapter UnOracleDataAdapter;
        private DataTable UneDataTable;
        private OracleTransaction UneOracleTransaction;
        //
        // méthodes
        //
        /// <summary>
        /// constructeur de la connexion
        /// </summary>
        /// <param name="UnLogin">login utilisateur</param>
        /// <param name="UnPwd">mot de passe utilisateur</param>
        public Bdd(String UnLogin, String UnPwd)
        {
            try
            {
                /// <remarks>on commence par récupérer dans CnString les informations contenues dans le fichier app.config
                /// pour la connectionString de nom StrConnMdl
                /// </remarks>
                ConnectionStringSettings CnString = ConfigurationManager.ConnectionStrings["StrConnMdlLocal"];
                ///<remarks>
                /// on va remplacer dans la chaine de connexion les paramètres par le login et le pwd saisis
                ///dans les zones de texte. Pour ça on va utiliser la méthode Format de la classe String.                /// 
                /// </remarks>
                CnOracle = new OracleConnection(string.Format(CnString.ConnectionString, UnLogin, UnPwd));
                CnOracle.Open();
            }
            catch (OracleException Oex)
            {
                try
                {
                    /// <remarks>on commence par récupérer dans CnString les informations contenues dans le fichier app.config
                    /// pour la connectionString de nom StrConnMdl
                    /// </remarks>
                    ConnectionStringSettings CnString = ConfigurationManager.ConnectionStrings["StrConnMdlDistant"];
                    ///<remarks>
                    /// on va remplacer dans la chaine de connexion les paramètres par le login et le pwd saisis
                    ///dans les zones de texte. Pour ça on va utiliser la méthode Format de la classe String.                /// 
                    /// </remarks>
                    CnOracle = new OracleConnection(string.Format(CnString.ConnectionString, UnLogin, UnPwd));
                    CnOracle.Open();
                }
                catch
                {
                    throw new Exception("Erreur à la connexion :" + Oex.Message);
                }
            }
        }
        /// <summary>
        /// Méthode permettant de fermer la connexion
        /// </summary>
        public void FermerConnexion()
        {
            this.CnOracle.Close();
        }
        /// <summary>
        /// méthode permettant de renvoyer un message d'erreur provenant de la bd
        /// après l'avoir formatté. On ne renvoie que le message, sans code erreur
        /// </summary>
        /// <param name="unMessage">message à formater</param>
        /// <returns>message formaté à afficher dans l'application</returns>
        private String GetMessageOracle(String unMessage)
        {
            String[] message = Regex.Split(unMessage, "ORA-");
            return (Regex.Split(message[1], ":"))[1];
        }
        /// <summary>
        /// permet de récupérer le contenu d'une table ou d'une vue. 
        /// </summary>
        /// <param name="UneTableOuVue"> nom de la table ou la vue dont on veut récupérer le contenu</param>
        /// <returns>un objet de type datatable contenant les données récupérées</returns>
        public DataTable ObtenirDonnesOracle(String UneTableOuVue)
        {
            string Sql = "select * from " + UneTableOuVue;
            this.UneOracleCommand = new OracleCommand(Sql, CnOracle);
            UnOracleDataAdapter = new OracleDataAdapter();
            UnOracleDataAdapter.SelectCommand = this.UneOracleCommand;
            UneDataTable = new DataTable();
            UnOracleDataAdapter.Fill(UneDataTable);
            return UneDataTable;
        }
        /// <summary>
        /// Procédure permettant de modifier une vacation
        /// </summary>
        /// <param name="pVacationsHeureDebut">Correspond à l'heure de début de la vacation</param>
        /// <param name="pVacationsHeureFin">Correspond à l'heure de fin de la vacation</param>
        /// <param name="pIdAtelier">Correspond à l'id de l'atelier</param>
        public void ValiderInscription(int pIdParticipant, string pWpa)
        {
            try
            {
                UneOracleCommand = new OracleCommand("pckparticipant.validerinscription", CnOracle);
                UneOracleCommand.CommandType = CommandType.StoredProcedure;

                UneOracleCommand.Parameters.Add("pId", OracleDbType.Int32, ParameterDirection.Input).Value = pIdParticipant;
                UneOracleCommand.Parameters.Add("pWpa", OracleDbType.Char, ParameterDirection.Input).Value = pWpa;
                UneOracleCommand.ExecuteNonQuery();
                MessageBox.Show("Validation effectuée.");
            }
            catch (OracleException Oex)
            {
                MessageBox.Show("Erreur Oracle \n" + Oex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Autre Erreur  \n" + ex.Message);
            }

        }
    }
}
