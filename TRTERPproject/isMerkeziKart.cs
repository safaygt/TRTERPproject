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
			this.Load += (s, e) => LoadComboBoxData();

			// ComboBox leave eventlerini bağla
			firmbox.Leave += (s, e) => ValidateAndAddData(firmbox, "COMCODE");
			comboBoxIsMerTip.Leave += (s, e) => ValidateAndAddData(comboBoxIsMerTip, "WCMDOCTYPE");
			//comboBoxOprCode.Leave += (s, e) => ValidateAndAddData(comboBoxOprCode, "operasyon");
			dilBox.Leave += (s, e) => ValidateAndAddData(dilBox, "LANCODE");
		}
		private void LoadComboBoxData()
		{
			try
			{
				con.Open();

				// Firma verilerini doldur
				string queryFirma = "SELECT DISTINCT COMCODE FROM BSMGRTRTWCMHEAD";
				SqlDataAdapter daFirma = new SqlDataAdapter(queryFirma, con);
				DataTable dtFirma = new DataTable();
				daFirma.Fill(dtFirma);
				firmbox.DataSource = dtFirma;
				firmbox.DisplayMember = "COMCODE";
				firmbox.ValueMember = "COMCODE";
				firmbox.DropDownStyle = ComboBoxStyle.DropDown; // Yeni veri girilebilir

				string queryisMtip = "SELECT DISTINCT WCMDOCTYPE FROM BSMGRTRTWCMHEAD"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daisMtip = new SqlDataAdapter(queryisMtip, con);
				DataTable dtisMtip = new DataTable();
				daisMtip.Fill(dtisMtip);
				comboBoxIsMerTip.DataSource = dtisMtip;
				comboBoxIsMerTip.DisplayMember = "WCMDOCTYPE";
				comboBoxIsMerTip.ValueMember = "WCMDOCTYPE";
				comboBoxIsMerTip.DropDownStyle = ComboBoxStyle.DropDown;
				
				string queryTtip = "SELECT DISTINCT LANCODE FROM BSMGRTRTGEN002"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daTtip = new SqlDataAdapter(queryTtip, con);
				DataTable dtTtip = new DataTable();
				daTtip.Fill(dtTtip);
				dilBox.DataSource = dtTtip;
				dilBox.DisplayMember = "LANCODE";
				dilBox.ValueMember = "LANCODE";
				dilBox.DropDownStyle = ComboBoxStyle.DropDown;
				
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}");
			}
			finally
			{
				con.Close();
			}
		}
		private void ValidateAndAddData(ComboBox comboBox, string columnName)
		{
			string checkQuery = $"SELECT COUNT(*) FROM BSMGRTRTWCMHEAD WHERE {columnName} = @userInput";
			con = new SqlConnection(ConnectionHelper.ConnectionString);
			cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandText = checkQuery;
			string userInput = comboBox.Text;
			if (string.IsNullOrEmpty(userInput)) return;

			try
			{
				con.Open();

				SqlCommand checkCmd = new SqlCommand(checkQuery, con);
				checkCmd.Parameters.AddWithValue("@userInput", userInput);

				int count = (int)checkCmd.ExecuteScalar();
				if (count == 0)
				{
					MessageBox.Show($"{columnName} '{userInput}' tablodaki verilerle uyuşmuyor.");
					comboBox.Text = string.Empty; // Kullanıcının yanlış girişini temizler
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Hata: {ex.Message}");
			}
			finally
			{
				con.Close();
			}
		}

		private void getBut_Click_1(object sender, EventArgs e)
		{
			// SQL sorgusu
			string query = @"
                        SELECT 
							WH.COMCODE AS 'Firma', 
                            WH.WCMDOCTYPE AS 'İş Merkezi Tipi', 
                            WH.WCMDOCNUM AS 'İş Merkezi  Numarası', 
                            WH.WCMDOCFROM AS 'Geçerlilik Başlangıç',
							WH.WCMDOCUNTIL	AS 'Geçerlilik Bitiş',
                            WH.MAINWCMDOCTYPE AS 'Ana İş Merkezi Tipi', 
                            WH.MAINWCMDOCNUM AS 'Ana İş Merkezi Numarası', 
							WH.ISDELETED AS 'Silindi mi?',
							WH.ISPASSIVE AS 'Pasif mi?',
							G2.LANCODE AS 'Dil'
							FROM 
							    BSMGRTRTWCMHEAD WH
							INNER JOIN 
							    BSMGRTRTWCMTEXT WT ON WH.WCMDOCNUM = WT.WCMDOCNUM
							INNER JOIN 
							    BSMGRTRTGEN002 G2 ON WT.LANCODE = G2.LANCODE";
 
			con = new SqlConnection(ConnectionHelper.ConnectionString);
			cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandText = query;

			try
			{
				using (con)
				{
					con.Open();



					// Verileri çekmek için SqlDataAdapter kullanımı
					SqlDataAdapter da = new SqlDataAdapter(query, con);
					DataSet dt = new DataSet();
					da.Fill(dt);

					// DataGridView'e verileri bağlama
					ismerkData.DataSource = dt.Tables[0];
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
