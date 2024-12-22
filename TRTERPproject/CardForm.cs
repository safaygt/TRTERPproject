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
            firmForm FirmaForm = new firmForm();
            FirmaForm.Show();
        }


	

		private void ulkeBut_Click(object sender, EventArgs e)
		{
			countryForm CountryForm = new countryForm();
			CountryForm.Show();
		}

		private void MaliyetBut_Click(object sender, EventArgs e)
		{
			MaliyetKartAna maliyetKartAna = new MaliyetKartAna();
			maliyetKartAna.Show();
		}

		private void WorkBut_Click(object sender, EventArgs e)
		{
			isMerkeziKart isMerkeziKart = new isMerkeziKart();
			isMerkeziKart.Show();
		}


		private void sehirBut_Click_1(object sender, EventArgs e)
		{
			cityForm CityForm = new cityForm();
			CityForm.Show();
		}

		private void UrunBut_Click(object sender, EventArgs e)
		{
			urunAgaciKart urunAgaciKart = new urunAgaciKart();
			urunAgaciKart.Show();
		}

		private void RotaBut_Click(object sender, EventArgs e)
		{
			RotaKartAna rotaKartAna = new RotaKartAna();
			rotaKartAna.Show();
		}


        private void dilBut_Click(object sender, EventArgs e)
        {
            lanForm LanForm = new lanForm();
            LanForm.Show();
        }
    }
	}


}