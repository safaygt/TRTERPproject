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


            firmCodeComboBox.Leave += (s, e) => ValidateAndAddData(firmCodeComboBox, "COMCODE", "BSMGRTRTGEN001");
            matTypeComboBox.Leave += (s, e) => ValidateAndAddData(matTypeComboBox, "DOCTYPE", "BSMGRTRTMAT001");
            brutWeightUnitComboBox.Leave += (s, e) => ValidateAndAddData(brutWeightUnitComboBox, "UNITCODE", "BSMGRTRTGEN005");
            netWeightUnitComboBox.Leave += (s, e) => ValidateAndAddData(netWeightUnitComboBox, "UNITCODE", "BSMGRTRTGEN005");
            matStockUnitComboBox.Leave += (s, e) => ValidateAndAddData(matStockUnitComboBox, "UNITCODE", "BSMGRTRTGEN005");
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

                    LoadComboBox(matStockUnitComboBox, "SELECT DISTINCT UNITCODE FROM BSMGRTRTGEN005", "UNITCODE");

                    LoadComboBox(netWeightUnitComboBox, "SELECT DISTINCT UNITCODE FROM BSMGRTRTGEN005", "UNITCODE");

                    LoadComboBox(brutWeightUnitComboBox, "SELECT DISTINCT UNITCODE FROM BSMGRTRTGEN005", "UNITCODE");

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

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string comCode = firmCodeComboBox.Text.Trim();
            string matDocNum = matCodeTextBox.Text.Trim();
            string matStatementShort = matStatementShortTextBox.Text.Trim();
            string matStatementLong = matStatementLongTextBox.Text.Trim();
            string matDocType = matTypeComboBox.Text.Trim();
            string supplyType = supplyTypeTextBox.Text.Trim();
            string lan = lanComboBox.Text.Trim();
            string netWeight = netWeightTextBox.Text.Trim();
            string brutWeight = brutWeightTextBox.Text.Trim();
            string matStockUnit = matStockUnitComboBox.Text.Trim();
            string netWeightUnit = netWeightUnitComboBox.Text.Trim();
            string brutWeightUnit = brutWeightUnitComboBox.Text.Trim();
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

            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {
                    string query1 = @"
                INSERT INTO BSMGRTRTMATHEAD 
                (COMCODE, MATDOCTYPE, MATDOCNUM, MATDOCFROM, MATDOCUNTIL, SUPPLYTYPE, STUNIT, NETWEIGHT, NWUNIT, 
                BRUTWEIGHT, BWUNIT, ISBOM, BOMDOCTYPE, BOMDOCNUM, ISROUTE, ROTDOCTYPE, ROTDOCNUM, ISDELETED, ISPASSIVE) 
                VALUES 
                (@COMCODE, @MATDOCTYPE, @MATDOCNUM, @MATDOCFROM, @MATDOCUNTIL, @SUPPLYTYPE, @STUNIT, @NETWEIGHT, @NWUNIT, 
                @BRUTWEIGHT, @BWUNIT, @ISBOM, @BOMDOCTYPE, @BOMDOCNUM, @ISROUTE, @ROTDOCTYPE, @ROTDOCNUM, @ISDELETED, @ISPASSIVE)";

                    using (SqlCommand cmd1 = new SqlCommand(query1, con, transaction))
                    {
                        cmd1.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd1.Parameters.AddWithValue("@MATDOCTYPE", matDocType);
                        cmd1.Parameters.AddWithValue("@MATDOCNUM", matDocNum);
                        cmd1.Parameters.AddWithValue("@MATDOCFROM", matDocFrom);
                        cmd1.Parameters.AddWithValue("@MATDOCUNTIL", matDocUntil);
                        cmd1.Parameters.AddWithValue("@SUPPLYTYPE", supplyTypeValue);
                        cmd1.Parameters.AddWithValue("@STUNIT", matStockUnit);
                        cmd1.Parameters.AddWithValue("@NETWEIGHT", (object)netWeightValue ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@NWUNIT", netWeightUnit);
                        cmd1.Parameters.AddWithValue("@BRUTWEIGHT", (object)brutWeightValue ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@BWUNIT", brutWeightUnit);
                        cmd1.Parameters.AddWithValue("@ISBOM", isTree ? 1 : 0);
                        cmd1.Parameters.AddWithValue("@BOMDOCTYPE", productTreeType);
                        cmd1.Parameters.AddWithValue("@BOMDOCNUM", productTreeCode);
                        cmd1.Parameters.AddWithValue("@ISROUTE", isRoute ? 1 : 0);
                        cmd1.Parameters.AddWithValue("@ROTDOCTYPE", routeType);
                        cmd1.Parameters.AddWithValue("@ROTDOCNUM", routeCode);
                        cmd1.Parameters.AddWithValue("@ISDELETED", isDeleted ? 1 : 0);
                        cmd1.Parameters.AddWithValue("@ISPASSIVE", isPassive ? 1 : 0);
                        cmd1.ExecuteNonQuery();
                    }

                    string query2 = @"
                INSERT INTO BSMGRTRTMATTEXT 
                (COMCODE, MATDOCTYPE, MATDOCNUM, MATDOCFROM, MATDOCUNTIL, LANCODE, MATSTEXT, MATLTEXT) 
                VALUES 
                (@COMCODE, @MATDOCTYPE, @MATDOCNUM, @MATDOCFROM, @MATDOCUNTIL, @LANCODE, @MATSTEXT, @MATLTEXT)";

                    using (SqlCommand cmd2 = new SqlCommand(query2, con, transaction))
                    {
                        cmd2.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd2.Parameters.AddWithValue("@MATDOCTYPE", matDocType);
                        cmd2.Parameters.AddWithValue("@MATDOCNUM", matDocNum);
                        cmd2.Parameters.AddWithValue("@MATDOCFROM", matDocFrom);
                        cmd2.Parameters.AddWithValue("@MATDOCUNTIL", matDocUntil);
                        cmd2.Parameters.AddWithValue("@LANCODE", lan);
                        cmd2.Parameters.AddWithValue("@MATSTEXT", matStatementShort);
                        cmd2.Parameters.AddWithValue("@MATLTEXT", matStatementLong);
                        cmd2.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();

                    if (ex.Message.Contains("UQ_MATDOCFROM"))
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
    }

}

