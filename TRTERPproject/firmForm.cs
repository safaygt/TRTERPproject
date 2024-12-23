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
    public partial class firmForm : Form
    {

        SqlDataReader reader;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        public firmForm()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {

            string query = "Select * from BSMGRTRTGEN001";
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
            string firmCode = firmCodeTextBox.Text;

            if (string.IsNullOrEmpty(firmCode))
            {
                MessageBox.Show("Lütfen bir Firma Kodu giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM BSMGRTRTGEN001 WHERE COMCODE = @COMCODE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@COMCODE", firmCode);

                try
                {
                    con.Open();
                    int recordExists = (int)cmd.ExecuteScalar();

                    if (recordExists > 0)
                    {
                        // COUNTRYCODE bulundu, Edit formuna geç
                        firmFormEdit FirmFormEdit = new firmFormEdit(firmCode);
                        FirmFormEdit.Show();
                    }
                    else
                    {
                        // COUNTRYCODE bulunamadı
                        MessageBox.Show("Belirtilen Firma Kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            string firmCode = firmCodeTextBox.Text.Trim();
            string firmName = firmNameTextBox.Text.Trim();
            string address1 = address1TextBox.Text.Trim();
            string address2 = address2TextBox.Text.Trim();
            string cityCode = cityCodeTextBox.Text.Trim();
            string countryCode = countryCodeTextBox.Text.Trim();

            // 1. Veri Kontrolü
            if (string.IsNullOrEmpty(firmCode) || string.IsNullOrEmpty(firmName) || string.IsNullOrEmpty(address1) || string.IsNullOrEmpty(cityCode) || string.IsNullOrEmpty(countryCode))
            {
                MessageBox.Show("Lütfen gerekli tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    // 2. COUNTRYCODE Kontrolü
                    string checkCountryCodeQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN001 WHERE COMCODE = @COMCODE";
                    using (cmd = new SqlCommand(checkCountryCodeQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", firmCode);

                        int countryCodeExists = (int)cmd.ExecuteScalar();

                        if (countryCodeExists > 0)
                        {
                            // COUNTRYCODE zaten mevcut
                            MessageBox.Show("Bu Firma Kodu zaten mevcut. Lütfen başka bir Firma Kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 3. COMCODE Kontrolü

                    /*
                    string checkComCodeQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN001 WHERE COMCODE = @COMCODE";
                    using (cmd = new SqlCommand(checkComCodeQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", firmCode);

                        int comCodeExists = (int)cmd.ExecuteScalar();

                        if (comCodeExists == 0)
                        {
                            MessageBox.Show("Belirtilen Firma Kodu mevcut değil. Lütfen doğru bir Firma Kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    */



                    // 4. Ekleme İşlemi
                    string insertQuery = "INSERT INTO BSMGRTRTGEN001 (COMCODE, COMTEXT, ADDRESS1, ADDRESS2, CITYCODE, COUNTRYCODE) VALUES (@COMCODE, @COMTEXT, @ADDRESS1, @ADDRESS2, @CITYCODE, @COUNTRYCODE)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", firmCode);
                        cmd.Parameters.AddWithValue("@COMTEXT", firmName);
                        cmd.Parameters.AddWithValue("@ADDRESS1", address1);
                        cmd.Parameters.AddWithValue("@ADDRESS2", address2);
                        cmd.Parameters.AddWithValue("@CITYCODE", cityCode);
                        cmd.Parameters.AddWithValue("@COUNTRYCODE", countryCode);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // TextBox'ları temizle
                            firmCodeTextBox.Clear();
                            firmNameTextBox.Clear();
                            address1TextBox.Clear();
                            address2TextBox.Clear();
                            cityCodeTextBox.Clear();
                            countryCodeTextBox.Clear();
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

            string firmCode = firmCodeTextBox.Text.Trim();

            // 1. Boş Veri Kontrolü
            if (string.IsNullOrEmpty(firmCode))
            {
                MessageBox.Show("Lütfen bir Firma Kodu giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    // 2. Kayıt Kontrolü
                    string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN001 WHERE COMCODE = @COMCODE";
                    cmd = new SqlCommand(checkQuery, con);
                    cmd.Parameters.AddWithValue("@COMCODE", firmCode);

                    int recordExists = (int)cmd.ExecuteScalar();

                    if (recordExists == 0)
                    {
                        // Kayıt bulunamadı
                        MessageBox.Show("Belirtilen Firma Kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 3. Silme İşlemi
                    string deleteQuery = "DELETE FROM BSMGRTRTGEN001 WHERE COMCODE = @COMCODE";
                    cmd = new SqlCommand(deleteQuery, con);
                    cmd.Parameters.AddWithValue("@COMCODE", firmCode);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // TextBox'ı temizle
                        firmCodeTextBox.Clear();
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
    }
}
