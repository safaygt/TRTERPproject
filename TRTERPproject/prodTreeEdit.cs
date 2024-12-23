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
    public partial class prodTreeEdit : Form

    {
        private string prodDocType;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        public prodTreeEdit(string prodDocType)
        {
            InitializeComponent();
            this.prodDocType = prodDocType;
        }




        private void prodTreeEdit_Load_1(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COMCODE, DOCTYPE, DOCTYPETEXT, ISPASSIVE FROM BSMGRTRTBOM001 WHERE DOCTYPE = @DOCTYPE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DOCTYPE", prodDocType);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        firmCodeTextBox.Text = reader["COMCODE"].ToString();
                        prodDoctypeTextBox.Text = prodDocType;
                        prodDoctypeTextTextBox.Text = reader["DOCTYPETEXT"].ToString();
                        prodDocispassiveBOX.Checked = Convert.ToBoolean(reader["ISPASSIVE"]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "UPDATE BSMGRTRTBOM001 SET COMCODE = @COMCODE,DOCTYPE = @DOCTYPE, DOCTYPETEXT = @DOCTYPETEXT, ISPASSIVE = @ISPASSIVE WHERE DOCTYPE = @DOCTYPE";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@COMCODE", firmCodeTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@DOCTYPE", prodDoctypeTextBox.Text);
                cmd.Parameters.AddWithValue("@DOCTYPETEXT", prodDoctypeTextTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@ISPASSIVE", prodDocispassiveBOX.Checked ? 1 : 0);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
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
