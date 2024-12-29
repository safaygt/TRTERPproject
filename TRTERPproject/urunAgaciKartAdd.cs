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
    public partial class urunAgaciKartAdd : Form
    {
        private string bomDocNum;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

        public urunAgaciKartAdd(string bomDocNum)
        {
            InitializeComponent();
            this.bomDocNum = bomDocNum;
            this.Load += (s, e) => LoadComboBoxData();

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            /*
            // Kullanıcı girişlerini al
            string comCode = firmbox.Text.Trim();
            string matDoctype = urnagamalztipbox.Text.Trim();
            string matDocnum = urnmalzemenumBox.Text.Trim();
            DateTime bomDocFrom = baslangicDateTimePicker.Value;
            DateTime bomDocUntil = bitisDateTimePicker.Value;
            string bomDoctype = urnAgaTipBox.Text.Trim();
            string bomDocnum = textBox1.Text.Trim();
            string quantity = temelmikBox.Text.Trim();
            string drawnum = cizmikBox.Text.Trim();
            bool isPassive = checkboxpas.Checked;
            bool isDeleted = deletedlbl.Checked;
            string contentnum = icerikNumBox.Text.Trim();
            string component = bilesenKodBox.Text.Trim();
            string compBomDoctype = kalemAgUrnTipBox.Text.Trim();
            string compBomDocnum = kalemAgUrnKodBox.Text.Trim();

            // Alanların boş olup olmadığını kontrol et
            if (string.IsNullOrWhiteSpace(comCode) || string.IsNullOrWhiteSpace(bomDocnum) || string.IsNullOrWhiteSpace(bomDoctype) ||
                string.IsNullOrEmpty(matDoctype) || string.IsNullOrEmpty(matDocnum) ||
                string.IsNullOrEmpty(contentnum) || string.IsNullOrEmpty(component) ||
                string.IsNullOrEmpty(compBomDoctype) || string.IsNullOrEmpty(compBomDocnum))
            {
                MessageBox.Show("Boş alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tarih mantık hatasını kontrol et
            if (bomDocFrom >= bomDocUntil)
            {
                MessageBox.Show("Başlangıç tarihi, bitiş tarihinden önce olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // İçerik numarasının sayı olduğunu kontrol et
            if (!int.TryParse(contentnum, out int contentnumValue))
            {
                MessageBox.Show("İçerik Numarası bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Temel miktar (quantity) için sayı doğrulaması
            decimal? quantityValue = null;
            if (!string.IsNullOrWhiteSpace(quantity))
            {
                if (!decimal.TryParse(quantity, out decimal parsedQuantity))
                {
                    MessageBox.Show("Temel Miktar geçerli bir sayı olmalıdır. Lütfen ,00 olmadan giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                quantityValue = parsedQuantity;
            }

            // BOMHEAD tablosuna veri ekleme
            string query = @"
                INSERT INTO BSMGRTRTBOMHEAD 
                (COMCODE, BOMDOCTYPE, BOMDOCNUM, BOMDOCFROM, BOMDOCUNTIL, MATDOCTYPE, MATDOCNUM, QUANTITY, ISDELETED, ISPASSIVE, DRAWNUM) 
                VALUES 
                (@COMCODE, @BOMDOCTYPE, @BOMDOCNUM, @BOMDOCFROM, @BOMDOCUNTIL, @MATDOCTYPE, @MATDOCNUM, @QUANTITY, @ISDELETED, @ISPASSIVE, @DRAWNUM)";

            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@COMCODE", comCode);
                cmd.Parameters.AddWithValue("@BOMDOCTYPE", bomDoctype);
                cmd.Parameters.AddWithValue("@BOMDOCNUM", bomDocnum);
                cmd.Parameters.AddWithValue("@BOMDOCFROM", bomDocFrom);
                cmd.Parameters.AddWithValue("@BOMDOCUNTIL", bomDocUntil);
                cmd.Parameters.AddWithValue("@MATDOCTYPE", matDoctype);
                cmd.Parameters.AddWithValue("@MATDOCNUM", matDocnum);
                cmd.Parameters.Add(new SqlParameter("@QUANTITY", SqlDbType.Decimal)
                {
                    Value = quantityValue.HasValue ? (object)quantityValue.Value : DBNull.Value
                });
                cmd.Parameters.AddWithValue("@ISDELETED", isDeleted);
                cmd.Parameters.AddWithValue("@ISPASSIVE", isPassive);
                cmd.Parameters.AddWithValue("@DRAWNUM", drawnum);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show(rowsAffected > 0 ? "Başarıyla kaydedildi." : "Kayıt başarısız oldu.", "Bilgi", MessageBoxButtons.OK, rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // BOMCONTENT tablosuna veri ekleme
            string query2 = @"
                INSERT INTO BSMGRTRTBOMCONTENT 
                (COMCODE, BOMDOCTYPE, BOMDOCNUM, BOMDOCFROM, BOMDOCUNTIL, MATDOCTYPE, MATDOCNUM, CONTENTNUM, COMPONENT, COMPBOMDOCTYPE, COMPBOMDOCNUM, QUANTITY) 
                VALUES 
                (@COMCODE, @BOMDOCTYPE, @BOMDOCNUM, @BOMDOCFROM, @BOMDOCUNTIL, @MATDOCTYPE, @MATDOCNUM, @CONTENTNUM, @COMPONENT, @COMPBOMDOCTYPE, @COMPBOMDOCNUM, @QUANTITY)";

            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query2, con))
            {
                cmd.Parameters.AddWithValue("@COMCODE", comCode);
                cmd.Parameters.AddWithValue("@BOMDOCTYPE", bomDoctype);
                cmd.Parameters.AddWithValue("@BOMDOCNUM", bomDocnum);
                cmd.Parameters.AddWithValue("@BOMDOCFROM", bomDocFrom);
                cmd.Parameters.AddWithValue("@BOMDOCUNTIL", bomDocUntil);
                cmd.Parameters.AddWithValue("@MATDOCTYPE", matDoctype);
                cmd.Parameters.AddWithValue("@MATDOCNUM", matDocnum);
                cmd.Parameters.AddWithValue("@CONTENTNUM", contentnum);
                cmd.Parameters.AddWithValue("@COMPONENT", component);
                cmd.Parameters.AddWithValue("@COMPBOMDOCTYPE", compBomDoctype);
                cmd.Parameters.AddWithValue("@COMPBOMDOCNUM", compBomDocnum);
                cmd.Parameters.Add(new SqlParameter("@QUANTITY", SqlDbType.Decimal)
                {
                    Value = quantityValue.HasValue ? (object)quantityValue.Value : DBNull.Value
                });

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show(rowsAffected > 0 ? "Başarıyla kaydedildi." : "Kayıt başarısız oldu.", "Bilgi", MessageBoxButtons.OK, rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kayıt Başarısız", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            ClearFormFields();
        
            */










            string comCode = firmbox.Text.Trim();
            string matDoctype = urnagamalztipbox.Text.Trim();
            string matDocnum = urnmalzemenumBox.Text.Trim();
            DateTime bomDocFrom = baslangicDateTimePicker.Value;
            DateTime bomDocUntil = bitisDateTimePicker.Value;
            string bomDoctype = urnAgaTipBox.Text.Trim();
            string bomDocnum = textBox1.Text.Trim();
            string quantity = temelmikBox.Text.Trim();
            string drawnum = cizmikBox.Text.Trim();
            bool isPassive = checkboxpas.Checked;
            bool isDeleted = deletedlbl.Checked;
            string contentnum = icerikNumBox.Text.Trim();
            string component = bilesenKodBox.Text.Trim();
            string compBomDoctype = kalemAgUrnTipBox.Text.Trim();
            string compBomDocnum = kalemAgUrnKodBox.Text.Trim();

            // Alanların boş olup olmadığını kontrol etme
            if (string.IsNullOrWhiteSpace(comCode) || string.IsNullOrWhiteSpace(bomDocnum) || string.IsNullOrWhiteSpace(bomDoctype) ||
                string.IsNullOrEmpty(matDoctype) || string.IsNullOrEmpty(matDocnum) ||
                string.IsNullOrEmpty(contentnum) || string.IsNullOrEmpty(component) ||
                string.IsNullOrEmpty(compBomDoctype) || string.IsNullOrEmpty(compBomDocnum))
            {
                MessageBox.Show("Boş alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tarih mantık hatasını kontrol etme
            if (bomDocFrom >= bomDocUntil)
            {
                MessageBox.Show("Başlangıç tarihi, bitiş tarihinden önce olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int supplyTypeValue;
            if (!int.TryParse(contentnum, out int contentnumValue))
            {
                MessageBox.Show("İçerik Numarası bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal? quantityValue = null;
            if (!string.IsNullOrWhiteSpace(quantity))
            {
                if (!decimal.TryParse(quantity, out decimal parsedQuantity))
                {
                    MessageBox.Show("Temel Miktar geçerli bir sayı olmalıdır. Lütfen ,00 olmadan giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                quantityValue = parsedQuantity;
            }



            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    string query1 = @"
                INSERT INTO BSMGRTRTBOMHEAD 
                (COMCODE, BOMDOCTYPE, BOMDOCNUM, BOMDOCFROM, BOMDOCUNTIL, MATDOCTYPE, MATDOCNUM, QUANTITY, ISDELETED, ISPASSIVE, DRAWNUM) 
                VALUES 
                (@COMCODE, @BOMDOCTYPE, @BOMDOCNUM, @BOMDOCFROM, @BOMDOCUNTIL, @MATDOCTYPE, @MATDOCNUM, @QUANTITY, @ISDELETED, @ISPASSIVE, @DRAWNUM)";

                    using (SqlCommand cmd = new SqlCommand(query1, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd.Parameters.AddWithValue("@BOMDOCTYPE", bomDoctype);
                        cmd.Parameters.AddWithValue("@BOMDOCNUM", bomDocnum);
                        cmd.Parameters.AddWithValue("@BOMDOCFROM", bomDocFrom);
                        cmd.Parameters.AddWithValue("@BOMDOCUNTIL", bomDocUntil);
                        cmd.Parameters.AddWithValue("@MATDOCTYPE", matDoctype);
                        cmd.Parameters.AddWithValue("@MATDOCNUM", matDocnum);
                        cmd.Parameters.Add(new SqlParameter("@QUANTITY", SqlDbType.Decimal)
                        {
                            Value = quantityValue.HasValue ? (object)quantityValue.Value : DBNull.Value
                        });
                        cmd.Parameters.AddWithValue("@ISDELETED", isDeleted);
                        cmd.Parameters.AddWithValue("@ISPASSIVE", isPassive);
                        cmd.Parameters.AddWithValue("@DRAWNUM", drawnum);
                        cmd.ExecuteNonQuery();
                    }

                    string query2 = @"
                INSERT INTO BSMGRTRTBOMCONTENT 
                (COMCODE, BOMDOCTYPE, BOMDOCNUM, BOMDOCFROM, BOMDOCUNTIL, MATDOCTYPE, MATDOCNUM, CONTENTNUM, COMPONENT, COMPBOMDOCTYPE, COMPBOMDOCNUM, QUANTITY) 
                VALUES 
                (@COMCODE, @BOMDOCTYPE, @BOMDOCNUM, @BOMDOCFROM, @BOMDOCUNTIL, @MATDOCTYPE, @MATDOCNUM, @CONTENTNUM, @COMPONENT, @COMPBOMDOCTYPE, @COMPBOMDOCNUM, @QUANTITY)";

                    using (SqlCommand cmd2 = new SqlCommand(query2, con, transaction))
                    {
                        cmd2.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd2.Parameters.AddWithValue("@BOMDOCTYPE", bomDoctype);
                        cmd2.Parameters.AddWithValue("@BOMDOCNUM", bomDocnum);
                        cmd2.Parameters.AddWithValue("@BOMDOCFROM", bomDocFrom);
                        cmd2.Parameters.AddWithValue("@BOMDOCUNTIL", bomDocUntil);
                        cmd2.Parameters.AddWithValue("@MATDOCTYPE", matDoctype);
                        cmd2.Parameters.AddWithValue("@MATDOCNUM", matDocnum);
                        cmd2.Parameters.AddWithValue("@CONTENTNUM", contentnum);
                        cmd2.Parameters.AddWithValue("@COMPONENT", component);
                        cmd2.Parameters.AddWithValue("@COMPBOMDOCTYPE", compBomDoctype);
                        cmd2.Parameters.AddWithValue("@COMPBOMDOCNUM", compBomDocnum);
                        cmd2.Parameters.Add(new SqlParameter("@QUANTITY", SqlDbType.Decimal)
                        {
                            Value = quantityValue.HasValue ? (object)quantityValue.Value : DBNull.Value
                        });
                        cmd2.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();

                    if (ex.Message.Contains("UQ_b" +
                        "BOMDOCFROM"))
                    {
                        MessageBox.Show("Bu tarih için zaten bir kayıt mevcut. Lütfen farklı bir tarih giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
            icerikNumBox.Text = string.Empty; // İçerik Numarası kutusunu temizle
            temelmikBox.Text = string.Empty; // Temel Miktar kutusunu temizle

            baslangicDateTimePicker.Value = DateTime.Now; // Başlangıç tarihi bugüne ayarla
            bitisDateTimePicker.Value = DateTime.Now; // Bitiş tarihi bugüne ayarla

            checkboxpas.Checked = false; // Pasiflik durumu kutusunu temizle
            deletedlbl.Checked = false; // Silinmiş durumu kutusunu temizle

            bilesenKodBox.Text = string.Empty; // Bileşen Kodu kutusunu temizle
            kalemAgUrnTipBox.Text = string.Empty; // Kalem Ürün Ağacı Tipi kutusunu temizle
            kalemAgUrnKodBox.Text = string.Empty; // Kalem Ürün Ağacı Numarası kutusunu temizle
        }

        private void urunAgaciKartAdd_Load(object sender, EventArgs e)
        {
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

                    // Bileşen Kodları
                    LoadComboBoxData("SELECT DISTINCT COMPONENT FROM BSMGRTRTBOMCONTENT", bilesenKodBox, "COMPONENT");

                    LoadComboBoxData("SELECT DISTINCT MATDOCNUM FROM BSMGRTRTMATHEAD", urnmalzemenumBox, "MATDOCNUM");

                    // Kalem Ürün Ağacı Tipi
                    LoadComboBoxData("SELECT DISTINCT COMPBOMDOCTYPE FROM BSMGRTRTBOMCONTENT", kalemAgUrnTipBox, "COMPBOMDOCTYPE");

                    // Kalem Ürün Ağacı Numarası
                    LoadComboBoxData("SELECT DISTINCT COMPBOMDOCNUM FROM BSMGRTRTBOMCONTENT", kalemAgUrnKodBox, "COMPBOMDOCNUM");
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
                comboBox.ValueMember = displayMember;
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

    }
}
