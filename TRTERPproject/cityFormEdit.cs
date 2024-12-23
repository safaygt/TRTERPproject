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

        private string cityCode;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        public cityFormEdit(string cityCode)
        {
            InitializeComponent();
            this.cityCode = cityCode;
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
                        firmCodeTextBox.Text = reader["COMCODE"].ToString();
                        cityCodeTextBox.Text = cityCode;
                        cityNameTextBox.Text = reader["CITYTEXT"].ToString();
                        countryCodeTextBox.Text = reader["COUNTRYCODE"].ToString();
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

                cmd.Parameters.AddWithValue("@COMCODE", firmCodeTextBox.Text);
                cmd.Parameters.AddWithValue("@CITYCODE", cityCodeTextBox.Text);
                cmd.Parameters.AddWithValue("@CITYTEXT", cityNameTextBox.Text);
                cmd.Parameters.AddWithValue("@COUNTRYCODE", countryCodeTextBox.Text);

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
