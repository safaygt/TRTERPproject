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
    public partial class firmFormEdit : Form
    {


        private string firmCode;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

        public firmFormEdit()
        {
        }

        public firmFormEdit(string firmCode)
        {
            InitializeComponent();
            this.firmCode = firmCode;
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
                cmd.Parameters.AddWithValue("@CITYCODE", cityCodeTextBox.Text);
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
                        cityCodeTextBox.Text = reader["CITYCODE"].ToString();
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
    }
}
