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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TRTERPproject
{
    public partial class MaliyetKartAnaEdit : Form
    {
        private string malCarNum;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

        public MaliyetKartAnaEdit(string malCarNum, string? firma, string? maliyetMerkeziTipi, string? gecerlilikBaslangic, string? gecerlilikBitis)
        {
            InitializeComponent();
            this.malCarNum = malCarNum;
        }

        public MaliyetKartAnaEdit(string malCarNum)
        {
        }

        private void MaliyetKartAnaEdit_Load(object sender, EventArgs e)
        {
            string query = @"SELECT 
                                COMCODE, 
                                CCMDOCTYPE, 
                                CCMDOCNUM, 
                                CCMDOCFROM,
                                CCMDOCUNTIL,
                                MAINCCMDOCTYPE, 
                                MAINCCMDOCNUM, 
                                ISDELETED,
                                ISPASSIVE
                                FROM BSMGRTRTCCMHEAD
                                WHERE CCMDOCNUM = @CCMDOCNUM";

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@CCMDOCNUM", malCarNum);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) // Eğer veri varsa
                {
                    // Form elemanlarını veritabanı ile dolduruyoruz
                    firmComboBox.Text = reader["COMCODE"].ToString();
                    comboBoxMalMerTip.Text = reader["CCMDOCTYPE"].ToString();
                    malNotxtBox.Text = reader["CCMDOCNUM"].ToString();
                    maliyTxtBox.Text = reader["MAINCCMDOCTYPE"].ToString();
                    maliKodTxtBox.Text = reader["MAINCCMDOCNUM"].ToString();

                    // DateTimePicker'lara tarihleri yerleştiriyoruz
                    dateTimePickerBaslangic.Value = Convert.ToDateTime(reader["CCMDOCFROM"]);
                    dateTimePickerBitis.Value = Convert.ToDateTime(reader["CCMDOCUNTIL"]);

                    checkboxpas.Checked = Convert.ToBoolean(reader["ISPASSIVE"]);
                    deletedlbl.Checked = Convert.ToBoolean(reader["ISDELETED"]);
                }
                else
                {
                    MessageBox.Show("Girilen Ürün Ağacı Numarasına ait bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close(); // Kayıt bulunamazsa formu kapatıyoruz
                }
                reader.Close(); // SqlDataReader'ı kapatıyoruz
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata111: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close(); // Bağlantıyı kapatıyoruz
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Tarihler arasındaki mantık hatasını kontrol etme
            DateTime CCMDOCFROM = dateTimePickerBaslangic.Value;
            DateTime CCMDOCUNTIL = dateTimePickerBitis.Value;

            if (CCMDOCFROM >= CCMDOCUNTIL)
            {
                MessageBox.Show("Başlangıç tarihi, bitiş tarihinden önce olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // İşlemi durdur
            }

            string query = @"UPDATE BSMGRTRTCCMHEAD
                            SET 
                                COMCODE = @COMCODE, 
                                CCMDOCTYPE = @CCMDOCTYPE, 
                                CCMDOCFROM = @CCMDOCFROM,
                                CCMDOCUNTIL = @CCMDOCUNTIL,
                                MAINCCMDOCTYPE = @MAINCCMDOCTYPE, 
                                MAINCCMDOCNUM = @MAINCCMDOCNUM, 
                                ISDELETED = @ISDELETED,
                                ISPASSIVE = @ISPASSIVE
                            WHERE CCMDOCNUM = @CCMDOCNUM";

            cmd = new SqlCommand(query, con);

            // Parametreleri ekliyoruz
            cmd.Parameters.AddWithValue("@COMCODE", firmComboBox.Text.Trim());
            cmd.Parameters.AddWithValue("@CCMDOCTYPE", comboBoxMalMerTip.Text.Trim());
            cmd.Parameters.AddWithValue("@MAINCCMDOCTYPE", maliyTxtBox.Text.Trim());
            cmd.Parameters.AddWithValue("@MAINCCMDOCNUM", maliKodTxtBox.Text.Trim());

            cmd.Parameters.AddWithValue("@CCMDOCFROM", CCMDOCFROM);
            cmd.Parameters.AddWithValue("@CCMDOCUNTIL", CCMDOCUNTIL);

            cmd.Parameters.AddWithValue("@ISPASSIVE", checkboxpas.Checked ? 1 : 0);
            cmd.Parameters.AddWithValue("@ISDELETED", deletedlbl.Checked ? 1 : 0);

            cmd.Parameters.AddWithValue("@CCMDOCNUM", malCarNum);
            //cmd.Parameters.AddWithValue("@MAINCCMDOCNUM", malCarNum);


            try
            {
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // SQL komutunu çalıştırıyoruz

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Kayıt başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Başarılıysa formu kapatıyoruz
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
            finally
            {
                con.Close(); // Bağlantıyı kapatıyoruz
            }
        }
    }
}
