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
		}
		//private string connectionString = "Server=DESKTOP-U86MLBA;Database=TRTdb;Integrated Security=True;";

		private void LoginBtn_Click(object sender, EventArgs e)
		{

			string username = usernameBox.Text.Trim();
			string password = pwdBox.Text.Trim();


			con = new SqlConnection(ConnectionHelper.ConnectionString);
			cmd = new SqlCommand();
			con.Open();
			cmd.Connection = con;
			cmd.CommandText = "Select * From login where userName= '" + username + "' And password= '" + password + "'";
			reader = cmd.ExecuteReader();
			if (reader.Read())
			{
				CardForm cardForm = new CardForm();
				cardForm.Show();
				this.Hide();
			}
			else
			{
				MessageBox.Show("Kullanıcı Adı veya şifre Yanlış!");
			}
		}
	}
}
