using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework;

namespace ComposantNuite
{
    public partial class ResaNuite : UserControl
    {
        public ResaNuite()
        {
            InitializeComponent();
        }
        private Int16 _IdNuite;
        
      //
        public delegate void NuitCochee(object sender, EventArgs e);
        public event EventHandler OnNuitChanged; 
//

        public ResaNuite(DataTable UnDataTableHotel, DataTable UnDataTableTypeChamre, String UneDateNuite, Int16 UnIdNuite)
        {
            InitializeComponent();
            try
            {
                

                CmbHotel.DataSource = UnDataTableHotel;                
                CmbHotel.DisplayMember = "libelle";
                CmbHotel.ValueMember = "id";
                CmbTypeChambre.DataSource = UnDataTableTypeChamre;
                CmbTypeChambre.DisplayMember = "libelle";
                CmbTypeChambre.ValueMember = "id";
                _IdNuite = UnIdNuite;
                ChkChoisir.Text += " " + UneDateNuite;
            }
            catch (Exception ex)
            {
                throw new Exception("erreur d'initialisation, la datasource doit être sur un format Id, libelle");
            }

        }
        public Int16 IdNuite {
            get {return this._IdNuite;}
        }
        public String GetHotelSelectionne()
        {
            return this.CmbHotel.SelectedValue.ToString();
        }
        public String GetNomHotelSelectionne()
        {
            return this.CmbHotel.Text;
        }
        public String GetTypeChambreSelectionnee()
        {
            return this.CmbTypeChambre.SelectedValue.ToString();
        }
        public String GetLibelleTypeChambreSelectionnee()
        {
            return this.CmbTypeChambre.Text;
        }
        public String GetLibelleNuitSelectionnee()
        {
            return this.ChkChoisir.Text;
        }
        public Boolean GetNuitSelectionnee()
        {
            return this.ChkChoisir.Checked;
        }
        private void ComposantNuite_StateChanged(object sender, EventArgs e)
        {
            if (e != null) OnNuitChanged(sender, e);
        }
        public Boolean NightChecked()
        {
            return this.ChkChoisir.Checked;
        }
        //
        //protected virtual void DeclencheCheckedNuit(EventArgs e);

        //
    }
}
