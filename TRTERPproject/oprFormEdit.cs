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
    public partial class oprFormEdit : Form
    {
        private string operationCode;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);


        public oprFormEdit(string operationCode)
        {
            InitializeComponent();
            this.operationCode = operationCode;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "UPDATE BSMGRTRTOPR001 SET COMCODE = @COMCODE, DOCTYPE = @DOCTYPE, DOCTYPETEXT = @DOCTYPETEXT, ISPASSIVE = @ISPASSIVE WHERE DOCTYPE = @NEWDOCTYPE";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@COMCODE", oprFirmCodeTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@DOCTYPE", oprTypeTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@DOCTYPETEXT", oprTypeDesTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@ISPASSIVE", isPassiveOprCheckbox.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@NEWDOCTYPE", operationCode);

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

       

        private void oprFormEdit_Load(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COMCODE, DOCTYPE, DOCTYPETEXT, ISPASSIVE FROM BSMGRTRTOPR001 WHERE DOCTYPE = @DOCTYPE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DOCTYPE", operationCode);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        oprFirmCodeTextBox.Text = reader["COMCODE"].ToString();
                        oprTypeTextBox.Text = operationCode;
                        oprTypeDesTextBox.Text = reader["DOCTYPETEXT"].ToString();
                        isPassiveOprCheckbox.Checked = Convert.ToBoolean(reader["ISPASSIVE"]);
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
