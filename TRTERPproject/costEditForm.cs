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
    public partial class costEditForm : Form
    {

        private string docType;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

        public costEditForm(string docType)
        {
            InitializeComponent();
            this.docType = docType;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "UPDATE BSMGRTRTCCM001 SET COMCODE = @COMCODE, DOCTYPE = @DOCTYPE, DOCTYPETEXT = @DOCTYPETEXT, ISPASSIVE = @ISPASSIVE WHERE DOCTYPE = @NEWDOCTYPE";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@COMCODE", firmCodeTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@DOCTYPE", costTypeTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@DOCTYPETEXT", costTypeStatementTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@ISPASSIVE", isPassiveCheckBox.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@NEWDOCTYPE", docType);


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

        private void costEditForm_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COMCODE, DOCTYPE, DOCTYPETEXT, ISPASSIVE FROM BSMGRTRTCCM001 WHERE DOCTYPE = @DOCTYPE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DOCTYPE", docType);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        firmCodeTextBox.Text = reader["COMCODE"].ToString();
                        costTypeTextBox.Text = docType;
                        costTypeStatementTextBox.Text = reader["DOCTYPETEXT"].ToString();
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
