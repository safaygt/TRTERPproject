using System.Data;
using System.Data.SqlClient;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
	public partial class RotaKartAna : Form
	{
		SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
		SqlDataReader reader;
		SqlCommand cmd;
		public RotaKartAna()
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
					string query = @"
                        SELECT 
							COMCODE AS 'Firma', 
                            ROTDOCTYPE AS 'Rota Tipi', 
                            ROTDOCNUM AS 'Rota Numarası',
							MATDOCTYPE AS 'Malzeme Tipi',
							MATDOCNUM AS 'Malzeme Numarası',
                            BOMDOCFROM AS 'Geçerlilik Başlangıç',
							BOMDOCUNTIL	AS 'Geçerlilik Bitiş',
							QUANTITY AS 'Miktar',
							DRAWNUM AS 'Çizim Numarası',
							ISDELETED AS 'Silindi mi?',
							ISPASSIVE AS 'Pasif mi?'
                        FROM BSMGRTRTROTHEAD";

					// Verileri çekmek için SqlDataAdapter kullanımı
					SqlDataAdapter da = new SqlDataAdapter(query, con);
					DataTable dt = new DataTable();
					da.Fill(dt);

					// DataGridView'e verileri bağlama
					RotaData.DataSource = dt;
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

