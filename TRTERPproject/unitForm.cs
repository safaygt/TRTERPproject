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
    public partial class unitForm : Form
    {

        SqlDataReader reader;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

        public unitForm()
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

            string query = "SELECT COMCODE AS 'Firma Kodu', UNITCODE AS 'Birim Kodu', UNITTEXT AS 'Birim', ISMAINUNIT AS 'Ana Birim mi?', MAINUNITCODE AS 'Ana Birim Kodu' FROM BSMGRTRTGEN005";
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
                unitDataGridView.DataSource = ds.Tables[0];  // Veritabanından çekilen ilk tabloyu DataGridView'e bağla
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
            if (unitDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = unitDataGridView.SelectedRows[0];

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
                unitFormEdit UnitFormEdit = new unitFormEdit();

                UnitFormEdit.firmCode = selectedRow.Cells["Firma Kodu"].Value != DBNull.Value
                    ? selectedRow.Cells["Firma Kodu"].Value.ToString()
                    : string.Empty;

                UnitFormEdit.unitCode = selectedRow.Cells["Birim Kodu"].Value != DBNull.Value
                    ? selectedRow.Cells["Birim Kodu"].Value.ToString()
                    : string.Empty;

                UnitFormEdit.unitText = selectedRow.Cells["Birim"].Value != DBNull.Value
                    ? selectedRow.Cells["Birim"].Value.ToString()
                    : string.Empty;

                UnitFormEdit.isMainUnit = selectedRow.Cells["Ana Birim mi?"].Value != DBNull.Value
                   ? Convert.ToBoolean(selectedRow.Cells["Ana Birim mi?"].Value)
                   : false;

                UnitFormEdit.mainUnitCode = selectedRow.Cells["Ana Birim Kodu"].Value != DBNull.Value
                  ? selectedRow.Cells["Ana Birim Kodu"].Value.ToString()
                  : string.Empty;



                UnitFormEdit.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            string comCode = comboBoxFirmCode.Text.Trim();
            string unitCode = unitCodeTextBox.Text.Trim();
            string unitText = unitTextBox.Text.Trim();
            int isMainUnit = isMainUnitCheckBox.Checked ? 1 : 0; // Checkbox durumunu belirle
            string mainUnitCode = mainUnitCodeTextBox.Text.Trim();


            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(unitCode) || string.IsNullOrEmpty(unitText))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN005 WHERE UNITCODE = @UNITCODE";
                    using (cmd = new SqlCommand(checkQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@UNITCODE", unitCode);

                        int unitCodeExists = (int)cmd.ExecuteScalar();

                        if (unitCodeExists > 0)
                        {
                            MessageBox.Show("Bu Birim Kodu zaten mevcut. Lütfen başka bir Birim Kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO BSMGRTRTGEN005 (COMCODE, UNITCODE, UNITTEXT, ISMAINUNIT,MAINUNITCODE) VALUES (@COMCODE, @UNITCODE, @UNITTEXT, @ISMAINUNIT, @MAINUNITCODE)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd.Parameters.AddWithValue("@UNITCODE", unitCode);
                        cmd.Parameters.AddWithValue("@UNITTEXT", unitText);
                        cmd.Parameters.AddWithValue("@ISMAINUNIT", isMainUnit);
                        cmd.Parameters.AddWithValue("@MAINUNITCODE", mainUnitCode);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            unitCodeTextBox.Clear();
                            unitTextBox.Clear();
                            isMainUnitCheckBox.Checked = false;
                            mainUnitCodeTextBox.Clear();
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
            if (unitDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = unitDataGridView.SelectedRows[0];
                string unitCode = selectedRow.Cells["Birim Kodu"].Value.ToString(); ;


                // Kullanıcıdan onay al
                DialogResult dialogResult = MessageBox.Show(
                    $"Birim Kodu {unitCode} olan veriyi silmek istediğinize emin misiniz?",
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

                        string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN005 WHERE UNITCODE = @UNITCODE";
                        cmd = new SqlCommand(checkQuery, con);
                        cmd.Parameters.AddWithValue("@UNITCODE", unitCode);

                        int recordExists = (int)cmd.ExecuteScalar();

                        if (recordExists == 0)
                        {
                            MessageBox.Show("Belirtilen Birim Kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string deleteQuery = "DELETE FROM BSMGRTRTGEN005 WHERE UNITCODE = @UNITCODE";
                        cmd = new SqlCommand(deleteQuery, con);
                        cmd.Parameters.AddWithValue("@UNITCODE", unitCode);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            unitCodeTextBox.Clear();
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

        private void btnFiltreliGetir_Click(object sender, EventArgs e)
        {
            string query = @"
            SELECT 
            COMCODE AS 'Firma Kodu',
            UNITCODE AS 'Birim Kodu',
            UNITTEXT AS 'Birim',
            ISMAINUNIT AS 'Ana Birim mi?',
            MAINUNITCODE AS 'Ana Birim Kodu'
            FROM 
            BSMGRTRTGEN005";

            // Filtreleme koşullarını tutacak liste
            List<string> filters = new List<string>();

            // Firma Kodu filtresi
            if (!string.IsNullOrEmpty(comboBoxFirmCode.Text))
            {
                filters.Add("COMCODE LIKE @COMCODE");
            }


            if (!string.IsNullOrEmpty(unitCodeTextBox.Text))
            {
                filters.Add("UNITCODE LIKE @UNITCODE");
            }

            if (!string.IsNullOrEmpty(unitTextBox.Text))
            {
                filters.Add("UNITTEXT LIKE @UNITTEXT");
            }

            if (!string.IsNullOrEmpty(mainUnitCodeTextBox.Text))
            {
                filters.Add("MAINUNITCODE LIKE @MAINUNITCODE");
            }

            if (isMainUnitCheckBox.Checked)
            {
                filters.Add("ISMAINUNIT = 1");
            }
            else
            {
                filters.Add("ISMAINUNIT = 0");
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

                    if (!string.IsNullOrEmpty(unitCodeTextBox.Text))
                    {
                        cmd.Parameters.AddWithValue("@UNITCODE", $"{unitCodeTextBox.Text}%");
                    }

                    if (!string.IsNullOrEmpty(unitTextBox.Text))
                    {
                        cmd.Parameters.AddWithValue("@UNITTEXT", $"{unitTextBox.Text}%");
                    }


                    if (!string.IsNullOrEmpty(mainUnitCodeTextBox.Text))
                    {
                        cmd.Parameters.AddWithValue("@MAINUNITCODE", $"{mainUnitCodeTextBox.Text}%");
                    }






                    try
                    {
                        con.Open();

                        // Verileri çekmek için DataAdapter kullan
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // DataGridView'e verileri bağla
                        unitDataGridView.DataSource = dt;
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
