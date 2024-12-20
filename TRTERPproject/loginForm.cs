using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TRTERPproject
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve şifreyi al
            string username = usernameBox.Text;  // TextBox'dan kullanıcı adı
            string password = pwdBox.Text;       // TextBox'dan şifre

            // Veritabanı bağlantı dizesi
            string connectionString = "server=.;initial catalog=TRTdb;integrated security=true"; // Burada güvenlik ile bağlantı sağlanıyor.

            // Veritabanına bağlan
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    // Kullanıcı adı ve şifreyi sorgulayan SQL sorgusu
                    string query = "SELECT COUNT(*) FROM login WHERE userName = @username AND password = @password";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Parametreleri ekleyelim
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        // Bağlantıyı aç
                        con.Open();

                        // Sorguyu çalıştır ve dönen sonucu kontrol et
                        int result = (int)cmd.ExecuteScalar(); // COUNT(*) değeri dönecek

                        // Eğer sonuç 1 veya daha büyükse, kullanıcı adı ve şifre doğru
                        if (result > 0)
                        {
                            CardForm cardForm = new CardForm();
                            cardForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda bir mesaj göster
                    MessageBox.Show($"Veritabanı bağlantısı sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
