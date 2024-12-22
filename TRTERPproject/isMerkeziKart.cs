using System.Data;
using System.Data.SqlClient;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
	public partial class isMerkeziKart : Form
	{
		SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
		SqlDataReader reader;
		SqlCommand cmd;
		public isMerkeziKart()
		{
			InitializeComponent();
		}

		private void getBut_Click_1(object sender, EventArgs e)
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
                            WCMDOCTYPE AS 'İş Merkezi Tipi', 
                            WCMDOCNUM AS 'İş Merkezi  Numarası', 
                            WCMDOCFROM AS 'Geçerlilik Başlangıç',
							WCMDOCUNTIL	AS 'Geçerlilik Bitiş',
                            MAINWCMDOCTYPE AS 'Ana İş Merkezi Tipi', 
                            MAINWCMDOCNUM AS 'Ana İş Merkezi Numarası', 
							ISDELETED AS 'Silindi mi?',
							ISPASSIVE AS 'Pasif mi?'
                        FROM BSMGRTRTWCMHEAD";

					// Verileri çekmek için SqlDataAdapter kullanımı
					SqlDataAdapter da = new SqlDataAdapter(query, con);
					DataTable dt = new DataTable();
					da.Fill(dt);

					// DataGridView'e verileri bağlama
					ismerkData.DataSource = dt;
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
