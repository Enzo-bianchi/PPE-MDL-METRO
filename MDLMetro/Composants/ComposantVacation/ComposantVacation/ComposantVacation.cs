using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace ComposantVacation
{
    public partial class ComposantVacation : MetroFramework.Controls.MetroUserControl
    {
        public ComposantVacation()
        {
            InitializeComponent();
        }

        private void ComposantVacationCheckHeure()
        {
            if (ComposantVacationMaskedTBHeureDebut.MaskCompleted && ComposantVacationMaskedTBHeureFin.MaskCompleted)
            {
                CultureInfo ComposantVacationProvider = new CultureInfo("fr-FR");
                DateTime ComposantVacationHeureDebut = DateTime.ParseExact(ComposantVacationMaskedTBHeureDebut.Text, "HH:mm", ComposantVacationProvider);
                DateTime ComposantVacationHeureFin = DateTime.ParseExact(ComposantVacationMaskedTBHeureFin.Text, "HH:mm", ComposantVacationProvider);
                if (DateTime.Compare(ComposantVacationHeureDebut, ComposantVacationHeureFin) >= 0)
                {
                    ComposantVacationMaskedTBHeureDebut.BackColor = Color.Red;
                    ComposantVacationMaskedTBHeureFin.BackColor = Color.Red;
                }
                else
                {
                    ComposantVacationMaskedTBHeureDebut.BackColor = Color.Green;
                    ComposantVacationMaskedTBHeureFin.BackColor = Color.Green;
                }
            }
        }
        private void ComposantVacationMaskedTBHeureDebut_Leave(object sender, EventArgs e)
        {
            ComposantVacationCheckHeure();
        }

        private void ComposantVacationMaskedTBHeureFin_Leave(object sender, EventArgs e)
        {
           ComposantVacationCheckHeure();
        }

        private void ComposantVacationMaskedTBHeureFin_Enter(object sender, EventArgs e)
        {
            ComposantVacationCheckHeure();
        }

        private void ComposantVacationMaskedTBHeureDebut_Enter(object sender, EventArgs e)
        {
            ComposantVacationCheckHeure();
        }

    }
}
