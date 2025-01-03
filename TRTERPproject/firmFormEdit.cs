﻿using System;
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
    public partial class firmFormEdit : Form
    {


        public string firmCode;
        public string firmName;
        public string Address1;
        public string Address2;
        public string cityCode;
        public string countryCode;


        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);



        public firmFormEdit()
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

        private void btnSave_Click(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "UPDATE BSMGRTRTGEN001 SET COMCODE = @COMCODE, COMTEXT = @COMTEXT, ADDRESS1 = @ADDRESS1, ADDRESS2 = @ADDRESS2, CITYCODE = @CITYCODE, COUNTRYCODE = @COUNTRYCODE WHERE COMCODE = @COMCODE";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@COMCODE", firmCodeTextBox.Text);
                cmd.Parameters.AddWithValue("@COMTEXT", firmNameTextBox.Text);
                cmd.Parameters.AddWithValue("@ADDRESS1", address1TextBox.Text);
                cmd.Parameters.AddWithValue("@ADDRESS2", address2TextBox.Text);
                cmd.Parameters.AddWithValue("@CITYCODE", comboBoxCity.Text);
                cmd.Parameters.AddWithValue("@COUNTRYCODE", comboBoxCountry.Text);
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

        private void firmFormEdit_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COMCODE, COMTEXT, ADDRESS1, ADDRESS2, CITYCODE, COUNTRYCODE FROM BSMGRTRTGEN001 WHERE COMCODE = @COMCODE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@COMCODE", firmCode);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        firmCodeTextBox.Text = firmCode;
                        firmNameTextBox.Text = reader["COMTEXT"].ToString();
                        address1TextBox.Text = reader["ADDRESS1"].ToString();
                        address2TextBox.Text = reader["ADDRESS2"].ToString();
                        comboBoxCity.Text = reader["CITYCODE"].ToString();
                        comboBoxCountry.Text = reader["COUNTRYCODE"].ToString();
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
