using System.Data;
using System.Data.SqlClient;
using TRTERPproject.Helpers;
namespace TRTERPproject
{
    public partial class malzemeKartAna : Form
    {
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd;


        public malzemeKartAna()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadComboBoxData();

            // ComboBox leave eventlerini bağla
            comboBoxMalzFirm.Leave += (s, e) => ValidateAndAddData(comboBoxMalzFirm, "COMCODE", "BSMGRTRTGEN001");
            malzTipcombo.Leave += (s, e) => ValidateAndAddData(malzTipcombo, "DOCTYPE", "BSMGRTRTMAT001");
            comboBoxTedTip.Leave += (s, e) => ValidateAndAddData(comboBoxTedTip, "SUPPLYTYPE", "BSMGRTRTMATHEAD");
            comboBoxDil.Leave += (s, e) => ValidateAndAddData(comboBoxDil, "LANCODE", "BSMGRTRTGEN002");
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


        private void LoadComboBoxData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    con.Open();

                    
                    LoadComboBox(comboBoxMalzFirm, "SELECT DISTINCT COMCODE FROM BSMGRTRTGEN001", "COMCODE");

                    
                    LoadComboBox(malzTipcombo, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTMAT001", "DOCTYPE");

                    
                    LoadComboBox(comboBoxDil, "SELECT DISTINCT LANCODE FROM BSMGRTRTGEN002", "LANCODE");

                    LoadComboBox(comboBoxTedTip, "SELECT DISTINCT SUPPLYTYPE FROM BSMGRTRTMATHEAD", "SUPPLYTYPE");

                   
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}");
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


