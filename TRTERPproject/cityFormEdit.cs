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
    public partial class cityFormEdit : Form
    {

        public string cityCode;
        public string comCode;
        public string countryCode;
        public string cityName;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        public cityFormEdit()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadComboBoxData();

            // ComboBox Leave eventlerini bağla
            comboBoxFirmCode.Leave += (s, e) => ValidateComboBox(comboBoxFirmCode, "COMCODE", "BSMGRTRTGEN001");
            comboBoxCountryCode.Leave += (s, e) => ValidateComboBox(comboBoxCountryCode, "COUNTRYCODE", "BSMGRTRTGEN003");
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





        private void cityFormEdit_Load(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COMCODE, CITYTEXT, COUNTRYCODE FROM BSMGRTRTGEN004 WHERE CITYCODE = @CITYCODE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CITYCODE", cityCode);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        comboBoxFirmCode.Text = reader["COMCODE"].ToString();
                        cityCodeTextBox.Text = cityCode;
                        cityNameTextBox.Text = reader["CITYTEXT"].ToString();
                        comboBoxCountryCode.Text = reader["COUNTRYCODE"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "UPDATE BSMGRTRTGEN004 SET COMCODE = @COMCODE, CITYTEXT = @CITYTEXT, COUNTRYCODE=@COUNTRYCODE WHERE CITYCODE = @CITYCODE";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@COMCODE", comboBoxFirmCode.Text);
                cmd.Parameters.AddWithValue("@CITYCODE", cityCodeTextBox.Text);
                cmd.Parameters.AddWithValue("@CITYTEXT", cityNameTextBox.Text);
                cmd.Parameters.AddWithValue("@COUNTRYCODE", comboBoxCountryCode.Text);

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
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
