using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApplicationTestComposant
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void resaNuite1_OnNuitChanged(object sender, EventArgs e)
        {
            MessageBox.Show(resaNuite1.NightChecked().ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(resaNuite1.GetLibelleNuitSelectionnee());
            MessageBox.Show(resaNuite1.GetLibelleTypeChambreSelectionnee());
            MessageBox.Show(resaNuite1.GetNomHotelSelectionne());
        }
    }
}
