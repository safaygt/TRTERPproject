using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using TRTERPproject.Helpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TRTERPproject
{

    public partial class MaliyetKartAna : Form
    {
        private readonly SqlConnection _connection = new SqlConnection(ConnectionHelper.ConnectionString);
        private SqlConnection con;
        private SqlCommand cmd;

        public MaliyetKartAna()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadComboBoxData();

            // ComboBox leave eventlerini bağla
            firmComboBox.Leave += (s, e) => ValidateAndAddData(firmComboBox, "COMCODE");
            comboBoxMalMerTip.Leave += (s, e) => ValidateAndAddData(comboBoxMalMerTip, "CCMDOCTYPE");
        }

        private void LoadComboBoxData()
        {
            try
            {
                _connection.Open();

                // Firma verilerini doldur
                PopulateComboBox("SELECT DISTINCT COMCODE FROM BSMGRTRTCCMHEAD", firmComboBox, "COMCODE");

                // Maliyet Merkezi Tipi verilerini doldur
                PopulateComboBox("SELECT DISTINCT CCMDOCTYPE FROM BSMGRTRTCCMHEAD", comboBoxMalMerTip, "CCMDOCTYPE");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }
        }

        private void PopulateComboBox(string query, System.Windows.Forms.ComboBox comboBox, string displayMember)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = displayMember;
            comboBox.DropDownStyle = ComboBoxStyle.DropDown;
        }

        private void ValidateAndAddData(System.Windows.Forms.ComboBox comboBox, string columnName)
        {
            if (string.IsNullOrEmpty(comboBox.Text)) return;

            string query = $"SELECT COUNT(*) FROM BSMGRTRTCCMHEAD WHERE {columnName} = @userInput";
            try
            {
                _connection.Open();

                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@userInput", comboBox.Text);
                    int count = (int)command.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show($"{columnName} '{comboBox.Text}' tablodaki verilerle uyuşmuyor.");
                        comboBox.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }
        }

        private void getBut_Click(object sender, EventArgs e)
        {
            // Temel SQL sorgusu
            string query = @"SELECT 
                                COMCODE AS 'Firma', 
                                CCMDOCTYPE AS 'Maliyet Merkezi Tipi', 
                                CCMDOCNUM AS 'Maliyet Merkezi Numarası', 
                                CCMDOCFROM AS 'Geçerlilik Başlangıç',
                                CCMDOCUNTIL AS 'Geçerlilik Bitiş',
                                MAINCCMDOCTYPE AS 'Ana Maliyet Merkezi Tipi', 
                                MAINCCMDOCNUM AS 'Ana Maliyet Merkezi Numarası', 
                                ISDELETED AS 'Silindi mi?',
                                ISPASSIVE AS 'Pasif mi?'
                            FROM BSMGRTRTCCMHEAD";

            // Filtreleme koşulları
            List<string> filters = new List<string>();

            // Firma filtresi
            if (!string.IsNullOrEmpty(firmComboBox.Text))
            {
                filters.Add("COMCODE = @COMCODE");
            }

            // Maliyet Merkezi Tipi filtresi
            if (!string.IsNullOrEmpty(comboBoxMalMerTip.Text))
            {
                filters.Add("CCMDOCTYPE = @CCMDOCTYPE");
            }

            // Maliyet Merkezi Numarası filtresi
            if (!string.IsNullOrEmpty(malNotxtBox.Text))
            {
                filters.Add("CCMDOCNUM = @CCMDOCNUM");
            }

            // Ana Maliyet Merkezi Tipi filtresi
            if (!string.IsNullOrEmpty(maliyTxtBox.Text))
            {
                filters.Add("MAINCCMDOCNUM = @MAINCCMDOCNUM");
            }


            // Tarih aralığı filtresi
            if (dateTimePickerBaslangic.Value.Date != DateTime.MinValue.Date && dateTimePickerBitis.Value.Date != DateTime.MinValue.Date)
            {
                filters.Add("CCMDOCFROM >= @CCMDOCFROM AND CCMDOCUNTIL <= @CCMDOCUNTIL");
            }

            // Pasiflik kontrolü
            if (checkboxpas.Checked)
            {
                filters.Add("ISPASSIVE = 1");
            }

            // Silinmişlik kontrolü
            if (deletedlbl.Checked)
            {
                filters.Add("ISDELETED = 1");
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
                if (!string.IsNullOrEmpty(firmComboBox.Text))
                {
                    cmd.Parameters.AddWithValue("@COMCODE", firmComboBox.Text);
                }

                if (!string.IsNullOrEmpty(comboBoxMalMerTip.Text))
                {
                    cmd.Parameters.AddWithValue("@CCMDOCTYPE", comboBoxMalMerTip.Text);
                }

                if (!string.IsNullOrEmpty(malNotxtBox.Text))
                {
                    cmd.Parameters.AddWithValue("@CCMDOCNUM", malNotxtBox.Text);
                }

                if (!string.IsNullOrEmpty(maliyTxtBox.Text))
                {
                    cmd.Parameters.AddWithValue("@MAINCCMDOCNUM", maliyTxtBox.Text);
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
                    maliyetdata.DataSource = dt.Tables[0];

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
                    _connection.Close();
                }
            }
        }

        private void addBut_Click(object sender, EventArgs e)
        {
            string comCode = firmComboBox.Text.Trim();
            string malMerType = comboBoxMalMerTip.Text.Trim();
            string malMerCode = malNotxtBox.Text.Trim();
            string startDate = dateTimePickerBaslangic.Text.Trim();
            string lastDate = dateTimePickerBitis.Text.Trim();
            string malDes = maliyTxtBox.Text.Trim();
            int isMainUnit = checkboxpas.Checked ? 1 : 0;
            int isDelUnit = deletedlbl.Checked ? 1 : 0;

            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(malMerType) || string.IsNullOrEmpty(malMerCode) || string.IsNullOrEmpty(malDes) ||
                string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(lastDate))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!DateTime.TryParse(startDate, out DateTime parsedStartDate) || !DateTime.TryParse(lastDate, out DateTime parsedLastDate))
                {
                    MessageBox.Show("Tarih formatı hatalı. Lütfen yyyy-MM-dd formatında giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTCCMHEAD WHERE CCMDOCNUM = @CCMDOCNUM";
                using (SqlCommand command = new SqlCommand(checkQuery, _connection))
                {
                    command.Parameters.AddWithValue("@CCMDOCNUM", malMerCode);
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Bu Birim Kodu zaten mevcut. Lütfen başka bir Birim Kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string insertQuery = @"INSERT INTO BSMGRTRTCCMHEAD 
                                       (COMCODE, CCMDOCTYPE, CCMDOCNUM, CCMDOCFROM, CCMDOCUNTIL, MAINCCMDOCTYPE, MAINCCMDOCNUM, ISDELETED, ISPASSIVE) 
                                       VALUES (@COMCODE, @CCMDOCTYPE, @CCMDOCNUM, @CCMDOCFROM, @CCMDOCUNTIL, @MAINCCMDOCTYPE, @MAINCCMDOCNUM, @ISDELETED, @ISPASSIVE)";

                using (SqlCommand command = new SqlCommand(insertQuery, _connection))
                {
                    command.Parameters.AddWithValue("@COMCODE", comCode);
                    command.Parameters.AddWithValue("@CCMDOCTYPE", malMerType);
                    command.Parameters.AddWithValue("@CCMDOCNUM", malMerCode);
                    command.Parameters.AddWithValue("@CCMDOCFROM", parsedStartDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@CCMDOCUNTIL", parsedLastDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@MAINCCMDOCTYPE", malDes);
                    command.Parameters.AddWithValue("@MAINCCMDOCNUM", malDes);
                    command.Parameters.AddWithValue("@ISDELETED", isDelUnit);
                    command.Parameters.AddWithValue("@ISPASSIVE", isMainUnit);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetFormFields();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt eklenemedi. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }
        }

        private void ResetFormFields()
        {
            firmComboBox.SelectedIndex = -1;
            comboBoxMalMerTip.SelectedIndex = -1;
            malNotxtBox.Clear();
            maliyTxtBox.Clear();
            dateTimePickerBaslangic.Checked = false;
            dateTimePickerBitis.Checked = false;
            checkboxpas.Checked = false;
            deletedlbl.Checked = false;
        }

        private void DelBut_Click(object sender, EventArgs e)
        {
            string malMerCode = malNotxtBox.Text.Trim();

            if (string.IsNullOrEmpty(malMerCode))
            {
                MessageBox.Show("Lütfen bir Maliyet Merkezi Numarası giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kullanıcıdan onay alıyoruz
            DialogResult result = MessageBox.Show(
                "Bu kaydı silmek istediğinizden emin misiniz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                // Kullanıcı "Hayır" seçtiyse işlemi iptal ediyoruz
                MessageBox.Show("Silme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                _connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTCCMHEAD WHERE CCMDOCNUM = @CCMDOCNUM";
                using (SqlCommand command = new SqlCommand(checkQuery, _connection))
                {
                    command.Parameters.AddWithValue("@CCMDOCNUM", malMerCode);

                    int recordExists = (int)command.ExecuteScalar();

                    if (recordExists == 0)
                    {
                        MessageBox.Show("Belirtilen Maliyet Merkezi Numarası için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string deleteQuery = "DELETE FROM BSMGRTRTCCMHEAD WHERE CCMDOCNUM = @CCMDOCNUM";
                using (SqlCommand command = new SqlCommand(deleteQuery, _connection))
                {
                    command.Parameters.AddWithValue("@CCMDOCNUM", malMerCode);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetFormFields();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt silinemedi. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }
        }


        private void firmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void getAll_Click(object sender, EventArgs e)
        {
            string query = @"SELECT 
                                COMCODE AS 'Firma', 
                                CCMDOCTYPE AS 'Maliyet Merkezi Tipi', 
                                CCMDOCNUM AS 'Maliyet Merkezi Numarası', 
                                CCMDOCFROM AS 'Geçerlilik Başlangıç',
                                CCMDOCUNTIL AS 'Geçerlilik Bitiş',
                                MAINCCMDOCTYPE AS 'Ana Maliyet Merkezi Tipi', 
                                MAINCCMDOCNUM AS 'Ana Maliyet Merkezi Numarası', 
                                ISDELETED AS 'Silindi mi?',
                                ISPASSIVE AS 'Pasif mi?'
                            FROM BSMGRTRTCCMHEAD";

            try
            {
                _connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                maliyetdata.DataSource = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            finally
            {
                _connection.Close();
            }
        }

        private void ClearFormFields()
        {
            firmComboBox.Text = string.Empty;
            comboBoxMalMerTip.Text = string.Empty;
            maliyTxtBox.Text = string.Empty;
            malNotxtBox.Text = string.Empty;
            dateTimePickerBaslangic.Value = DateTime.Now;
            dateTimePickerBitis.Value = DateTime.Now;
            checkboxpas.Checked = false;
            deletedlbl.Checked = false;
        }

        private void duzBut_Click(object sender, EventArgs e)
        {
            string malCarNum = malNotxtBox.Text; // Ürün Ağacı Numarası alınır

            if (string.IsNullOrEmpty(malCarNum))
            {
                MessageBox.Show("Lütfen bir Maliyet Merkezi Kodu giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM BSMGRTRTCCMHEAD WHERE CCMDOCNUM = @CCMDOCNUM";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CCMDOCNUM", malCarNum);

                try
                {
                    con.Open();
                    int recordExists = (int)cmd.ExecuteScalar();

                    if (recordExists > 0)
                    {

                        MaliyetKartAnaEdit MaliyetKartForm = new MaliyetKartAnaEdit(malCarNum);
                        MaliyetKartForm.Show();
                    }
                    else
                    {
                        // Kayıt bulunamadı
                        MessageBox.Show("Belirtilen Maliyet Merkezi Kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }

}
