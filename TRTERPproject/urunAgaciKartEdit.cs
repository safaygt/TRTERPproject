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
    public partial class urunAgaciKartEdit : Form
    {
        private string bomDocNum;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

        // Özellikler
        public string Firma { get; set; }
        public string UrunAgaciTipi { get; set; }
        public string UrunAgaciNumarasi { get; set; }
        public string MalzemeTipi { get; set; }
        public string MalzemeNumarasi { get; set; }
        public decimal TemelMiktar { get; set; }
        public string CizimNumarasi { get; set; }
        public DateTime GecerlilikBaslangic { get; set; }
        public DateTime GecerlilikBitis { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPassive { get; set; }
        public string BilesenKodu { get; set; }
        public string KalemUrunAgaciTipi { get; set; }
        public string KalemUrunAgaciNumarasi { get; set; }
        public string? IcerikNumarasi { get; internal set; }

        public urunAgaciKartEdit()
        {
            InitializeComponent();
            this.bomDocNum = bomDocNum;
        }

        private void urunAgaciKartEdit_Load(object sender, EventArgs e)
        {
            // Formdaki alanları özelliklerle doldurma
            firmbox.Text = Firma;
            urnAgaTipBox.Text = UrunAgaciTipi;
            textBox1.Text = UrunAgaciNumarasi;
            urnagamalztipbox.Text = MalzemeTipi;
            urnmalzemenumBox.Text = MalzemeNumarasi;
            cizmikBox.Text = CizimNumarasi;
            temelmikBox.Text = TemelMiktar.ToString();

            baslangicDateTimePicker.Value = GecerlilikBaslangic;
            bitisDateTimePicker.Value = GecerlilikBitis;

            checkboxpas.Checked = IsPassive;
            deletedlbl.Checked = IsDeleted;

            // Combobox verilerini doldur
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    con.Open();

                    // Firma verileri
                    LoadComboBoxData("SELECT DISTINCT COMCODE FROM BSMGRTRTGEN001", firmbox, "COMCODE");

                    // Malzeme Tipi
                    LoadComboBoxData("SELECT DISTINCT DOCTYPE FROM BSMGRTRTMAT001", urnagamalztipbox, "DOCTYPE");

                    // Ürün Ağacı Tipi
                    LoadComboBoxData("SELECT DISTINCT DOCTYPE FROM BSMGRTRTBOM001", urnAgaTipBox, "DOCTYPE");

                    LoadComboBoxData("SELECT DISTINCT MATDOCNUM FROM BSMGRTRTMATHEAD", urnmalzemenumBox, "MATDOCNUM");
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxData(string query, ComboBox comboBox, string displayMember)
        {
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox.DataSource = dt;
                comboBox.DisplayMember = displayMember;
                comboBox.ValueMember = displayMember;            }
        }

        public void LoadDataFromDatabase(string bomDocNum)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    string query = @"
                        SELECT 
                            H.COMCODE AS Firma, 
                            H.BOMDOCTYPE AS [Ürün Ağacı Tipi], 
                            H.BOMDOCNUM AS [Ürün Ağacı Numarası], 
                            H.BOMDOCFROM AS [Geçerlilik Başlangıç],
                            H.BOMDOCUNTIL AS [Geçerlilik Bitiş],
                            H.MATDOCTYPE AS [Malzeme Tipi], 
                            H.MATDOCNUM AS [Malzeme Numarası], 
                            H.QUANTITY AS [Temel Miktar],
                            H.DRAWNUM AS [Çizim Numarası],
                            H.ISDELETED AS [Silindi mi?],
                            H.ISPASSIVE AS [Pasif mi?],
                            C.CONTENTNUM AS [İçerik Numarası],
                            C.COMPONENT AS [Bileşen Kodu],
                            C.COMPBOMDOCTYPE AS [Kalem Ürün Ağacı Tipi],
                            C.COMPBOMDOCNUM AS [Kalem Ürün Ağacı Numarası],
                            D.DOCTYPE AS [Ürün Ağacı Tipi],
                            D.DOCTYPETEXT AS [Ürün Ağacı Tipi Açıklaması]
                        FROM BSMGRTRTBOMHEAD H
                        LEFT JOIN BSMGRTRTBOMCONTENT C ON H.BOMDOCTYPE = C.BOMDOCTYPE
                        LEFT JOIN BSMGRTRTBOM001 D ON H.BOMDOCTYPE = D.DOCTYPE
                        WHERE H.BOMDOCNUM = @BOMDOCNUM";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@BOMDOCNUM", bomDocNum);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Firma = reader["Firma"].ToString();
                                UrunAgaciTipi = reader["Ürün Ağacı Tipi"].ToString();
                                UrunAgaciNumarasi = reader["Ürün Ağacı Numarası"].ToString();
                                MalzemeTipi = reader["Malzeme Tipi"].ToString();
                                MalzemeNumarasi = reader["Malzeme Numarası"].ToString();
                                TemelMiktar = Convert.ToDecimal(reader["Temel Miktar"]);
                                CizimNumarasi = reader["Çizim Numarası"].ToString();
                                GecerlilikBaslangic = Convert.ToDateTime(reader["Geçerlilik Başlangıç"]);
                                GecerlilikBitis = Convert.ToDateTime(reader["Geçerlilik Bitiş"]);
                                IsDeleted = Convert.ToBoolean(reader["Silindi mi?"]);
                                IsPassive = Convert.ToBoolean(reader["Pasif mi?"]);
                                BilesenKodu = reader["Bileşen Kodu"].ToString();
                                KalemUrunAgaciTipi = reader["Kalem Ürün Ağacı Tipi"].ToString();
                                KalemUrunAgaciNumarasi = reader["Kalem Ürün Ağacı Numarası"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Girilen Ürün Ağacı Numarasına ait bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    con.Open();

                    // UNIQUE KEY ihlalini kontrol et
                    string checkUniqueQuery = @"
                SELECT COUNT(*)
                FROM BSMGRTRTBOMHEAD
                WHERE BOMDOCFROM = @BOMDOCFROM AND BOMDOCNUM != @BOMDOCNUM";

                    using (SqlCommand checkCmd = new SqlCommand(checkUniqueQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@BOMDOCFROM", baslangicDateTimePicker.Value);
                        checkCmd.Parameters.AddWithValue("@BOMDOCNUM", textBox1.Text);

                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Aynı tarihli başka bir kayıt zaten mevcut. Lütfen farklı bir tarih girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // BSMGR0BOMHEAD tablosunu güncelle
                    string queryHead = @"
                UPDATE BSMGRTRTBOMHEAD
                SET 
                    COMCODE = @COMCODE,
                    BOMDOCTYPE = @BOMDOCTYPE,
                    BOMDOCNUM = @BOMDOCNUM,
                    BOMDOCFROM = @BOMDOCFROM,
                    BOMDOCUNTIL = @BOMDOCUNTIL,
                    MATDOCTYPE = @MATDOCTYPE,
                    MATDOCNUM = @MATDOCNUM,
                    QUANTITY = @QUANTITY,
                    DRAWNUM = @DRAWNUM,
                    ISDELETED = @ISDELETED,
                    ISPASSIVE = @ISPASSIVE
                WHERE 
                    BOMDOCNUM = @BOMDOCNUM;";

                    using (SqlCommand cmdHead = new SqlCommand(queryHead, con))
                    {
                        cmdHead.Parameters.AddWithValue("@COMCODE", firmbox.Text ?? (object)DBNull.Value);
                        cmdHead.Parameters.AddWithValue("@BOMDOCTYPE", urnAgaTipBox.Text ?? (object)DBNull.Value);
                        cmdHead.Parameters.AddWithValue("@BOMDOCNUM", textBox1.Text ?? (object)DBNull.Value);
                        cmdHead.Parameters.AddWithValue("@BOMDOCFROM", baslangicDateTimePicker.Value);
                        cmdHead.Parameters.AddWithValue("@BOMDOCUNTIL", bitisDateTimePicker.Value);
                        cmdHead.Parameters.AddWithValue("@MATDOCTYPE", urnagamalztipbox.Text ?? (object)DBNull.Value);
                        cmdHead.Parameters.AddWithValue("@MATDOCNUM", urnmalzemenumBox.Text ?? (object)DBNull.Value);
                        cmdHead.Parameters.AddWithValue("@QUANTITY", decimal.TryParse(temelmikBox.Text, out decimal quantity) ? quantity : (object)DBNull.Value);
                        cmdHead.Parameters.AddWithValue("@DRAWNUM", cizmikBox.Text ?? (object)DBNull.Value);
                        cmdHead.Parameters.AddWithValue("@ISDELETED", deletedlbl.Checked ? 1 : 0);
                        cmdHead.Parameters.AddWithValue("@ISPASSIVE", checkboxpas.Checked ? 1 : 0);

                        cmdHead.ExecuteNonQuery();
                    }

                    MessageBox.Show("Kayıt başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}\n{ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }


    }
}
        
     