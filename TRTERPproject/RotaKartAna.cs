using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
	public partial class RotaKartAna : Form
	{
		SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
		private readonly SqlConnection _connection = new SqlConnection(ConnectionHelper.ConnectionString);
		SqlDataReader reader;
		SqlCommand cmd;
		public RotaKartAna()
		{
			InitializeComponent();
			this.Load += (s, e) => LoadComboBoxData();

			// ComboBox leave eventlerini bağla
			firmbox.Leave += (s, e) => ValidateAndAddData(firmbox, "COMCODE", "BSMGRTRTGEN001");
			comboBoxRotTip.Leave += (s, e) => ValidateAndAddData(comboBoxRotTip, "DOCTYPE", "BSMGRTRTROT001");
			dilBox.Leave += (s, e) => ValidateAndAddData(dilBox, "LANCODE", "BSMGRTRTGEN002");
			comboBoxurnAgaTip.Leave += (s, e) => ValidateAndAddData(comboBoxurnAgaTip, "DOCTYPE", "BSMGRTRTBOM001");
			comboBoxMalzTip.Leave += (s, e) => ValidateAndAddData(comboBoxMalzTip, "DOCTYPE", "BSMGRTRTMAT001");
			comboBoxIsmerkTip.Leave += (s, e) => ValidateAndAddData(comboBoxIsmerkTip, "DOCTYPE", "BSMGRTRTWCM001");
		}
		private void LoadComboBoxData()
		{
			try
			{
				con.Open();

				// Firma verilerini doldur
				string queryFirma = "SELECT DISTINCT COMCODE FROM BSMGRTRTGEN001";
				SqlDataAdapter daFirma = new SqlDataAdapter(queryFirma, con);
				DataTable dtFirma = new DataTable();
				daFirma.Fill(dtFirma);
				firmbox.DataSource = dtFirma;
				firmbox.DisplayMember = "COMCODE";
				firmbox.ValueMember = "COMCODE";

				string queryMtip = "SELECT DISTINCT DOCTYPE FROM BSMGRTRTROT001"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daMtip = new SqlDataAdapter(queryMtip, con);
				DataTable dtMtip = new DataTable();
				daMtip.Fill(dtMtip);
				comboBoxRotTip.DataSource = dtMtip;
				comboBoxRotTip.DisplayMember = "DOCTYPE";
				comboBoxRotTip.ValueMember = "DOCTYPE";


				string queryTtip = "SELECT DISTINCT LANCODE FROM BSMGRTRTGEN002"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daTtip = new SqlDataAdapter(queryTtip, con);
				DataTable dtTtip = new DataTable();
				daTtip.Fill(dtTtip);
				dilBox.DataSource = dtTtip;
				dilBox.DisplayMember = "LANCODE";
				dilBox.ValueMember = "LANCODE";


				string queryUtip = "SELECT DISTINCT DOCTYPE FROM BSMGRTRTBOM001"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daUtip = new SqlDataAdapter(queryUtip, con);
				DataTable dtUtip = new DataTable();
				daUtip.Fill(dtUtip);
				comboBoxurnAgaTip.DataSource = dtUtip;
				comboBoxurnAgaTip.DisplayMember = "DOCTYPE";
				comboBoxurnAgaTip.ValueMember = "DOCTYPE";

				string queryMLtip = "SELECT DISTINCT DOCTYPE FROM BSMGRTRTMAT001"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daMLtip = new SqlDataAdapter(queryMLtip, con);
				DataTable dtMLtip = new DataTable();
				daMLtip.Fill(dtMLtip);
				comboBoxMalzTip.DataSource = dtMLtip;
				comboBoxMalzTip.DisplayMember = "DOCTYPE";
				comboBoxMalzTip.ValueMember = "DOCTYPE";

				string queryIMtip = "SELECT DISTINCT DOCTYPE FROM BSMGRTRTWCM001"; // Tablo ve sütun adını kontrol edin
				SqlDataAdapter daIMtip = new SqlDataAdapter(queryIMtip, con);
				DataTable dtIMtip = new DataTable();
				daIMtip.Fill(dtIMtip);
				comboBoxIsmerkTip.DataSource = dtIMtip;
				comboBoxIsmerkTip.DisplayMember = "DOCTYPE";
				comboBoxIsmerkTip.ValueMember = "DOCTYPE";

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
		private void ValidateAndAddData(ComboBox comboBox, string columnName, string tableName)
		{
			string checkQuery = $@"
SELECT COUNT(*) 
FROM {tableName} 
WHERE {columnName} = @userInput";

			if (string.IsNullOrEmpty(comboBox.Text)) return;

			try
			{
				using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
				{
					con.Open();
					using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
					{
						checkCmd.Parameters.AddWithValue("@userInput", comboBox.Text);
						int count = (int)checkCmd.ExecuteScalar();

						if (count == 0)
						{
							MessageBox.Show($"{columnName} '{comboBox.Text}' tablodaki verilerle uyuşmuyor.");
							comboBox.Text = string.Empty; // Kullanıcının yanlış girişini temizler
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Hata: {ex.Message}");
			}
		}

		private void getBut_Click(object sender, EventArgs e)
		{// Temel SQL sorgusu
			string query = @"
                        SELECT
					    RH.COMCODE AS 'Firma',
					    BROC.ROTDOCTYPE AS 'Rota Tipi',
						BROC.ROTDOCNUM AS 'Rota Numarası',
					    B1.DOCTYPE AS 'Ürün Ağacı Tipi',
					    RH.MATDOCTYPE AS 'Malzeme Tipi',
					    RH.MATDOCNUM AS 'Malzeme Kodu',
					    BROC.WCMDOCTYPE AS 'İş Merkezi Tipi',
					    BROC.WCMDOCNUM AS 'İş Merkezi Kodu',
					    BROC.OPRDOCTYPE AS 'Operasyon Kodu',
					    BROC.OPRNUM AS 'Operasyon Numarası',
					    BROC.SETUPTIME AS 'Operasyon Hazırlık Süresi(Saat)',
					    BROC.MACHINETIME AS 'Operasyon Makine Süresi(Saat)',
					    BROC.LABOURTIME AS 'Operasyon İşçilik Süresi(Saat)',
					    BRBC.CONTENTNUM AS 'İçerik Numarası',						
					    BRBC.COMPONENT AS 'Bileşen Kodu',
					    BRBC.QUANTITY AS 'Bileşen Miktarı',
					    RH.ROTDOCFROM AS 'Geçerlilik Başlangıç',
					    RH.ROTDOCUNTIL AS 'Geçerlilik Bitiş',
					    RH.QUANTITY AS 'Miktar',
					    RH.DRAWNUM AS 'Çizim Numarası',
					    RH.ISDELETED AS 'Silindi mi?',
					    RH.ISPASSIVE AS 'Pasif mi?',
					    G2.LANCODE AS 'Dil'
					FROM 
					    BSMGRTRTROTHEAD RH
					INNER JOIN 
					    BSMGRTRTMATHEAD MH ON MH.MATDOCNUM = RH.MATDOCNUM
					INNER JOIN
					    BSMGRTRTMATTEXT MT ON MH.MATDOCNUM = MT.MATDOCNUM
					INNER JOIN
					    BSMGRTRTBOM001 B1 ON RH.ROTDOCTYPE = B1.DOCTYPE
					INNER JOIN
					    BSMGRTRTROTOPRCONTENT BROC ON RH.ROTDOCNUM = BROC.ROTDOCNUM
					INNER JOIN
					    BSMGRTRTROTBOMCONTENT BRBC ON RH.ROTDOCNUM = BRBC.ROTDOCNUM
					INNER JOIN 
					    BSMGRTRTGEN002 G2 ON MT.LANCODE = G2.LANCODE";

			// Filtreleme koşulları
			List<string> filters = new List<string>();

			// Firma filtresi (LIKE ile kısmi eşleşme)
			if (!string.IsNullOrEmpty(firmbox.Text))
			{
				filters.Add("RH.COMCODE LIKE @COMCODE");
			}

			// Maliyet Merkezi Tipi filtresi (Tam eşleşme)
			if (!string.IsNullOrEmpty(comboBoxRotTip.Text))
			{
				filters.Add("BROC.ROTDOCTYPE = @ROTDOCTYPE");
			}

			// Maliyet Merkezi Numarası filtresi (LIKE ile kısmi eşleşme)
			if (!string.IsNullOrEmpty(RotaNumBox.Text))
			{
				filters.Add("RH.ROTDOCNUM LIKE @ROTDOCNUM");
			}

			// Ana Maliyet Merkezi Tipi filtresi (LIKE ile kısmi eşleşme)
			if (!string.IsNullOrEmpty(comboBoxMalzTip.Text))
			{
				filters.Add("RH.MATDOCTYPE LIKE @MATDOCTYPE");
			}

			// Dil filtresi
			if (!string.IsNullOrEmpty(dilBox.Text))
			{
				filters.Add("G2.LANCODE = @LANCODE");
			}

			// Tarih aralığı filtresi
			if (dateTimePickerBaslangic.Value.Date != DateTime.MinValue.Date && dateTimePickerBitis.Value.Date != DateTime.MinValue.Date)
			{
				filters.Add("RH.ROTDOCFROM >= @CCMDOCFROM AND RH.ROTDOCUNTIL <= @CCMDOCUNTIL");
			}

			// Pasiflik kontrolü
			if (checkboxpas.Checked)
			{
				filters.Add("RH.ISPASSIVE = 1");
			}

			// Silinmişlik kontrolü
			if (deletedlbl.Checked)
			{
				filters.Add("RH.ISDELETED = 1");
			}

			// Filtreleri sorguya ekle
			if (filters.Count > 0)
			{
				query += " WHERE " + string.Join(" AND ", filters);
			}

			// SQL bağlantısı ve komutu
			using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
			using (SqlCommand cmd = new SqlCommand(query, con))
			{
				// Parametreleri ekle
				if (!string.IsNullOrEmpty(firmbox.Text))
				{
					cmd.Parameters.AddWithValue("@COMCODE", $"{firmbox.Text}%");
				}

				if (!string.IsNullOrEmpty(comboBoxRotTip.Text))
				{
					cmd.Parameters.AddWithValue("@ROTDOCTYPE", comboBoxRotTip.Text);
				}

				if (!string.IsNullOrEmpty(RotaNumBox.Text))
				{
					cmd.Parameters.AddWithValue("@ROTDOCNUM", $"{RotaNumBox.Text}%");
				}

				if (!string.IsNullOrEmpty(comboBoxMalzTip.Text))
				{
					cmd.Parameters.AddWithValue("@MATDOCTYPE", $"{comboBoxMalzTip.Text}%");
				}

				if (!string.IsNullOrEmpty(dilBox.Text))
				{
					cmd.Parameters.AddWithValue("@LANCODE", dilBox.Text);
				}

				if (dateTimePickerBaslangic.Value.Date != DateTime.MinValue.Date && dateTimePickerBitis.Value.Date != DateTime.MinValue.Date)
				{
					cmd.Parameters.AddWithValue("@CCMDOCFROM", dateTimePickerBaslangic.Value.Date);
					cmd.Parameters.AddWithValue("@CCMDOCUNTIL", dateTimePickerBitis.Value.Date);
				}

				try
				{
					con.Open();
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataSet dt = new DataSet();
					da.Fill(dt);

					// DataGridView'e sonuçları bağla
					RotaData.DataSource = dt.Tables[0];

					// Veri olmadığında kullanıcıyı bilgilendir
					if (dt.Tables[0].Rows.Count == 0)
					{
						MessageBox.Show("Filtrelerinizle eşleşen bir veri bulunamadı.");
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

		}

		private void getAllBut_Click(object sender, EventArgs e)
		{
			string query = @"
                        SELECT
					    RH.COMCODE AS 'Firma',
					    BROC.ROTDOCTYPE AS 'Rota Tipi',
						BROC.ROTDOCNUM AS 'Rota Numarası',
					    B1.DOCTYPE AS 'Ürün Ağacı Tipi',
						BRBC.BOMDOCNUM AS 'Ürün Ağacı Kodu',
					    RH.MATDOCTYPE AS 'Malzeme Tipi',
					    RH.MATDOCNUM AS 'Malzeme Kodu',
					    BROC.WCMDOCTYPE AS 'İş Merkezi Tipi',
					    BROC.WCMDOCNUM AS 'İş Merkezi Kodu',
					    BROC.OPRDOCTYPE AS 'Operasyon Kodu',
					    BROC.OPRNUM AS 'Operasyon Numarası',
					    BROC.SETUPTIME AS 'Operasyon Hazırlık Süresi(Saat)',
					    BROC.MACHINETIME AS 'Operasyon Makine Süresi(Saat)',
					    BROC.LABOURTIME AS 'Operasyon İşçilik Süresi(Saat)',
					    BRBC.CONTENTNUM AS 'İçerik Numarası',						
					    BRBC.COMPONENT AS 'Bileşen Kodu',
					    BRBC.QUANTITY AS 'Bileşen Miktarı',
					    RH.ROTDOCFROM AS 'Geçerlilik Başlangıç',
					    RH.ROTDOCUNTIL AS 'Geçerlilik Bitiş',
					    RH.QUANTITY AS 'Temel Miktar',
					    RH.DRAWNUM AS 'Çizim Numarası',
					    RH.ISDELETED AS 'Silindi mi?',
					    RH.ISPASSIVE AS 'Pasif mi?',
					    G2.LANCODE AS 'Dil'
					FROM 
					    BSMGRTRTROTHEAD RH
					INNER JOIN 
					    BSMGRTRTMATHEAD MH ON MH.MATDOCNUM = RH.MATDOCNUM
					INNER JOIN
					    BSMGRTRTMATTEXT MT ON MH.MATDOCNUM = MT.MATDOCNUM
					INNER JOIN
					    BSMGRTRTBOM001 B1 ON RH.ROTDOCTYPE = B1.DOCTYPE
					INNER JOIN
					    BSMGRTRTROTOPRCONTENT BROC ON RH.ROTDOCNUM = BROC.ROTDOCNUM
					INNER JOIN
					    BSMGRTRTROTBOMCONTENT BRBC ON RH.ROTDOCNUM = BRBC.ROTDOCNUM
					INNER JOIN 
					    BSMGRTRTGEN002 G2 ON MT.LANCODE = G2.LANCODE";
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

		private void DelBut_Click(object sender, EventArgs e)
		{
			// DataGridView'den seçilen satırı kontrol et
			if (RotaData.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = RotaData.SelectedRows[0];

				// Silinecek ROTDOCNUM değerini al
				string rotDocNum = selectedRow.Cells["Rota Numarası"].Value?.ToString();

				if (string.IsNullOrEmpty(rotDocNum))
				{
					MessageBox.Show("Seçili satırda 'ROTDOCNUM' değeri bulunamadı.");
					return;
				}

				// Kullanıcıdan onay iste
				DialogResult result = MessageBox.Show(
					$"'{rotDocNum}' koduna sahip rota kaydını ve ilişkili verileri silmek istediğinize emin misiniz?",
					"Silme Onayı",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Warning);

				if (result == DialogResult.Yes)
				{
					// Silme sorguları
					string deleteBomContentQuery = "DELETE FROM BSMGRTRTROTBOMCONTENT WHERE ROTDOCNUM = @rotDocNum";
					string deleteOprContentQuery = "DELETE FROM BSMGRTRTROTOPRCONTENT WHERE ROTDOCNUM = @rotDocNum";
					string deleteHeadQuery = "DELETE FROM BSMGRTRTROTHEAD WHERE ROTDOCNUM = @rotDocNum";

					con = new SqlConnection(ConnectionHelper.ConnectionString);

					try
					{
						con.Open();
						using (SqlTransaction transaction = con.BeginTransaction())
						{
							try
							{
								// BOMCONTENT tablosundan silme
								cmd = new SqlCommand(deleteBomContentQuery, con, transaction);
								cmd.Parameters.AddWithValue("@rotDocNum", rotDocNum);
								cmd.ExecuteNonQuery();

								// OPRCONTENT tablosundan silme
								cmd = new SqlCommand(deleteOprContentQuery, con, transaction);
								cmd.Parameters.AddWithValue("@rotDocNum", rotDocNum);
								cmd.ExecuteNonQuery();

								// HEAD tablosundan silme
								cmd = new SqlCommand(deleteHeadQuery, con, transaction);
								cmd.Parameters.AddWithValue("@rotDocNum", rotDocNum);
								cmd.ExecuteNonQuery();

								// İşlemleri onayla
								transaction.Commit();

								MessageBox.Show("Kayıt başarıyla silindi.");
								// Silinen kaydı DataGridView'den güncelle
								RotaData.Rows.RemoveAt(selectedRow.Index);
							}
							catch (Exception ex)
							{
								// Hata durumunda işlemleri geri al
								transaction.Rollback();
								MessageBox.Show($"Silme işlemi sırasında bir hata oluştu: {ex.Message}");
							}
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
			}
			else
			{
				MessageBox.Show("Lütfen silinecek bir satır seçin.");
			}
		}

		private void duzBut_Click(object sender, EventArgs e)
		{
			// DataGridView'den seçilen satırı kontrol et
			if (RotaData.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = RotaData.SelectedRows[0];

				// Yeni bir düzenleme formu oluştur ve seçilen verileri aktar
				RotaKartEdit rotaKartEdit = new RotaKartEdit();

				// Formdaki alanlara DataGridView'deki değerleri aktar
				rotaKartEdit.Firma = selectedRow.Cells["Firma"].Value != DBNull.Value
					? selectedRow.Cells["Firma"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Rotdoctype = selectedRow.Cells["Rota Tipi"].Value != DBNull.Value
					? selectedRow.Cells["Rota Tipi"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Rotdocnum = selectedRow.Cells["Rota Numarası"].Value != DBNull.Value
					? selectedRow.Cells["Rota Numarası"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Doctype = selectedRow.Cells["Ürün Ağacı Tipi"].Value != DBNull.Value
					? selectedRow.Cells["Ürün Ağacı Tipi"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Bomdocnum = selectedRow.Cells["Ürün Ağacı Kodu"].Value != DBNull.Value
					? selectedRow.Cells["Ürün Ağacı Kodu"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Matdoctype = selectedRow.Cells["Malzeme Tipi"].Value != DBNull.Value
					? selectedRow.Cells["Malzeme Tipi"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Matdocnum = selectedRow.Cells["Malzeme Kodu"].Value != DBNull.Value
					? selectedRow.Cells["Malzeme Kodu"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Wcmdoctype = selectedRow.Cells["İş Merkezi Tipi"].Value != DBNull.Value
					? selectedRow.Cells["İş Merkezi Tipi"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Wcmdocnum = selectedRow.Cells["İş Merkezi Kodu"].Value != DBNull.Value
					? selectedRow.Cells["İş Merkezi Kodu"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Oprdoctype = selectedRow.Cells["Operasyon Kodu"].Value != DBNull.Value
					? selectedRow.Cells["Operasyon Kodu"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Oprnum = selectedRow.Cells["Operasyon Numarası"].Value != DBNull.Value
					? selectedRow.Cells["Operasyon Numarası"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Setuptime = selectedRow.Cells["Operasyon Hazırlık Süresi(Saat)"].Value != DBNull.Value
					? selectedRow.Cells["Operasyon Hazırlık Süresi(Saat)"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Machinetime = selectedRow.Cells["Operasyon Makine Süresi(Saat)"].Value != DBNull.Value
					? selectedRow.Cells["Operasyon Makine Süresi(Saat)"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Labourtime = selectedRow.Cells["Operasyon İşçilik Süresi(Saat)"].Value != DBNull.Value
					? selectedRow.Cells["Operasyon İşçilik Süresi(Saat)"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Contentnum = selectedRow.Cells["İçerik Numarası"].Value != DBNull.Value
					? selectedRow.Cells["İçerik Numarası"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Component = selectedRow.Cells["Bileşen Kodu"].Value != DBNull.Value
					? selectedRow.Cells["Bileşen Kodu"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Quantity = selectedRow.Cells["Bileşen Miktarı"].Value != DBNull.Value
					? selectedRow.Cells["Bileşen Miktarı"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Drawnum = selectedRow.Cells["Çizim Numarası"].Value != DBNull.Value
					? selectedRow.Cells["Çizim Numarası"].Value.ToString()
					: string.Empty;

				rotaKartEdit.Quantity2 = selectedRow.Cells["Temel Miktar"].Value != DBNull.Value
					? selectedRow.Cells["Temel Miktar"].Value.ToString()
					: string.Empty;

				rotaKartEdit.GecerliBaslangic = selectedRow.Cells["Geçerlilik Başlangıç"].Value != DBNull.Value
					? Convert.ToDateTime(selectedRow.Cells["Geçerlilik Başlangıç"].Value)
					: DateTime.MinValue;

				rotaKartEdit.GecerliBitis = selectedRow.Cells["Geçerlilik Bitiş"].Value != DBNull.Value
					? Convert.ToDateTime(selectedRow.Cells["Geçerlilik Bitiş"].Value)
					: DateTime.MaxValue;

				rotaKartEdit.IsDeleted = selectedRow.Cells["Silindi mi?"].Value != DBNull.Value
					? Convert.ToBoolean(selectedRow.Cells["Silindi mi?"].Value)
					: false;

				rotaKartEdit.IsPassive = selectedRow.Cells["Pasif mi?"].Value != DBNull.Value
					? Convert.ToBoolean(selectedRow.Cells["Pasif mi?"].Value)
					: false;

				rotaKartEdit.Dil = selectedRow.Cells["Dil"].Value != DBNull.Value
					? selectedRow.Cells["Dil"].Value.ToString()
					: string.Empty;

				// Diğer alanları burada ekleyebilirsiniz...

				// Düzenleme formunu göster
				rotaKartEdit.ShowDialog();
			}
			else
			{
				MessageBox.Show("Lütfen düzenlemek için bir satır seçin.");
			}
		}

		private void addBut_Click(object sender, EventArgs e)
		{
			RotaKartAdd rotaKartAdd = new RotaKartAdd();
			rotaKartAdd.ShowDialog();
		}
	}
}

