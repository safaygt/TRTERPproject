using System.Data;
using System.Data.SqlClient;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
	public partial class urunAgaciKart : Form
	{
		SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
		SqlDataReader reader;
		SqlCommand cmd;
		public urunAgaciKart()
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
                            BOMDOCTYPE AS 'Ürün Ağacı Tipi', 
                            BOMDOCNUM AS 'Ürün Ağacı Numarası', 
                            BOMDOCFROM AS 'Geçerlilik Başlangıç',
							BOMDOCUNTIL	AS 'Geçerlilik Bitiş',
                            MATDOCTYPE AS 'Malzeme Tipi', 
                            MATDOCNUM AS 'Malzeme Numarası', 
						    QUANTITY AS 'Temel Miktar',
						    DRAWNUM AS 'Çizim Numarası',
							ISDELETED AS 'Silindi mi?',
							ISPASSIVE AS 'Pasif mi?'
                        FROM BSMGRTRTBOMHEAD";

					// Verileri çekmek için SqlDataAdapter kullanımı
					SqlDataAdapter da = new SqlDataAdapter(query, con);
					DataTable dt = new DataTable();
					da.Fill(dt);

					// DataGridView'e verileri bağlama
					urnAgcData.DataSource = dt;
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
