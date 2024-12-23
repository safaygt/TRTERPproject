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
			this.Load += (s, e) => LoadComboBoxData();

			// ComboBox leave eventlerini bağla
			comboBoxMalzFirm.Leave += (s, e) => ValidateAndAddData(comboBoxMalzFirm, "COMCODE");
			malzTipcombo.Leave += (s, e) => ValidateAndAddData(malzTipcombo, "MATDOCTYPE");
			comboBoxTedTip.Leave += (s, e) => ValidateAndAddData(comboBoxTedTip, "SUPPLYTYPE");
		}
		private void LoadComboBoxData()
		{
			try
			{
				con.Open();

				// Firma verilerini doldur
				string queryFirma = "SELECT DISTINCT COMCODE FROM BSMGRTRTMATHEAD";
				SqlDataAdapter daFirma = new SqlDataAdapter(queryFirma, con);
				DataTable dtFirma = new DataTable();
				daFirma.Fill(dtFirma);
				comboBoxMalzFirm.DataSource = dtFirma;
				comboBoxMalzFirm.DisplayMember = "COMCODE";
				comboBoxMalzFirm.ValueMember = "COMCODE";
				comboBoxMalzFirm.DropDownStyle = ComboBoxStyle.DropDown; // Yeni veri girilebilir

				string queryMtip = "SELECT DISTINCT MATDOCTYPE FROM BSMGRTRTMATHEAD"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daMtip = new SqlDataAdapter(queryMtip, con);
				DataTable dtMtip = new DataTable();
				daMtip.Fill(dtMtip);
				malzTipcombo.DataSource = dtMtip;
				malzTipcombo.DisplayMember = "MATDOCTYPE";
				malzTipcombo.ValueMember = "MATDOCTYPE";
				malzTipcombo.DropDownStyle = ComboBoxStyle.DropDown;

				string queryTtip = "SELECT DISTINCT SUPPLYTYPE FROM BSMGRTRTMATHEAD"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daTtip = new SqlDataAdapter(queryTtip, con);
				DataTable dtTtip = new DataTable();
				daTtip.Fill(dtTtip);
				comboBoxTedTip.DataSource = dtTtip;
				comboBoxTedTip.DisplayMember = "SUPPLYTYPE";
				comboBoxTedTip.ValueMember = "SUPPLYTYPE";
				comboBoxTedTip.DropDownStyle = ComboBoxStyle.DropDown;
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
			string checkQuery = $"SELECT COUNT(*) FROM BSMGRTRTMATHEAD WHERE {columnName} = @userInput";
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
                            MATDOCTYPE AS 'Malzeme Tipi', 
                            MATDOCNUM AS 'Malzeme Numarası', 
                            MATDOCFROM AS 'Geçerlilik Başlangıç',
							MATDOCUNTIL	AS 'Geçerlilik Bitiş',
							SUPPLYTYPE AS 'Tedarik Tipi',
							STUNIT AS 'Stok Birimi',
							NETWEIGHT AS 'Net Ağırlık',
							NWUNIT AS 'Net Ağırlık Birimi',
							BRUTWEIGHT AS 'Brüt Ağırlık',
							BWUNIT AS 'Brüt Ağırlık Brimi',
							ISBOM AS 'Ürün Ağacı Var mı?',
							BOMDOCTYPE AS 'Ürün Ağacı Tipi',
							BOMDOCNUM AS 'Ürün Ağacı Kodu',
							ISROUTE AS 'Rota Var mı?',
							ROTDOCTYPE AS 'Rota Tipi',
							ROTDOCNUM AS 'Rota Kodu',
							ISDELETED AS 'Silindi mi?',
							ISPASSIVE AS 'Pasif mi?'
                        FROM BSMGRTRTMATHEAD";

			con = new SqlConnection(ConnectionHelper.ConnectionString);
			cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandText = query;

			try
			{

				{
					con.Open();

					// Verileri çekmek için SqlDataAdapter kullanımı
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataSet dt = new DataSet();
					da.Fill(dt);

					// DataGridView'e verileri bağlama
					malKartAna.DataSource = dt.Tables[0];
				}
			}
			catch (Exception ex)
			{
				// Hata mesajı
				MessageBox.Show($"Hata: {ex.Message}");
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}