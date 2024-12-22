using System.Data;
using System.Data.SqlClient;
using TRTERPproject.Helpers;
namespace TRTERPproject
{
	public partial class malzemeKartAna : Form
	{
		SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
		SqlDataReader reader;
		SqlCommand cmd;


		public malzemeKartAna()
		{
			InitializeComponent();
		}

		private void getBut_Click(object sender, EventArgs e)
		{

			try
			{
				using (con)
				{
					con.Open();

					// SQL sorgusu
					string query = "SELECT * FROM BSMGRTRTMAT001"; // Tablo adınızı buraya yazın

					// Verileri çekmek için SqlDataAdapter kullanımı
					SqlDataAdapter da = new SqlDataAdapter(query, con);
					DataTable dt = new DataTable();
					da.Fill(dt);

					// DataGridView'e verileri bağlama
					malKartAna.DataSource = dt;
				}
			}
			catch (Exception ex)
			{
				// Hata mesajı
				MessageBox.Show($"Hata: {ex.Message}");
			}
		}
	}
}