        private void getBut_Click(object sender, EventArgs e)
        {
            // Temel SQL sorgusu
            string query = @"
    SELECT 
        MH.COMCODE AS 'Firma', 
        MH.MATDOCTYPE AS 'Malzeme Tipi', 
        MH.MATDOCNUM AS 'Malzeme Numarası', 
        MH.MATDOCFROM AS 'Geçerlilik Başlangıç',
        MH.MATDOCUNTIL AS 'Geçerlilik Bitiş',
        MH.SUPPLYTYPE AS 'Tedarik Tipi',
        MH.STUNIT AS 'Stok Birimi',
        MH.NETWEIGHT AS 'Net Ağırlık',
        MH.NWUNIT AS 'Net Ağırlık Birimi',
        MH.BRUTWEIGHT AS 'Brüt Ağırlık',
        MH.BWUNIT AS 'Brüt Ağırlık Birimi',
        MH.ISBOM AS 'Ürün Ağacı Var mı?',
        MH.BOMDOCTYPE AS 'Ürün Ağacı Tipi',
        MH.BOMDOCNUM AS 'Ürün Ağacı Kodu',
        MH.ISROUTE AS 'Rota Var mı?',
        MH.ROTDOCTYPE AS 'Rota Tipi',
        MH.ROTDOCNUM AS 'Rota Kodu',
        MH.ISDELETED AS 'Silindi mi?',
        MH.ISPASSIVE AS 'Pasif mi?',
        G2.LANCODE AS 'Dil'
    FROM 
        BSMGRTRTMATHEAD MH
    INNER JOIN 
        BSMGRTRTMATTEXT MT ON MH.MATDOCNUM = MT.MATDOCNUM
    INNER JOIN 
        BSMGRTRTGEN002 G2 ON MT.LANCODE = G2.LANCODE";

            // Filtreleme koşulları
            List<string> filters = new List<string>();

            // Firma filtresi
            if (!string.IsNullOrEmpty(comboBoxMalzFirm.Text))
            {
                filters.Add("MH.COMCODE = @COMCODE");
            }

            // Malzeme tipi filtresi
            if (!string.IsNullOrEmpty(malzTipcombo.Text))
            {
                filters.Add("MH.MATDOCTYPE = @MATDOCTYPE");
            }

            // Tedarik tipi filtresi
            if (!string.IsNullOrEmpty(comboBoxTedTip.Text))
            {
                filters.Add("MH.SUPPLYTYPE = @SUPPLYTYPE");
            }

            // Dil filtresi
            if (!string.IsNullOrEmpty(comboBoxDil.Text))
            {
                filters.Add("G2.LANCODE = @LANCODE");
            }

            // Tarih aralığı filtresi
            if (dateTimePickerBaslangic.Value != null && dateTimePickerBitis.Value != null)
            {
                filters.Add("MH.MATDOCFROM >= @MATDOCFROM AND MH.MATDOCUNTIL <= @MATDOCUNTIL");
            }

            // isPassiveCheckBox filtresi
            if (isPassiveCheckBox.Checked)
            {
                filters.Add("MH.ISPASSIVE = 1");
            }
            else
            {
                filters.Add("MH.ISPASSIVE = 0");
            }

            // isDeletedCheckBox filtresi
            if (isDeletedCheckBox.Checked)
            {
                filters.Add("MH.ISDELETED = 1");
            }
            else
            {
                filters.Add("MH.ISDELETED = 0");
            }

            // Filtreleri sorguya ekle
            if (filters.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", filters);
            }

            // SQL bağlantısı ve komutu
            con = new SqlConnection(ConnectionHelper.ConnectionString);
            cmd = new SqlCommand(query, con);

            // Parametreleri ekle
            if (!string.IsNullOrEmpty(comboBoxMalzFirm.Text))
            {
                cmd.Parameters.AddWithValue("@COMCODE", comboBoxMalzFirm.Text);
            }

            if (!string.IsNullOrEmpty(malzTipcombo.Text))
            {
                cmd.Parameters.AddWithValue("@MATDOCTYPE", malzTipcombo.Text);
            }

            if (!string.IsNullOrEmpty(comboBoxTedTip.Text))
            {
                cmd.Parameters.AddWithValue("@SUPPLYTYPE", comboBoxTedTip.Text);
            }

            if (!string.IsNullOrEmpty(comboBoxDil.Text))
            {
                cmd.Parameters.AddWithValue("@LANCODE", comboBoxDil.Text);
            }

            if (dateTimePickerBaslangic.Value != null && dateTimePickerBitis.Value != null)
            {
                cmd.Parameters.AddWithValue("@MATDOCFROM", dateTimePickerBaslangic.Value);
                cmd.Parameters.AddWithValue("@MATDOCUNTIL", dateTimePickerBitis.Value);
            }

            // Verileri getir
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                da.Fill(dt);
                malKartAna.DataSource = dt.Tables[0];
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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void malzemeKartAna_Load(object sender, EventArgs e)
        {

        }

        private void getAll_Click(object sender, EventArgs e)
        {
            string query = @"
    SELECT 
        MH.COMCODE AS 'Firma', 
        MH.MATDOCTYPE AS 'Malzeme Tipi', 
        MH.MATDOCNUM AS 'Malzeme Numarası', 
        MH.MATDOCFROM AS 'Geçerlilik Başlangıç',
        MH.MATDOCUNTIL AS 'Geçerlilik Bitiş',
        MH.SUPPLYTYPE AS 'Tedarik Tipi',
        MH.STUNIT AS 'Stok Birimi',
        MH.NETWEIGHT AS 'Net Ağırlık',
        MH.NWUNIT AS 'Net Ağırlık Birimi',
        MH.BRUTWEIGHT AS 'Brüt Ağırlık',
        MH.BWUNIT AS 'Brüt Ağırlık Birimi',
        MH.ISBOM AS 'Ürün Ağacı Var mı?',
        MH.BOMDOCTYPE AS 'Ürün Ağacı Tipi',
        MH.BOMDOCNUM AS 'Ürün Ağacı Kodu',
        MH.ISROUTE AS 'Rota Var mı?',
        MH.ROTDOCTYPE AS 'Rota Tipi',
        MH.ROTDOCNUM AS 'Rota Kodu',
        MH.ISDELETED AS 'Silindi mi?',
        MH.ISPASSIVE AS 'Pasif mi?',
        G2.LANCODE AS 'Dil'
    FROM 
        BSMGRTRTMATHEAD MH
    INNER JOIN 
        BSMGRTRTMATTEXT MT ON MH.MATDOCNUM = MT.MATDOCNUM
    INNER JOIN 
        BSMGRTRTGEN002 G2 ON MT.LANCODE = G2.LANCODE";

            // Verileri getirme işlemi
            try
            {
                con.Open();
                SqlCommand getAllCmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(getAllCmd);
                DataSet dt = new DataSet();
                da.Fill(dt);
                malKartAna.DataSource = dt.Tables[0]; // DataGridView'e atama
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

        private void addBut_Click(object sender, EventArgs e)
        {
            matAddForm MatAddForm = new matAddForm();
            MatAddForm.Show();

        }


        private void DelBut_Click(object sender, EventArgs e)
        {
            // DataGridView'den seçilen satırı kontrol et
            if (malKartAna.SelectedRows.Count > 0)
            {
                // Seçilen satırdaki "Malzeme Numarası" bilgisini al
                DataGridViewRow selectedRow = malKartAna.SelectedRows[0];

                // "Malzeme Numarası" hücresinin değeri boşsa (null ya da boş)
                if (selectedRow.Cells["Malzeme Numarası"].Value == null || string.IsNullOrWhiteSpace(selectedRow.Cells["Malzeme Numarası"].Value.ToString()))
                {
                    MessageBox.Show("Boş bir satır seçemezsiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Boş satırda işlem yapılmasın
                }

                string matDocNum = selectedRow.Cells["Malzeme Numarası"].Value.ToString();

                // Kullanıcıdan onay al
                DialogResult dialogResult = MessageBox.Show(
                    $"Malzeme Numarası {matDocNum} olan veriyi silmek istediğinize emin misiniz?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();

                        // BSMGRTRTMATHEAD tablosundan silme sorgusu
                        string deleteFromMatHead = "DELETE FROM BSMGRTRTMATHEAD WHERE MATDOCNUM = @MATDOCNUM";
                        SqlCommand cmdMatHead = new SqlCommand(deleteFromMatHead, con);
                        cmdMatHead.Parameters.AddWithValue("@MATDOCNUM", matDocNum);
                        cmdMatHead.ExecuteNonQuery();

                        // BSMGRTRTMATTEXT tablosundan silme sorgusu
                        string deleteFromMatText = "DELETE FROM BSMGRTRTMATTEXT WHERE MATDOCNUM = @MATDOCNUM";
                        SqlCommand cmdMatText = new SqlCommand(deleteFromMatText, con);
                        cmdMatText.Parameters.AddWithValue("@MATDOCNUM", matDocNum);
                        cmdMatText.ExecuteNonQuery();

                        // Başarılı silme mesajı
                        MessageBox.Show("Seçilen veri başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // DataGridView'i güncelle
                        malKartAna.Rows.Remove(selectedRow);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.Close();
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
            if (malKartAna.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = malKartAna.SelectedRows[0];

                // Yeni bir edit form oluştur ve seçilen veriyi aktar
                malzemeAnaTabloEdit MalzemeAnaTabloEdit = new malzemeAnaTabloEdit();

                // Formdaki alanlara DataGridView'deki değerleri aktar
                MalzemeAnaTabloEdit.Firma = selectedRow.Cells["Firma"].Value != DBNull.Value
                    ? selectedRow.Cells["Firma"].Value.ToString()
                    : string.Empty;

                MalzemeAnaTabloEdit.MatdocType = selectedRow.Cells["Malzeme Tipi"].Value != DBNull.Value
                    ? selectedRow.Cells["Malzeme Tipi"].Value.ToString()
                    : string.Empty;

                MalzemeAnaTabloEdit.MatCode = selectedRow.Cells["Malzeme Numarası"].Value != DBNull.Value
                    ? Convert.ToInt32(selectedRow.Cells["Malzeme Numarası"].Value)
                    : 0;

                MalzemeAnaTabloEdit.GecerliBaslangic = selectedRow.Cells["Geçerlilik Başlangıç"].Value != DBNull.Value
                    ? Convert.ToDateTime(selectedRow.Cells["Geçerlilik Başlangıç"].Value)
                    : DateTime.MinValue;

                MalzemeAnaTabloEdit.GecerliBitis = selectedRow.Cells["Geçerlilik Bitiş"].Value != DBNull.Value
                    ? Convert.ToDateTime(selectedRow.Cells["Geçerlilik Bitiş"].Value)
                    : DateTime.MaxValue;

                MalzemeAnaTabloEdit.supplytype = selectedRow.Cells["Tedarik Tipi"].Value != DBNull.Value
                    ? Convert.ToInt32(selectedRow.Cells["Tedarik Tipi"].Value)
                    : 0;

                MalzemeAnaTabloEdit.malzemeStokBirimi = selectedRow.Cells["Stok Birimi"].Value != DBNull.Value
                    ? selectedRow.Cells["Stok Birimi"].Value.ToString()
                    : string.Empty;

                MalzemeAnaTabloEdit.netWeight = selectedRow.Cells["Net Ağırlık"].Value != DBNull.Value
                    ? Convert.ToInt32(selectedRow.Cells["Net Ağırlık"].Value)
                    : (int?)null;

                MalzemeAnaTabloEdit.netWeightUnit = selectedRow.Cells["Net Ağırlık Birimi"].Value != DBNull.Value
                    ? selectedRow.Cells["Net Ağırlık Birimi"].Value.ToString()
                    : string.Empty;

                // MATSTEXT ve MATLTEXT verilerini al
                string selectMatsText = "SELECT MATSTEXT, MATLTEXT FROM BSMGRTRTMATTEXT WHERE MATDOCNUM = @MATDOCNUM";

                // Ensure the connection is opened before executing the command

                /*
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    con.Open();  // Open the connection

                    using (SqlCommand cmdMatText = new SqlCommand(selectMatsText, con))
                    {
                        cmdMatText.Parameters.AddWithValue("@MATDOCNUM", selectedRow.Cells["Malzeme Numarası"].Value != DBNull.Value
                            ? Convert.ToInt32(selectedRow.Cells["Malzeme Numarası"].Value)
                            : 0);

                        using (SqlDataReader reader = cmdMatText.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // MATSTEXT ve MATLTEXT verilerini al
                                MalzemeAnaTabloEdit.kisaAciklama = reader["MATSTEXT"] != DBNull.Value.ToString()
                                    ? reader["MATSTEXT"].ToString()
                                    : string.Empty;

                                // Set MATLTEXT value as well
                                MalzemeAnaTabloEdit.uzunAciklama = reader["MATLTEXT"] != DBNull.Value.ToString()
                                    ? reader["MATLTEXT"].ToString()
                                    : string.Empty;
                            }
                        }
                    }
                }

                */

                // Other fields assignment
                MalzemeAnaTabloEdit.brutWeightUnit = selectedRow.Cells["Brüt Ağırlık Birimi"].Value != DBNull.Value
                    ? selectedRow.Cells["Brüt Ağırlık Birimi"].Value.ToString()
                    : string.Empty;

                MalzemeAnaTabloEdit.isTree = selectedRow.Cells["Ürün Ağacı Var mı?"].Value != DBNull.Value
                    ? Convert.ToBoolean(selectedRow.Cells["Ürün Ağacı Var mı?"].Value)
                    : false;

                MalzemeAnaTabloEdit.treeType = selectedRow.Cells["Ürün Ağacı Tipi"].Value != DBNull.Value
                    ? selectedRow.Cells["Ürün Ağacı Tipi"].Value.ToString()
                    : string.Empty;

                MalzemeAnaTabloEdit.treeCode = selectedRow.Cells["Ürün Ağacı Kodu"].Value != DBNull.Value
                    ? selectedRow.Cells["Ürün Ağacı Kodu"].Value.ToString()
                    : string.Empty;

                MalzemeAnaTabloEdit.isRot = selectedRow.Cells["Rota Var mı?"].Value != DBNull.Value
                    ? Convert.ToBoolean(selectedRow.Cells["Rota Var mı?"].Value)
                    : false;

                MalzemeAnaTabloEdit.rotType = selectedRow.Cells["Rota Tipi"].Value != DBNull.Value
                    ? selectedRow.Cells["Rota Tipi"].Value.ToString()
                    : string.Empty;

                MalzemeAnaTabloEdit.rotCode = selectedRow.Cells["Rota Kodu"].Value != DBNull.Value
                    ? selectedRow.Cells["Rota Kodu"].Value.ToString()
                    : string.Empty;

                MalzemeAnaTabloEdit.IsDeleted = selectedRow.Cells["Silindi mi?"].Value != DBNull.Value
                    ? Convert.ToBoolean(selectedRow.Cells["Silindi mi?"].Value)
                    : false;

                MalzemeAnaTabloEdit.IsPassive = selectedRow.Cells["Pasif mi?"].Value != DBNull.Value
                    ? Convert.ToBoolean(selectedRow.Cells["Pasif mi?"].Value)
                    : false;

                MalzemeAnaTabloEdit.Dil = selectedRow.Cells["Dil"].Value != DBNull.Value
                    ? selectedRow.Cells["Dil"].Value.ToString()
                    : string.Empty;

                // Edit formu göster
                MalzemeAnaTabloEdit.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek için bir satır seçin.");
            }
        }

        }
}