using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Windows.Forms;
using TRTERPproject.Helpers;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace TRTERPproject
{
	public partial class isMerkeziAddForm : Form
	{
		SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
		SqlDataReader reader;
		SqlCommand cmd;

		public isMerkeziAddForm()
		{
			InitializeComponent();
			this.Load += (s, e) => LoadComboBoxData();

			firmbox.Leave += (s, e) => ValidateAndAddData(firmbox, "COMCODE");
			comboBoxIsMerTip.Leave += (s, e) => ValidateAndAddData(comboBoxIsMerTip, "DOCTYPE");
			dilBox.Leave += (s, e) => ValidateAndAddData(dilBox, "LANCODE");
			comboBoxOprCode.Leave += (s, e) => ValidateAndAddData(comboBoxOprCode, "DOCTYPE");
			comboBoxMaliMerk.Leave += (s, e) => ValidateAndAddData(comboBoxMaliMerk, "DOCTYPE");
		}

		private void LoadComboBoxData()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
				{
					con.Open();

					// Firma verileri
					LoadComboBox(firmbox, "SELECT DISTINCT COMCODE FROM BSMGRTRTGEN001", "COMCODE");

					// İş Merkezi Tipi
					LoadComboBox(comboBoxIsMerTip, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTWCM001", "DOCTYPE");

					// Dil Kodları
					LoadComboBox(dilBox, "SELECT DISTINCT LANCODE FROM BSMGRTRTGEN002", "LANCODE");

					// Operasyon Kodu
					LoadComboBox(comboBoxOprCode, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTOPR001", "DOCTYPE");

					// Maliyet Merkezi
					LoadComboBox(comboBoxMaliMerk, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTCCM001", "DOCTYPE");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}");
			}
		}

		private void LoadComboBox(ComboBox comboBox, string query, string columnName)
		{
			using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
			{
				using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
				{
					DataTable dt = new DataTable();
					adapter.Fill(dt);
					comboBox.DataSource = dt;
					comboBox.DisplayMember = columnName;
					comboBox.ValueMember = columnName;
					comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
				}
			}
		}
		private void ValidateAndAddData(ComboBox comboBox, string columnName)
		{
			string userInput = comboBox.Text.Trim();
			if (string.IsNullOrEmpty(userInput))
				return;

			string checkQuery = $"SELECT COUNT(*) FROM BSMGRTRTWCMHEAD WHERE {columnName} = @userInput";

			try
			{
				using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
				{
					using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
					{
						checkCmd.Parameters.AddWithValue("@userInput", userInput);
						con.Open();

						int count = (int)checkCmd.ExecuteScalar();
						if (count == 0)
						{
							MessageBox.Show($"'{userInput}' değeri {columnName} sütunu için geçerli değil.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							comboBox.Text = string.Empty;
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Hata: {ex.Message}");
			}
		}


		private bool ValidateFields()
		{
			// Tüm metin kutularının dolu olduğunu kontrol et
			if (string.IsNullOrEmpty(firmbox.Text.Trim()))
			{
				MessageBox.Show("Firma kodu alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				firmbox.Focus();
				return false;
			}

			if (string.IsNullOrEmpty(comboBoxIsMerTip.Text.Trim()))
			{
				MessageBox.Show("İş merkezi tipi alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				comboBoxIsMerTip.Focus();
				return false;
			}

			if (string.IsNullOrEmpty(ismerkodtxtBox.Text.Trim()))
			{
				MessageBox.Show("İş merkezi numarası alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				ismerkodtxtBox.Focus();
				return false;
			}

			if (string.IsNullOrEmpty(anaismerkod.Text.Trim()))
			{
				MessageBox.Show("Ana iş merkezi numarası alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				anaismerkod.Focus();
				return false;
			}

			if (string.IsNullOrEmpty(anaismertip.Text.Trim()))
			{
				MessageBox.Show("Ana iş merkezi tipi alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				anaismertip.Focus();
				return false;
			}

			if (string.IsNullOrEmpty(comboBoxOprCode.Text.Trim()))
			{
				MessageBox.Show("Operasyon kodu alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				comboBoxOprCode.Focus();
				return false;
			}

			if (string.IsNullOrEmpty(comboBoxMaliMerk.Text.Trim()))
			{
				MessageBox.Show("Maliyet merkezi tipi alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				comboBoxMaliMerk.Focus();
				return false;
			}

			if (string.IsNullOrEmpty(maliyMerkTxtBox.Text.Trim()))
			{
				MessageBox.Show("Maliyet merkezi kodu alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				maliyMerkTxtBox.Focus();
				return false;
			}

			if (string.IsNullOrEmpty(ismerkKATxtBox.Text.Trim()))
			{
				MessageBox.Show("İş merkezi açıklama alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				ismerkKATxtBox.Focus();
				return false;
			}

			if (string.IsNullOrEmpty(ismerkUAtextBox.Text.Trim()))
			{
				MessageBox.Show("İş merkezi açıklama 2 alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				ismerkUAtextBox.Focus();
				return false;
			}

			if (string.IsNullOrEmpty(dilBox.Text.Trim()))
			{
				MessageBox.Show("Dil alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				dilBox.Focus();
				return false;
			}

			// Tarihlerin geçerli olduğunu kontrol et
			if (dateTimeBas.Value >= dateTimeBit.Value)
			{
				MessageBox.Show("Geçerli başlangıç tarihi, bitiş tarihinden sonra olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				dateTimeBas.Focus();
				return false;
			}

			// Mantıksal değerlerin doğru olduğunu kontrol et
			if (!checkboxpas.Checked && !deletedlbl.Checked)
			{
				MessageBox.Show("İş merkezi durumu (aktif/pasif) belirtilmelidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				checkboxpas.Focus();
				return false;
			}

			// Günlük çalışma saati kontrolü
			if (string.IsNullOrEmpty(textBoxGunlukCal.Text.Trim()))
			{
				MessageBox.Show("Günlük çalışma saati alanı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				textBoxGunlukCal.Focus();
				return false;
			}

			return true;
		}

		private void saveBut_Click_1(object sender, EventArgs e)
		{
			// Alanları kontrol et
			if (!ValidateFields())
			{
				return;
			}

			// Veritabanına kaydetme işlemleri
			string Firma = firmbox.Text.Trim();
			string IsMerkeziTipi = comboBoxIsMerTip.Text.Trim();
			string IsMerkeziNumarasi = ismerkodtxtBox.Text.Trim();
			DateTime GecerliBaslangic = dateTimeBas.Value;
			DateTime GecerliBitis = dateTimeBit.Value;
			string AnaIsMerkeziTipi = anaismertip.Text.Trim();
			string AnaIsMerkeziNumarasi = anaismerkod.Text.Trim();
			string OperasyonKodu = comboBoxOprCode.Text.Trim();
			string MaliMerTip = comboBoxMaliMerk.Text.Trim();
			string MaliMerKod = maliyMerkTxtBox.Text.Trim();
			string IMKA = ismerkKATxtBox.Text.Trim();
			string IMUA = ismerkUAtextBox.Text.Trim();
			string Dil = dilBox.Text.Trim();
			bool IsDeleted = deletedlbl.Checked;
			bool IsPassive = checkboxpas.Checked;
			string Worktime = textBoxGunlukCal.Text.Trim();

			try
			{
				using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
				{
					con.Open();

					// BSMGRTRTWCMHEAD Tablosuna Ekleme
					string query1 = @"
                INSERT INTO BSMGRTRTWCMHEAD (
                    COMCODE, WCMDOCTYPE, WCMDOCNUM, CCMDOCTYPE, CCMDOCNUM, 
                    WCMDOCFROM, WCMDOCUNTIL, MAINWCMDOCTYPE, MAINWCMDOCNUM, 
                    WORKTIME, ISDELETED, ISPASSIVE)
                VALUES (
                    @COMCODE, @WCMDOCTYPE, @WCMDOCNUM, @CCMDOCTYPE, @CCMDOCNUM, 
                    @WCMDOCFROM, @WCMDOCUNTIL, @MAINWCMDOCTYPE, @MAINWCMDOCNUM, 
                    @WORKTIME, @ISDELETED, @ISPASSIVE);";

					using (SqlCommand command1 = new SqlCommand(query1, con))
					{
						command1.Parameters.AddWithValue("@COMCODE", firmbox.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@WCMDOCTYPE", comboBoxIsMerTip.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@WCMDOCNUM", ismerkodtxtBox.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@CCMDOCTYPE", comboBoxMaliMerk.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@CCMDOCNUM", maliyMerkTxtBox.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@WCMDOCFROM", DateTime.TryParse(dateTimeBas.Text, out DateTime fromDate) ? fromDate : (object)DBNull.Value);
						command1.Parameters.AddWithValue("@WCMDOCUNTIL", DateTime.TryParse(dateTimeBit.Text, out DateTime untilDate) ? untilDate : (object)DBNull.Value);
						command1.Parameters.AddWithValue("@MAINWCMDOCTYPE", anaismertip.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@MAINWCMDOCNUM", anaismerkod.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@WORKTIME", textBoxGunlukCal.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@ISDELETED", checkboxpas.Checked ? 1 : 0);
						command1.Parameters.AddWithValue("@ISPASSIVE", deletedlbl.Checked ? 1 : 0);

						command1.ExecuteNonQuery();
					}

					// BSMGRTRTWCMTEXT Tablosuna Ekleme
					string query2 = @"
                INSERT INTO BSMGRTRTWCMTEXT (WCMDOCNUM, WCMSTEXT, WCMLTEXT)
                VALUES (@WCMDOCNUM, @WCMSTEXT, @WCMLTEXT);";

					using (SqlCommand command2 = new SqlCommand(query2, con))
					{
						command2.Parameters.AddWithValue("@WCMDOCNUM", ismerkodtxtBox.Text ?? (object)DBNull.Value);
						command2.Parameters.AddWithValue("@WCMSTEXT", ismerkKATxtBox.Text ?? (object)DBNull.Value);
						command2.Parameters.AddWithValue("@WCMLTEXT", ismerkUAtextBox.Text ?? (object)DBNull.Value);

						command2.ExecuteNonQuery();
					}

					// BSMGRTRTWCMOPR Tablosuna Ekleme
					string query3 = @"
                INSERT INTO BSMGRTRTWCMOPR (WCMDOCNUM, OPRDOCTYPE)
                VALUES (@WCMDOCNUM, @OPRDOCTYPE);";

					using (SqlCommand command3 = new SqlCommand(query3, con))
					{
						command3.Parameters.AddWithValue("@WCMDOCNUM", ismerkodtxtBox.Text ?? (object)DBNull.Value);
						command3.Parameters.AddWithValue("@OPRDOCTYPE", comboBoxOprCode.Text ?? (object)DBNull.Value);

						command3.ExecuteNonQuery();
					}

					MessageBox.Show("Yeni kayıt başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

	}
}
