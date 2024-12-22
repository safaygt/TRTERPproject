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
    public partial class unitFormEdit : Form
    {
        private string unitCode;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

        public unitFormEdit(string unitCode)
        {
            InitializeComponent();
            this.unitCode = unitCode;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "UPDATE BSMGRTRTGEN005 SET COMCODE = @COMCODE, UNITCODE = @UNITCODE, UNITTEXT = @UNITTEXT, ISMAINUNIT = @ISMAINUNIT, MAINUNITCODE = @MAINUNITCODE WHERE UNITCODE = @UNITCODE";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@COMCODE", firmCodeTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@UNITCODE", unitCode);
                cmd.Parameters.AddWithValue("@UNITTEXT", unitTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@ISMAINUNIT", isMainUnitCheckBox.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@MAINUNITCODE", mainUnitCodeTextBox.Text.Trim());


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

        private void unitFormEdit_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COMCODE, UNITCODE, UNITTEXT, ISMAINUNIT, MAINUNITCODE FROM BSMGRTRTGEN005 WHERE UNITCODE = @UNITCODE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UNITCODE", unitCode);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        firmCodeTextBox.Text = reader["COMCODE"].ToString();
                        unitCodeTextBox.Text = unitCode;
                        unitTextBox.Text = reader["UNITTEXT"].ToString();
                        isMainUnitCheckBox.Checked = Convert.ToBoolean(reader["ISMAINUNIT"]);
                        mainUnitCodeTextBox.Text += reader["MAINUNITCODE"].ToString();
                        
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
