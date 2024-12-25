using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class matAddForm : Form
    {

        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd;
        public matAddForm()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadComboBoxData();
            

            firmCodeComboBox.Leave += (s, e) => ValidateAndAddData(firmCodeComboBox, "COMCODE");
            matTypeComboBox.Leave += (s, e) => ValidateAndAddData(matTypeComboBox, "MATDOCTYPE");
            supplyTypeComboBox.Leave += (s, e) => ValidateAndAddData(supplyTypeComboBox, "SUPPLYTYPE");
            lanComboBox.Leave += (s, e) => ValidateAndAddData(lanComboBox, "LANCODE");
            routeTypeComboBox.Leave += (s, e) => ValidateAndAddData(routeTypeComboBox, "ROTDOCTYPE");
            productTreeTypeComboBox.Leave += (s, e) => ValidateAndAddData(productTreeTypeComboBox, "BOMDOCTYPE");
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



        private void matAddForm_Load(object sender, EventArgs e)
        {
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


        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void ValidateAndAddData(ComboBox comboBox, string columnName)
        {
            string userInput = comboBox.Text.Trim();
            if (string.IsNullOrEmpty(userInput))
                return;

            string checkQuery = $"SELECT COUNT(*) FROM BSMGRTRTMATHEAD WHERE {columnName} = @userInput";

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@userInput", userInput);
                        con.Open();

                        int count = (int)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            MessageBox.Show($"'{userInput}' değeri {columnName} sütunu için geçerli değil.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            comboBox.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string comCode = firmCodeComboBox.Text.Trim();
            string matDocNum = matCodeTextBox.Text.Trim();
            string matStatementShort = matStatementShortTextBox.Text.Trim();
            string matStatementLong = matStatementLongTextBox.Text.Trim();
            string matDocType = matTypeComboBox.Text.Trim();
            string supplyType = supplyTypeComboBox.Text.Trim();
            string lan = lanComboBox.Text.Trim();
            string netWeight = netWeightTextBox.Text.Trim();
            string brutWeight = brutWeightTextBox.Text.Trim();
            string matStockUnit = matStockUnitComboBox.Text.Trim();
            string netWeightUnit = netWeightUnitTextBox.Text.Trim();
            string brutWeightUnit = brutWeightUnitTextBox.Text.Trim();
            DateTime matDocFrom = DateTimePickerBaslangic.Value;
            DateTime matDocUntil = DateTimePickerBitis.Value;
            string productTreeType = productTreeTypeComboBox.Text.Trim();
            string productTreeCode = productTreeCodeTextBox.Text.Trim();
            string routeType = routeTypeComboBox.Text.Trim();
            string routeCode = routeCodeTextBox.Text.Trim();
            bool isTree = isTreeCheckBox.Checked;
            bool isRoute = isRouteCheckBox.Checked;
            bool isPassive = isPassiveCheckBox.Checked;
            bool isDeleted = isDeletedCheckBox.Checked;

            // Alanların boş olup olmadığını kontrol etme
            if (string.IsNullOrWhiteSpace(comCode) || string.IsNullOrWhiteSpace(matDocType) || string.IsNullOrWhiteSpace(matDocNum) || string.IsNullOrEmpty(supplyType) || string.IsNullOrEmpty(lan) || string.IsNullOrEmpty(matDocFrom.ToString()) || string.IsNullOrEmpty(matDocUntil.ToString()))
            {
                MessageBox.Show("Boş alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tarih mantık hatasını kontrol etme
            if (matDocFrom >= matDocUntil)
            {
                MessageBox.Show("Başlangıç tarihi, bitiş tarihinden önce olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int supplyTypeValue;

            if (!int.TryParse(supplyType, out supplyTypeValue))
            {
                MessageBox.Show("Tedarik türü geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal? netWeightValue = null;
            decimal parsedNetWeight = 0;
            if (!string.IsNullOrWhiteSpace(netWeight) && !decimal.TryParse(netWeight, out parsedNetWeight))
            {
                MessageBox.Show("Net ağırlık geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!string.IsNullOrWhiteSpace(netWeight))
            {
                netWeightValue = parsedNetWeight;
            }

            decimal? brutWeightValue = null;
            decimal parsedBrutWeight = 0;
            if (!string.IsNullOrWhiteSpace(brutWeight) && !decimal.TryParse(brutWeight, out parsedBrutWeight))
            {
                MessageBox.Show("Brüt ağırlık geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!string.IsNullOrWhiteSpace(brutWeight))
            {
                brutWeightValue = parsedBrutWeight;
            }




            // Veritabanı sorgusu
            string query = @"
        INSERT INTO BSMGRTRTMATHEAD 
        (COMCODE, MATDOCTYPE, MATDOCNUM, MATDOCFROM, MATDOCUNTIL, SUPPLYTYPE, STUNIT, NETWEIGHT, NWUNIT, 
        BRUTWEIGHT, BWUNIT, ISBOM, BOMDOCTYPE, BOMDOCNUM, ISROUTE, ROTDOCTYPE, ROTDOCNUM, ISDELETED, ISPASSIVE) 
        VALUES 
        (@COMCODE, @MATDOCTYPE, @MATDOCNUM, @MATDOCFROM, @MATDOCUNTIL, @SUPPLYTYPE, @STUNIT, @NETWEIGHT, @NWUNIT, 
        @BRUTWEIGHT, @BWUNIT, @ISBOM, @BOMDOCTYPE, @BOMDOCNUM, @ISROUTE, @ROTDOCTYPE, @ROTDOCNUM, @ISDELETED, @ISPASSIVE)";



            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            using (cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@COMCODE", SqlDbType.VarChar).Value = comCode;
                cmd.Parameters.AddWithValue("@MATDOCTYPE", SqlDbType.VarChar).Value = matDocType;
                cmd.Parameters.AddWithValue("@MATDOCNUM", SqlDbType.VarChar).Value = matDocNum;
                cmd.Parameters.AddWithValue("@MATDOCFROM", SqlDbType.DateTime).Value = matDocFrom;
                cmd.Parameters.AddWithValue("@MATDOCUNTIL", SqlDbType.DateTime).Value = matDocUntil;
                cmd.Parameters.AddWithValue("@SUPPLYTYPE", SqlDbType.Int).Value = supplyTypeValue;
                cmd.Parameters.AddWithValue("@STUNIT", SqlDbType.VarChar).Value = matStockUnit;
                cmd.Parameters.Add(new SqlParameter("@NETWEIGHT", SqlDbType.Decimal)
                {
                    Value = netWeightValue.HasValue ? (object)netWeightValue.Value : DBNull.Value
                }); ;
                cmd.Parameters.AddWithValue("@NWUNIT", SqlDbType.VarChar).Value = netWeightUnit;
                cmd.Parameters.Add(new SqlParameter("@BRUTWEIGHT", SqlDbType.Decimal)
                {
                    Value = brutWeightValue.HasValue ? (object)brutWeightValue.Value : DBNull.Value
                });
                cmd.Parameters.AddWithValue("@BWUNIT", SqlDbType.VarChar).Value = brutWeightUnit;
                cmd.Parameters.AddWithValue("@ISBOM", SqlDbType.Int).Value = isTree ? 1 : 0; ;
                cmd.Parameters.AddWithValue("@BOMDOCTYPE", SqlDbType.VarChar).Value = productTreeType;
                cmd.Parameters.AddWithValue("@BOMDOCNUM", SqlDbType.VarChar).Value = productTreeCode;
                cmd.Parameters.AddWithValue("@ISROUTE", SqlDbType.Int).Value = isRoute ? 1 : 0;
                cmd.Parameters.AddWithValue("@ROTDOCTYPE", SqlDbType.VarChar).Value = routeType;
                cmd.Parameters.AddWithValue("@ROTDOCNUM", SqlDbType.VarChar).Value = routeCode;
                cmd.Parameters.AddWithValue("@ISDELETED", SqlDbType.Int).Value = isDeleted;
                cmd.Parameters.AddWithValue("@ISPASSIVE", SqlDbType.Int).Value = isPassive;


                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show(rowsAffected > 0 ? "Kayıt başarıyla eklendi." : "Kayıt eklenemedi.", "Bilgi", MessageBoxButtons.OK, rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            string query2 = @"INSERT INTO BSMGRTRTMATTEXT 
        (COMCODE, MATDOCTYPE, MATDOCNUM, MATDOCFROM, MATDOCUNTIL, LANCODE, MATSTEXT, MATLTEXT) 
        VALUES 
        (@COMCODE, @MATDOCTYPE, @MATDOCNUM, @MATDOCFROM, @MATDOCUNTIL, @LANCODE, @MATSTEXT, @MATLTEXT)";


            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            using (cmd = new SqlCommand(query2, con))
            {
                cmd.Parameters.AddWithValue("@COMCODE", SqlDbType.VarChar).Value = comCode;
                cmd.Parameters.AddWithValue("@MATDOCTYPE", SqlDbType.VarChar).Value = matDocType;
                cmd.Parameters.AddWithValue("@MATDOCNUM", SqlDbType.VarChar).Value = matDocNum;
                cmd.Parameters.AddWithValue("@MATDOCFROM", SqlDbType.DateTime).Value = matDocFrom;
                cmd.Parameters.AddWithValue("@MATDOCUNTIL", SqlDbType.DateTime).Value = matDocUntil;
                cmd.Parameters.AddWithValue("@LANCODE", SqlDbType.VarChar).Value = lan;
                cmd.Parameters.AddWithValue("@MATSTEXT", SqlDbType.VarChar).Value = matStatementShort;
                cmd.Parameters.AddWithValue("@MATLTEXT", SqlDbType.VarChar).Value = matStatementLong;

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show(rowsAffected > 0 ? "Kayıt başarıyla eklendi." : "Kayıt eklenemedi.", "Bilgi", MessageBoxButtons.OK, rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }


        }


    }
}
