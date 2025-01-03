using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using TRTERPproject.Helpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TRTERPproject
{

    public partial class MaliyetKartAna : Form
    {
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd;

        public MaliyetKartAna()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadComboBoxData();

            // ComboBox leave eventlerini bağla
            firmComboBox.Leave += (s, e) => ValidateAndAddData(firmComboBox, "COMCODE", "BSMGRTRTGEN001");
            comboBoxMalMerTip.Leave += (s, e) => ValidateAndAddData(comboBoxMalMerTip, "DOCTYPE", "BSMGRTRTCCM001");
            dilCombo.Leave += (s, e) => ValidateAndAddData(dilCombo, "LANCODE", "BSMGRTRTGEN002");
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
                firmComboBox.DataSource = dtFirma;
                firmComboBox.DisplayMember = "COMCODE";
                firmComboBox.ValueMember = "COMCODE";
                

                // Maliyet Merkezi verilerini doldur
                string queryMaltip = "SELECT DISTINCT DOCTYPE FROM BSMGRTRTCCM001";
                SqlDataAdapter daMaltip = new SqlDataAdapter(queryMaltip, con);
                DataTable dtMaltip = new DataTable();
                daMaltip.Fill(dtMaltip);
                comboBoxMalMerTip.DataSource = dtMaltip;
                comboBoxMalMerTip.DisplayMember = "DOCTYPE";
                comboBoxMalMerTip.ValueMember = "DOCTYPE";

                string queryTtip = "SELECT DISTINCT LANCODE FROM BSMGRTRTGEN002";
                SqlDataAdapter daTtip = new SqlDataAdapter(queryTtip, con);
                DataTable dtTtip = new DataTable();
                daTtip.Fill(dtTtip);
                dilCombo.DataSource = dtTtip;
                dilCombo.DisplayMember = "LANCODE";
                dilCombo.ValueMember = "LANCODE";

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

       /* private void PopulateComboBox(string query, System.Windows.Forms.ComboBox comboBox, string displayMember)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = displayMember;
            comboBox.DropDownStyle = ComboBoxStyle.DropDown;
        }*/

        private void ValidateAndAddData(System.Windows.Forms.ComboBox comboBox, string columnName, string tableName)
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
        {
            // Temel SQL sorgusu
            string query = @"SELECT 
                     HD.COMCODE AS 'Firma', 
                     HD.CCMDOCTYPE AS 'Maliyet Merkezi Tipi', 
                     HD.CCMDOCNUM AS 'Maliyet Merkezi Numarası', 
                     HD.CCMDOCFROM AS 'Geçerlilik Başlangıç',
                     HD.CCMDOCUNTIL AS 'Geçerlilik Bitiş',
                     HD.MAINCCMDOCTYPE AS 'Ana Maliyet Merkezi Tipi', 
                     HD.MAINCCMDOCNUM AS 'Ana Maliyet Merkezi Numarası', 
                     HD.ISDELETED AS 'Silindi mi?',
                     HD.ISPASSIVE AS 'Pasif mi?',
                     CT.CCMSTEXT AS 'Maliyet Açıklaması',
                     G2.LANCODE AS 'Dil Kodu'
                 FROM 
                     BSMGRTRTCCMHEAD HD
                 LEFT JOIN 
                     BSMGRTRTCCMTEXT CT ON HD.CCMDOCNUM = CT.CCMDOCNUM
                 LEFT JOIN
                     BSMGRTRTGEN002 G2 ON CT.LANCODE = G2.LANCODE";

            // Filtreleme koşulları
            List<string> filters = new List<string>();

            // Firma filtresi
            if (!string.IsNullOrEmpty(firmComboBox.Text))
            {
                filters.Add("HD.COMCODE = @COMCODE");
            }

            // Maliyet Merkezi Tipi filtresi
            if (!string.IsNullOrEmpty(comboBoxMalMerTip.Text))
            {
                filters.Add("HD.CCMDOCTYPE = @CCMDOCTYPE");
            }

            // Maliyet Merkezi Numarası filtresi
            if (!string.IsNullOrEmpty(malNotxtBox.Text))
            {
                filters.Add("HD.CCMDOCNUM LIKE @CCMDOCNUM");
            }

            // Ana Maliyet Merkezi Tipi filtresi
            if (!string.IsNullOrEmpty(maliyTxtBox.Text))
            {
                filters.Add("HD.MAINCCMDOCNUM LIKE @MAINCCMDOCNUM");
            }

            // Tarih aralığı filtresi
            if (dateTimePickerBaslangic.Value.Date != DateTime.MinValue.Date && dateTimePickerBitis.Value.Date != DateTime.MinValue.Date)
            {
                filters.Add("HD.CCMDOCFROM >= @CCMDOCFROM AND HD.CCMDOCUNTIL <= @CCMDOCUNTIL");
            }

            // Pasiflik kontrolü
            if (checkboxpas.Checked)
            {
                filters.Add("HD.ISPASSIVE = 1");
            }

            // Silinmişlik kontrolü
            if (deletedlbl.Checked)
            {
                filters.Add("HD.ISDELETED = 1");
            }

            // Dil Kodu filtresi
            if (!string.IsNullOrEmpty(dilCombo.Text))
            {
                filters.Add("G2.LANCODE = @LANCODE");
            }

            // Filtreleri sorguya ekle
            if (filters.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", filters);
            }

            con = new SqlConnection(ConnectionHelper.ConnectionString);
            cmd = new SqlCommand(query, con);

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
                cmd.Parameters.AddWithValue("@CCMDOCNUM", $"{malNotxtBox.Text}%");//
            }

            if (!string.IsNullOrEmpty(maliyTxtBox.Text))
            {
                cmd.Parameters.AddWithValue("@MAINCCMDOCNUM",$"{maliyTxtBox.Text}%");//
            }

            if (dateTimePickerBaslangic.Value.Date != DateTime.MinValue.Date && dateTimePickerBitis.Value.Date != DateTime.MinValue.Date)
            {
                cmd.Parameters.AddWithValue("@CCMDOCFROM", dateTimePickerBaslangic.Value.Date);
                cmd.Parameters.AddWithValue("@CCMDOCUNTIL", dateTimePickerBitis.Value.Date);
            }

            if (!string.IsNullOrEmpty(dilCombo.Text))
            {
                cmd.Parameters.AddWithValue("@LANCODE", dilCombo.Text);
            }

            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                da.Fill(dt);
                maliyetdata.DataSource = dt.Tables[0];
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

                con.Open();

                string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTCCMHEAD WHERE CCMDOCNUM = @CCMDOCNUM";
                using (SqlCommand command = new SqlCommand(checkQuery, con))
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

                using (SqlCommand command = new SqlCommand(insertQuery, con))
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
                con.Close();
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
            if (maliyetdata.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = maliyetdata.SelectedRows[0];

                if (selectedRow.Cells["Maliyet Merkezi Numarası"].Value == null || string.IsNullOrWhiteSpace(selectedRow.Cells["Maliyet Merkezi Numarası"].Value.ToString()))
                {
                    MessageBox.Show("Boş bir satır seçemezsiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Boş satırda işlem yapılmasın
                }

                string malMerCode = selectedRow.Cells["Maliyet Merkezi Numarası"].Value.ToString();

                // Kullanıcıdan onay al
                DialogResult result = MessageBox.Show(
                    $"Maliyet Merkezi Numarası {malMerCode} olan veriyi silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                {
                    // Kullanıcı "Hayır" seçtiyse işlemi iptal ediyoruz
                    MessageBox.Show("Silme işlemi iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTCCMHEAD WHERE CCMDOCNUM = @CCMDOCNUM";
                    using (SqlCommand command = new SqlCommand(checkQuery, con))
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
                    using (SqlCommand command = new SqlCommand(deleteQuery, con))
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
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }



        private void firmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void getAll_Click(object sender, EventArgs e)
        {
            string query = @"SELECT 
                            HD.COMCODE AS 'Firma', 
                            HD.CCMDOCTYPE AS 'Maliyet Merkezi Tipi', 
                            HD.CCMDOCNUM AS 'Maliyet Merkezi Numarası', 
                            HD.CCMDOCFROM AS 'Geçerlilik Başlangıç',
                            HD.CCMDOCUNTIL AS 'Geçerlilik Bitiş',
                            HD.MAINCCMDOCTYPE AS 'Ana Maliyet Merkezi Tipi', 
                            HD.MAINCCMDOCNUM AS 'Ana Maliyet Merkezi Numarası', 
                            HD.ISDELETED AS 'Silindi mi?',
                            HD.ISPASSIVE AS 'Pasif mi?',
                            CT.CCMSTEXT AS 'Maliyet Açıklaması',
                            G2.LANCODE AS 'Dil Kodu'
                        FROM 
                            BSMGRTRTCCMHEAD HD
                        LEFT JOIN 
                            BSMGRTRTCCMTEXT CT ON HD.CCMDOCNUM = CT.CCMDOCNUM
                        INNER JOIN
                                BSMGRTRTGEN002 G2 ON CT.LANCODE = G2.LANCODE";

            try
            {
                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
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
                con.Close();
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
            if (maliyetdata.SelectedRows.Count > 0)
            {
                // Seçilen satırı alın
                DataGridViewRow selectedRow = maliyetdata.SelectedRows[0];

                // Seçilen satırdaki Maliyet Merkezi Numarasını alın
                string malCarNum = selectedRow.Cells["Maliyet Merkezi Numarası"].Value?.ToString();

                if (!string.IsNullOrEmpty(malCarNum))
                {
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
                                // Seçilen satırdaki bilgileri düzenleme ekranına aktar
                                string firma = selectedRow.Cells["Firma"].Value?.ToString();
                                string maliyetMerkeziTipi = selectedRow.Cells["Maliyet Merkezi Tipi"].Value?.ToString();
                                string gecerlilikBaslangic = selectedRow.Cells["Geçerlilik Başlangıç"].Value?.ToString();
                                string gecerlilikBitis = selectedRow.Cells["Geçerlilik Bitiş"].Value?.ToString();
                                //string maliyetAciklama = selectedRow.Cells["Maliyet Açıklaması"].Value?.ToString();

                                // MaliyetKartAnaEdit formunu aç ve bilgileri aktar
                                MaliyetKartAnaEdit maliyetKartForm = new MaliyetKartAnaEdit(malCarNum, firma, maliyetMerkeziTipi, gecerlilikBaslangic, gecerlilikBitis);
                                maliyetKartForm.Show();
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
                else
                {
                    MessageBox.Show("Seçilen satırdaki Maliyet Merkezi Numarası boş. Lütfen geçerli bir satır seçin.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için bir satır seçin.");
            }
        }

        private void MaliyetKartAna_Load(object sender, EventArgs e)
        {

        }
    }

}
