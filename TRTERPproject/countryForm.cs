using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class countryForm : Form
    {

        SqlDataReader reader;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

        public countryForm()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadComboBoxData();

            // ComboBox Leave eventlerini bağla
            comboBoxFirmCode.Leave += (s, e) => ValidateComboBox(comboBoxFirmCode, "COMCODE", "BSMGRTRTGEN001");
          


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

            string query = "SELECT \r\n            COMCODE AS 'Firma Kodu',\r\n            COUNTRYCODE AS 'Ülke Kodu',\r\n     COUNTRYTEXT AS 'Ülke Adı'\r\n          FROM \r\n            BSMGRTRTGEN003";
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
                countryDataGridView.DataSource = ds.Tables[0];  // Veritabanından çekilen ilk tabloyu DataGridView'e bağla
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
            if (countryDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = countryDataGridView.SelectedRows[0];

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
                countryFormEdit CountryFormEdit = new countryFormEdit();

                // Formdaki alanlara DataGridView'deki değerleri aktar
                CountryFormEdit.firmCode = selectedRow.Cells["Firma Kodu"].Value != DBNull.Value
                    ? selectedRow.Cells["Firma Kodu"].Value.ToString()
                    : string.Empty;

                CountryFormEdit.countryCode = selectedRow.Cells["Ülke Kodu"].Value != DBNull.Value
                    ? selectedRow.Cells["Ülke Kodu"].Value.ToString()
                    : string.Empty;

                CountryFormEdit.countryName = selectedRow.Cells["Ülke Adı"].Value != DBNull.Value
                    ? selectedRow.Cells["Ülke Adı"].Value.ToString()
                    : string.Empty;

                CountryFormEdit.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            string firmCode = comboBoxFirmCode.Text.Trim();
            string countryCode = countryCodeTextBox.Text.Trim();
            string countryName = countryNameTextBox.Text.Trim();


            // 1. Veri Kontrolü
            if (string.IsNullOrEmpty(firmCode) || string.IsNullOrEmpty(countryCode) || string.IsNullOrEmpty(countryName))
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
                    string checkCountryCodeQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN003 WHERE COUNTRYCODE = @COUNTRYCODE";
                    using (cmd = new SqlCommand(checkCountryCodeQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COUNTRYCODE", countryCode);

                        int countryCodeExists = (int)cmd.ExecuteScalar();

                        if (countryCodeExists > 0)
                        {
                            // COUNTRYCODE zaten mevcut
                            MessageBox.Show("Bu Ülke Kodu zaten mevcut. Lütfen başka bir Ülke Kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }





                    // 4. Ekleme İşlemi
                    string insertQuery = "INSERT INTO BSMGRTRTGEN003 (COMCODE, COUNTRYCODE, COUNTRYTEXT) VALUES (@COMCODE, @COUNTRYCODE, @COUNTRYTEXT)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", firmCode);
                        cmd.Parameters.AddWithValue("@COUNTRYTEXT", countryName);
                        cmd.Parameters.AddWithValue("@COUNTRYCODE", countryCode);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // TextBox'ları temizle


                            countryNameTextBox.Clear();
                            countryCodeTextBox.Clear();

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
            if (countryDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = countryDataGridView.SelectedRows[0];
                string countryCode = selectedRow.Cells["Ülke Kodu"].Value.ToString(); ;


                // Kullanıcıdan onay al
                DialogResult dialogResult = MessageBox.Show(
                    $"Ülke Kodu {countryCode} olan veriyi silmek istediğinize emin misiniz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dialogResult != DialogResult.Yes)
                {
                    // Kullanıcı "Hayır" seçerse işlem iptal edilir
                    return;
                }

                using (con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    try
                    {
                        con.Open();

                        // 2. Kayıt Kontrolü
                        string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN003 WHERE COUNTRYCODE = @COUNTRYCODE";
                        cmd = new SqlCommand(checkQuery, con);
                        cmd.Parameters.AddWithValue("@COUNTRYCODE", countryCode);

                        int recordExists = (int)cmd.ExecuteScalar();

                        if (recordExists == 0)
                        {
                            // Kayıt bulunamadı
                            MessageBox.Show("Belirtilen Ülke Kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // 3. Silme İşlemi
                        string deleteQuery = "DELETE FROM BSMGRTRTGEN003 WHERE COUNTRYCODE = @COUNTRYCODE";
                        cmd = new SqlCommand(deleteQuery, con);
                        cmd.Parameters.AddWithValue("@COUNTRYCODE", countryCode);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // TextBox'ı temizle
                            countryCodeTextBox.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt silinemedi. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void countryForm_Load(object sender, EventArgs e)
        {
            // DataGridView hücre stilini ayarla
            countryDataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            countryDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            countryDataGridView.DefaultCellStyle.BackColor = Color.White;
            countryDataGridView.DefaultCellStyle.SelectionBackColor = Color.Blue;
            countryDataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

            // Satır başlıklarının stilini ayarla
            countryDataGridView.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
            countryDataGridView.RowHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);

            // Başlık stilini ayarla
            countryDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            countryDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            countryDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            countryDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnFiltreliGetir_Click(object sender, EventArgs e)
        {
            string query = @"
            SELECT 
            COMCODE AS 'Firma Kodu',
            COUNTRYCODE AS 'Ülke Kodu',
            COUNTRYTEXT AS 'Ülke Adı'
            FROM 
            BSMGRTRTGEN003";

            // Filtreleme koşullarını tutacak liste
            List<string> filters = new List<string>();

            // Firma Kodu filtresi
            if (!string.IsNullOrEmpty(comboBoxFirmCode.Text))
            {
                filters.Add("COMCODE LIKE @COMCODE");
            }

            // Firma Adı filtresi
            if (!string.IsNullOrEmpty(countryCodeTextBox.Text))
            {
                filters.Add("COUNTRYCODE LIKE @COUNTRYCODE");
            }

            // Şehir Kodu filtresi
            if (!string.IsNullOrEmpty(countryNameTextBox.Text))
            {
                filters.Add("COUNTRYTEXT LIKE @COUNTRYTEXT");
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

                    if (!string.IsNullOrEmpty(countryCodeTextBox.Text))
                    {
                        cmd.Parameters.AddWithValue("@COUNTRYCODE", $"{countryCodeTextBox.Text}%");
                    }

                    if (!string.IsNullOrEmpty(countryNameTextBox.Text))
                    {
                        cmd.Parameters.AddWithValue("@COUNTRYTEXT", $"{countryNameTextBox.Text}%");
                    }



                    try
                    {
                        con.Open();

                        // Verileri çekmek için DataAdapter kullan
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // DataGridView'e verileri bağla
                        countryDataGridView.DataSource = dt;
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
