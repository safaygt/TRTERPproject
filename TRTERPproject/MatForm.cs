using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class MatForm : Form
    {
        SqlDataReader reader;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        public MatForm()
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

            string query = "SELECT COMCODE AS 'Firma Kodu', DOCTYPE AS 'Malzeme Tipi', DOCTYPETEXT AS 'Malzeme Tipi Açıklama', ISPASSIVE AS 'Pasif mi?' FROM BSMGRTRTMAT001";
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
                MatDataGridWiew.DataSource = ds.Tables[0];  // Veritabanından çekilen ilk tabloyu DataGridView'e bağla
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
            if (MatDataGridWiew.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = MatDataGridWiew.SelectedRows[0];

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
                MatFormEdit matFormEdit = new MatFormEdit();

                matFormEdit.firmCode = selectedRow.Cells["Firma Kodu"].Value != DBNull.Value
                    ? selectedRow.Cells["Firma Kodu"].Value.ToString()
                    : string.Empty;

                matFormEdit.docType = selectedRow.Cells["Malzeme Tipi"].Value != DBNull.Value
                    ? selectedRow.Cells["Malzeme Tipi"].Value.ToString()
                    : string.Empty;

                matFormEdit.docTypeText = selectedRow.Cells["Malzeme Tipi Açıklama"].Value != DBNull.Value
                    ? selectedRow.Cells["Malzeme Tipi Açıklama"].Value.ToString()
                    : string.Empty;

                matFormEdit.isPassive = selectedRow.Cells["Pasif mi?"].Value != DBNull.Value
                   ? Convert.ToBoolean(selectedRow.Cells["Pasif mi?"].Value)
                   : false;




                matFormEdit.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string comCode = comboBoxFirmCode.Text.Trim();
            string docType = MatCodeBox.Text.Trim();
            string matName = MatNameBox.Text.Trim();
            int isPassive = ispassiveBOX.Checked ? 1 : 0; // Checkbox durumunu belirle



            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(docType) || string.IsNullOrEmpty(matName))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTMAT001 WHERE DOCTYPE = @DOCTYPE";
                    using (cmd = new SqlCommand(checkQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@DOCTYPE", docType);

                        int unitCodeExists = (int)cmd.ExecuteScalar();

                        if (unitCodeExists > 0)
                        {
                            MessageBox.Show("Bu Malzeme Tipi zaten mevcut. Lütfen başka bir Malzeme Tipi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO BSMGRTRTMAT001 (COMCODE, DOCTYPE, DOCTYPETEXT, ISPASSIVE) VALUES (@COMCODE, @DOCTYPE, @DOCTYPETEXT, @ISPASSIVE)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd.Parameters.AddWithValue("@DOCTYPE", docType);
                        cmd.Parameters.AddWithValue("@DOCTYPETEXT", matName);
                        cmd.Parameters.AddWithValue("@ISPASSIVE", isPassive);


                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            MatCodeBox.Clear();
                            ispassiveBOX.Checked = false;
                            MatNameBox.Clear();
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
            if (MatDataGridWiew.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = MatDataGridWiew.SelectedRows[0];
                string MatCode = selectedRow.Cells["Malzeme Tipi"].Value.ToString(); ;

                // Kullanıcıdan onay al
                DialogResult dialogResult = MessageBox.Show(
                    $"Malzeme Tipi {MatCode} olan veriyi silmek istediğinize emin misiniz?",
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
                        string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTMAT001 WHERE DOCTYPE = @DOCTYPE";
                        cmd = new SqlCommand(checkQuery, con);
                        cmd.Parameters.AddWithValue("@DOCTYPE", MatCode);

                        int recordExists = (int)cmd.ExecuteScalar();


                        if (recordExists == 0)
                        {
                            // Kayıt bulunamadı
                            MessageBox.Show("Belirtilen Malzeme Tipi için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // 3. Silme İşlemi
                        string deleteQuery = "DELETE FROM BSMGRTRTMAT001 WHERE DOCTYPE = @DOCTYPE";
                        cmd = new SqlCommand(deleteQuery, con);
                        cmd.Parameters.AddWithValue("@DOCTYPE", MatCode);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MatCodeBox.Clear();
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

        }

        private void btnFiltreliGetir_Click(object sender, EventArgs e)
        {
            string query = @"
            SELECT 
            COMCODE AS 'Firma Kodu',
            DOCTYPE AS 'Malzeme Tipi',
            DOCTYPETEXT AS 'Malzeme Tipi Açıklaması',
            ISPASSIVE AS 'Pasif mi?'
            FROM 
            BSMGRTRTMAT001";

            // Filtreleme koşullarını tutacak liste
            List<string> filters = new List<string>();

            // Firma Kodu filtresi
            if (!string.IsNullOrEmpty(comboBoxFirmCode.Text))
            {
                filters.Add("COMCODE LIKE @COMCODE");
            }


            if (!string.IsNullOrEmpty(MatCodeBox.Text))
            {
                filters.Add("DOCTYPE LIKE @DOCTYPE");
            }

            if (!string.IsNullOrEmpty(MatNameBox.Text))
            {
                filters.Add("DOCTYPETEXT LIKE @DOCTYPETEXT");
            }



            if (ispassiveBOX.Checked)
            {
                filters.Add("ISPASSIVE = 1");
            }
            else
            {
                filters.Add("ISPASSIVE = 0");
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

                    if (!string.IsNullOrEmpty(MatCodeBox.Text))
                    {
                        cmd.Parameters.AddWithValue("@DOCTYPE", $"{MatCodeBox.Text}%");
                    }

                    if (!string.IsNullOrEmpty(MatNameBox.Text))
                    {
                        cmd.Parameters.AddWithValue("@DOCTYPETEXT", $"{MatNameBox.Text}%");
                    }




                    try
                    {
                        con.Open();

                        // Verileri çekmek için DataAdapter kullan
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // DataGridView'e verileri bağla
                        MatDataGridWiew.DataSource = dt;
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
