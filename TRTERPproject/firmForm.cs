using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class firmForm : Form
    {

        SqlDataReader reader;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        public firmForm()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadComboBoxData();

            // ComboBox Leave eventlerini bağla
            comboBoxCity.Leave += (s, e) => ValidateComboBox(comboBoxCity, "CITYCODE", "BSMGRTRTGEN004");
            comboBoxCountry.Leave += (s, e) => ValidateComboBox(comboBoxCountry, "COUNTRYCODE", "BSMGRTRTGEN003");
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

                    // Varsayılan seçim ilk satır olarak ayarlanır
                    if (comboBox.SelectedValue == null && dt.Rows.Count > 0)
                    {
                        comboBox.SelectedValue = dt.Rows[0][columnName];
                    }
                }
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    con.Open();

                    // ComboBox'ları doldur
                    LoadComboBox(comboBoxCity, "SELECT DISTINCT CITYCODE FROM BSMGRTRTGEN004", "CITYCODE");
                    LoadComboBox(comboBoxCountry, "SELECT DISTINCT COUNTRYCODE FROM BSMGRTRTGEN003", "COUNTRYCODE");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}");
            }
        }

        private void ValidateComboBox(ComboBox comboBox, string columnName, string tableName)
        {
            string checkQuery = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = @userInput";

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

        private void btnGet_Click(object sender, EventArgs e)
        {

            string query = "SELECT \r\n            COMCODE AS 'Firma Kodu',\r\n            COMTEXT AS 'Firma Adı',\r\n            ADDRESS1 AS 'Adres 1',\r\n            ADDRESS2 AS 'Adres 2',\r\n            CITYCODE AS 'Şehir Kodu',\r\n            COUNTRYCODE AS 'Ülke Kodu'\r\n            FROM \r\n            BSMGRTRTGEN001";
            con = new SqlConnection(ConnectionHelper.ConnectionString);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;


            try
            {

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);


                DataSet ds = new DataSet();

                da.Fill(ds);

                // DataGridView'e veri bağla
                firmFormDataGridView.DataSource = ds.Tables[0];  // Veritabanından çekilen ilk tabloyu DataGridView'e bağla
            }
            catch (Exception ex)
            {
                // Hata durumunda mesaj göster
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                con.Close();
            }



        }






        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (firmFormDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = firmFormDataGridView.SelectedRows[0];

                // Yeni bir edit form oluştur ve seçilen veriyi aktar
                firmFormEdit FirmFormEdit = new firmFormEdit();

                // Formdaki alanlara DataGridView'deki değerleri aktar
                FirmFormEdit.firmCode = selectedRow.Cells["Firma Kodu"].Value != DBNull.Value
                    ? selectedRow.Cells["Firma Kodu"].Value.ToString()
                    : string.Empty;

                FirmFormEdit.firmName = selectedRow.Cells["Firma Adı"].Value != DBNull.Value
                    ? selectedRow.Cells["Firma Adı"].Value.ToString()
                    : string.Empty;

                FirmFormEdit.Address1 = selectedRow.Cells["Adres 1"].Value != DBNull.Value
                 ? selectedRow.Cells["Adres 1"].Value.ToString()
                 : string.Empty;

                FirmFormEdit.Address2 = selectedRow.Cells["Adres 2"].Value != DBNull.Value
                 ? selectedRow.Cells["Adres 2"].Value.ToString()
                 : string.Empty;

                FirmFormEdit.cityCode = selectedRow.Cells["Şehir Kodu"].Value != DBNull.Value
                 ? selectedRow.Cells["Şehir Kodu"].Value.ToString()
                 : string.Empty;

                FirmFormEdit.countryCode = selectedRow.Cells["Ülke Kodu"].Value != DBNull.Value
                 ? selectedRow.Cells["Ülke Kodu"].Value.ToString()
                 : string.Empty;


                FirmFormEdit.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için bir satır seçin.");
            }
        
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            string firmCode = firmCodeTextBox.Text.Trim();
            string firmName = firmNameTextBox.Text.Trim();
            string address1 = address1TextBox.Text.Trim();
            string address2 = address2TextBox.Text.Trim();
            string cityCode = comboBoxCity.Text.Trim();
            string countryCode = comboBoxCountry.Text.Trim();

            // 1. Veri Kontrolü
            if (string.IsNullOrEmpty(firmCode) || string.IsNullOrEmpty(firmName) || string.IsNullOrEmpty(address1) || string.IsNullOrEmpty(cityCode) || string.IsNullOrEmpty(countryCode))
            {
                MessageBox.Show("Lütfen gerekli tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    // 2. COUNTRYCODE Kontrolü
                    string checkCountryCodeQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN001 WHERE COMCODE = @COMCODE";
                    using (cmd = new SqlCommand(checkCountryCodeQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", firmCode);

                        int countryCodeExists = (int)cmd.ExecuteScalar();

                        if (countryCodeExists > 0)
                        {
                            // COUNTRYCODE zaten mevcut
                            MessageBox.Show("Bu Firma Kodu zaten mevcut. Lütfen başka bir Firma Kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }





                    // 4. Ekleme İşlemi
                    string insertQuery = "INSERT INTO BSMGRTRTGEN001 (COMCODE, COMTEXT, ADDRESS1, ADDRESS2, CITYCODE, COUNTRYCODE) VALUES (@COMCODE, @COMTEXT, @ADDRESS1, @ADDRESS2, @CITYCODE, @COUNTRYCODE)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", firmCode);
                        cmd.Parameters.AddWithValue("@COMTEXT", firmName);
                        cmd.Parameters.AddWithValue("@ADDRESS1", address1);
                        cmd.Parameters.AddWithValue("@ADDRESS2", address2);
                        cmd.Parameters.AddWithValue("@CITYCODE", cityCode);
                        cmd.Parameters.AddWithValue("@COUNTRYCODE", countryCode);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // TextBox'ları temizle
                            firmCodeTextBox.Clear();
                            firmNameTextBox.Clear();
                            address1TextBox.Clear();
                            address2TextBox.Clear();

                        }
                        else
                        {
                            MessageBox.Show("Kayıt eklenemedi. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            // Seçili satır var mı kontrolü
            if (firmFormDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = firmFormDataGridView.SelectedRows[0];
                string firmCode = selectedRow.Cells["Firma Kodu"].Value.ToString(); ;

                // Kullanıcıdan onay al
                DialogResult dialogResult = MessageBox.Show(
                    $"Firma Kodu {firmCode} olan veriyi silmek istediğinize emin misiniz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dialogResult != DialogResult.Yes)
                {
                    // Kullanıcı "Hayır" seçerse işlem iptal edilir
                    return;
                }

                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    try
                    {
                        con.Open();

                        // 2. Kayıt Kontrolü
                        string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN001 WHERE COMCODE = @COMCODE";
                        using (SqlCommand cmd = new SqlCommand(checkQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@COMCODE", firmCode);
                            int recordExists = (int)cmd.ExecuteScalar();

                            if (recordExists == 0)
                            {
                                // Kayıt bulunamadı
                                MessageBox.Show("Belirtilen Firma Kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // 3. Silme İşlemi
                        string deleteQuery = "DELETE FROM BSMGRTRTGEN001 WHERE COMCODE = @COMCODE";
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@COMCODE", firmCode);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Seçilen satırı DataGridView'den sil
                                firmFormDataGridView.Rows.Remove(selectedRow);

                                // TextBox'ı temizle
                                firmCodeTextBox.Clear();
                            }
                            else
                            {
                                MessageBox.Show("Kayıt silinemedi. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void firmForm_Load(object sender, EventArgs e)
        {
            // DataGridView hücre stilini ayarla
            firmFormDataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            firmFormDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            firmFormDataGridView.DefaultCellStyle.BackColor = Color.White;
            firmFormDataGridView.DefaultCellStyle.SelectionBackColor = Color.Blue;
            firmFormDataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

            // Satır başlıklarının stilini ayarla
            firmFormDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
            firmFormDataGridView.RowHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            // Başlık stilini ayarla
            firmFormDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            firmFormDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            firmFormDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            firmFormDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT 
            COMCODE AS 'Firma Kodu',
            COMTEXT AS 'Firma Adı',
            ADDRESS1 AS 'Adres 1',
            ADDRESS2 AS 'Adres 2',
            CITYCODE AS 'Şehir Kodu',
            COUNTRYCODE AS 'Ülke Kodu'
            FROM 
            BSMGRTRTGEN001";

            // Filtreleme koşullarını tutacak liste
            List<string> filters = new List<string>();

            // Firma Kodu filtresi
            if (!string.IsNullOrEmpty(firmCodeTextBox.Text))
            {
                filters.Add("COMCODE LIKE @COMCODE");
            }

            // Firma Adı filtresi
            if (!string.IsNullOrEmpty(firmNameTextBox.Text))
            {
                filters.Add("COMTEXT LIKE @COMTEXT");
            }

            // Şehir Kodu filtresi
            if (!string.IsNullOrEmpty(comboBoxCity.Text))
            {
                filters.Add("CITYCODE = @CITYCODE");
            }

            // Ülke Kodu filtresi
            if (!string.IsNullOrEmpty(comboBoxCountry.Text))
            {
                filters.Add("COUNTRYCODE = @COUNTRYCODE");
            }

            // Filtreleri sorguya ekle
            if (filters.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", filters);
            }

            // SQL bağlantısı ve komut
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Parametreleri ekle
                    if (!string.IsNullOrEmpty(firmCodeTextBox.Text))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", $"{firmCodeTextBox.Text}%");
                    }

                    if (!string.IsNullOrEmpty(firmNameTextBox.Text))
                    {
                        cmd.Parameters.AddWithValue("@COMTEXT", $"{firmNameTextBox.Text}%");
                    }

                    if (!string.IsNullOrEmpty(comboBoxCity.Text))
                    {
                        cmd.Parameters.AddWithValue("@CITYCODE", comboBoxCity.Text);
                    }

                    if (!string.IsNullOrEmpty(comboBoxCountry.Text))
                    {
                        cmd.Parameters.AddWithValue("@COUNTRYCODE", comboBoxCountry.Text);
                    }

                    try
                    {
                        con.Open();

                        // Verileri çekmek için DataAdapter kullan
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // DataGridView'e verileri bağla
                        firmFormDataGridView.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }

}
