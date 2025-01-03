﻿using System;
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
            lanComboBox.Leave += (s, e) => ValidateAndAddData(matStockUnitComboBox, "UNITCODE", "BSMGRTRTGEN005");
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

        private void malzemeAnaTabloEdit_Load(object sender, EventArgs e)
        {
            firmCodeComboBox.Text = Firma;
            matTypeComboBox.Text = MatdocType;
            DateTimePickerBaslangic.Value = GecerliBaslangic;
            DateTimePickerBitis.Value = GecerliBitis;
            matCodeTextBox.Text = MatCode.ToString();
            supplyTypeTextBox.Text = supplytype.ToString();
            matStatementLongTextBox.Text = uzunAciklama;
            matStatementShortTextBox.Text = kisaAciklama;
            netWeightTextBox.Text = netWeight.ToString();
            brutWeightTextBox.Text = brutWeight.ToString();
            netWeightUnitComboBox.Text = netWeightUnit;
            brutWeightUnitComboBox.Text = brutWeightUnit;
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

                        // Transaction başlat
                        using (SqlTransaction transaction = con.BeginTransaction())
                        {
                            cmd.Connection = con;
                            cmd.Transaction = transaction;

                            // Başlangıç ve Bitiş tarihlerini al
                            DateTime bomDocFrom = DateTime.TryParse(DateTimePickerBaslangic.Text, out DateTime fromDate) ? fromDate : DateTime.MinValue;
                            DateTime bomDocUntil = DateTime.TryParse(DateTimePickerBitis.Text, out DateTime untilDate) ? untilDate : DateTime.MinValue;

                            // Bitiş tarihinin, Başlangıç tarihinden önce olup olmadığını kontrol et
                            if (bomDocFrom >= bomDocUntil)
                            {
                                MessageBox.Show("Başlangıç tarihi, bitiş tarihinden önce olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // İşlemi durdur
                            }

                            try
                            {
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

                                using (SqlCommand command1 = new SqlCommand(query1, con, transaction))
                                {
                                    // Parametreler ekleme
                                    command1.Parameters.AddWithValue("@COMCODE", firmCodeComboBox.Text ?? (object)DBNull.Value);
                                    command1.Parameters.AddWithValue("@MATDOCTYPE", matTypeComboBox.Text ?? (object)DBNull.Value);
                                    command1.Parameters.AddWithValue("@MATDOCNUM", matCodeTextBox.Text ?? (object)DBNull.Value);
                                    command1.Parameters.AddWithValue("@MATDOCFROM", bomDocFrom);
                                    command1.Parameters.AddWithValue("@MATDOCUNTIL", bomDocUntil);
                                    command1.Parameters.AddWithValue("@SUPPLYTYPE", int.TryParse(supplyTypeTextBox.Text, out int supplytype) ? supplytype : (object)DBNull.Value);
                                    command1.Parameters.AddWithValue("@STUNIT", matStockUnitComboBox.Text ?? (object)DBNull.Value);
                                    command1.Parameters.AddWithValue("@NETWEIGHT", decimal.TryParse(netWeightTextBox.Text, out decimal netWeight) ? netWeight : (object)DBNull.Value);
                                    command1.Parameters.AddWithValue("@NWUNIT", netWeightUnitComboBox.Text ?? (object)DBNull.Value);
                                    command1.Parameters.AddWithValue("@BRUTWEIGHT", decimal.TryParse(brutWeightTextBox.Text, out decimal brutWeight) ? brutWeight : (object)DBNull.Value);
                                    command1.Parameters.AddWithValue("@BWUNIT", brutWeightUnitComboBox.Text ?? (object)DBNull.Value);
                                    command1.Parameters.AddWithValue("@ISBOM", isTreeCheckBox.Checked ? 1 : 0);
                                    command1.Parameters.AddWithValue("@BOMDOCTYPE", productTreeTypeComboBox.Text ?? (object)DBNull.Value);
                                    command1.Parameters.AddWithValue("@BOMDOCNUM", productTreeCodeTextBox.Text ?? (object)DBNull.Value);
                                    command1.Parameters.AddWithValue("@ISROUTE", isRouteCheckBox.Checked ? 1 : 0);
                                    command1.Parameters.AddWithValue("@ROTDOCTYPE", routeTypeComboBox.Text ?? (object)DBNull.Value);
                                    command1.Parameters.AddWithValue("@ROTDOCNUM", routeCodeTextBox.Text ?? (object)DBNull.Value);
                                    command1.Parameters.AddWithValue("@ISDELETED", isDeletedCheckBox.Checked ? 1 : 0);
                                    command1.Parameters.AddWithValue("@ISPASSIVE", isPassiveCheckBox.Checked ? 1 : 0);

                                    command1.ExecuteNonQuery();
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

                                using (SqlCommand command2 = new SqlCommand(query2, con, transaction))
                                {
                                    // Parametreler ekleme
                                    command2.Parameters.AddWithValue("@COMCODE", firmCodeComboBox.Text ?? (object)DBNull.Value);
                                    command2.Parameters.AddWithValue("@MATDOCTYPE", matTypeComboBox.Text ?? (object)DBNull.Value);
                                    command2.Parameters.AddWithValue("@MATDOCNUM", matCodeTextBox.Text ?? (object)DBNull.Value);
                                    command2.Parameters.AddWithValue("@MATDOCFROM", bomDocFrom);
                                    command2.Parameters.AddWithValue("@MATDOCUNTIL", bomDocUntil);
                                    command2.Parameters.AddWithValue("@LANCODE", lanComboBox.Text ?? (object)DBNull.Value);
                                    command2.Parameters.AddWithValue("@MATSTEXT", matStatementShortTextBox.Text ?? (object)DBNull.Value);
                                    command2.Parameters.AddWithValue("@MATLTEXT", matStatementLongTextBox.Text ?? (object)DBNull.Value);

                                    command2.ExecuteNonQuery();
                                }

                                // İşlem başarılı ise transaction'ı tamamla
                                transaction.Commit();
                                MessageBox.Show("Kayıt başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (SqlException sqlEx)
                            {
                                // SQL Hatası yakalama ve rollback
                                transaction.Rollback();
                                MessageBox.Show($"SQL Hatası: {sqlEx.Message} (Hata Kodu: {sqlEx.Number})", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            catch (Exception ex)
                            {
                                // Diğer hatalar için rollback
                                transaction.Rollback();
                                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }


    }
}





