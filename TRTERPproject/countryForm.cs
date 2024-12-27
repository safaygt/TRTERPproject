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
        }
        //private string connectionString = "Server=DESKTOP-U86MLBA;Database=TRTdb;Integrated Security=True;";


        private void btnGet_Click(object sender, EventArgs e)
        {

            string query = "Select * from BSMGRTRTGEN003";
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
            // DataGridView'de bir satır seçilip seçilmediğini kontrol et
            if (countryDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen düzenlemek için bir satır seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçilen satırdan COUNTRYCODE bilgisi al
            string countryCode = countryDataGridView.SelectedRows[0].Cells["COUNTRYCODE"].Value?.ToString();

            if (string.IsNullOrEmpty(countryCode))
            {
                MessageBox.Show("Seçilen satırda geçerli bir COUNTRYCODE bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM BSMGRTRTGEN003 WHERE COUNTRYCODE = @COUNTRYCODE";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@COUNTRYCODE", countryCode);

                    try
                    {
                        con.Open();
                        int recordExists = (int)cmd.ExecuteScalar();

                        if (recordExists > 0)
                        {
                            // COUNTRYCODE bulundu, Edit formuna geç
                            countryFormEdit countryFormEdit = new countryFormEdit(countryCode);
                            countryFormEdit.Show();
                        }
                        else
                        {
                            // COUNTRYCODE bulunamadı
                            MessageBox.Show("Belirtilen COUNTRYCODE için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string comCode = firmCodeTextBox.Text.Trim();
            string countryCode = countryCodeTextBox.Text.Trim();
            string countryText = countryNameTextBox.Text.Trim();

            // 1. Veri Kontrolü
            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(countryCode) || string.IsNullOrEmpty(countryText))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            MessageBox.Show("Bu COUNTRYCODE zaten mevcut. Lütfen başka bir COUNTRYCODE giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 3. COMCODE Kontrolü
                    string checkComCodeQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN003 WHERE COMCODE = @COMCODE";
                    using (cmd = new SqlCommand(checkComCodeQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);

                        int comCodeExists = (int)cmd.ExecuteScalar();

                        if (comCodeExists == 0)
                        {
                            MessageBox.Show("Belirtilen COMCODE mevcut değil. Lütfen doğru bir COMCODE giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 4. Ekleme İşlemi
                    string insertQuery = "INSERT INTO BSMGRTRTGEN003 (COMCODE, COUNTRYCODE, COUNTRYTEXT) VALUES (@COMCODE, @COUNTRYCODE, @COUNTRYTEXT)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd.Parameters.AddWithValue("@COUNTRYCODE", countryCode);
                        cmd.Parameters.AddWithValue("@COUNTRYTEXT", countryText);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // TextBox'ları temizle
                            firmCodeTextBox.Clear();
                            countryCodeTextBox.Clear();
                            countryNameTextBox.Clear();
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
                string countryCode = selectedRow.Cells["COUNTRYCODE"].Value.ToString(); ;

               
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
                            MessageBox.Show("Belirtilen COUNTRYCODE için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
