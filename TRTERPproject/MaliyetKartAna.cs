using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class MaliyetKartAna : Form
    {
        private readonly SqlConnection _connection = new SqlConnection(ConnectionHelper.ConnectionString);

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

        private void PopulateComboBox(string query, ComboBox comboBox, string displayMember)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            comboBox.DataSource = dataTable;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = displayMember;
            comboBox.DropDownStyle = ComboBoxStyle.DropDown;
        }

        private void ValidateAndAddData(ComboBox comboBox, string columnName)
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

        private void addBut_Click(object sender, EventArgs e)
        {
            string comCode = firmComboBox.Text.Trim();
            string malMerType = comboBoxMalMerTip.Text.Trim();
            string malMerCode = malNotxtBox.Text.Trim();
            string startDate = basTarTxtBox.Text.Trim();
            string lastDate = bitistarTxtBox.Text.Trim();
            string malDes = maliyTxtBox.Text.Trim();
            int isMainUnit = checkboxpas.Checked ? 1 : 0;
            int isDelUnit = deletedlbl.Checked ? 1 : 0;

            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(malMerType) || string.IsNullOrEmpty(malMerCode) ||
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
            basTarTxtBox.Clear();
            bitistarTxtBox.Clear();
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
    }
}
