using System.Data.SqlClient;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class MatFormEdit : Form
    {
        private string matCode;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        public MatFormEdit(string materialCode)
        {
            InitializeComponent();
            this.matCode = materialCode;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "UPDATE BSMGRTRTMAT001 SET COMCODE = @COMCODE,DOCTYPE = @DOCTYPE, DOCTYPETEXT = @DOCTYPETEXT, ISPASSIVE = @ISPASSIVE WHERE DOCTYPE = @DOCTYPE";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@COMCODE", firmCodeTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@DOCTYPE", matCode);
                cmd.Parameters.AddWithValue("@DOCTYPETEXT", MALNAMEBOX.Text.Trim());
                cmd.Parameters.AddWithValue("@ISPASSIVE", ispassiveBOX.Checked ? 1 : 0);

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

        private void MatFormEdit_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COMCODE, DOCTYPE, DOCTYPETEXT, ISPASSIVE FROM BSMGRTRTMAT001 WHERE DOCTYPE = @DOCTYPE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DOCTYPE", matCode);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        firmCodeTextBox.Text = reader["COMCODE"].ToString();
                        MALCODEBOX.Text = matCode;
                        MALNAMEBOX.Text = reader["DOCTYPETEXT"].ToString();
                        ispassiveBOX.Checked = Convert.ToBoolean(reader["ISPASSIVE"]);
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
