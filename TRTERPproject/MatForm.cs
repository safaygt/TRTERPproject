using System.Data;
using System.Data.SqlClient;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class MatForm : Form
    {
        SqlDataReader reader;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        public MatForm()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {

            string query = "Select * from BSMGRTRTMAT001";
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
                MatDataGridWiew.DataSource = ds.Tables[0];  // Veritabanından çekilen ilk tabloyu DataGridView'e bağla
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
            string materialCode = MatCodeBox.Text;

            if (string.IsNullOrEmpty(materialCode))
            {
                MessageBox.Show("Lütfen bir Malzeme kodu giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM BSMGRTRTMAT001 WHERE DOCTYPE = @DOCTYPE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DOCTYPE", materialCode);

                try
                {
                    con.Open();
                    int recordExists = (int)cmd.ExecuteScalar();

                    if (recordExists > 0)
                    {
                        // COUNTRYCODE bulundu, Edit formuna geç
                        MatFormEdit matFormEdit = new MatFormEdit(materialCode);
                        matFormEdit.Show();
                    }
                    else
                    {
                        // COUNTRYCODE bulunamadı
                        MessageBox.Show("Belirtilen Malzeme kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string materialCode = MatCodeBox.Text.Trim();
            string materialText = MatNameBox.Text.Trim();
            int isPassive = ispassiveBOX.Checked ? 1 : 0;

            // 1. Veri Kontrolü
            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(materialCode) || string.IsNullOrEmpty(materialText))
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
                    string checkMatCodeQuery = "SELECT COUNT(*) FROM BSMGRTRTMAT001 WHERE DOCTYPE = @DOCTYPE";
                    using (cmd = new SqlCommand(checkMatCodeQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@DOCTYPE", materialCode);

                        int MatCodeExists = (int)cmd.ExecuteScalar();

                        if (MatCodeExists > 0)
                        {
                            // COUNTRYCODE zaten mevcut
                            MessageBox.Show("Bu Materyal kodu zaten mevcut. Lütfen başka bir materyal kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 3. COMCODE Kontrolü
                    string checkComCodeQuery = "SELECT COUNT(*) FROM BSMGRTRTMAT001 WHERE COMCODE = @COMCODE";
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
                    string insertQuery = "INSERT INTO BSMGRTRTMAT001 (COMCODE, DOCTYPE, DOCTYPETEXT, ISPASSIVE) VALUES (@COMCODE, @DOCTYPE, @DOCTYPETEXT, @ISPASSIVE)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd.Parameters.AddWithValue("@DOCTYPE", materialCode);
                        cmd.Parameters.AddWithValue("@DOCTYPETEXT", materialText);
                        cmd.Parameters.AddWithValue("@ISPASSIVE", isPassive);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // TextBox'ları temizle
                            firmCodeTextBox.Clear();
                            MatCodeBox.Clear();
                            MatNameBox.Clear();
                            ispassiveBOX.Checked = false;
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

            string MatCode = MatCodeBox.Text.Trim();

            // 1. Boş Veri Kontrolü
            if (string.IsNullOrEmpty(MatCode))
            {
                MessageBox.Show("Lütfen bir Malzeme Kodu giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    // 2. Kayıt Kontrolü
                    string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTMAT001 WHERE DOCTYPE = @DOCTYPE";
                    cmd = new SqlCommand(checkQuery, con);
                    cmd.Parameters.AddWithValue("@DOCTYPE", MatCode);

                    int recordExists = (int)cmd.ExecuteScalar();

                    if (recordExists == 0)
                    {
                        // Kayıt bulunamadı
                        MessageBox.Show("Belirtilen Malzeme kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 3. Silme İşlemi
                    string deleteQuery = "DELETE FROM BSMGRTRTMAT001 WHERE DOCTYPE = @DOCTYPE";
                    cmd = new SqlCommand(deleteQuery, con);
                    cmd.Parameters.AddWithValue("@DOCTYPE", MatCode);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // TextBox'ı temizle
                        MatCodeBox.Clear();
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
