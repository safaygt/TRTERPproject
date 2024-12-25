﻿using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class RotaKartAna : Form
    {
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        private readonly SqlConnection _connection = new SqlConnection(ConnectionHelper.ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd;
        public RotaKartAna()
        {
            InitializeComponent();
            this.Load += (s, e) => LoadComboBoxData();

            // ComboBox leave eventlerini bağla
            firmbox.Leave += (s, e) => ValidateAndAddData(firmbox, "COMCODE");
            comboBoxRotTip.Leave += (s, e) => ValidateAndAddData(comboBoxRotTip, "ROTDOCTYPE");
            dilBox.Leave += (s, e) => ValidateAndAddData(dilBox, "LANCODE");
        }
        private void LoadComboBoxData()
        {
            try
            {
                con.Open();

                // Firma verilerini doldur
                string queryFirma = "SELECT DISTINCT COMCODE FROM BSMGRTRTROTHEAD";
                SqlDataAdapter daFirma = new SqlDataAdapter(queryFirma, con);
                DataTable dtFirma = new DataTable();
                daFirma.Fill(dtFirma);
                firmbox.DataSource = dtFirma;
                firmbox.DisplayMember = "COMCODE";
                firmbox.ValueMember = "COMCODE";
                firmbox.DropDownStyle = ComboBoxStyle.DropDown; // Yeni veri girilebilir

                string queryMtip = "SELECT DISTINCT ROTDOCTYPE FROM BSMGRTRTROTHEAD"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daMtip = new SqlDataAdapter(queryMtip, con);
                DataTable dtMtip = new DataTable();
                daMtip.Fill(dtMtip);
                comboBoxRotTip.DataSource = dtMtip;
                comboBoxRotTip.DisplayMember = "ROTDOCTYPE";
                comboBoxRotTip.ValueMember = "ROTDOCTYPE";
                comboBoxRotTip.DropDownStyle = ComboBoxStyle.DropDown;

                string queryTtip = "SELECT DISTINCT LANCODE FROM BSMGRTRTGEN002"; // Tablo ve sütun adını kontrol edin
                SqlDataAdapter daTtip = new SqlDataAdapter(queryTtip, con);
                DataTable dtTtip = new DataTable();
                daTtip.Fill(dtTtip);
                dilBox.DataSource = dtTtip;
                dilBox.DisplayMember = "LANCODE";
                dilBox.ValueMember = "LANCODE";
                dilBox.DropDownStyle = ComboBoxStyle.DropDown;

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
            string checkQuery = $"SELECT COUNT(*) FROM BSMGRTRTROTHEAD WHERE {columnName} = @userInput";
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
        {// Temel SQL sorgusu
            string query = @"SELECT 
					RH.COMCODE AS 'Firma', 
                    RH.ROTDOCTYPE AS 'Rota Tipi', 
                    RH.ROTDOCNUM AS 'Rota Numarası',
					RH.MATDOCTYPE AS 'Malzeme Tipi',
					RH.MATDOCNUM AS 'Malzeme Numarası',
                    RH.ROTDOCFROM AS 'Geçerlilik Başlangıç',
					RH.ROTDOCUNTIL	AS 'Geçerlilik Bitiş',
					RH.QUANTITY AS 'Miktar',
					RH.DRAWNUM AS 'Çizim Numarası',
					RH.ISDELETED AS 'Silindi mi?',
					RH.ISPASSIVE AS 'Pasif mi?',
					G2.LANCODE AS 'Dil'
					FROM 
					    BSMGRTRTROTHEAD RH
					INNER JOIN 
					    BSMGRTRTMATHEAD MH ON MH.MATDOCNUM = RH.MATDOCNUM
					INNER JOIN
						BSMGRTRTMATTEXT MT ON MH.MATDOCNUM = MT.MATDOCNUM
					INNER JOIN 
					    BSMGRTRTGEN002 G2 ON MT.LANCODE = G2.LANCODE";

            // Filtreleme koşulları
            List<string> filters = new List<string>();

            // Firma filtresi
            if (!string.IsNullOrEmpty(firmbox.Text))
            {
                filters.Add("RH.COMCODE = @COMCODE");
            }

            // Maliyet Merkezi Tipi filtresi
            if (!string.IsNullOrEmpty(comboBoxRotTip.Text))
            {
                filters.Add("RH.ROTDOCTYPE = @ROTDOCTYPE");
            }

            // Maliyet Merkezi Numarası filtresi
            if (!string.IsNullOrEmpty(malNotxtBox.Text))
            {
                filters.Add("RH.ROTDOCNUM = @ROTDOCNUM");
            }

            // Ana Maliyet Merkezi Tipi filtresi
            if (!string.IsNullOrEmpty(maliyTxtBox.Text))
            {
                filters.Add("RH.MATDOCNUM = @MATDOCNUM");
            }

            // Tarih aralığı filtresi
            if (dateTimePickerBaslangic.Value.Date != DateTime.MinValue.Date && dateTimePickerBitis.Value.Date != DateTime.MinValue.Date)
            {
                filters.Add("RH.ROTDOCFROM >= @CCMDOCFROM AND RH.ROTDOCUNTIL <= @CCMDOCUNTIL");
            }

            // Pasiflik kontrolü
            if (checkboxpas.Checked)
            {
                filters.Add("RH.ISPASSIVE = 1");
            }

            // Silinmişlik kontrolü
            if (deletedlbl.Checked)
            {
                filters.Add("RH.ISDELETED = 1");
            }

            // Filtreleri sorguya ekle
            if (filters.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", filters);
            }

            // SQL bağlantısı ve komutu
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Parametreleri ekle
                if (!string.IsNullOrEmpty(firmbox.Text))
                {
                    cmd.Parameters.AddWithValue("@COMCODE", firmbox.Text);
                }

                if (!string.IsNullOrEmpty(comboBoxRotTip.Text))
                {
                    cmd.Parameters.AddWithValue("@ROTDOCTYPE", comboBoxRotTip.Text);
                }

                if (!string.IsNullOrEmpty(malNotxtBox.Text))
                {
                    cmd.Parameters.AddWithValue("@ROTDOCNUM", malNotxtBox.Text);
                }

                if (!string.IsNullOrEmpty(maliyTxtBox.Text))
                {
                    cmd.Parameters.AddWithValue("@MATDOCNUM", maliyTxtBox.Text);
                }

                if (dateTimePickerBaslangic.Value.Date != DateTime.MinValue.Date && dateTimePickerBitis.Value.Date != DateTime.MinValue.Date)
                {
                    cmd.Parameters.AddWithValue("@CCMDOCFROM", dateTimePickerBaslangic.Value.Date);
                    cmd.Parameters.AddWithValue("@CCMDOCUNTIL", dateTimePickerBitis.Value.Date);
                }

                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dt = new DataSet();
                    da.Fill(dt);

                    // DataGridView'e sonuçları bağla
                    RotaData.DataSource = dt.Tables[0];

                    // Veri olmadığında kullanıcıyı bilgilendir
                    if (dt.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("Filtrelerinizle eşleşen bir veri bulunamadı.");
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
        }

        private void getAllBut_Click(object sender, EventArgs e)
        {
            string query = @"
                        SELECT 
							RH.COMCODE AS 'Firma', 
                            RH.ROTDOCTYPE AS 'Rota Tipi', 
                            RH.ROTDOCNUM AS 'Rota Numarası',
							RH.MATDOCTYPE AS 'Malzeme Tipi',
							RH.MATDOCNUM AS 'Malzeme Numarası',
                            RH.ROTDOCFROM AS 'Geçerlilik Başlangıç',
							RH.ROTDOCUNTIL	AS 'Geçerlilik Bitiş',
							RH.QUANTITY AS 'Miktar',
							RH.DRAWNUM AS 'Çizim Numarası',
							RH.ISDELETED AS 'Silindi mi?',
							RH.ISPASSIVE AS 'Pasif mi?',
							G2.LANCODE AS 'Dil'
							FROM 
							    BSMGRTRTROTHEAD RH
							INNER JOIN 
							    BSMGRTRTMATHEAD MH ON MH.MATDOCNUM = RH.MATDOCNUM
							INNER JOIN
								BSMGRTRTMATTEXT MT ON MH.MATDOCNUM = MT.MATDOCNUM
							INNER JOIN 
							    BSMGRTRTGEN002 G2 ON MT.LANCODE = G2.LANCODE";
            con = new SqlConnection(ConnectionHelper.ConnectionString);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            try
            {
                using (con)
                {
                    con.Open();

                    // Verileri çekmek için SqlDataAdapter kullanımı
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dt = new DataSet();
                    da.Fill(dt);

                    // DataGridView'e verileri bağlama
                    RotaData.DataSource = dt.Tables[0];
                }
            }
            catch (Exception ex)
            {
                // Hata mesajı
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

    }
}

