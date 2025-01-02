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
    public partial class businessEditForm : Form
    {

        public string firmCode;
        public string docType;
        public string docTypeText;
        public bool isPassive;
    
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        public businessEditForm()
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "UPDATE BSMGRTRTWCM001 SET COMCODE = @COMCODE, DOCTYPETEXT = @DOCTYPETEXT, ISPASSIVE = @ISPASSIVE WHERE DOCTYPE = @DOCTYPE";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@COMCODE", comboBoxFirmCode.Text.Trim());
                cmd.Parameters.AddWithValue("@DOCTYPE", businessTypeTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@DOCTYPETEXT", businessTypeStatementTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@ISPASSIVE", isPassiveCheckBox.Checked ? 1 : 0);
               


                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Kayıt güncellenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Hata kodlarına göre özel mesajlar döndür
                    if (sqlEx.Number == 547) // Foreign key constraint violation error
                    {
                        MessageBox.Show("Bu işlemi gerçekleştiremezsiniz! Başka bir tablodaki bu veriyle ilgili kayıtlar var.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // Diğer SQL hatalarını göster
                        MessageBox.Show("Hata: " + sqlEx.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Diğer genel hatalar için
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void businessEditForm_Load(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COMCODE, DOCTYPE, DOCTYPETEXT, ISPASSIVE FROM BSMGRTRTWCM001 WHERE DOCTYPE = @DOCTYPE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DOCTYPE", docType);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        comboBoxFirmCode.Text = reader["COMCODE"].ToString();
                        businessTypeTextBox.Text = docType;
                        businessTypeStatementTextBox.Text = reader["DOCTYPETEXT"].ToString();
                        isPassiveCheckBox.Checked = Convert.ToBoolean(reader["ISPASSIVE"]);


                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
