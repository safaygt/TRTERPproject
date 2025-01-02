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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TRTERPproject
{
    public partial class urunPatlatma : Form
    {
        public urunPatlatma()
        {
            InitializeComponent();
        }

        public string? UrunAgaciNumarasi { get; internal set; }

        private void urunPatlatma_Load(object sender, EventArgs e)
        {// Ürün Ağacı Numarası kontrolü
            if (string.IsNullOrEmpty(UrunAgaciNumarasi))
            {
                MessageBox.Show("Ürün ağacı numarası alınamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // İlk sorgu: BOMHEAD tablosundan ürün ağacı bilgilerini çek
            string queryHead = @"
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
                    BSMGRTRTBOMHEAD H
                WHERE 
                    H.BOMDOCNUM = @BOMDOCNUM";

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(queryHead, con))
                    {
                        cmd.Parameters.AddWithValue("@BOMDOCNUM", UrunAgaciNumarasi);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        // İlk DataGridView'e verileri bağlama
                        secilenPatlatma.DataSource = ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // BOMCONTENT tablosundan verileri çekmek için SQL sorgusu
            string queryFilteredData = @"
                SELECT 
                    C.COMCODE AS 'Firma', 
                    C.BOMDOCTYPE AS 'Ürün Ağacı Tipi', 
                    C.BOMDOCNUM AS 'Ürün Ağacı Numarası', 
                    C.BOMDOCFROM AS 'Geçerlilik Başlangıç',
                    C.BOMDOCUNTIL AS 'Geçerlilik Bitiş',
                    C.MATDOCTYPE AS 'Malzeme Tipi', 
                    C.MATDOCNUM AS 'Malzeme Numarası', 
                    C.CONTENTNUM AS 'İçerik Numarası',
                    C.COMPONENT AS 'Bileşen Kodu',
                    C.COMPBOMDOCTYPE AS 'Kalem Ağacı Tipi',
                    C.COMPBOMDOCNUM AS 'Kalem Ağacı Kodu',
                    C.QUANTITY AS 'Bileşen Miktarı'
                FROM 
                    BSMGRTRTBOMCONTENT C
                WHERE 
                    C.BOMDOCNUM = @BOMDOCNUM
                ORDER BY 
                    C.COMPBOMDOCNUM"; // COMBOMDOCNUM değerine göre sıralama yapılır

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(queryFilteredData, con))
                    {
                        // BOMDOCNUM değerini parametre olarak ekle
                        cmd.Parameters.AddWithValue("@BOMDOCNUM", UrunAgaciNumarasi);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        // Eğer hiçbir sonuç yoksa kullanıcıya bilgi göster
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("Bu sorgu için herhangi bir sonuç bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // DataGridView'e verileri bağlama
                        urunPatlatmaData.DataSource = ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getBut_Click(object sender, EventArgs e)
        {
            // BOMCONTENT tablosundan verileri çekmek için SQL sorgusu
            string queryFilteredData = @"
                SELECT 
                    C.COMCODE AS 'Firma', 
                    C.BOMDOCTYPE AS 'Ürün Ağacı Tipi', 
                    C.BOMDOCNUM AS 'Ürün Ağacı Numarası', 
                    C.BOMDOCFROM AS 'Geçerlilik Başlangıç',
                    C.BOMDOCUNTIL AS 'Geçerlilik Bitiş',
                    C.MATDOCTYPE AS 'Malzeme Tipi', 
                    C.MATDOCNUM AS 'Malzeme Numarası', 
                    C.CONTENTNUM AS 'İçerik Numarası',
                    C.COMPONENT AS 'Bileşen Kodu',
                    C.COMPBOMDOCTYPE AS 'Kalem Ağacı Tipi',
                    C.COMPBOMDOCNUM AS 'Kalem Ağacı Kodu',
                    C.QUANTITY AS 'Bileşen Miktarı'
                FROM 
                    BSMGRTRTBOMCONTENT C
                WHERE 
                    C.BOMDOCNUM = @BOMDOCNUM
                ORDER BY 
                    C.COMPBOMDOCNUM"; // COMBOMDOCNUM değerine göre sıralama yapılır

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(queryFilteredData, con))
                    {
                        // BOMDOCNUM değerini parametre olarak ekle
                        cmd.Parameters.AddWithValue("@BOMDOCNUM", UrunAgaciNumarasi);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        // Eğer hiçbir sonuç yoksa kullanıcıya bilgi göster
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("Bu sorgu için herhangi bir sonuç bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // DataGridView'e verileri bağlama
                        urunPatlatmaData.DataSource = ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void duzBut_Click(object sender, EventArgs e)
        {
            // DataGridView'den seçilen satırı kontrol et
            if (urunPatlatmaData.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = urunPatlatmaData.SelectedRows[0];

                // Yeni bir edit form oluştur ve seçilen veriyi aktar
                urunPatlatmaEdit UrunPatlatmaEdit = new urunPatlatmaEdit();

                // DataGridView'den alınan verileri, UrunAgaciForm'a aktar
                UrunPatlatmaEdit.Firma = selectedRow.Cells["Firma"].Value.ToString();
                UrunPatlatmaEdit.UrunAgaciTipi = selectedRow.Cells["Ürün Ağacı Tipi"].Value.ToString();
                UrunPatlatmaEdit.UrunAgaciNumarasi = selectedRow.Cells["Ürün Ağacı Numarası"].Value.ToString();
                UrunPatlatmaEdit.MalzemeTipi = selectedRow.Cells["Malzeme Tipi"].Value.ToString();
                UrunPatlatmaEdit.MalzemeNumarasi = selectedRow.Cells["Malzeme Numarası"].Value.ToString();
                UrunPatlatmaEdit.TemelMiktar = Convert.ToDecimal(selectedRow.Cells["Bileşen Miktarı"].Value);
                UrunPatlatmaEdit.GecerlilikBaslangic = Convert.ToDateTime(selectedRow.Cells["Geçerlilik Başlangıç"].Value);
                UrunPatlatmaEdit.GecerlilikBitis = Convert.ToDateTime(selectedRow.Cells["Geçerlilik Bitiş"].Value);

                UrunPatlatmaEdit.IcerikNumarasi = selectedRow.Cells["İçerik Numarası"].Value.ToString();
                UrunPatlatmaEdit.BilesenKodu = selectedRow.Cells["Bileşen Kodu"].Value.ToString();
                UrunPatlatmaEdit.KalemUrunAgaciNumarasi = selectedRow.Cells["Kalem Ağacı Tipi"].Value.ToString();
                UrunPatlatmaEdit.KalemUrunAgaciTipi = selectedRow.Cells["Kalem Ağacı Kodu"].Value.ToString();

                // Edit formu göster
                UrunPatlatmaEdit.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için bir satır seçin.");
            }
        }

        private void DelBut_Click(object sender, EventArgs e)
        {

            // DataGridView'den seçilen satırı kontrol et
            if (urunPatlatmaData.SelectedRows.Count > 0)
            {
                // Seçilen satırdaki "Malzeme Numarası" bilgisini al
                DataGridViewRow selectedRow = urunPatlatmaData.SelectedRows[0];
                string contentnum = selectedRow.Cells["İçerik Numarası"].Value.ToString(); // doğru sütunu kontrol edin

                // Kullanıcıdan onay al
                DialogResult dialogResult = MessageBox.Show(
                    $"İçerik Numarası {contentnum} olan veriyi silmek istediğinize emin misiniz?",
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
                            string deleteFromMatHead = "DELETE FROM BSMGRTRTBOMCONTENT WHERE CONTENTNUM = @CONTENTNUM";
                            SqlCommand cmdMatHead = new SqlCommand(deleteFromMatHead, con);
                            cmdMatHead.Parameters.AddWithValue("@CONTENTNUM", contentnum);
                            cmdMatHead.ExecuteNonQuery();

                            // Başarılı silme mesajı
                            MessageBox.Show("Seçilen veri başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // DataGridView'i güncelle
                            urunPatlatmaData.Rows.Remove(selectedRow);
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
    }
}

