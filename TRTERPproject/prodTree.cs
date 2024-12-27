using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
    public partial class prodTree : Form
    {
        SqlDataReader reader;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

        public prodTree()
        {
            InitializeComponent();
        }

        private void prodTree_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            string query = "Select * from BSMGRTRTBOM001";
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
                prodTreeDataGridView.DataSource = ds.Tables[0];  // Veritabanından çekilen ilk tabloyu DataGridView'e bağla
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
            if (prodTreeDataGridView.SelectedRows.Count > 0)
            {
                // Seçilen satırdaki DOCTYPE (prodDoctype) değerini al
                string prodDoctype = prodTreeDataGridView.SelectedRows[0].Cells["DOCTYPE"].Value.ToString();

                if (string.IsNullOrEmpty(prodDoctype))
                {
                    MessageBox.Show("Lütfen geçerli bir Ürün Ağacı Tipi seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    string query = "SELECT COUNT(*) FROM BSMGRTRTBOM001 WHERE DOCTYPE = @DOCTYPE";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@DOCTYPE", prodDoctype);

                    try
                    {
                        con.Open();
                        int recordExists = (int)cmd.ExecuteScalar();

                        if (recordExists > 0)
                        {
                            // prodDoctype bulundu, Edit formuna geç
                            prodTreeEdit ProdTreeEdit = new prodTreeEdit(prodDoctype);
                            ProdTreeEdit.Show();
                        }
                        else
                        {
                            // prodDoctype bulunamadı
                            MessageBox.Show("Belirtilen Ürün Ağacı Tipi için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (prodTreeDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = prodTreeDataGridView.SelectedRows[0];
                string prodTree = selectedRow.Cells["DOCTYPE"].Value.ToString(); ;


                // Kullanıcıdan onay al
                DialogResult dialogResult = MessageBox.Show(
                    $"Ürün Ağacı Tipi {prodTree} olan veriyi silmek istediğinize emin misiniz?",
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

                        // 2. Kayıt Kontrolü
                        string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTBOM001 WHERE DOCTYPE = @DOCTYPE";
                        cmd = new SqlCommand(checkQuery, con);
                        cmd.Parameters.AddWithValue("@DOCTYPE", prodTree);

                        int recordExists = (int)cmd.ExecuteScalar();

                        if (recordExists == 0)
                        {
                            // Kayıt bulunamadı
                            MessageBox.Show("Belirtilen Ürün Tipi Ağacı için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // 3. Silme İşlemi
                        string deleteQuery = "DELETE FROM BSMGRTRTBOM001 WHERE DOCTYPE = @DOCTYPE";
                        cmd = new SqlCommand(deleteQuery, con);
                        cmd.Parameters.AddWithValue("@DOCTYPE", prodTree);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // TextBox'ı temizle
                            prodDoctypeTextBox.Clear();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {

            string comCode = firmCodeTextBox.Text.Trim();
            string prodDoc = prodDoctypeTextBox.Text.Trim();
            string procDocText = prodDoctypeTextTextBox.Text.Trim();
            int isPassive = prodDocispassiveBOX.Checked ? 1 : 0;

            // 1. Veri Kontrolü
            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(prodDoc) || string.IsNullOrEmpty(procDocText))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    // 2. DOCTYPE Kontrolü
                    string checkprodDocQuery = "SELECT COUNT(*) FROM BSMGRTRTBOM001 WHERE DOCTYPE = @DOCTYPE";
                    using (cmd = new SqlCommand(checkprodDocQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@DOCTYPE", prodDoc);

                        int MatCodeExists = (int)cmd.ExecuteScalar();

                        if (MatCodeExists > 0)
                        {
                            // DOCTYPE zaten mevcut
                            MessageBox.Show("Bu Ürün Ağacı Tipi zaten mevcut. Lütfen başka bir Ürün Ağacı Tipi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 3. COMCODE Kontrolü
                    string checkComCodeQuery = "SELECT COUNT(*) FROM BSMGRTRTBOM001 WHERE COMCODE = @COMCODE";
                    using (cmd = new SqlCommand(checkComCodeQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);

                        int comCodeExists = (int)cmd.ExecuteScalar();

                        if (comCodeExists == 0)
                        {
                            MessageBox.Show("Belirtilen COMCODE mevcut değil. Lütfen doğru bir COMCODE giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 4. Ekleme İşlemi
                    string insertQuery = "INSERT INTO BSMGRTRTBOM001 (COMCODE, DOCTYPE, DOCTYPETEXT, ISPASSIVE) VALUES (@COMCODE, @DOCTYPE, @DOCTYPETEXT, @ISPASSIVE)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd.Parameters.AddWithValue("@DOCTYPE", prodDoc);
                        cmd.Parameters.AddWithValue("@DOCTYPETEXT", procDocText);
                        cmd.Parameters.AddWithValue("@ISPASSIVE", isPassive);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // TextBox'ları temizle
                            firmCodeTextBox.Clear();
                            prodDoctypeTextBox.Clear();
                            prodDoctypeTextTextBox.Clear();
                            prodDocispassiveBOX.Checked = false;
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
    }
}
