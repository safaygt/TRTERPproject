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

namespace TRTERPproject
{
    public partial class countryForm : Form
    {

        SqlConnection con;
        SqlDataReader reader;
        SqlCommand cmd;

        public countryForm()
        {
            InitializeComponent();
        }



        private void btnGet_Click(object sender, EventArgs e)
        {

            string query = "Select * from BSMGRTRTGEN003";
            con = new SqlConnection("Server=MSI\\SQLEXPRESS;Database=TRTdb;Integrated Security=True;");
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;


            try
            {

                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);


                DataSet ds = new DataSet();

                da.Fill(ds);

                // DataGridView'e veri bağla
                CountryDataGridView.DataSource = ds.Tables[0];  // Veritabanından çekilen ilk tabloyu DataGridView'e bağla
            }
            catch (Exception ex)
            {
                // Hata durumunda mesaj göster
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                con.Close();
            }


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string countryCode = countryCodeTextBox.Text;

            if (string.IsNullOrEmpty(countryCode))
            {
                MessageBox.Show("Lütfen bir COUNTRYCODE giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection("Server=MSI\\SQLEXPRESS;Database=TRTdb;Integrated Security=True;"))
            {
                string query = "SELECT COUNT(*) FROM BSMGRTRTGEN003 WHERE COUNTRYCODE = @COUNTRYCODE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@COUNTRYCODE", countryCode);

                try
                {
                    con.Open();
                    int recordExists = (int)cmd.ExecuteScalar();

                    if (recordExists > 0)
                    {
                        // COUNTRYCODE bulundu, Edit formuna geç
                        countryFormEdit CountryFormEdit = new countryFormEdit(countryCode);
                        CountryFormEdit.Show();
                    }
                    else
                    {
                        // COUNTRYCODE bulunamadı
                        MessageBox.Show("Belirtilen COUNTRYCODE için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnDel_Click(object sender, EventArgs e)
        {

        }
    }
}
