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
    public partial class lanForm : Form
    {
        SqlDataReader reader;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        public lanForm()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {

            string query = "Select * from BSMGRTRTGEN002";
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
                CountryDataGridView.DataSource = ds.Tables[0];  // Veritabanından çekilen ilk tabloyu DataGridView'e bağla
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

            string lanCode = lanCodeTextBox.Text;

            if (string.IsNullOrEmpty(lanCode))
            {
                MessageBox.Show("Lütfen bir Dil Kodu giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM BSMGRTRTGEN002 WHERE LANCODE = @LANCODE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@LANCODE", lanCode);

                try
                {
                    con.Open();
                    int recordExists = (int)cmd.ExecuteScalar();

                    if (recordExists > 0)
                    {
                        // COUNTRYCODE bulundu, Edit formuna geç
                        lanFormEdit LanFormEdit = new lanFormEdit(lanCode);
                        LanFormEdit.Show();
                    }
                    else
                    {
                        // COUNTRYCODE bulunamadı
                        MessageBox.Show("Belirtilen Dil Kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string comCode = firmCodeTextBox.Text.Trim();
            string lanCode = lanCodeTextBox.Text.Trim();
            string lanText = lanTextBox.Text.Trim();

            // 1. Veri Kontrolü
            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(lanCode) || string.IsNullOrEmpty(lanText))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    // 2. COUNTRYCODE Kontrolü
                    string checkCountryCodeQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN002 WHERE LANCODE = @LANCODE";
                    using (cmd = new SqlCommand(checkCountryCodeQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@LANCODE", lanCode);

                        int countryCodeExists = (int)cmd.ExecuteScalar();

                        if (countryCodeExists > 0)
                        {
                            // COUNTRYCODE zaten mevcut
                            MessageBox.Show("Bu Dil Kodu zaten mevcut. Lütfen başka bir Dil Kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 3. COMCODE Kontrolü
                    string checkComCodeQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN002 WHERE COMCODE = @COMCODE";
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
                    string insertQuery = "INSERT INTO BSMGRTRTGEN002 (COMCODE, LANCODE, LANTEXT) VALUES (@COMCODE, @LANCODE, @LANTEXT)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd.Parameters.AddWithValue("@LANCODE", lanCode);
                        cmd.Parameters.AddWithValue("@LANTEXT", lanText);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // TextBox'ları temizle
                            firmCodeTextBox.Clear();
                            lanCodeTextBox.Clear();
                            lanTextBox.Clear();
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


            string lanCode = lanCodeTextBox.Text.Trim();

            // 1. Boş Veri Kontrolü
            if (string.IsNullOrEmpty(lanCode))
            {
                MessageBox.Show("Lütfen bir Dil Kodu giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    // 2. Kayıt Kontrolü
                    string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN002 WHERE LANCODE = @LANCODE";
                    cmd = new SqlCommand(checkQuery, con);
                    cmd.Parameters.AddWithValue("@LANCODE", lanCode);

                    int recordExists = (int)cmd.ExecuteScalar();

                    if (recordExists == 0)
                    {
                        // Kayıt bulunamadı
                        MessageBox.Show("Belirtilen Dil Kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 3. Silme İşlemi
                    string deleteQuery = "DELETE FROM BSMGRTRTGEN002 WHERE LANCODE = @LANCODE";
                    cmd = new SqlCommand(deleteQuery, con);
                    cmd.Parameters.AddWithValue("@LANCODE", lanCode);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // TextBox'ı temizle
                        lanCodeTextBox.Clear();
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

        private void CountryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lanTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void lanCodeTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void firmCodeTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
