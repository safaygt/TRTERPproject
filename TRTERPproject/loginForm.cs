using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.Sql;
namespace TRTERPproject
{
	public partial class LoginForm : Form
	{
		SqlConnection con;
		SqlDataReader reader;
		SqlCommand cmd;
		
		public LoginForm()
		{
			InitializeComponent();
		}

		private void LoginBtn_Click(object sender, EventArgs e)
		{
			string username = usernameBox.Text.Trim();
			string password = pwdBox.Text.Trim();

			//string connectionString = "Server=DESKTOP-U86MLBA;Database=TRTdb;Integrated Security=True;";
			//a
			//b
			con = new SqlConnection("Server=DESKTOP-U86MLBA;Database=TRTdb;Integrated Security=True;");
			cmd = new SqlCommand();
			con.Open();
			cmd.Connection = con;
			cmd.CommandText = "Select * From login where userName= '"  + username + "' And password= '" + password + "'";
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
