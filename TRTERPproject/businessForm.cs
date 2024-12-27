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
    public partial class businessForm : Form
    {
        SqlDataReader reader;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

        public businessForm()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            string query = "Select * from BSMGRTRTWCM001";
            con = new SqlConnection(ConnectionHelper.ConnectionString);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;


            try
            {

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);


                DataSet ds = new DataSet();

                da.Fill(ds);

                // DataGridView'e veri bağla
                businessDataGridView.DataSource = ds.Tables[0];  // Veritabanından çekilen ilk tabloyu DataGridView'e bağla
            }
            catch (Exception ex)
            {
                // Hata durumunda mesaj göster
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                con.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (businessDataGridView.SelectedRows.Count > 0)
            {
                // Seçilen satırdaki DOCTYPE (docType) değerini al
                string docType = businessDataGridView.SelectedRows[0].Cells["DOCTYPE"].Value.ToString();

                if (string.IsNullOrEmpty(docType))
                {
                    MessageBox.Show("Lütfen geçerli bir İş Merkezi Tipi seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    string query = "SELECT COUNT(*) FROM BSMGRTRTWCM001 WHERE DOCTYPE = @DOCTYPE";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@DOCTYPE", docType);

                    try
                    {
                        con.Open();
                        int recordExists = (int)cmd.ExecuteScalar();

                        if (recordExists > 0)
                        {
                            // docType bulundu, Edit formuna geç
                            businessEditForm BusinessEditForm = new businessEditForm(docType);
                            BusinessEditForm.Show();
                        }
                        else
                        {
                            // docType bulunamadı
                            MessageBox.Show("Belirtilen İş Merkezi Tipi için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            string comCode = firmCodeTextBox.Text.Trim();
            string docType = businessTypeTextBox.Text.Trim();
            string docTypeStatement = businessTypeStatementTextBox.Text.Trim();
            int isPassive = isPassiveCheckBox.Checked ? 1 : 0; // Checkbox durumunu belirle



            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(docType) || string.IsNullOrEmpty(docTypeStatement))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTWCM001 WHERE DOCTYPE = @DOCTYPE";
                    using (cmd = new SqlCommand(checkQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@DOCTYPE", docType);

                        int unitCodeExists = (int)cmd.ExecuteScalar();

                        if (unitCodeExists > 0)
                        {
                            MessageBox.Show("Bu İş Merkezi Tipi zaten mevcut. Lütfen başka bir İş Merkezi Tipi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO BSMGRTRTWCM001 (COMCODE, DOCTYPE, DOCTYPETEXT, ISPASSIVE) VALUES (@COMCODE, @DOCTYPE, @DOCTYPETEXT, @ISPASSIVE)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd.Parameters.AddWithValue("@DOCTYPE", docType);
                        cmd.Parameters.AddWithValue("@DOCTYPETEXT", docTypeStatement);
                        cmd.Parameters.AddWithValue("@ISPASSIVE", isPassive);


                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            firmCodeTextBox.Clear();
                            businessTypeTextBox.Clear();
                            businessTypeStatementTextBox.Clear();
                            isPassiveCheckBox.Checked = false;

                        }
                        else
                        {
                            MessageBox.Show("Kayıt eklenemedi. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (businessDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = businessDataGridView.SelectedRows[0];
                string docType = selectedRow.Cells["DOCTYPE"].Value.ToString(); ;

                // Kullanıcıdan onay al
                DialogResult dialogResult = MessageBox.Show(
                    $"İş Merkezi Tipi {docType} olan veriyi silmek istediğinize emin misiniz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dialogResult != DialogResult.Yes)
                {
                    // Kullanıcı "Hayır" seçerse işlem iptal edilir
                    return;
                }

                using (con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    try
                    {
                        con.Open();

                        string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTWCM001 WHERE DOCTYPE = @DOCTYPE";
                        cmd = new SqlCommand(checkQuery, con);
                        cmd.Parameters.AddWithValue("@DOCTYPE", docType);

                        int recordExists = (int)cmd.ExecuteScalar();

                        if (recordExists == 0)
                        {
                            MessageBox.Show("Belirtilen İş Merkezi Tipi için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string deleteQuery = "DELETE FROM BSMGRTRTWCM001 WHERE DOCTYPE = @DOCTYPE";
                        cmd = new SqlCommand(deleteQuery, con);
                        cmd.Parameters.AddWithValue("@DOCTYPE", docType);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            businessTypeTextBox.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt silinemedi. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
