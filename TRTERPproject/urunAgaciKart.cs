using System.Data;
using System.Data.SqlClient;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class urunAgaciKart : Form
    {
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd;
        public urunAgaciKart()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadComboBoxData();

            // ComboBox Leave eventlerini bağla
            firmbox.Leave += (s, e) => ValidateAndAddData(firmbox, "COMCODE");
            urnagamalztipbox.Leave += (s, e) => ValidateAndAddData(urnagamalztipbox, "MATDOCTYPE");
            urnmalznumBox.Leave += (s, e) => ValidateAndAddData(urnmalznumBox, "MATDOCNUM");
            urnAgaTipBox.Leave += (s, e) => ValidateAndAddData(urnAgaTipBox, "BOMDOCTYPE");
        }
        private void LoadComboBoxData()
        {

            try
            {
                con.Open();

                // Firma verilerini doldur
                string queryFirma = "SELECT DISTINCT COMCODE FROM BSMGRTRTBOMHEAD";
                SqlDataAdapter daFirma = new SqlDataAdapter(queryFirma, con);
                DataTable dtFirma = new DataTable();
                daFirma.Fill(dtFirma);
                firmbox.DataSource = dtFirma;
                firmbox.DisplayMember = "COMCODE";
                firmbox.ValueMember = "COMCODE";
                firmbox.DropDownStyle = ComboBoxStyle.DropDown; // Yeni veri girilebilir

                string queryMtip = "SELECT DISTINCT MATDOCTYPE FROM BSMGRTRTBOMHEAD"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daMtip = new SqlDataAdapter(queryMtip, con);
                DataTable dtMtip = new DataTable();
                daMtip.Fill(dtMtip);
                urnagamalztipbox.DataSource = dtMtip;
                urnagamalztipbox.DisplayMember = "MATDOCTYPE";
                urnagamalztipbox.ValueMember = "MATDOCTYPE";
                urnagamalztipbox.DropDownStyle = ComboBoxStyle.DropDown;

                string queryTtip = "SELECT DISTINCT MATDOCNUM FROM BSMGRTRTBOMHEAD"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daTtip = new SqlDataAdapter(queryTtip, con);
                DataTable dtTtip = new DataTable();
                daTtip.Fill(dtTtip);
                urnmalznumBox.DataSource = dtTtip;
                urnmalznumBox.DisplayMember = "MATDOCNUM";
                urnmalznumBox.ValueMember = "MATDOCNUM";
                urnmalznumBox.DropDownStyle = ComboBoxStyle.DropDown;

                string queryGtip = "SELECT DISTINCT BOMDOCTYPE FROM BSMGRTRTBOMHEAD"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daGtip = new SqlDataAdapter(queryGtip, con);
                DataTable dtGtip = new DataTable();
                daGtip.Fill(dtGtip);
                urnAgaTipBox.DataSource = dtGtip;
                urnAgaTipBox.DisplayMember = "BOMDOCTYPE";
                urnAgaTipBox.ValueMember = "BOMDOCTYPE";
                urnAgaTipBox.DropDownStyle = ComboBoxStyle.DropDown;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }
        private void ValidateAndAddData(ComboBox comboBox, string columnName)
        {
            string checkQuery = $"SELECT COUNT(*) FROM BSMGRTRTBOMHEAD WHERE {columnName} = @userInput";
            con = new SqlConnection(ConnectionHelper.ConnectionString);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = checkQuery;

            string userInput = comboBox.Text;
            if (string.IsNullOrEmpty(userInput)) return;

            try
            {
                con.Open();
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@userInput", userInput);

                int count = (int)checkCmd.ExecuteScalar();
                if (count == 0)
                {
                    // Kullanıcı yanlışlıkla mevcut olmayan bir veri girmişse uyarı ver
                    MessageBox.Show($"{columnName} '{userInput}' tablodaki verilerle uyuşmuyor.");
                    comboBox.Text = string.Empty; // Kullanıcının yanlış girişini temizler
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }


        private void getBut_Click(object sender, EventArgs e)
        {
            // Temel SQL sorgusu
            string query = @"
            SELECT 
            COMCODE AS 'Firma', 
            BOMDOCTYPE AS 'Ürün Ağacı Tipi', 
            BOMDOCNUM AS 'Ürün Ağacı Numarası', 
            BOMDOCFROM AS 'Geçerlilik Başlangıç',
            BOMDOCUNTIL AS 'Geçerlilik Bitiş',
            MATDOCTYPE AS 'Malzeme Tipi', 
            MATDOCNUM AS 'Malzeme Numarası', 
            QUANTITY AS 'Temel Miktar',
            DRAWNUM AS 'Çizim Numarası',
            ISDELETED AS 'Silindi mi?',
            ISPASSIVE AS 'Pasif mi?'
            FROM BSMGRTRTBOMHEAD";

            // Filtreleme koşulları
            List<string> filters = new List<string>();

            // Firma filtresi
            if (!string.IsNullOrEmpty(firmbox.Text))
            {
                filters.Add("COMCODE = @COMCODE");
            }

            // Malzeme tipi filtresi
            if (!string.IsNullOrEmpty(urnagamalztipbox.Text))
            {
                filters.Add("MATDOCTYPE = @MATDOCTYPE");
            }

            // Malzeme numarası filtresi
            if (!string.IsNullOrEmpty(urnmalznumBox.Text))
            {
                filters.Add("MATDOCNUM = @MATDOCNUM");
            }

            // Ürün ağacı numarası filtresi
            if (!string.IsNullOrEmpty(urnAgaTipBox.Text))
            {
                filters.Add("BOMDOCTYPE = @BOMDOCTYPE");
            }

            if (!string.IsNullOrEmpty(textBox1.Text))
            {

                filters.Add("BOMDOCNUM = @BOMDOCNUM");

            }

            // Tarih aralığı filtresi
            if (dateTimePickerBaslangic.Value.Date != DateTime.MinValue.Date && dateTimePickerBitis.Value.Date != DateTime.MinValue.Date)
            {
                filters.Add("BOMDOCFROM >= @BOMDOCFROM AND BOMDOCUNTIL <= @BOMDOCUNTIL");
            }

            // Pasiflik kontrolü
            if (checkboxpas.Checked)
            {
                filters.Add("ISPASSIVE = 1");
            }

            // Silinmişlik kontrolü
            if (deletedlbl.Checked)
            {
                filters.Add("ISDELETED = 1");
            }

            // Filtreleri sorguya ekle
            if (filters.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", filters);
            }

            // SQL bağlantısı ve komutu
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Parametreleri ekle
                if (!string.IsNullOrEmpty(firmbox.Text))
                {
                    cmd.Parameters.AddWithValue("@COMCODE", firmbox.Text);
                }

                if (!string.IsNullOrEmpty(urnagamalztipbox.Text))
                {
                    cmd.Parameters.AddWithValue("@MATDOCTYPE", urnagamalztipbox.Text);
                }

                if (!string.IsNullOrEmpty(urnmalznumBox.Text))
                {
                    cmd.Parameters.AddWithValue("@MATDOCNUM", urnmalznumBox.Text);
                }

                if (!string.IsNullOrEmpty(urnAgaTipBox.Text))
                {
                    cmd.Parameters.AddWithValue("@BOMDOCTYPE", urnAgaTipBox.Text);
                }

                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    cmd.Parameters.AddWithValue("@BOMDOCNUM", textBox1.Text);
                }

                if (dateTimePickerBaslangic.Value.Date != DateTime.MinValue.Date && dateTimePickerBitis.Value.Date != DateTime.MinValue.Date)
                {
                    cmd.Parameters.AddWithValue("@BOMDOCFROM", dateTimePickerBaslangic.Value.Date);
                    cmd.Parameters.AddWithValue("@BOMDOCUNTIL", dateTimePickerBitis.Value.Date);
                }

                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dt = new DataSet();
                    da.Fill(dt);

                    // DataGridView'e sonuçları bağla
                    urnAgcData.DataSource = dt.Tables[0];

                    // Veri olmadığında kullanıcıyı bilgilendir
                    if (dt.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("Filtrelerinizle eşleşen bir veri bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }






        private void addBut_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınacak form verilerini okuma
            string comCode = firmbox.Text.Trim(); // Firma
            string matDocType = urnagamalztipbox.Text.Trim(); // Malzeme Tipi
            string matDocNum = urnmalznumBox.Text.Trim(); // Malzeme Numarası
            string bomDocType = urnAgaTipBox.Text.Trim(); // Ürün Ağacı Tipi
            string bomDocNum = textBox1.Text.Trim(); // Ürün Ağacı Numarası
            string drawNum = cizmikBox.Text.Trim(); // Çizim Numarası
            string quantityText = temelmikBox.Text.Trim(); // Temel Miktar
            DateTime bomDocFrom = dateTimePickerBaslangic.Value; // Başlangıç Tarihi
            DateTime bomDocUntil = dateTimePickerBitis.Value; // Bitiş Tarihi

            // Alanların boş olup olmadığını kontrol etme
            if (string.IsNullOrWhiteSpace(comCode) ||
                string.IsNullOrWhiteSpace(matDocType) ||
                string.IsNullOrWhiteSpace(matDocNum) ||
                string.IsNullOrWhiteSpace(bomDocType) ||
                string.IsNullOrWhiteSpace(bomDocNum) || // Ürün Ağacı Numarası zorunlu
                string.IsNullOrWhiteSpace(drawNum) || // Çizim Numarası zorunlu
                string.IsNullOrWhiteSpace(quantityText))
            {
                MessageBox.Show("Tüm alanların doldurulması gerekiyor!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Temel miktarı kontrol etme ve dönüştürme
            if (!decimal.TryParse(quantityText, out decimal quantity))
            {
                MessageBox.Show("Geçerli bir temel miktar girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tarihler arasındaki mantık hatasını kontrol etme
            if (bomDocFrom >= bomDocUntil)
            {
                MessageBox.Show("Başlangıç tarihi, bitiş tarihinden önce olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // CheckBox değerleri
            bool isPassive = checkboxpas.Checked; // Pasif mi?
            bool isDeleted = deletedlbl.Checked; // Silindi mi?

            // Veritabanı sorgusu
            string query = @"
    INSERT INTO BSMGRTRTBOMHEAD
    (COMCODE, MATDOCTYPE, MATDOCNUM, BOMDOCTYPE, BOMDOCNUM, BOMDOCFROM, BOMDOCUNTIL, DRAWNUM, QUANTITY, ISPASSIVE, ISDELETED)
    VALUES
    (@COMCODE, @MATDOCTYPE, @MATDOCNUM, @BOMDOCTYPE, @BOMDOCNUM, @BOMDOCFROM, @BOMDOCUNTIL, @DRAWNUM, @QUANTITY, @ISPASSIVE, @ISDELETED)";

            // Bağlantı nesnesi
            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            using (cmd = new SqlCommand(query, con))
            {
                // Parametreleri ekleme
                cmd.Parameters.Add("@COMCODE", SqlDbType.VarChar).Value = comCode;
                cmd.Parameters.Add("@MATDOCTYPE", SqlDbType.VarChar).Value = matDocType;
                cmd.Parameters.Add("@MATDOCNUM", SqlDbType.VarChar).Value = matDocNum;
                cmd.Parameters.Add("@BOMDOCTYPE", SqlDbType.VarChar).Value = bomDocType;
                cmd.Parameters.Add("@BOMDOCNUM", SqlDbType.VarChar).Value = bomDocNum;
                cmd.Parameters.Add("@BOMDOCFROM", SqlDbType.DateTime).Value = bomDocFrom;
                cmd.Parameters.Add("@BOMDOCUNTIL", SqlDbType.DateTime).Value = bomDocUntil;
                cmd.Parameters.Add("@DRAWNUM", SqlDbType.VarChar).Value = drawNum;
                cmd.Parameters.Add("@QUANTITY", SqlDbType.Decimal).Value = quantity;
                cmd.Parameters.Add("@ISPASSIVE", SqlDbType.Bit).Value = isPassive;
                cmd.Parameters.Add("@ISDELETED", SqlDbType.Bit).Value = isDeleted;

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Tüm alanları temizle
                        ClearFormFields();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt eklenemedi. Lütfen girdiğiniz bilgileri kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException sqlEx)
                {
                    string errorMessage = "Bir veritabanı hatası oluştu. Lütfen tekrar deneyin.";
                    // SQL hata koduna göre mesajları özelleştir
                    switch (sqlEx.Number)
                    {
                        case 2627: // Primary Key violation
                            errorMessage = "Böyle bir ürün ağacı zaten kayıtlı. Lütfen farklı bir numara veya tarih girin.";
                            break;
                        case 547: // Foreign Key violation
                            errorMessage = "Girilen bilgiler veritabanındaki diğer kayıtlarla uyumlu değil. Lütfen kontrol edin.";
                            break;

                        default:
                            errorMessage = $"Veritabanı hatası: {sqlEx.Message}";
                            break;
                    }
                    MessageBox.Show(errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu. Lütfen girdiğiniz bilgileri kontrol edin ve tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        // Form alanlarını temizleme metodu
        private void ClearFormFields()
        {
            firmbox.Text = string.Empty;
            urnagamalztipbox.Text = string.Empty;
            urnmalznumBox.Text = string.Empty;
            urnAgaTipBox.Text = string.Empty;
            textBox1.Text = string.Empty;
            cizmikBox.Text = string.Empty;
            temelmikBox.Text = string.Empty;
            dateTimePickerBaslangic.Value = DateTime.Now;
            dateTimePickerBitis.Value = DateTime.Now;
            checkboxpas.Checked = false;
            deletedlbl.Checked = false;
        }


        private void basTarTxtBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void DelBut_Click(object sender, EventArgs e)
        {
            // Silme butonuna basıldığında çalışacak kod
            string bomDocNum = textBox1.Text; // Silinecek BOMDOCNUM değeri

            if (string.IsNullOrWhiteSpace(bomDocNum))
            {
                MessageBox.Show("Silinecek Ürün Ağacı Numarasını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Veritabanı sorgusu
            string query = "DELETE FROM BSMGRTRTBOMHEAD WHERE BOMDOCNUM = @BOMDOCNUM";

            // Bağlantı nesnesi
            con = new SqlConnection(ConnectionHelper.ConnectionString);
            cmd = new SqlCommand(query, con);

            // Parametreyi ekleme
            cmd.Parameters.AddWithValue("@BOMDOCNUM", bomDocNum);

            try
            {
                using (con)
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Silinecek kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda mesaj gösterme
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void duzBut_Click(object sender, EventArgs e)
        {
            string bomDocNum = textBox1.Text; // Ürün Ağacı Numarası alınır

            if (string.IsNullOrEmpty(bomDocNum))
            {
                MessageBox.Show("Lütfen bir Ürün Ağacı Numarası giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM BSMGRTRTBOMHEAD WHERE BOMDOCNUM = @BOMDOCNUM";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BOMDOCNUM", bomDocNum);

                try
                {
                    con.Open();
                    int recordExists = (int)cmd.ExecuteScalar();

                    if (recordExists > 0)
                    {
                        // Kayıt bulundu, urunAgaciKartEdit formuna geçiş yap ve parametre ile aç
                        urunAgaciKartEdit UrunAgaciForm = new urunAgaciKartEdit(bomDocNum);
                        UrunAgaciForm.Show();
                    }
                    else
                    {
                        // Kayıt bulunamadı
                        MessageBox.Show("Belirtilen Ürün Ağacı Numarası için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void getAll_Click(object sender, EventArgs e)
        {

            string query = @"
                        SELECT 
							COMCODE AS 'Firma', 
                            BOMDOCTYPE AS 'Ürün Ağacı Tipi', 
                            BOMDOCNUM AS 'Ürün Ağacı Numarası', 
                            BOMDOCFROM AS 'Geçerlilik Başlangıç',
							BOMDOCUNTIL	AS 'Geçerlilik Bitiş',
                            MATDOCTYPE AS 'Malzeme Tipi', 
                            MATDOCNUM AS 'Malzeme Numarası', 
						    QUANTITY AS 'Temel Miktar',
						    DRAWNUM AS 'Çizim Numarası',
							ISDELETED AS 'Silindi mi?',
							ISPASSIVE AS 'Pasif mi?'
                        FROM BSMGRTRTBOMHEAD";
            con = new SqlConnection(ConnectionHelper.ConnectionString);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            try
            {
                using (con)
                {
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dt = new DataSet();
                    da.Fill(dt);

                    // DataGridView'e verileri bağlama
                    urnAgcData.DataSource = dt.Tables[0];
                }
            }
            catch (Exception ex)
            {
                // Hata mesajı
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }
    }
    
}