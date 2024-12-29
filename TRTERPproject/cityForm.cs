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
    public partial class cityForm : Form
    {

        SqlDataReader reader;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        public cityForm()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadComboBoxData();

            // ComboBox Leave eventlerini bağla
            comboBoxFirmCode.Leave += (s, e) => ValidateComboBox(comboBoxFirmCode, "COMCODE", "BSMGRTRTGEN001");
            comboBoxFirmCode.Leave += (s, e) => ValidateComboBox(comboBoxCountryCode, "COUNTRYCODE", "BSMGRTRTGEN003");
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
                    LoadComboBox(comboBoxFirmCode, "SELECT DISTINCT COMCODE FROM BSMGRTRTGEN001", "COMCODE");
                    LoadComboBox(comboBoxCountryCode, "SELECT DISTINCT COUNTRYCODE FROM BSMGRTRTGEN003", "COUNTRYCODE");
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
            string query = "SELECT COMCODE AS 'Firma Kodu', CITYCODE AS 'Şehir Kodu', CITYTEXT AS 'Şehir Adı', COUNTRYCODE AS 'Ülke Kodu' FROM BSMGRTRTGEN004";
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
                cityDataGridView.DataSource = ds.Tables[0];  // Veritabanından çekilen ilk tabloyu DataGridView'e bağla
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
            if (cityDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = cityDataGridView.SelectedRows[0];

                // Seçilen satırdaki tüm hücrelerin boş olup olmadığını kontrol et
                bool isRowEmpty = true;
                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        isRowEmpty = false;
                        break;
                    }
                }

                if (isRowEmpty)
                {
                    MessageBox.Show("Boş bir satır seçtiniz. Lütfen dolu bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Yeni bir edit form oluştur ve seçilen veriyi aktar
                cityFormEdit CityFormEdit = new cityFormEdit();

                CityFormEdit.comCode = selectedRow.Cells["Firma Kodu"].Value != DBNull.Value
                    ? selectedRow.Cells["Firma Kodu"].Value.ToString()
                    : string.Empty;

                CityFormEdit.cityCode = selectedRow.Cells["Şehir Kodu"].Value != DBNull.Value
                    ? selectedRow.Cells["Şehir Kodu"].Value.ToString()
                    : string.Empty;

                CityFormEdit.cityName = selectedRow.Cells["Şehir Adı"].Value != DBNull.Value
                    ? selectedRow.Cells["Şehir Adı"].Value.ToString()
                    : string.Empty;

                CityFormEdit.countryCode = selectedRow.Cells["Ülke Kodu"].Value != DBNull.Value
                    ? selectedRow.Cells["Ülke Kodu"].Value.ToString()
                    : string.Empty;

                CityFormEdit.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string comCode = comboBoxFirmCode.Text.Trim();
            string cityCode = cityCodeTextBox.Text.Trim();
            string cityName = cityNameTextBox.Text.Trim();
            string countryCode = comboBoxCountryCode.Text.Trim();

            // 1. Veri Kontrolü
            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(countryCode) || string.IsNullOrEmpty(cityName) || string.IsNullOrEmpty(cityCode))
            {
                MessageBox.Show("Lütfen gerekli tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    // 2. CITYCODE Kontrolü
                    string checkCityCodeQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN004 WHERE CITYCODE = @CITYCODE";
                    using (cmd = new SqlCommand(checkCityCodeQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@CITYCODE", cityCode);

                        int cityCodeExists = (int)cmd.ExecuteScalar();

                        if (cityCodeExists > 0)
                        {
                            MessageBox.Show("Bu Şehir Kodu zaten mevcut. Lütfen başka bir Şehir Kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }



                    // 4. Ekleme İşlemi
                    string insertQuery = "INSERT INTO BSMGRTRTGEN004 (COMCODE, CITYCODE, CITYTEXT, COUNTRYCODE) VALUES (@COMCODE, @CITYCODE, @CITYTEXT, @COUNTRYCODE)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd.Parameters.AddWithValue("@CITYCODE", cityCode);
                        cmd.Parameters.AddWithValue("@CITYTEXT", cityName);
                        cmd.Parameters.AddWithValue("@COUNTRYCODE", countryCode);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // TextBox'ları temizle

                            cityCodeTextBox.Clear();
                            cityNameTextBox.Clear();

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
            if (cityDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = cityDataGridView.SelectedRows[0];
                string cityCode = selectedRow.Cells["Şehir Kodu"].Value?.ToString();

                if (string.IsNullOrEmpty(cityCode))
                {
                    MessageBox.Show("Geçersiz bir kayıt seçildi. Lütfen geçerli bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kullanıcıdan onay al
                DialogResult dialogResult = MessageBox.Show(
                    $"Şehir Kodu {cityCode} olan veriyi silmek istediğinize emin misiniz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dialogResult != DialogResult.Yes)
                {
                    return;
                }

                using (con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    try
                    {
                        con.Open();

                        // 2. Kayıt Kontrolü
                        string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN004 WHERE CITYCODE = @CITYCODE";
                        using (cmd = new SqlCommand(checkQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@CITYCODE", cityCode);

                            int recordExists = (int)cmd.ExecuteScalar();

                            if (recordExists == 0)
                            {
                                MessageBox.Show("Belirtilen Şehir Kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // 3. Silme İşlemi
                        string deleteQuery = "DELETE FROM BSMGRTRTGEN004 WHERE CITYCODE = @CITYCODE";
                        using (cmd = new SqlCommand(deleteQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@CITYCODE", cityCode);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // TextBox'ı temizle
                                cityCodeTextBox.Clear();
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

        private void btnFiltreliGetir_Click(object sender, EventArgs e)
        {
            string query = @"
            SELECT 
            COMCODE AS 'Firma Kodu',
            CITYCODE AS 'Şehir Kodu',
            CITYTEXT AS 'Şehir Adı',
            COUNTRYCODE AS 'Ülke Kodu'
            FROM 
            BSMGRTRTGEN004";

            // Filtreleme koşullarını tutacak liste
            List<string> filters = new List<string>();

            // Firma Kodu filtresi
            if (!string.IsNullOrEmpty(comboBoxFirmCode.Text))
            {
                filters.Add("COMCODE LIKE @COMCODE");
            }

            // Firma Adı filtresi
        

            // Şehir Kodu filtresi
            if (!string.IsNullOrEmpty(cityCodeTextBox.Text))
            {
                filters.Add("CITYCODE LIKE @CITYCODE");
            }

            if (!string.IsNullOrEmpty(cityNameTextBox.Text))
            {
                filters.Add("CITYTEXT LIKE @CITYTEXT");
            }

            if (!string.IsNullOrEmpty(comboBoxCountryCode.Text))
            {
                filters.Add("COUNTRYCODE LIKE @COUNTRYCODE");
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
                    if (!string.IsNullOrEmpty(comboBoxFirmCode.Text))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", $"{comboBoxFirmCode.Text}%");
                    }

                    if (!string.IsNullOrEmpty(cityCodeTextBox.Text))
                    {
                        cmd.Parameters.AddWithValue("@CITYCODE", $"{cityCodeTextBox.Text}%");
                    }

                    if (!string.IsNullOrEmpty(cityNameTextBox.Text))
                    {
                        cmd.Parameters.AddWithValue("@CITYTEXT", $"{cityNameTextBox.Text}%");
                    }



                    if (!string.IsNullOrEmpty(comboBoxCountryCode.Text))
                    {
                        cmd.Parameters.AddWithValue("@COUNTRYCODE", $"{comboBoxCountryCode.Text}%");
                    }



                    try
                    {
                        con.Open();

                        // Verileri çekmek için DataAdapter kullan
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // DataGridView'e verileri bağla
                        cityDataGridView.DataSource = dt;
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

