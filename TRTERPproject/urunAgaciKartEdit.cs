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
    public partial class urunAgaciKartEdit : Form
    {
        private string bomDocNum;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

        public urunAgaciKartEdit(string bomDocNum)
        {
            InitializeComponent();
            this.bomDocNum = bomDocNum;
        }

        private void urunAgaciKartEdit_Load(object sender, EventArgs e)
        {
            string query = @"
    SELECT 
        COMCODE, 
        MATDOCTYPE, 
        MATDOCNUM, 
        BOMDOCTYPE, 
        BOMDOCNUM, 
        DRAWNUM, 
        QUANTITY, 
        BOMDOCFROM, 
        BOMDOCUNTIL, 
        ISPASSIVE, 
        ISDELETED 
    FROM BSMGRTRTBOMHEAD 
    WHERE BOMDOCNUM = @BOMDOCNUM"; // BOMDOCNUM parametresi

            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BOMDOCNUM", bomDocNum); // 'bomDocNum' parametre olarak ekleniyor

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) // Eğer veri varsa
                {
                    // Form elemanlarını veritabanı ile dolduruyoruz
                    firmbox.Text = reader["COMCODE"].ToString();
                    urnagamalztipbox.Text = reader["MATDOCTYPE"].ToString();
                    urnmalznumBox.Text = reader["MATDOCNUM"].ToString();
                    urnAgaTipBox.Text = reader["BOMDOCTYPE"].ToString();
                    textBox1.Text = reader["BOMDOCNUM"].ToString();
                    cizmikBox.Text = reader["DRAWNUM"].ToString();
                    temelmikBox.Text = reader["QUANTITY"].ToString();

                    // DateTimePicker'lara tarihleri yerleştiriyoruz
                    baslangicDateTimePicker.Value = Convert.ToDateTime(reader["BOMDOCFROM"]);
                    bitisDateTimePicker.Value = Convert.ToDateTime(reader["BOMDOCUNTIL"]);

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
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close(); // Bağlantıyı kapatıyoruz
            }
        }



        private void btnSave_Click_1(object sender, EventArgs e)
        {
            // Tarihler arasındaki mantık hatasını kontrol etme
            DateTime bomDocFrom = baslangicDateTimePicker.Value;
            DateTime bomDocUntil = bitisDateTimePicker.Value;

            if (bomDocFrom >= bomDocUntil)
            {
                MessageBox.Show("Başlangıç tarihi, bitiş tarihinden önce olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // İşlemi durdur
            }

            string query = @"
    UPDATE BSMGRTRTBOMHEAD 
    SET 
        COMCODE = @COMCODE,
        MATDOCTYPE = @MATDOCTYPE,
        MATDOCNUM = @MATDOCNUM,
        BOMDOCTYPE = @BOMDOCTYPE,
        DRAWNUM = @DRAWNUM,
        QUANTITY = @QUANTITY,
        BOMDOCFROM = @BOMDOCFROM,
        BOMDOCUNTIL = @BOMDOCUNTIL,
        ISPASSIVE = @ISPASSIVE,
        ISDELETED = @ISDELETED 
    WHERE BOMDOCNUM = @BOMDOCNUM";

            cmd = new SqlCommand(query, con);

            // Parametreleri ekliyoruz
            cmd.Parameters.AddWithValue("@COMCODE", firmbox.Text.Trim());
            cmd.Parameters.AddWithValue("@MATDOCTYPE", urnagamalztipbox.Text.Trim());
            cmd.Parameters.AddWithValue("@MATDOCNUM", urnmalznumBox.Text.Trim());
            cmd.Parameters.AddWithValue("@BOMDOCTYPE", urnAgaTipBox.Text.Trim());
            cmd.Parameters.AddWithValue("@DRAWNUM", cizmikBox.Text.Trim());
            cmd.Parameters.AddWithValue("@QUANTITY", temelmikBox.Text.Trim());

            // DateTimePicker'dan alınan tarihleri SQL'e ekliyoruz
            cmd.Parameters.AddWithValue("@BOMDOCFROM", bomDocFrom);
            cmd.Parameters.AddWithValue("@BOMDOCUNTIL", bomDocUntil);

            cmd.Parameters.AddWithValue("@ISPASSIVE", checkboxpas.Checked ? 1 : 0);
            cmd.Parameters.AddWithValue("@ISDELETED", deletedlbl.Checked ? 1 : 0);
            cmd.Parameters.AddWithValue("@BOMDOCNUM", bomDocNum); // Güncellenmek istenen BOMDOCNUM parametre olarak veriliyor

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


