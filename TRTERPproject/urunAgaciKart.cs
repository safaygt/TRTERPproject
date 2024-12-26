using System.Data;
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
                firmbox.DropDownStyle = ComboBoxStyle.DropDown; // Yeni veri girilebilir

                string queryMtip = "SELECT DISTINCT DOCTYPE FROM BSMGRTRTMAT001"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daMtip = new SqlDataAdapter(queryMtip, con);
                DataTable dtMtip = new DataTable();
                daMtip.Fill(dtMtip);
                urnagamalztipbox.DataSource = dtMtip;
                urnagamalztipbox.DisplayMember = "DOCTYPE";
                urnagamalztipbox.ValueMember = "DOCTYPE";
                urnagamalztipbox.DropDownStyle = ComboBoxStyle.DropDown;

                string queryUrnMalNum = "SELECT DISTINCT MATDOCNUM FROM BSMGRTRTMATHEAD"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daUrnMalNum = new SqlDataAdapter(queryUrnMalNum, con);
                DataTable dtUrnMalNum = new DataTable();
                daUrnMalNum.Fill(dtUrnMalNum);
                urnmalzemenumBox.DataSource = dtUrnMalNum;
                urnmalzemenumBox.DisplayMember = "MATDOCNUM";
                urnmalzemenumBox.ValueMember = "MATDOCNUM";
                urnmalzemenumBox.DropDownStyle = ComboBoxStyle.DropDown;

                string queryGtip = "SELECT DISTINCT DOCTYPE FROM BSMGRTRTBOM001"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daGtip = new SqlDataAdapter(queryGtip, con);
                DataTable dtGtip = new DataTable();
                daGtip.Fill(dtGtip);
                urnAgaTipBox.DataSource = dtGtip;
                urnAgaTipBox.DisplayMember = "DOCTYPE";
                urnAgaTipBox.ValueMember = "DOCTYPE";
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
            // Sorgu, hem BSMGRTRTBOMHEAD hem de BSMGRTRTBOMCONTENT tablolarını kontrol eder
            string checkQuery = $@"
            SELECT COUNT(*) 
            FROM BSMGRTRTBOMHEAD H
            LEFT JOIN BSMGRTRTBOMCONTENT C ON H.BOMDOCTYPE = C.BOMDOCTYPE AND H.BOMDOCNUM = C.BOMDOCNUM
            WHERE H.{columnName} = @userInput OR C.{columnName} = @userInput";

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
            urunAgaciKartAdd urunAgaciKartAdd = new urunAgaciKartAdd(bomDocNum);
            urunAgaciKartAdd.Show();
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

                            // BSMGRTRTBOMCONTENT tablosundan silme sorgusu
                            string deleteFromMatText = "DELETE FROM BSMGRTRTBOMCONTENT WHERE BOMDOCNUM = @BOMDOCNUM";
                            SqlCommand cmdMatText = new SqlCommand(deleteFromMatText, con);
                            cmdMatText.Parameters.AddWithValue("@BOMDOCNUM", bomDocNum);
                            cmdMatText.ExecuteNonQuery();

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
                urunAgaciForm.IcerikNumarasi = selectedRow.Cells["İçerik Numarası"].Value.ToString();
                urunAgaciForm.GecerlilikBaslangic = Convert.ToDateTime(selectedRow.Cells["Geçerlilik Başlangıç"].Value);
                urunAgaciForm.GecerlilikBitis = Convert.ToDateTime(selectedRow.Cells["Geçerlilik Bitiş"].Value);
                urunAgaciForm.IsDeleted = Convert.ToBoolean(selectedRow.Cells["Silindi mi?"].Value);
                urunAgaciForm.IsPassive = Convert.ToBoolean(selectedRow.Cells["Pasif mi?"].Value);
                urunAgaciForm.BilesenKodu = selectedRow.Cells["Bileşen Kodu"].Value.ToString();
                urunAgaciForm.KalemUrunAgaciTipi = selectedRow.Cells["Kalem Ürün Ağacı Tipi"].Value.ToString();
                urunAgaciForm.KalemUrunAgaciNumarasi = selectedRow.Cells["Kalem Ürün Ağacı Numarası"].Value.ToString();
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
                        H.DRAWNUM AS 'Çizim Numarası',

                        C.CONTENTNUM AS 'İçerik Numarası',
                        C.COMPONENT AS 'Bileşen Kodu',
                        C.COMPBOMDOCTYPE AS 'Kalem Ürün Ağacı Tipi',
                        C.COMPBOMDOCNUM AS 'Kalem Ürün Ağacı Numarası',

                        D.DOCTYPE AS 'Ürün Ağacı Tipi',
                        D.DOCTYPETEXT AS 'Ürün Ağacı Tipi Açıklaması'
                    FROM 
                        BSMGRTRTBOMHEAD H
                    INNER JOIN 
                        BSMGRTRTBOMCONTENT C 
                        ON H.BOMDOCNUM = C.BOMDOCNUM 
                    INNER JOIN 
                        BSMGRTRTBOM001 D 
                        ON H.BOMDOCTYPE = D.DOCTYPE;



                ";

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
    }

}