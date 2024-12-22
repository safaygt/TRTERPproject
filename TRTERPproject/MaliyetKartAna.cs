using System.Data;
using System.Data.SqlClient;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
	public partial class MaliyetKartAna : Form
	{
		SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
		SqlDataReader reader;
		SqlCommand cmd;
		public MaliyetKartAna()
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
                            CCMDOCTYPE AS 'Maliyet Merkezi Tipi', 
                            CCMDOCNUM AS 'Maliyet Merkezi  Numarası', 
                            CCMDOCFROM AS 'Geçerlilik Başlangıç',
							CCMDOCUNTIL	AS 'Geçerlilik Bitiş',
                            MAINCCMDOCTYPE AS 'Ana Maliyet Merkezi Tipi', 
                            MAINCCMDOCNUM AS 'Ana Maliyet Merkezi Numarası', 
							ISDELETED AS 'Silindi mi?',
							ISPASSIVE AS 'Pasif mi?'
                        FROM BSMGRTRTCCMHEAD";

					// Verileri çekmek için SqlDataAdapter kullanımı
					SqlDataAdapter da = new SqlDataAdapter(query, con);
					DataTable dt = new DataTable();
					da.Fill(dt);

					// DataGridView'e verileri bağlama
					maliyetdata.DataSource = dt;
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
