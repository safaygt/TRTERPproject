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
			this.Load += (s, e) => LoadComboBoxData();

			// ComboBox leave eventlerini bağla
			firmbox.Leave += (s, e) => ValidateAndAddData(firmbox, "COMCODE");
			comboBoxRotTip.Leave += (s, e) => ValidateAndAddData(comboBoxRotTip, "ROTDOCTYPE");
			//dilCombo.Leave += (s, e) => ValidateAndAddData(dilCombo, "");
		}
		private void LoadComboBoxData()
		{
			try
			{
				con.Open();

				// Firma verilerini doldur
				string queryFirma = "SELECT DISTINCT COMCODE FROM BSMGRTRTROTHEAD";
				SqlDataAdapter daFirma = new SqlDataAdapter(queryFirma, con);
				DataTable dtFirma = new DataTable();
				daFirma.Fill(dtFirma);
				firmbox.DataSource = dtFirma;
				firmbox.DisplayMember = "COMCODE";
				firmbox.ValueMember = "COMCODE";
				firmbox.DropDownStyle = ComboBoxStyle.DropDown; // Yeni veri girilebilir

				string queryMtip = "SELECT DISTINCT ROTDOCTYPE FROM BSMGRTRTROTHEAD"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daMtip = new SqlDataAdapter(queryMtip, con);
				DataTable dtMtip = new DataTable();
				daMtip.Fill(dtMtip);
				comboBoxRotTip.DataSource = dtMtip;
				comboBoxRotTip.DisplayMember = "ROTDOCTYPE";
				comboBoxRotTip.ValueMember = "ROTDOCTYPE";
				comboBoxRotTip.DropDownStyle = ComboBoxStyle.DropDown;
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
			string checkQuery = $"SELECT COUNT(*) FROM BSMGRTRTROTHEAD WHERE {columnName} = @userInput";
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
                            ROTDOCTYPE AS 'Rota Tipi', 
                            ROTDOCNUM AS 'Rota Numarası',
							MATDOCTYPE AS 'Malzeme Tipi',
							MATDOCNUM AS 'Malzeme Numarası',
                            ROTDOCFROM AS 'Geçerlilik Başlangıç',
							ROTDOCUNTIL	AS 'Geçerlilik Bitiş',
							QUANTITY AS 'Miktar',
							DRAWNUM AS 'Çizim Numarası',
							ISDELETED AS 'Silindi mi?',
							ISPASSIVE AS 'Pasif mi?'
                        FROM BSMGRTRTROTHEAD";
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
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataSet dt = new DataSet();
					da.Fill(dt);

					// DataGridView'e verileri bağlama
					RotaData.DataSource = dt.Tables[0];
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

