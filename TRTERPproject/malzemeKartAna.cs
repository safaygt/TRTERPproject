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
			comboBoxDil.Leave += (s, e) => ValidateAndAddData(comboBoxDil, "SUPPLYTYPE");
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
							MH.COMCODE AS 'Firma', 
                            MH.MATDOCTYPE AS 'Malzeme Tipi', 
                            MH.MATDOCNUM AS 'Malzeme Numarası', 
                            MH.MATDOCFROM AS 'Geçerlilik Başlangıç',
							MH.MATDOCUNTIL	AS 'Geçerlilik Bitiş',
							MH.SUPPLYTYPE AS 'Tedarik Tipi',
							MH.STUNIT AS 'Stok Birimi',
							MH.NETWEIGHT AS 'Net Ağırlık',
							MH.NWUNIT AS 'Net Ağırlık Birimi',
							MH.BRUTWEIGHT AS 'Brüt Ağırlık',
							MH.BWUNIT AS 'Brüt Ağırlık Brimi',
							MH.ISBOM AS 'Ürün Ağacı Var mı?',
							MH.BOMDOCTYPE AS 'Ürün Ağacı Tipi',
							MH.BOMDOCNUM AS 'Ürün Ağacı Kodu',
							MH.ISROUTE AS 'Rota Var mı?',
							MH.ROTDOCTYPE AS 'Rota Tipi',
							MH.ROTDOCNUM AS 'Rota Kodu',
							MH.ISDELETED AS 'Silindi mi?',
							MH.ISPASSIVE AS 'Pasif mi?',
							G2.LANCODE AS 'Dil'
							FROM 
							    BSMGRTRTMATHEAD MH
							INNER JOIN 
							    BSMGRTRTMATTEXT MT ON MH.MATDOCNUM = MT.MATDOCNUM
							INNER JOIN 
							    BSMGRTRTGEN002 G2 ON MT.LANCODE = G2.LANCODE";

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