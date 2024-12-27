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
    public partial class malzemeAnaTabloEdit : Form
    {

        public string Firma { get; set; }

        public string MatdocType { get; set; }
        public DateTime GecerliBaslangic { get; set; }
        public DateTime GecerliBitis { get; set; }
        public int MatCode { get; set; }
        public int supplytype { get; set; }
        public string Dil { get; set; }
        public string uzunAciklama { get; set; }
        public string kisaAciklama { get; set; }

        public int? netWeight { get; set; }
        public int? brutWeight { get; set; }

        public string malzemeStokBirimi { get; set; }

        public string netWeightUnit { get; set; }
        public string brutWeightUnit { get; set; }

        public string treeType { get; set; }

        public string treeCode { get; set; }
        public string rotCode { get; set; }
        public string rotType { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPassive { get; set; }

        public bool isTree { get; set; }

        public bool isRot { get; set; }


        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd;

        public malzemeAnaTabloEdit()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadComboBoxData();


            firmCodeComboBox.Leave += (s, e) => ValidateAndAddData(firmCodeComboBox, "COMCODE", "BSMGRTRTGEN001");
            matTypeComboBox.Leave += (s, e) => ValidateAndAddData(matTypeComboBox, "DOCTYPE", "BSMGRTRTMAT001");
            supplyTypeComboBox.Leave += (s, e) => ValidateAndAddData(supplyTypeComboBox, "SUPPLYTYPE", "BSMGRTRTMATHEAD");
            lanComboBox.Leave += (s, e) => ValidateAndAddData(lanComboBox, "LANCODE", "BSMGRTRTGEN002");
            routeTypeComboBox.Leave += (s, e) => ValidateAndAddData(routeTypeComboBox, "DOCTYPE", "BSMGRTRTROT001");
            productTreeTypeComboBox.Leave += (s, e) => ValidateAndAddData(productTreeTypeComboBox, "DOCTYPE", "BSMGRTRTBOM001");
        }


        private void LoadComboBox(ComboBox comboBox, string query, string columnName)
        {
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    comboBox.DataSource = dt;
                    comboBox.DisplayMember = columnName;
                    comboBox.ValueMember = columnName;
                    comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                    // Seçilen değeri doğru şekilde ata
                    if (comboBox.SelectedValue == null && dt.Rows.Count > 0)
                    {
                        comboBox.SelectedValue = dt.Rows[0][columnName]; // Varsayılan değeri ilk satır olarak ayarlayın
                    }
                }
            }
        }

        private void ValidateAndAddData(ComboBox comboBox, string columnName, string tableName)
        {
            string checkQuery = $@"
            SELECT COUNT(*) 
            FROM {tableName} 
            WHERE {columnName} = @userInput";

            if (string.IsNullOrEmpty(comboBox.Text)) return;

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    con.Open();
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@userInput", comboBox.Text);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count == 0)
                        {
                            MessageBox.Show($"{columnName} '{comboBox.Text}' tablodaki verilerle uyuşmuyor.");
                            comboBox.Text = string.Empty; // Kullanıcının yanlış girişini temizler
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }


        private void LoadComboBoxData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    con.Open();

                    // Firma verileri
                    LoadComboBox(firmCodeComboBox, "SELECT DISTINCT COMCODE FROM BSMGRTRTGEN001", "COMCODE");

                    // İş Merkezi Tipi
                    LoadComboBox(matTypeComboBox, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTMAT001", "DOCTYPE");

                    // Dil Kodları
                    LoadComboBox(lanComboBox, "SELECT DISTINCT LANCODE FROM BSMGRTRTGEN002", "LANCODE");

                    // Operasyon Kodu
                    LoadComboBox(routeTypeComboBox, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTROT001", "DOCTYPE");

                    // Maliyet Merkezi
                    LoadComboBox(productTreeTypeComboBox, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTBOM001", "DOCTYPE");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}");
            }
        }

        private void malzemeAnaTabloEdit_Load(object sender, EventArgs e)
        {
            firmCodeComboBox.Text = Firma;
            matTypeComboBox.Text = MatdocType;
            DateTimePickerBaslangic.Value = GecerliBaslangic;
            DateTimePickerBitis.Value = GecerliBitis;
            matCodeTextBox.Text = MatCode.ToString();
            supplyTypeComboBox.Text = supplytype.ToString();
            matStatementLongTextBox.Text = uzunAciklama;
            matStatementShortTextBox.Text = kisaAciklama;
            netWeightTextBox.Text = netWeight.ToString();
            brutWeightTextBox.Text = brutWeight.ToString();
            netWeightUnitTextBox.Text = netWeightUnit;
            brutWeightUnitTextBox.Text = brutWeightUnit;
            lanComboBox.Text = Dil;
            matStockUnitComboBox.Text = malzemeStokBirimi;
            isDeletedCheckBox.Checked = IsDeleted;
            isPassiveCheckBox.Checked = IsPassive;
            isTreeCheckBox.Checked = isTree;
            isRouteCheckBox.Checked = isRot;
            productTreeTypeComboBox.Text = treeType;
            routeTypeComboBox.Text = rotType;
            productTreeCodeTextBox.Text = treeCode;
            routeCodeTextBox.Text = rotCode;

        }


        /*
        private void btnKaydet_Click(object sender, EventArgs e)
        {


            DateTime bomDocFrom = DateTimePickerBaslangic.Value;
            DateTime bomDocUntil = DateTimePickerBitis.Value;

            if (bomDocFrom >= bomDocUntil)
            {
                MessageBox.Show("Başlangıç tarihi, bitiş tarihinden önce olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // İşlemi durdur
            }

            string query = @"
    UPDATE BSMGRTRTMATHEAD 
SET 
    COMCODE = @COMCODE,
    MATDOCTYPE = @MATDOCTYPE,
    MATDOCFROM = @MATDOCFROM,
MATDOCNUM = @MATDOCNUM,
    MATDOCUNTIL = @MATDOCUNTIL,
    SUPPLYTYPE = @SUPPLYTYPE,
    STUNIT = @STUNIT,
    NETWEIGHT = @NETWEIGHT,
    NWUNIT = @NWUNIT,
    BRUTWEIGHT = @BRUTWEIGHT,
    BWUNIT = @BWUNIT,
    ISBOM = @ISBOM,
    BOMDOCTYPE = @BOMDOCTYPE,
    BOMDOCNUM = @BOMDOCNUM,
    ISROUTE = @ISROUTE,
    ROTDOCTYPE = @ROTDOCTYPE,
    ROTDOCNUM = @ROTDOCNUM,
    ISDELETED = @ISDELETED,
    ISPASSIVE = @ISPASSIVE 
WHERE MATDOCNUM = @MATDOCNUM";



            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            using (cmd = new SqlCommand(query, con))
            {
                // Parametreleri ekliyoruz
                cmd.Parameters.AddWithValue("@COMCODE", SqlDbType.VarChar).Value = Firma;
                cmd.Parameters.AddWithValue("@MATDOCTYPE", SqlDbType.VarChar).Value = MatdocType;
                cmd.Parameters.AddWithValue("@MATDOCNUM", SqlDbType.Int).Value = MatCode; // Malzeme numarası bir int olarak belirlenmiş
                cmd.Parameters.AddWithValue("@MATDOCFROM", SqlDbType.DateTime).Value = GecerliBaslangic;
                cmd.Parameters.AddWithValue("@MATDOCUNTIL", SqlDbType.DateTime).Value = GecerliBitis;
                cmd.Parameters.AddWithValue("@SUPPLYTYPE", SqlDbType.Int).Value = supplytype;
                cmd.Parameters.AddWithValue("@STUNIT", SqlDbType.VarChar).Value = malzemeStokBirimi;
                cmd.Parameters.Add(new SqlParameter("@NETWEIGHT", SqlDbType.Int)
                {
                    Value = netWeight.HasValue ? (object)netWeight.Value : DBNull.Value // Nullable kontrolü
                });
                cmd.Parameters.AddWithValue("@NWUNIT", SqlDbType.VarChar).Value = netWeightUnit;
                cmd.Parameters.Add(new SqlParameter("@BRUTWEIGHT", SqlDbType.Int)
                {
                    Value = brutWeight.HasValue ? (object)brutWeight.Value : DBNull.Value // Nullable kontrolü
                });
                cmd.Parameters.AddWithValue("@BWUNIT", SqlDbType.VarChar).Value = brutWeightUnit;
                cmd.Parameters.AddWithValue("@ISBOM", SqlDbType.Bit).Value = isTree ? 1 : 0; // bool değer için
                cmd.Parameters.AddWithValue("@BOMDOCTYPE", SqlDbType.VarChar).Value = treeType;
                cmd.Parameters.AddWithValue("@BOMDOCNUM", SqlDbType.VarChar).Value = treeCode;
                cmd.Parameters.AddWithValue("@ISROUTE", SqlDbType.Bit).Value = isRot ? 1 : 0; // bool değer için
                cmd.Parameters.AddWithValue("@ROTDOCTYPE", SqlDbType.VarChar).Value = rotType;
                cmd.Parameters.AddWithValue("@ROTDOCNUM", SqlDbType.VarChar).Value = rotCode;
                cmd.Parameters.AddWithValue("@ISDELETED", SqlDbType.Bit).Value = IsDeleted ? 1 : 0; // bool değer için
                cmd.Parameters.AddWithValue("@ISPASSIVE", SqlDbType.Bit).Value = IsPassive ? 1 : 0; // bool değer için

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery(); // SQL komutunu çalıştırıyoruz

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Başarılıysa formu kapatıyoruz
                    }
                    else
                    {
                        MessageBox.Show("Kayıt güncellenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException sqlEx)
                {
                    string errorMessage = "Bir veritabanı hatası oluştu. Lütfen tekrar deneyin.";

                    // SQL hata koduna göre mesajları özelleştir
                    switch (sqlEx.Number)
                    {
                        case 2627: // UNIQUE constraint violation
                            errorMessage = "Girilen başlangıç tarihi zaten mevcut. Lütfen farklı bir tarih girin.";
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




            string query2 = @"
    UPDATE BSMGRTRTMATTEXT 
SET 
    COMCODE = @COMCODE,
    MATDOCTYPE = @MATDOCTYPE,
    MATDOCNUM = @MATDOCNUM,
    MATDOCFROM = @MATDOCFROM,
    MATDOCUNTIL = @MATDOCUNTIL,
    LANCODE = @LANCODE,
    MATSTEXT = @MATSTEXT,
    MATLTEXT = @MATLTEXT
   
WHERE MATDOCNUM = @MATDOCNUM";



            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            using (cmd = new SqlCommand(query2, con))
            {
                // Parametreleri ekliyoruz
                cmd.Parameters.AddWithValue("@COMCODE", SqlDbType.VarChar).Value = Firma;
                cmd.Parameters.AddWithValue("@MATDOCTYPE", SqlDbType.VarChar).Value = MatdocType;
                cmd.Parameters.AddWithValue("@MATDOCNUM", SqlDbType.Int).Value = MatCode; // Malzeme numarası bir int olarak belirlenmiş
                cmd.Parameters.AddWithValue("@MATDOCFROM", SqlDbType.DateTime).Value = GecerliBaslangic;
                cmd.Parameters.AddWithValue("@MATDOCUNTIL", SqlDbType.DateTime).Value = GecerliBitis;

                // Dil parametresi için doğru veri tipi kullanın (int veya string)
                cmd.Parameters.AddWithValue("@LANCODE", SqlDbType.VarChar).Value = Dil; // Burada Dil'in tipi doğru olmalı, gerekirse int yapabilirsiniz

                // MATSTEXT parametresi eklendi ve kisaAciklama ile bağlandı
                cmd.Parameters.AddWithValue("@MATSTEXT", SqlDbType.VarChar).Value = kisaAciklama; // kisaAciklama parametresi burada kullanılıyor

                cmd.Parameters.AddWithValue("@MATLTEXT", SqlDbType.VarChar).Value = uzunAciklama;

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery(); // SQL komutunu çalıştırıyoruz

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Başarılıysa formu kapatıyoruz
                    }
                    else
                    {
                        MessageBox.Show("Kayıt güncellenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException sqlEx)
                {
                    string errorMessage = "Bir veritabanı hatası oluştu. Lütfen tekrar deneyin.";

                    // SQL hata koduna göre mesajları özelleştir
                    switch (sqlEx.Number)
                    {
                        case 2627: // UNIQUE constraint violation
                            errorMessage = "Girilen başlangıç tarihi zaten mevcut. Lütfen farklı bir tarih girin.";
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
    }
}

        */



        private void btnKaydet_Click(object sender, EventArgs e)
        {


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = new SqlConnection(ConnectionHelper.ConnectionString);

                try
                {
                    using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                    {
                        con.Open();

                        // Başlangıç ve Bitiş tarihlerini al
                        DateTime bomDocFrom = DateTime.TryParse(DateTimePickerBaslangic.Text, out DateTime fromDate) ? fromDate : DateTime.MinValue;
                        DateTime bomDocUntil = DateTime.TryParse(DateTimePickerBitis.Text, out DateTime untilDate) ? untilDate : DateTime.MinValue;

                        // Bitiş tarihinin, Başlangıç tarihinden önce olup olmadığını kontrol et
                        if (bomDocFrom >= bomDocUntil)
                        {
                            MessageBox.Show("Başlangıç tarihi, bitiş tarihinden önce olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // İşlemi durdur
                        }

                        // Mathead Tablosu Güncelleme
                        string query1 = @"
                    UPDATE BSMGRTRTMATHEAD
                    SET 
                        COMCODE = @COMCODE,
                        MATDOCTYPE = @MATDOCTYPE,
                        MATDOCNUM = @MATDOCNUM,
                        MATDOCFROM = @MATDOCFROM,
                        MATDOCUNTIL = @MATDOCUNTIL,
                        SUPPLYTYPE = @SUPPLYTYPE,
                        STUNIT = @STUNIT,
                        NETWEIGHT = @NETWEIGHT,
                        NWUNIT = @NWUNIT,
                        BRUTWEIGHT = @BRUTWEIGHT,
                        BWUNIT = @BWUNIT,
                        ISBOM = @ISBOM,
                        BOMDOCTYPE = @BOMDOCTYPE,
                        BOMDOCNUM = @BOMDOCNUM,
                        ISROUTE = @ISROUTE,
                        ROTDOCTYPE = @ROTDOCTYPE,
                        ROTDOCNUM = @ROTDOCNUM,
                        ISDELETED = @ISDELETED,
                        ISPASSIVE = @ISPASSIVE
                    WHERE 
                        MATDOCNUM = @MATDOCNUM;";

                        using (SqlCommand command1 = new SqlCommand(query1, con))
                        {
                            // Güncelleme için parametreler
                            command1.Parameters.AddWithValue("@COMCODE", firmCodeComboBox.Text ?? (object)DBNull.Value);
                            command1.Parameters.AddWithValue("@MATDOCTYPE", matTypeComboBox.Text ?? (object)DBNull.Value);
                            command1.Parameters.AddWithValue("@MATDOCNUM", matCodeTextBox.Text ?? (object)DBNull.Value);

                            command1.Parameters.AddWithValue("@MATDOCFROM", bomDocFrom);
                            command1.Parameters.AddWithValue("@MATDOCUNTIL", bomDocUntil);

                            command1.Parameters.AddWithValue("@SUPPLYTYPE", int.TryParse(supplyTypeComboBox.Text, out int supplytype) ? supplytype : (object)DBNull.Value);
                            command1.Parameters.AddWithValue("@STUNIT", matStockUnitComboBox.Text ?? (object)DBNull.Value);

                            command1.Parameters.AddWithValue("@NETWEIGHT", decimal.TryParse(netWeightTextBox.Text, out decimal netWeight) ? netWeight : (object)DBNull.Value);
                            command1.Parameters.AddWithValue("@NWUNIT", netWeightUnitTextBox.Text ?? (object)DBNull.Value);

                            command1.Parameters.AddWithValue("@BRUTWEIGHT", decimal.TryParse(brutWeightTextBox.Text, out decimal brutWeight) ? brutWeight : (object)DBNull.Value);
                            command1.Parameters.AddWithValue("@BWUNIT", brutWeightUnitTextBox.Text ?? (object)DBNull.Value);

                            command1.Parameters.AddWithValue("@ISBOM", isTreeCheckBox.Checked ? 1 : 0);
                            command1.Parameters.AddWithValue("@BOMDOCTYPE", productTreeTypeComboBox.Text ?? (object)DBNull.Value);
                            command1.Parameters.AddWithValue("@BOMDOCNUM", productTreeCodeTextBox.Text ?? (object)DBNull.Value);

                            command1.Parameters.AddWithValue("@ISROUTE", isRouteCheckBox.Checked ? 1 : 0);
                            command1.Parameters.AddWithValue("@ROTDOCTYPE", routeTypeComboBox.Text ?? (object)DBNull.Value);
                            command1.Parameters.AddWithValue("@ROTDOCNUM", routeCodeTextBox.Text ?? (object)DBNull.Value);

                            command1.Parameters.AddWithValue("@ISDELETED", isDeletedCheckBox.Checked ? 1 : 0);
                            command1.Parameters.AddWithValue("@ISPASSIVE", isPassiveCheckBox.Checked ? 1 : 0);

                            int rowsAffected = command1.ExecuteNonQuery(); // SQL komutunu çalıştırıyoruz

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Kayıt başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt güncellenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        // Mattext Tablosu Güncelleme
                        string query2 = @"
                    UPDATE BSMGRTRTMATTEXT
                    SET 
                        COMCODE = @COMCODE,
                        MATDOCTYPE = @MATDOCTYPE,
                        MATDOCNUM = @MATDOCNUM,
                        MATDOCFROM = @MATDOCFROM,
                        MATDOCUNTIL = @MATDOCUNTIL,
                        LANCODE = @LANCODE,
                        MATSTEXT = @MATSTEXT,
                        MATLTEXT = @MATLTEXT
                    WHERE MATDOCNUM = @MATDOCNUM;";

                        using (SqlCommand command2 = new SqlCommand(query2, con))
                        {
                            // Güncelleme için parametreler
                            command2.Parameters.AddWithValue("@COMCODE", firmCodeComboBox.Text ?? (object)DBNull.Value);
                            command2.Parameters.AddWithValue("@MATDOCTYPE", matTypeComboBox.Text ?? (object)DBNull.Value);
                            command2.Parameters.AddWithValue("@MATDOCNUM", matCodeTextBox.Text ?? (object)DBNull.Value);

                            command2.Parameters.AddWithValue("@MATDOCFROM", bomDocFrom);
                            command2.Parameters.AddWithValue("@MATDOCUNTIL", bomDocUntil);

                            command2.Parameters.AddWithValue("@LANCODE", lanComboBox.Text ?? (object)DBNull.Value);
                            command2.Parameters.AddWithValue("@MATSTEXT", matStatementShortTextBox.Text ?? (object)DBNull.Value);
                            command2.Parameters.AddWithValue("@MATLTEXT", matStatementLongTextBox.Text ?? (object)DBNull.Value);

                            int rowsAffected = command2.ExecuteNonQuery(); // SQL komutunu çalıştırıyoruz

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Kayıt başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Kayıt güncellenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    string errorMessage = "Bir veritabanı hatası oluştu. Lütfen tekrar deneyin.";

                    // SQL hata koduna göre mesajları özelleştir
                    switch (sqlEx.Number)
                    {
                        case 2627: // UNIQUE constraint violation
                            errorMessage = "Girilen malzeme numarası zaten mevcut. Lütfen farklı bir numara girin.";
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
        }
    }

        


