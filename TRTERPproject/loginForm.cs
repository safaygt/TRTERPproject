using System.Data.SqlClient;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class LoginForm : Form
    {
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd;

        public LoginForm()
        {
            InitializeComponent();

            // Formun KeyPreview özelliğini etkinleştiriyoruz
            this.KeyPreview = true;

            // KeyDown olayını form için bağlayın
            this.KeyDown += new KeyEventHandler(LoginForm_KeyDown);
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter tuşuna basıldığında giriş işlemini çağır
            if (e.KeyCode == Keys.Enter)
            {
                PerformLogin();
            }
        }

        private void PerformLogin()
        {
            string username = usernameBox.Text.Trim();
            string password = pwdBox.Text.Trim();

            try
            {
                using (con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    cmd = new SqlCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM login WHERE userName = @username AND password = @password";

                    // SQL enjeksiyon riskine karşı parametreli sorgu
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        CardForm cardForm = new CardForm();
                        cardForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı Adı veya Şifre Yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
