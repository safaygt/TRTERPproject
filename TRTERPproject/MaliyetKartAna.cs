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
			this.Load += (s, e) => LoadComboBoxData();

			// ComboBox leave eventlerini bağla
			firmComboBox.Leave += (s, e) => ValidateAndAddData(firmComboBox, "COMCODE");
			comboBoxMalMerTip.Leave += (s, e) => ValidateAndAddData(comboBoxMalMerTip, "CCMDOCTYPE");
			//dilCombo.Leave += (s, e) => ValidateAndAddData(dilCombo, "");
		}
		private void LoadComboBoxData()
		{
			try
			{
				con.Open();

				// Firma verilerini doldur
				string queryFirma = "SELECT DISTINCT COMCODE FROM BSMGRTRTCCMHEAD";
				SqlDataAdapter daFirma = new SqlDataAdapter(queryFirma, con);
				DataTable dtFirma = new DataTable();
				daFirma.Fill(dtFirma);
				firmComboBox.DataSource = dtFirma;
				firmComboBox.DisplayMember = "COMCODE";
				firmComboBox.ValueMember = "COMCODE";
				firmComboBox.DropDownStyle = ComboBoxStyle.DropDown; // Yeni veri girilebilir

				string queryMtip = "SELECT DISTINCT CCMDOCTYPE FROM BSMGRTRTCCMHEAD"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daMtip = new SqlDataAdapter(queryMtip, con);
				DataTable dtMtip = new DataTable();
				daMtip.Fill(dtMtip);
				comboBoxMalMerTip.DataSource = dtMtip;
				comboBoxMalMerTip.DisplayMember = "CCMDOCTYPE";
				comboBoxMalMerTip.ValueMember = "CCMDOCTYPE";
				comboBoxMalMerTip.DropDownStyle = ComboBoxStyle.DropDown;
				/*
				string queryTtip = "SELECT DISTINCT SUPPLYTYPE FROM BSMGRTRTCCMHEAD"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daTtip = new SqlDataAdapter(queryTtip, con);
				DataTable dtTtip = new DataTable();
				daTtip.Fill(dtTtip);
				comboBoxTedTip.DataSource = dtTtip;
				comboBoxTedTip.DisplayMember = "SUPPLYTYPE";
				comboBoxTedTip.ValueMember = "SUPPLYTYPE";
				comboBoxTedTip.DropDownStyle = ComboBoxStyle.DropDown;
				*/
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
			string checkQuery = $"SELECT COUNT(*) FROM BSMGRTRTCCMHEAD WHERE {columnName} = @userInput";
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
			con = new SqlConnection(ConnectionHelper.ConnectionString);
			cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandText = query;

			try
			{


				con.Open();

				// Verileri çekmek için SqlDataAdapter kullanımı
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataSet dt = new DataSet();
				da.Fill(dt);

				// DataGridView'e verileri bağlama
				maliyetdata.DataSource = dt.Tables[0];

			}
			catch (Exception ex)
			{
				// Hata mesajı
				MessageBox.Show($"Hata: {ex.Message}");
			}
		}
	}
}
