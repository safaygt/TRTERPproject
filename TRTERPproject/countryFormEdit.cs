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

namespace TRTERPproject
{
    public partial class countryFormEdit : Form
    {
        private string countryCode;
        SqlConnection con;
        SqlCommand cmd;
        public countryFormEdit(string countryCode)
        {
            InitializeComponent();
            this.countryCode = countryCode;
        }

        private string connectionString = "Server=MSI\\SQLEXPRESS;Database=TRTdb;Integrated Security=True;";

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection("Server=MSI\\SQLEXPRESS;Database=TRTdb;Integrated Security=True;"))
            {
                string query = "UPDATE BSMGRTRTGEN003 SET COMCODE = @COMCODE, COUNTRYTEXT = @COUNTRYTEXT WHERE COUNTRYCODE = @COUNTRYCODE";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@COMCODE", firmCodeTextBox.Text);
                cmd.Parameters.AddWithValue("@COUNTRYCODE", countryCodeTextBox.Text);
                cmd.Parameters.AddWithValue("@COUNTRYTEXT", countryNameTextBox.Text);

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

        private void countryFormEdit_Load(object sender, EventArgs e)
        {
            using (con = new SqlConnection("Server=MSI\\SQLEXPRESS;Database=TRTdb;Integrated Security=True;"))
            {
                string query = "SELECT COMCODE, COUNTRYTEXT FROM BSMGRTRTGEN003 WHERE COUNTRYCODE = @COUNTRYCODE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@COUNTRYCODE", countryCode);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        firmCodeTextBox.Text = reader["COMCODE"].ToString();
                        countryCodeTextBox.Text = countryCode;
                        countryNameTextBox.Text = reader["COUNTRYTEXT"].ToString();
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
