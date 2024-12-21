using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRTERPproject
{

    public partial class CardForm : Form
    {
        public CardForm()
        {
            InitializeComponent();

        }

        private void Malcard_Click(object sender, EventArgs e)
        {
            malzemeKartAna malzemeKartAna = new malzemeKartAna();
            malzemeKartAna.Show();
        }
       

        private void firmaBut_Click_1(object sender, EventArgs e)
        {
         
        }

        private void ulkeBut_Click(object sender, EventArgs e)
        {
            countryForm CountryForm = new countryForm();
            CountryForm.Show();
        }
    }

}