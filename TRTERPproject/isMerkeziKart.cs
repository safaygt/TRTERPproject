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
			firmbox.Leave += (s, e) => ValidateAndAddData(firmbox, "COMCODE", "BSMGRTRTGEN001");
			comboBoxIsMerTip.Leave += (s, e) => ValidateAndAddData(comboBoxIsMerTip, "DOCTYPE", "BSMGRTRTWCM001");
			dilBox.Leave += (s, e) => ValidateAndAddData(dilBox, "LANCODE", "BSMGRTRTGEN002");
			comboBoxOprCode.Leave += (s, e) => ValidateAndAddData(comboBoxOprCode, "DOCTYPE", "BSMGRTRTOPR001");
			comboBoxMalytMer.Leave += (s, e) => ValidateAndAddData(comboBoxMalytMer, "DOCTYPE", "BSMGRTRTCCM001");
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

				// İş Merkezi Tipi verilerini doldur
				string queryisMtip = "SELECT DISTINCT DOCTYPE FROM BSMGRTRTWCM001";
				SqlDataAdapter daisMtip = new SqlDataAdapter(queryisMtip, con);
				DataTable dtisMtip = new DataTable();
				daisMtip.Fill(dtisMtip);
				comboBoxIsMerTip.DataSource = dtisMtip;
				comboBoxIsMerTip.DisplayMember = "DOCTYPE";
				comboBoxIsMerTip.ValueMember = "DOCTYPE";

				// Dil Kodları verilerini doldur
				string queryTtip = "SELECT DISTINCT LANCODE FROM BSMGRTRTGEN002";
				SqlDataAdapter daTtip = new SqlDataAdapter(queryTtip, con);
				DataTable dtTtip = new DataTable();
				daTtip.Fill(dtTtip);
				dilBox.DataSource = dtTtip;
				dilBox.DisplayMember = "LANCODE";
				dilBox.ValueMember = "LANCODE";

				// Operasyon Kodu verilerini doldur
				string queryOprtip = "SELECT DISTINCT DOCTYPE FROM BSMGRTRTOPR001";
				SqlDataAdapter daOprtip = new SqlDataAdapter(queryOprtip, con);
				DataTable dtOprtip = new DataTable();
				daOprtip.Fill(dtOprtip);
				comboBoxOprCode.DataSource = dtOprtip;
				comboBoxOprCode.DisplayMember = "DOCTYPE";
				comboBoxOprCode.ValueMember = "DOCTYPE";
				// Maliyet Merkezi verilerini doldur
				string queryMaltip = "SELECT DISTINCT DOCTYPE FROM BSMGRTRTCCM001";
				SqlDataAdapter daMaltip = new SqlDataAdapter(queryMaltip, con);
				DataTable dtMaltip = new DataTable();
				daMaltip.Fill(dtMaltip);
				comboBoxMalytMer.DataSource = dtMaltip;
				comboBoxMalytMer.DisplayMember = "DOCTYPE";
				comboBoxMalytMer.ValueMember = "DOCTYPE";
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

		private void getBut_Click_1(object sender, EventArgs e)
		{
			// Temel SQL sorgusu
			string query = @"
SELECT 
    WH.COMCODE AS 'Firma', 
    WH.WCMDOCTYPE AS 'İş Merkezi Tipi', 
    WH.WCMDOCNUM AS 'İş Merkezi  Numarası',
    O1.DOCTYPE AS 'Operasyon Kodu', 
    WH.CCMDOCTYPE AS 'Maliyet Merkezi Tipi', 
    WH.CCMDOCNUM AS 'Maliyet Merkezi Kodu', 
    WH.WCMDOCFROM AS 'Geçerlilik Başlangıç', 
    WH.WCMDOCUNTIL AS 'Geçerlilik Bitiş', 
    WH.MAINWCMDOCTYPE AS 'Ana İş Merkezi Tipi', 
    WH.MAINWCMDOCNUM AS 'Ana İş Merkezi Numarası', 
    WT.WCMSTEXT AS 'İş Merkezi Kısa Açıklama', 
    WT.WCMLTEXT AS 'İş Merkezi Uzun Açıklama', 
    WH.WORKTIME AS 'Çalışma Süresi (Saat)',
    WH.ISDELETED AS 'Silindi mi?', 
    WH.ISPASSIVE AS 'Pasif mi?', 
    G2.LANCODE AS 'Dil' 
FROM 
    BSMGRTRTWCMHEAD WH
INNER JOIN 
    BSMGRTRTWCMTEXT WT ON WH.WCMDOCNUM = WT.WCMDOCNUM
INNER JOIN 
    BSMGRTRTGEN002 G2 ON WT.LANCODE = G2.LANCODE
INNER JOIN 
    BSMGRTRTWCMOPR WO ON WH.WCMDOCNUM = WO.WCMDOCNUM
INNER JOIN
    BSMGRTRTOPR001 O1 ON WO.OPRDOCTYPE = O1.DOCTYPE";

			// Filtreleme koşulları
			List<string> filters = new List<string>();

			// Firma filtresi
			if (!string.IsNullOrEmpty(firmbox.Text))
			{
				filters.Add("WH.COMCODE = @COMCODE");
			}

			// İş merkezi tipi filtresi
			if (!string.IsNullOrEmpty(comboBoxIsMerTip.Text))
			{
				filters.Add("WH.WCMDOCTYPE = @WCMDOCTYPE");
			}

			// İş merkezi kodu filtresi
			if (!string.IsNullOrEmpty(ismerkodtxtBox.Text))
			{
				filters.Add("WH.WCMDOCNUM = @WCMDOCNUM");
			}

			// Operasyon kodu filtresi
			if (!string.IsNullOrEmpty(comboBoxOprCode.Text))
			{
				filters.Add("O1.DOCTYPE = @DOCTYPE");
			}

			// Dil filtresi
			if (!string.IsNullOrEmpty(dilBox.Text))
			{
				filters.Add("G2.LANCODE = @LANCODE");
			}

			// Maliyet merkezi tipi filtresi
			if (!string.IsNullOrEmpty(comboBoxMalytMer.Text))
			{
				filters.Add("WH.CCMDOCTYPE = @CCMDOCTYPE");
			}

			// Tarih aralığı filtresi
			if (dateTimeBas.Value != DateTime.MinValue && dateTimeBit.Value != DateTime.MinValue)
			{
				filters.Add("WH.WCMDOCFROM >= @WCMDOCFROM AND WH.WCMDOCUNTIL <= @WCMDOCUNTIL");
			}

			// Pasif mi filtresi
			if (checkboxpas.Checked)
			{
				filters.Add("WH.ISPASSIVE = 1");
			}
			else
			{
				filters.Add("WH.ISPASSIVE = 0");
			}

			// Silindi mi filtresi
			if (deletedlbl.Checked)
			{
				filters.Add("WH.ISDELETED = 1");
			}
			else
			{
				filters.Add("WH.ISDELETED = 0");
			}

			// Filtreleri sorguya ekle
			if (filters.Count > 0)
			{
				query += " WHERE " + string.Join(" AND ", filters);
			}

			// SQL bağlantısı ve komutu
			con = new SqlConnection(ConnectionHelper.ConnectionString);
			cmd = new SqlCommand(query, con);

			// Parametreleri ekle
			if (!string.IsNullOrEmpty(firmbox.Text))
			{
				cmd.Parameters.AddWithValue("@COMCODE", firmbox.Text);
			}

			if (!string.IsNullOrEmpty(comboBoxIsMerTip.Text))
			{
				cmd.Parameters.AddWithValue("@WCMDOCTYPE", comboBoxIsMerTip.Text);
			}

			if (!string.IsNullOrEmpty(comboBoxMalytMer.Text))
			{
				cmd.Parameters.AddWithValue("@CCMDOCTYPE", comboBoxMalytMer.Text);
			}

			if (!string.IsNullOrEmpty(ismerkodtxtBox.Text))
			{
				cmd.Parameters.AddWithValue("@WCMDOCNUM", ismerkodtxtBox.Text);
			}

			if (!string.IsNullOrEmpty(comboBoxOprCode.Text))
			{
				cmd.Parameters.AddWithValue("@DOCTYPE", comboBoxOprCode.Text);
			}

			if (!string.IsNullOrEmpty(dilBox.Text))
			{
				cmd.Parameters.AddWithValue("@LANCODE", dilBox.Text);
			}

			if (dateTimeBas.Value != DateTime.MinValue && dateTimeBit.Value != DateTime.MinValue)
			{
				cmd.Parameters.AddWithValue("@WCMDOCFROM", dateTimeBas.Value);
				cmd.Parameters.AddWithValue("@WCMDOCUNTIL", dateTimeBit.Value);
			}

			// Verileri getir
			try
			{
				con.Open();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				DataSet dt = new DataSet();
				da.Fill(dt);
				ismerkData.DataSource = dt.Tables[0];
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


		private void duzBut_Click(object sender, EventArgs e)
		{
			// DataGridView'den seçilen satırı kontrol et
			if (ismerkData.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = ismerkData.SelectedRows[0];

				// Yeni bir edit form oluştur ve seçilen veriyi aktar
				isMerkeziKartAnaEditForm isMerkeziKartAnaEditForm = new isMerkeziKartAnaEditForm();

				// Formdaki alanlara DataGridView'deki değerleri aktar
				isMerkeziKartAnaEditForm.Firma = selectedRow.Cells["Firma"].Value.ToString();
				isMerkeziKartAnaEditForm.IsMerkeziTipi = selectedRow.Cells["İş Merkezi Tipi"].Value.ToString();
				isMerkeziKartAnaEditForm.IsMerkeziNumarasi = selectedRow.Cells["İş Merkezi  Numarası"].Value.ToString();
				isMerkeziKartAnaEditForm.GecerliBaslangic = Convert.ToDateTime(selectedRow.Cells["Geçerlilik Başlangıç"].Value);
				isMerkeziKartAnaEditForm.GecerliBitis = Convert.ToDateTime(selectedRow.Cells["Geçerlilik Bitiş"].Value);
				isMerkeziKartAnaEditForm.AnaIsMerkeziTipi = selectedRow.Cells["Ana İş Merkezi Tipi"].Value.ToString();
				isMerkeziKartAnaEditForm.AnaIsMerkeziNumarasi = selectedRow.Cells["Ana İş Merkezi Numarası"].Value.ToString();
				isMerkeziKartAnaEditForm.OperasyonKodu = selectedRow.Cells["Operasyon Kodu"].Value.ToString();
				isMerkeziKartAnaEditForm.MaliMerTip = selectedRow.Cells["Maliyet Merkezi Tipi"].Value.ToString();
				isMerkeziKartAnaEditForm.MaliMerKod = selectedRow.Cells["Maliyet Merkezi Kodu"].Value.ToString();
				isMerkeziKartAnaEditForm.IMKA = selectedRow.Cells["İş Merkezi Kısa Açıklama"].Value.ToString();
				isMerkeziKartAnaEditForm.IMUA = selectedRow.Cells["İş Merkezi Uzun Açıklama"].Value.ToString();
				isMerkeziKartAnaEditForm.Dil = selectedRow.Cells["Dil"].Value.ToString();
				isMerkeziKartAnaEditForm.IsDeleted = Convert.ToBoolean(selectedRow.Cells["Silindi mi?"].Value != DBNull.Value ? Convert.ToBoolean(selectedRow.Cells["Silindi mi?"].Value) : 0);
				isMerkeziKartAnaEditForm.IsPassive = Convert.ToBoolean(selectedRow.Cells["Pasif mi?"].Value != DBNull.Value ? Convert.ToBoolean(selectedRow.Cells["Pasif Mi?"].Value) : 0);
				isMerkeziKartAnaEditForm.Worktime = selectedRow.Cells["Çalışma Süresi (Saat)"].Value.ToString();

				// Edit formu göster
				isMerkeziKartAnaEditForm.ShowDialog();
			}
			else
			{
				MessageBox.Show("Lütfen düzenlemek için bir satır seçin.");
			}
		}

		private void addBut_Click(object sender, EventArgs e)
		{
			isMerkeziAddForm isMerkeziAddForm = new isMerkeziAddForm();
			isMerkeziAddForm.ShowDialog();

		}

		private void geAllBut_Click(object sender, EventArgs e)
		{
			// SQL sorgusu
			string query = @"
                        SELECT 
							WH.COMCODE AS 'Firma', 
                            WH.WCMDOCTYPE AS 'İş Merkezi Tipi', 
                            WH.WCMDOCNUM AS 'İş Merkezi  Numarası',
							O1.DOCTYPE AS 'Operasyon Kodu', 
							WH.CCMDOCTYPE AS 'Maliyet Merkezi Tipi', 
							WH.CCMDOCNUM AS 'Maliyet Merkezi Kodu', 
                            WH.WCMDOCFROM AS 'Geçerlilik Başlangıç', 
							WH.WCMDOCUNTIL	AS 'Geçerlilik Bitiş', 
                            WH.MAINWCMDOCTYPE AS 'Ana İş Merkezi Tipi', 
                            WH.MAINWCMDOCNUM AS 'Ana İş Merkezi Numarası', 
							WT.WCMSTEXT AS 'İş Merkezi Kısa Açıklama', 
							WT.WCMLTEXT AS 'İş Merkezi Uzun Açıklama', 
							WH.WORKTIME AS 'Çalışma Süresi (Saat)',
							WH.ISDELETED AS 'Silindi mi?', 
							WH.ISPASSIVE AS 'Pasif mi?', 
							G2.LANCODE AS 'Dil' 
							FROM 
							    BSMGRTRTWCMHEAD WH
							INNER JOIN 
							    BSMGRTRTWCMTEXT WT ON WH.WCMDOCNUM = WT.WCMDOCNUM
							INNER JOIN 
							    BSMGRTRTGEN002 G2 ON WT.LANCODE = G2.LANCODE
							INNER JOIN 
								BSMGRTRTWCMOPR WO ON WH.WCMDOCNUM = WO.WCMDOCNUM
							INNER JOIN
								BSMGRTRTOPR001 O1 ON WO.OPRDOCTYPE = O1.DOCTYPE";

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

		private void DelBut_Click(object sender, EventArgs e)
		{
			// DataGridView'den seçilen satırı kontrol et
			if (ismerkData.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = ismerkData.SelectedRows[0];

				// Silinecek iş merkezi numarasını al
				string isMerkeziNumarasi = selectedRow.Cells["İş Merkezi  Numarası"].Value.ToString();

				// Kullanıcıdan onay iste
				DialogResult result = MessageBox.Show($"'{isMerkeziNumarasi}' numaralı iş merkezi kaydını silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

				if (result == DialogResult.Yes)
				{
					// SQL DELETE sorguları
					string deleteQueryHead = "DELETE FROM BSMGRTRTWCMHEAD WHERE WCMDOCNUM = @isMerkeziNumarasi";
					string deleteQueryText = "DELETE FROM BSMGRTRTWCMTEXT WHERE WCMDOCNUM = @isMerkeziNumarasi";

					con = new SqlConnection(ConnectionHelper.ConnectionString);

					try
					{
						con.Open();
						using (SqlTransaction transaction = con.BeginTransaction())
						{
							try
							{
								// HEAD tablosundan silme
								cmd = new SqlCommand(deleteQueryHead, con, transaction);
								cmd.Parameters.AddWithValue("@isMerkeziNumarasi", isMerkeziNumarasi);
								cmd.ExecuteNonQuery();

								// TEXT tablosundan silme
								cmd = new SqlCommand(deleteQueryText, con, transaction);
								cmd.Parameters.AddWithValue("@isMerkeziNumarasi", isMerkeziNumarasi);
								cmd.ExecuteNonQuery();

								// İşlemleri onayla
								transaction.Commit();

								MessageBox.Show("İş Merkezi kaydı başarıyla silindi.");
								// Silinen kaydı DataGridView'den güncelle
								ismerkData.Rows.RemoveAt(selectedRow.Index);
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


	}
}
