using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TRTERPproject
{
	public partial class malzemeKartAna : Form
	{
		SqlConnection con;
		SqlDataReader reader;
		SqlCommand cmd;


		public malzemeKartAna()
		{
			InitializeComponent();
		}

		private void getBut_Click(object sender, EventArgs e)
		{
			// Veritabanı bağlantısı
			string connectionString = "Server=DESKTOP-U86MLBA;Database=TRTdb;Integrated Security=True;;";

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					// SQL sorgusu
					string query = "SELECT * FROM BSMGRTRTMAT001"; // Tablo adınızı buraya yazın

					// Verileri çekmek için SqlDataAdapter kullanımı
					SqlDataAdapter da = new SqlDataAdapter(query, conn);
					DataTable dt = new DataTable();
					da.Fill(dt);

					// DataGridView'e verileri bağlama
					malKartAna.DataSource = dt;
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
