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

            firmbox.Leave += (s, e) => ValidateAndAddData(firmbox, "COMCODE", "BSMGRTRTGEN001");
            comboBoxIsMerTip.Leave += (s, e) => ValidateAndAddData(comboBoxIsMerTip, "DOCTYPE", "BSMGRTRTWCM001");
            dilBox.Leave += (s, e) => ValidateAndAddData(dilBox, "LANCODE", "BSMGRTRTGEN002");
            comboBoxOprCode.Leave += (s, e) => ValidateAndAddData(comboBoxOprCode, "DOCTYPE", "BSMGRTRTOPR001");
            comboBoxMaliMerk.Leave += (s, e) => ValidateAndAddData(comboBoxMaliMerk, "DOCTYPE", "BSMGRTRTCCM001");
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

                    // Seçilen değeri doğru şekilde ata
                    if (comboBox.SelectedValue == null && dt.Rows.Count > 0)
                    {
                        comboBox.SelectedValue = dt.Rows[0][columnName]; // Varsayılan değeri ilk satır olarak ayarlayın
                    }
                }
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
            // 1. Aşama: Alanları kontrol et
            if (!ValidateFields())
            {
                return;
            }

            // Veritabanına kaydetme işlemleri için veri ataması
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
            string Worktime = textBoxGunlukCal.Text.Trim();
            // 3. Aşama: Durum kontrolü (checkbox pasif ve silme durumları)
            bool IsDeleted = deletedlbl.Checked; ;  // Eğer işaretli değilse, false olacak
            bool IsPassive = checkboxpas.Checked; // Eğer işaretli değilse, false olacak

            // 2. Aşama: Veritabanında veri kontrolü
            if (!CheckIfDataExists("BSMGRTRTGEN001", "COMCODE", Firma) ||
                !CheckIfDataExists("BSMGRTRTWCM001", "DOCTYPE", IsMerkeziTipi) ||
                !CheckIfDataExists("BSMGRTRTGEN002", "LANCODE", Dil) ||
                !CheckIfDataExists("BSMGRTRTOPR001", "DOCTYPE", OperasyonKodu) ||
                !CheckIfDataExists("BSMGRTRTCCM001", "DOCTYPE", MaliMerTip))
            {
                return;
            }



            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    con.Open();

                    // 4. Aşama: Veritabanı güncelleme (Veri ekleme)
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
                        command1.Parameters.AddWithValue("@COMCODE", Firma);
                        command1.Parameters.AddWithValue("@WCMDOCTYPE", IsMerkeziTipi);
                        command1.Parameters.AddWithValue("@WCMDOCNUM", IsMerkeziNumarasi);
                        command1.Parameters.AddWithValue("@CCMDOCTYPE", MaliMerTip);
                        command1.Parameters.AddWithValue("@CCMDOCNUM", MaliMerKod);
                        command1.Parameters.AddWithValue("@WCMDOCFROM", GecerliBaslangic);
                        command1.Parameters.AddWithValue("@WCMDOCUNTIL", GecerliBitis);
                        command1.Parameters.AddWithValue("@MAINWCMDOCTYPE", AnaIsMerkeziTipi);
                        command1.Parameters.AddWithValue("@MAINWCMDOCNUM", AnaIsMerkeziNumarasi);
                        command1.Parameters.AddWithValue("@WORKTIME", Worktime);
                        command1.Parameters.AddWithValue("@ISDELETED", IsDeleted ? 1 : 0);  // Eğer işaretli değilse 0 olacak
                        command1.Parameters.AddWithValue("@ISPASSIVE", IsPassive ? 1 : 0);  // Eğer işaretli değilse 0 olacak

                        command1.ExecuteNonQuery();
                    }

                    // Diğer tabloları güncelleme
                    string query2 = @"
                INSERT INTO BSMGRTRTWCMTEXT (COMCODE, WCMDOCTYPE, WCMDOCNUM, WCMDOCFROM, WCMDOCUNTIL, LANCODE, WCMSTEXT, WCMLTEXT)
                VALUES (@COMCODE, @WCMDOCTYPE, @WCMDOCNUM, @WCMDOCFROM, @WCMDOCUNTIL, @LANCODE, @WCMSTEXT, @WCMLTEXT);";

                    using (SqlCommand command2 = new SqlCommand(query2, con))
                    {
                        command2.Parameters.AddWithValue("@COMCODE", Firma);
                        command2.Parameters.AddWithValue("@WCMDOCTYPE", IsMerkeziTipi);
                        command2.Parameters.AddWithValue("@WCMDOCNUM", IsMerkeziNumarasi);
                        command2.Parameters.AddWithValue("@WCMDOCFROM", GecerliBaslangic);
                        command2.Parameters.AddWithValue("@WCMDOCUNTIL", GecerliBitis);
                        command2.Parameters.AddWithValue("@LANCODE", Dil);
                        command2.Parameters.AddWithValue("@WCMSTEXT", IMKA);
                        command2.Parameters.AddWithValue("@WCMLTEXT", IMUA);

                        command2.ExecuteNonQuery();
                    }

                    string query3 = @"
                INSERT INTO BSMGRTRTWCMOPR (COMCODE, WCMDOCTYPE, WCMDOCNUM, WCMDOCFROM, WCMDOCUNTIL, OPRDOCTYPE)
                VALUES (@COMCODE, @WCMDOCTYPE, @WCMDOCNUM, @WCMDOCFROM, @WCMDOCUNTIL, @OPRDOCTYPE);";

                    using (SqlCommand command3 = new SqlCommand(query3, con))
                    {
                        command3.Parameters.AddWithValue("@COMCODE", Firma);
                        command3.Parameters.AddWithValue("@WCMDOCTYPE", IsMerkeziTipi);
                        command3.Parameters.AddWithValue("@WCMDOCNUM", IsMerkeziNumarasi);
                        command3.Parameters.AddWithValue("@WCMDOCFROM", GecerliBaslangic);
                        command3.Parameters.AddWithValue("@WCMDOCUNTIL", GecerliBitis);
                        command3.Parameters.AddWithValue("@OPRDOCTYPE", OperasyonKodu);

                        command3.ExecuteNonQuery();
                    }

                    MessageBox.Show("Yeni kayıt başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // 4. Aşama: Hata yönetimi
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Veritabanında veri kontrolü (aşama 2)
        private bool CheckIfDataExists(string tableName, string columnName, string value)
        {
            string query = $@"
        SELECT COUNT(*) 
        FROM {tableName} 
        WHERE {columnName} = @userInput";

            if (string.IsNullOrEmpty(value)) return false;

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    con.Open();
                    using (SqlCommand checkCmd = new SqlCommand(query, con))
                    {
                        checkCmd.Parameters.AddWithValue("@userInput", value);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count == 0)
                        {
                            MessageBox.Show($"{columnName} '{value}' tablodaki verilerle uyuşmuyor.");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
                return false;
            }
            return true;
        }


    }
}
