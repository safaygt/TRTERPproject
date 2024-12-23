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
			this.Load += (s, e) => LoadComboBoxData();

			// ComboBox leave eventlerini bağla
			firmbox.Leave += (s, e) => ValidateAndAddData(firmbox, "COMCODE");
			urnagamalztipbox.Leave += (s, e) => ValidateAndAddData(urnagamalztipbox, "MATDOCTYPE");
			urnmalznumBox.Leave += (s, e) => ValidateAndAddData(urnmalznumBox, "MATDOCNUM");
			urnAgaTipBox.Leave += (s, e) => ValidateAndAddData(urnAgaTipBox, "BOMDOCTYPE");
		}
		private void LoadComboBoxData()
		{

			try
			{
				con.Open();

				// Firma verilerini doldur
				string queryFirma = "SELECT DISTINCT COMCODE FROM BSMGRTRTBOMHEAD";
				SqlDataAdapter daFirma = new SqlDataAdapter(queryFirma, con);
				DataTable dtFirma = new DataTable();
				daFirma.Fill(dtFirma);
				firmbox.DataSource = dtFirma;
				firmbox.DisplayMember = "COMCODE";
				firmbox.ValueMember = "COMCODE";
				firmbox.DropDownStyle = ComboBoxStyle.DropDown; // Yeni veri girilebilir

				string queryMtip = "SELECT DISTINCT MATDOCTYPE FROM BSMGRTRTBOMHEAD"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daMtip = new SqlDataAdapter(queryMtip, con);
				DataTable dtMtip = new DataTable();
				daMtip.Fill(dtMtip);
				urnagamalztipbox.DataSource = dtMtip;
				urnagamalztipbox.DisplayMember = "MATDOCTYPE";
				urnagamalztipbox.ValueMember = "MATDOCTYPE";
				urnagamalztipbox.DropDownStyle = ComboBoxStyle.DropDown;

				string queryTtip = "SELECT DISTINCT MATDOCNUM FROM BSMGRTRTBOMHEAD"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daTtip = new SqlDataAdapter(queryTtip, con);
				DataTable dtTtip = new DataTable();
				daTtip.Fill(dtTtip);
				urnmalznumBox.DataSource = dtTtip;
				urnmalznumBox.DisplayMember = "MATDOCNUM";
				urnmalznumBox.ValueMember = "MATDOCNUM";
				urnmalznumBox.DropDownStyle = ComboBoxStyle.DropDown;

				string queryGtip = "SELECT DISTINCT BOMDOCTYPE FROM BSMGRTRTBOMHEAD"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daGtip = new SqlDataAdapter(queryGtip, con);
				DataTable dtGtip = new DataTable();
				daGtip.Fill(dtGtip);
				urnAgaTipBox.DataSource = dtGtip;
				urnAgaTipBox.DisplayMember = "BOMDOCTYPE";
				urnAgaTipBox.ValueMember = "BOMDOCTYPE";
				urnAgaTipBox.DropDownStyle = ComboBoxStyle.DropDown;
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
			string checkQuery = $"SELECT COUNT(*) FROM BSMGRTRTBOMHEAD WHERE {columnName} = @userInput";
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

		private void getBut_Click(object sender, EventArgs e)
		{
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
			con = new SqlConnection(ConnectionHelper.ConnectionString);
			cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandText = query;
			try
			{
				using (con)
				{
					con.Open();

					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataSet dt = new DataSet();
					da.Fill(dt);

					// DataGridView'e verileri bağlama
					urnAgcData.DataSource = dt.Tables[0];
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
