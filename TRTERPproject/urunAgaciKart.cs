using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class urunAgaciKart : Form
    {
        private string bomDocNum;
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
            urnmalzemenumBox.Leave += (s, e) => ValidateAndAddData(urnmalzemenumBox, "MATDOCNUM");
            urnAgaTipBox.Leave += (s, e) => ValidateAndAddData(urnAgaTipBox, "BOMDOCTYPE");
        }
        private void LoadComboBoxData()
        {

            try
            {
                con.Open();

                // Firma verilerini doldur
                string queryFirma = "SELECT DISTINCT COMCODE FROM BSMGRTRTGEN001";
                SqlDataAdapter daFirma = new SqlDataAdapter(queryFirma, con);
                DataTable dtFirma = new DataTable();
                daFirma.Fill(dtFirma);
                firmbox.DataSource = dtFirma;
                firmbox.DisplayMember = "COMCODE";
                firmbox.ValueMember = "COMCODE";

                string queryMtip = "SELECT DISTINCT DOCTYPE FROM BSMGRTRTMAT001"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daMtip = new SqlDataAdapter(queryMtip, con);
                DataTable dtMtip = new DataTable();
                daMtip.Fill(dtMtip);
                urnagamalztipbox.DataSource = dtMtip;
                urnagamalztipbox.DisplayMember = "DOCTYPE";
                urnagamalztipbox.ValueMember = "DOCTYPE";

                string queryUrnMalNum = "SELECT DISTINCT MATDOCNUM FROM BSMGRTRTMATHEAD"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daUrnMalNum = new SqlDataAdapter(queryUrnMalNum, con);
                DataTable dtUrnMalNum = new DataTable();
                daUrnMalNum.Fill(dtUrnMalNum);
                urnmalzemenumBox.DataSource = dtUrnMalNum;
                urnmalzemenumBox.DisplayMember = "MATDOCNUM";
                urnmalzemenumBox.ValueMember = "MATDOCNUM";

                string queryGtip = "SELECT DISTINCT DOCTYPE FROM BSMGRTRTBOM001"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daGtip = new SqlDataAdapter(queryGtip, con);
                DataTable dtGtip = new DataTable();
                daGtip.Fill(dtGtip);
                urnAgaTipBox.DataSource = dtGtip;
                urnAgaTipBox.DisplayMember = "DOCTYPE";
                urnAgaTipBox.ValueMember = "DOCTYPE";
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
            
        }


        private void getBut_Click(object sender, EventArgs e)
        {
            // Temel SQL sorgusu
            string query = @"
            SELECT 

                        H.COMCODE AS 'Firma', 
                        H.BOMDOCTYPE AS 'Ürün Ağacı Tipi', 
                        H.BOMDOCNUM AS 'Ürün Ağacı Numarası', 
                        H.BOMDOCFROM AS 'Geçerlilik Başlangıç',
                        H.BOMDOCUNTIL AS 'Geçerlilik Bitiş',
                        H.MATDOCTYPE AS 'Malzeme Tipi', 
                        H.MATDOCNUM AS 'Malzeme Numarası', 
                        H.QUANTITY AS 'Temel Miktar',
                        H.ISDELETED AS 'Silindi mi?',
                        H.ISPASSIVE AS 'Pasif mi?',
                        H.DRAWNUM AS 'Çizim Numarası'
                    FROM BSMGRTRTBOMHEAD H ";


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
            if (!string.IsNullOrEmpty(urnmalzemenumBox.Text))
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

                if (!string.IsNullOrEmpty(urnmalzemenumBox.Text))
                {
                    cmd.Parameters.AddWithValue("@MATDOCNUM", urnmalzemenumBox.Text);
                }

                if (!string.IsNullOrEmpty(urnAgaTipBox.Text))
                {
                    cmd.Parameters.AddWithValue("@BOMDOCTYPE", urnAgaTipBox.Text);
                }

                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    cmd.Parameters.AddWithValue("@BOMDOCNUM", textBox1.Text);
                }


                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    cmd.Parameters.AddWithValue("@QUANTITY", temelmikBox.Text);
                }

                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    cmd.Parameters.AddWithValue("@DRAWNUM", cizmikBox.Text);
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
            // DataGridView'den seçilen satırı kontrol et
            if (urnAgcData.SelectedRows.Count > 0)
            {
                // Seçilen satırı al
                DataGridViewRow selectedRow = urnAgcData.SelectedRows[0];

                // BOMDOCNUM değerini al
                string bomDocNum = selectedRow.Cells["Ürün Ağacı Numarası"].Value?.ToString();

                // Yeni bir urunAgaciDetayAdd formu oluştur ve BOMDOCNUM değerini aktar
                urunAgaciDetayAdd UrunAgaciDetayAdd = new urunAgaciDetayAdd
                {
                    BOMDocNum = bomDocNum // Property'yi ayarla
                };

                // Edit formu göster
                UrunAgaciDetayAdd.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void basTarTxtBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void DelBut_Click(object sender, EventArgs e)
        {

            // DataGridView'den seçilen satırı kontrol et
            if (urnAgcData.SelectedRows.Count > 0)
            {
                // Seçilen satırdaki "Malzeme Numarası" bilgisini al
                DataGridViewRow selectedRow = urnAgcData.SelectedRows[0];
                string bomDocNum = selectedRow.Cells["Ürün Ağacı Numarası"].Value.ToString(); // doğru sütunu kontrol edin

                // Kullanıcıdan onay al
                DialogResult dialogResult = MessageBox.Show(
                    $"Ürün Ağacı Kodu {bomDocNum} olan veriyi silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        // Bağlantı dizesinin null veya boş olmadığını kontrol et
                        if (string.IsNullOrEmpty(ConnectionHelper.ConnectionString))
                        {
                            MessageBox.Show("Bağlantı dizesi doğru şekilde ayarlanmamış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Bağlantıyı oluştur ve aç
                        using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                        {
                            con.Open();

                            // BSMGRTRTMBOMHEAD tablosundan silme sorgusu
                            string deleteFromMatHead = "DELETE FROM BSMGRTRTBOMHEAD WHERE BOMDOCNUM = @BOMDOCNUM";
                            SqlCommand cmdMatHead = new SqlCommand(deleteFromMatHead, con);
                            cmdMatHead.Parameters.AddWithValue("@BOMDOCNUM", bomDocNum);
                            cmdMatHead.ExecuteNonQuery();

                            // Başarılı silme mesajı
                            MessageBox.Show("Seçilen veri başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // DataGridView'i güncelle
                            urnAgcData.Rows.Remove(selectedRow);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void duzBut_Click(object sender, EventArgs e)
        {

            // DataGridView'den seçilen satırı kontrol et
            if (urnAgcData.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = urnAgcData.SelectedRows[0];

                // Yeni bir edit form oluştur ve seçilen veriyi aktar
                urunAgaciKartEdit urunAgaciForm = new urunAgaciKartEdit();

                // DataGridView'den alınan verileri, UrunAgaciForm'a aktar
                urunAgaciForm.Firma = selectedRow.Cells["Firma"].Value.ToString();
                urunAgaciForm.UrunAgaciTipi = selectedRow.Cells["Ürün Ağacı Tipi"].Value.ToString();
                urunAgaciForm.UrunAgaciNumarasi = selectedRow.Cells["Ürün Ağacı Numarası"].Value.ToString();
                urunAgaciForm.MalzemeTipi = selectedRow.Cells["Malzeme Tipi"].Value.ToString();
                urunAgaciForm.MalzemeNumarasi = selectedRow.Cells["Malzeme Numarası"].Value.ToString();
                urunAgaciForm.TemelMiktar = Convert.ToDecimal(selectedRow.Cells["Temel Miktar"].Value);
                urunAgaciForm.CizimNumarasi = selectedRow.Cells["Çizim Numarası"].Value.ToString();
                urunAgaciForm.GecerlilikBaslangic = Convert.ToDateTime(selectedRow.Cells["Geçerlilik Başlangıç"].Value);
                urunAgaciForm.GecerlilikBitis = Convert.ToDateTime(selectedRow.Cells["Geçerlilik Bitiş"].Value);
                urunAgaciForm.IsDeleted = Convert.ToBoolean(selectedRow.Cells["Silindi mi?"].Value);
                urunAgaciForm.IsPassive = Convert.ToBoolean(selectedRow.Cells["Pasif mi?"].Value);
                // Edit formu göster
                urunAgaciForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için bir satır seçin.");
            }
        }

        private void getAll_Click(object sender, EventArgs e)
        {

            string query = @"
                                   SELECT 
                        H.COMCODE AS 'Firma', 
                        H.BOMDOCTYPE AS 'Ürün Ağacı Tipi', 
                        H.BOMDOCNUM AS 'Ürün Ağacı Numarası', 
                        H.BOMDOCFROM AS 'Geçerlilik Başlangıç',
                        H.BOMDOCUNTIL AS 'Geçerlilik Bitiş',
                        H.MATDOCTYPE AS 'Malzeme Tipi', 
                        H.MATDOCNUM AS 'Malzeme Numarası', 
                        H.QUANTITY AS 'Temel Miktar',
                        H.ISDELETED AS 'Silindi mi?',
                        H.ISPASSIVE AS 'Pasif mi?',
                        H.DRAWNUM AS 'Çizim Numarası'
                    FROM 
                        BSMGRTRTBOMHEAD H";

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

        private void urnAgaTipBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void urunAgaciKart_Load(object sender, EventArgs e)
        {

        }

        private void firmbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void urnagamalztipbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void urunPatlat_Click(object sender, EventArgs e)
        {
            // DataGridView'den seçilen satırı kontrol et
            if (urnAgcData.SelectedRows.Count > 0)
            {
                // Seçilen satırı al
                DataGridViewRow selectedRow = urnAgcData.SelectedRows[0];

                // Ürün Ağacı Numarası sütununu al (sütun adı veya indeks ile erişebilirsiniz)
                string urunAgaciNumarası = selectedRow.Cells["Ürün Ağacı Numarası"].Value.ToString();

                // Yeni bir form oluştur ve değeri aktar
                urunPatlatma urunPatlatmaForm = new urunPatlatma
                {
                    UrunAgaciNumarasi = urunAgaciNumarası // Değer atanıyor
                };

                // Yeni formu aç
                urunPatlatmaForm.ShowDialog();
            }
            else
            {
                // Hiçbir satır seçilmediyse kullanıcıyı uyar
                MessageBox.Show("Lütfen ürün ağacını patlatmak için bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void saveAgac_Click(object sender, EventArgs e)
        {
            string comCode = firmbox.Text.Trim();
            string bomDoctype = urnAgaTipBox.Text.Trim();
            string bomDocnum = textBox1.Text.Trim();
            string bomDocFrom = dateTimePickerBaslangic.Text.Trim();
            string bomDocUntil = dateTimePickerBitis.Text.Trim();
            string matDoctype = urnagamalztipbox.Text.Trim();
            string matDocnum = urnmalzemenumBox.Text.Trim();
            string quantity = temelmikBox.Text.Trim();
            int isDeleted = deletedlbl.Checked ? 1 : 0;
            int isPassive = checkboxpas.Checked ? 1 : 0;
            string drawnum = cizmikBox.Text.Trim();

            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(bomDoctype) || string.IsNullOrEmpty(bomDocnum) || string.IsNullOrEmpty(bomDocFrom) ||
                string.IsNullOrEmpty(bomDocUntil) || string.IsNullOrEmpty(matDoctype) || string.IsNullOrEmpty(matDocnum) || string.IsNullOrEmpty(quantity))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!DateTime.TryParse(bomDocFrom, out DateTime parsedStartDate) || !DateTime.TryParse(bomDocUntil, out DateTime parsedLastDate))
                {
                    MessageBox.Show("Tarih formatı hatalı. Lütfen yyyy-MM-dd formatında giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Burada ConnectionString'in doğru olduğundan emin olmak için debug ekleyin
                string connectionString = ConnectionHelper.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Bağlantı dizesi hatalı. Lütfen bağlantı ayarlarını kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SqlConnection con = new SqlConnection(connectionString); // Bağlantıyı başlat
                con.Open();

                string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTBOMHEAD WHERE BOMDOCNUM = @BOMDOCNUM";
                using (SqlCommand command = new SqlCommand(checkQuery, con))
                {
                    command.Parameters.AddWithValue("@BOMDOCNUM", bomDoctype);
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Bu Ürün Ağacı Numarası zaten mevcut. Lütfen başka Ürün Ağacı Numarası giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string insertQuery = @"INSERT INTO BSMGRTRTBOMHEAD 
                     (COMCODE, BOMDOCTYPE, BOMDOCNUM, BOMDOCFROM, BOMDOCUNTIL, MATDOCTYPE, MATDOCNUM, QUANTITY, ISDELETED, ISPASSIVE, DRAWNUM) 
                     VALUES (@COMCODE, @BOMDOCTYPE, @BOMDOCNUM, @BOMDOCFROM, @BOMDOCUNTIL, @MATDOCTYPE, @MATDOCNUM, @QUANTITY, @ISDELETED, @ISPASSIVE, @DRAWNUM)";

                using (SqlCommand command = new SqlCommand(insertQuery, con))
                {
                    command.Parameters.AddWithValue("@COMCODE", comCode);
                    command.Parameters.AddWithValue("@BOMDOCTYPE", bomDoctype);
                    command.Parameters.AddWithValue("@BOMDOCNUM", bomDocnum);
                    command.Parameters.AddWithValue("@BOMDOCFROM", parsedStartDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@BOMDOCUNTIL", parsedLastDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@MATDOCTYPE", matDoctype);
                    command.Parameters.AddWithValue("@MATDOCNUM", matDocnum);
                    command.Parameters.AddWithValue("@QUANTITY", quantity);
                    command.Parameters.AddWithValue("@ISDELETED", isDeleted);
                    command.Parameters.AddWithValue("@ISPASSIVE", isPassive);
                    command.Parameters.AddWithValue("@DRAWNUM", drawnum);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFormFields();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt eklenemedi. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close(); // Bağlantıyı kapat
            }
        }

        // Form alanlarını temizleme metodu
        private void ClearFormFields()
        {
            firmbox.Text = string.Empty; // Firma kutusunu temizle
            urnAgaTipBox.Text = string.Empty; // Ürün Ağacı Tipi kutusunu temizle
            textBox1.Text = string.Empty; // Ürün Ağacı Numarası kutusunu temizle
            urnagamalztipbox.Text = string.Empty; // Malzeme Tipi kutusunu temizle
            urnmalzemenumBox.Text = string.Empty; // Malzeme Numarası kutusunu temizle
            cizmikBox.Text = string.Empty; // Çizim Numarası kutusunu temizle
            temelmikBox.Text = string.Empty; // Temel Miktar kutusunu temizle

            dateTimePickerBaslangic.Value = DateTime.Now; // Başlangıç tarihi bugüne ayarla
            dateTimePickerBitis.Value = DateTime.Now; // Bitiş tarihi bugüne ayarla

            checkboxpas.Checked = false; // Pasiflik durumu kutusunu temizle
            deletedlbl.Checked = false; // Silinmiş durumu kutusunu temizle


        }

    }

}