using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Windows.Forms;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class OperasyonlarKartAna : Form
    {
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd;
        public OperasyonlarKartAna()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadComboBoxData();

            // ComboBox leave eventlerini bağla
            firmbox.Leave += (s, e) => ValidateAndAddData(firmbox, "COMCODE");
            rotaTipComboBox.Leave += (s, e) => ValidateAndAddData(rotaTipComboBox, "ROTDOCTYPE");
            comboBoxOprCode.Leave += (s, e) => ValidateAndAddData(comboBoxOprCode, "OPRDOCTYPE");
            MalzTipComboBox.Leave += (s, e) => ValidateAndAddData(MalzTipComboBox, "MATDOCTYPE");
        }
        private void LoadComboBoxData()
        {
            try
            {
                con.Open();

                // Firma verilerini doldur
                string queryFirma = "SELECT DISTINCT COMCODE FROM BSMGRTRTROTOPRCONTENT";
                SqlDataAdapter daFirma = new SqlDataAdapter(queryFirma, con);
                DataTable dtFirma = new DataTable();
                daFirma.Fill(dtFirma);
                firmbox.DataSource = dtFirma;
                firmbox.DisplayMember = "COMCODE";
                firmbox.ValueMember = "COMCODE";
                firmbox.DropDownStyle = ComboBoxStyle.DropDown; // Yeni veri girilebilir

                string queryRtip = "SELECT DISTINCT ROTDOCTYPE FROM BSMGRTRTROTOPRCONTENT"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daRtip = new SqlDataAdapter(queryRtip, con);
                DataTable dtRtip = new DataTable();
                daRtip.Fill(dtRtip);
                rotaTipComboBox.DataSource = dtRtip;
                rotaTipComboBox.DisplayMember = "ROTDOCTYPE";
                rotaTipComboBox.ValueMember = "ROTDOCTYPE";
                rotaTipComboBox.DropDownStyle = ComboBoxStyle.DropDown;

                string queryOtip = "SELECT DISTINCT OPRDOCTYPE FROM BSMGRTRTROTOPRCONTENT"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daOtip = new SqlDataAdapter(queryOtip, con);
                DataTable dtOtip = new DataTable();
                daOtip.Fill(dtOtip);
                comboBoxOprCode.DataSource = dtOtip;
                comboBoxOprCode.DisplayMember = "OPRDOCTYPE";
                comboBoxOprCode.ValueMember = "OPRDOCTYPE";
                comboBoxOprCode.DropDownStyle = ComboBoxStyle.DropDown;

                string queryMtip = "SELECT DISTINCT MATDOCTYPE FROM BSMGRTRTROTOPRCONTENT"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daMtip = new SqlDataAdapter(queryMtip, con);
                DataTable dtMtip = new DataTable();
                daMtip.Fill(dtMtip);
                MalzTipComboBox.DataSource = dtMtip;
                MalzTipComboBox.DisplayMember = "MATDOCTYPE";
                MalzTipComboBox.ValueMember = "MATDOCTYPE";
                MalzTipComboBox.DropDownStyle = ComboBoxStyle.DropDown;

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
            string checkQuery = $"SELECT COUNT(*) FROM BSMGRTRTROTOPRCONTENT WHERE {columnName} = @userInput";
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
            // SQL sorgusu
            string query = @"
                        SELECT 
							COMCODE AS 'Firma', 
                            ROTDOCTYPE AS 'Rota Tipi', 
                            ROTDOCNUM AS 'Rota Numarası',
							MATDOCTYPE AS 'Malzeme Tipi', 
                            MATDOCNUM AS 'Malzeme Numarası', 
                            BOMDOCFROM AS 'Geçerlilik Başlangıç',
							BOMDOCUNTIL	AS 'Geçerlilik Bitiş',
                            OPRDOCTYPE AS 'Operasyon Tipi', 
                            OPRNUM AS 'Operasyon Numarası',
							WCMDOCTYPE AS 'İş Merkezi Tipi',
							WCMDOCNUM AS 'İş Merkezi Numarası',
							SETUPTIME AS 'Op. Hazırlık Süresi',
							MACHINETIME AS 'Op. Makine Süresi',
							LABOURTIME AS 'Op. İşçilik Süresi'
							FROM 
							    BSMGRTRTROTOPRCONTENT";
            con = new SqlConnection(ConnectionHelper.ConnectionString);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;

            try
            {


                con.Open();

                // Verileri çekmek için SqlDataAdapter kullanımı
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dt = new DataSet();
                da.Fill(dt);

                // DataGridView'e verileri bağlama
                OprData.DataSource = dt.Tables[0];

            }
            catch (Exception ex)
            {
                // Hata mesajı
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void OperasyonlarKartAna_Load(object sender, EventArgs e)
        {

        }

        private void addBut_Click(object sender, EventArgs e)
        {
            string comCode = firmbox.Text.Trim();
            string rotDocType = rotaTipComboBox.Text.Trim();
            string rotDocNum = rotaNumBox.Text.Trim();
            string bomDocFrom = basTarTxtBox.Text.Trim();
            string bomDocUntil = bitistarTxtBox.Text.Trim();
            string matDocType = MalzTipComboBox.Text.Trim();
            string matDocNum = malzemeNumTBox.Text.Trim();
            string oprDocType = comboBoxOprCode.Text.Trim();
            string oprNum = operasNumTxtBox.Text.Trim();
            string wcmDocType = wcmTextTip.Text.Trim();
            string wcmDocNum = wcmNumBox.Text.Trim();
            string setupTime = opHazSureTextBox.Text.Trim();
            string machTime = OprMakSurTBox.Text.Trim();
            string labourTime = opIscılıkSurTBox.Text.Trim();

            // 1. Veri Kontrolü
            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(rotDocType) || string.IsNullOrEmpty(rotDocNum) || string.IsNullOrEmpty(bomDocFrom)
                || string.IsNullOrEmpty(bomDocUntil) || string.IsNullOrEmpty(matDocType) || string.IsNullOrEmpty(matDocNum)
                || string.IsNullOrEmpty(oprDocType) || string.IsNullOrEmpty(oprNum) || string.IsNullOrEmpty(wcmDocType) || string.IsNullOrEmpty(wcmDocNum)
                || string.IsNullOrEmpty(setupTime) || string.IsNullOrEmpty(machTime) || string.IsNullOrEmpty(labourTime))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    //COMCODE
                    string checkComCodeQuery = "SELECT COUNT(*) FROM BSMGRTRTROTOPRCONTENT WHERE COMCODE = @COMCODE";
                    using (cmd = new SqlCommand(checkComCodeQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);

                        int countryCodeExists = (int)cmd.ExecuteScalar();

                        if (countryCodeExists > 0)
                        {

                            MessageBox.Show("Bu COMCODE zaten mevcut. Lütfen başka bir COMCODE giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    //ROCDOCTYPE
                    string checkRotDocQuery = "SELECT COUNT(*) FROM BSMGRTRTROTOPRCONTENT WHERE ROTDOCTYPE = @ROTDOCTYPE";
                    using (cmd = new SqlCommand(checkRotDocQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ROTDOCTYPE", rotDocType);

                        int comCodeExists = (int)cmd.ExecuteScalar();

                        if (comCodeExists == 0)
                        {
                            MessageBox.Show("Belirtilen ROTDOCTYPE mevcut değil. Lütfen doğru bir ROTDOCTYPE giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    //MATDOCTYPE
                    string checkMatDocQuery = "SELECT COUNT(*) FROM BSMGRTRTROTOPRCONTENT WHERE MATDOCTYPE = @MATDOCTYPE";
                    using (cmd = new SqlCommand(checkMatDocQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@MATDOCTYPE", matDocType);

                        int comCodeExists = (int)cmd.ExecuteScalar();

                        if (comCodeExists == 0)
                        {
                            MessageBox.Show("Belirtilen MATDOCTYPE mevcut değil. Lütfen doğru bir MATDOCTYPE giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    //WCMDOCTYPE
                    string checkWcmDocQuery = "SELECT COUNT(*) FROM BSMGRTRTROTOPRCONTENT WHERE WCMDOCTYPE = @WCMDOCTYPE";
                    using (cmd = new SqlCommand(checkWcmDocQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@WCMDOCTYPE", wcmDocType);

                        int comCodeExists = (int)cmd.ExecuteScalar();

                        if (comCodeExists == 0)
                        {
                            MessageBox.Show("Belirtilen WCMDOCTYPE mevcut değil. Lütfen doğru bir WCMDOCTYPE giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 4. Ekleme İşlemi
                    string insertQuery = "INSERT INTO BSMGRTRTROTOPRCONTENT (COMCODE, ROTDOCTYPE, ROTDOCNUM, MATDOCTYPE, MATDOCNUM, BOCDOCFROM, BOMDOCUNTIL,OPRDOCTYPE," +
                        " OPRNUM, WCMDOCTYPE, WCMDOCNUM, SETUPTIME, MACHINETIME, LABOURTIME) VALUES (@COMCODE, @ROTDOCTYPE, @ROTDOCNUM, @MATDOCTYPE, @MATDOCNUM, @BOCDOCFROM, @BOMDOCUNTIL, @OPRDOCTYPE," +
                        "@OPRNUM ,@WCMDOCTYPE ,@WCMDOCNUM ,@SETUPTIME ,@MACHINETIME ,@LABOURTIME)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd.Parameters.AddWithValue("@ROTDOCTYPE", rotDocType);
                        cmd.Parameters.AddWithValue("@ROTDOCNUM", rotDocNum);
                        cmd.Parameters.AddWithValue("@MATDOCTYPE", matDocType);
                        cmd.Parameters.AddWithValue("@MATDOCNUM", matDocNum);
                        cmd.Parameters.AddWithValue("@BOMDOCFROM", bomDocFrom);
                        cmd.Parameters.AddWithValue("@BOMDOCUNTIL", bomDocUntil);
                        cmd.Parameters.AddWithValue("@OPRDOCTYPE", oprDocType);
                        cmd.Parameters.AddWithValue("@OPRNUM", oprNum);
                        cmd.Parameters.AddWithValue("@WCMDOCTYPE", wcmDocType);
                        cmd.Parameters.AddWithValue("@WCMDOCNUM", wcmDocNum);
                        cmd.Parameters.AddWithValue("@SETUPTIME", setupTime);
                        cmd.Parameters.AddWithValue("@MACHINETIME", machTime);
                        cmd.Parameters.AddWithValue("@LABOURTIME", labourTime);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // TextBox'ları temizle
                            firmbox.Items.Clear();
                            rotaTipComboBox.Items.Clear();
                            rotaNumBox.Clear();
                            basTarTxtBox.Clear();
                            bitistarTxtBox.Clear();
                            MalzTipComboBox.Items.Clear();
                            malzemeNumTBox.Clear();
                            comboBoxOprCode.Items.Clear();
                            operasNumTxtBox.Clear();
                            wcmTextTip.Clear();
                            wcmNumBox.Clear();
                            opHazSureTextBox.Clear();
                            OprMakSurTBox.Clear();
                            opIscılıkSurTBox.Clear();


                        }
                        else
                        {
                            MessageBox.Show("Kayıt eklenemedi. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
