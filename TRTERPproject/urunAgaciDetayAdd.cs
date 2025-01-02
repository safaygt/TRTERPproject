using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class urunAgaciDetayAdd : Form
    {
        public string BOMDocNum { get; set; }



        public urunAgaciDetayAdd()
        {
            InitializeComponent();
        }

        private void contentbtnSave_Click(object sender, EventArgs e)
        {
            string comCode = firmbox.Text.Trim();
            string bomDoctype = urnAgaTipBox.Text.Trim();
            string bomDocnum = textBox1.Text.Trim();
            DateTime bomDocFrom = baslangicDateTimePicker.Value;
            DateTime bomDocUntil = bitisDateTimePicker.Value;
            string matDoctype = urnagamalztipbox.Text.Trim();
            string matDocnum = urnmalzemenumBox.Text.Trim();
            string contentnum = icerikNumBox.Text.Trim();
            string component = bilesenKodBox.Text.Trim();
            string compBomDoctype = kalemAgUrnTipBox.Text.Trim();
            string compBomDocnum = kalemAgUrnKodBox.Text.Trim();
            string quantity = temelmikBox.Text.Trim();

            // Boş alanların kontrolü
            if (string.IsNullOrWhiteSpace(comCode) || string.IsNullOrWhiteSpace(bomDocnum) || string.IsNullOrWhiteSpace(bomDoctype) ||
                string.IsNullOrEmpty(matDoctype) || string.IsNullOrEmpty(matDocnum) ||
                string.IsNullOrEmpty(contentnum) || string.IsNullOrEmpty(component) ||
                string.IsNullOrEmpty(compBomDoctype) || string.IsNullOrEmpty(compBomDocnum))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tarih kontrolü
            if (bomDocFrom >= bomDocUntil)
            {
                MessageBox.Show("Başlangıç tarihi, bitiş tarihinden önce olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Temel Miktar kontrolü
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
                    // Kayıt ekleme
                    string query = @"
            INSERT INTO BSMGRTRTBOMCONTENT 
            (COMCODE, BOMDOCTYPE, BOMDOCNUM, BOMDOCFROM, BOMDOCUNTIL, MATDOCTYPE, MATDOCNUM, CONTENTNUM, COMPONENT, COMPBOMDOCTYPE, COMPBOMDOCNUM, QUANTITY) 
            VALUES 
            (@COMCODE, @BOMDOCTYPE, @BOMDOCNUM, @BOMDOCFROM, @BOMDOCUNTIL, @MATDOCTYPE, @MATDOCNUM, @CONTENTNUM, @COMPONENT, @COMPBOMDOCTYPE, @COMPBOMDOCNUM, @QUANTITY)";

                    using (SqlCommand cmd = new SqlCommand(query, con, transaction))
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

                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        private void urunAgaciDetayAdd_Load(object sender, EventArgs e)
        {
            try
            {
                // BOMDOCNUM değerini textBox1'e ata
                textBox1.Text = BOMDocNum;

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


                    LoadComboBoxData("SELECT DISTINCT COMPONENT FROM BSMGRTRTBOMCONTENT", bilesenKodBox, "COMPONENT");

                    // Kalem Ürün Ağacı Tipi
                    LoadComboBoxData("SELECT DISTINCT DOCTYPE FROM BSMGRTRTBOM001", kalemAgUrnTipBox, "DOCTYPE");

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
                comboBox.ValueMember = displayMember;            }
        }

        private void kalemAgUrnKodBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
