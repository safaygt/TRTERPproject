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
    public partial class unitForm : Form
    {

        SqlDataReader reader;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

        public unitForm()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {

            string query = "Select * from BSMGRTRTGEN005";
            con = new SqlConnection(ConnectionHelper.ConnectionString);
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

            string unitCode = unitCodeTextBox.Text;

            if (string.IsNullOrEmpty(unitCode))
            {
                MessageBox.Show("Lütfen bir Birim Kodu giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM BSMGRTRTGEN005 WHERE UNITCODE = @UNITCODE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UNITCODE", unitCode);

                try
                {
                    con.Open();
                    int recordExists = (int)cmd.ExecuteScalar();

                    if (recordExists > 0)
                    {
                        // UNITCODE bulundu, Edit formuna geç
                        unitFormEdit UnitFormEdit = new unitFormEdit(unitCode);
                        UnitFormEdit.Show();
                    }
                    else
                    {
                        MessageBox.Show("Belirtilen Birim Kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            string comCode = firmCodeTextBox.Text.Trim();
            string unitCode = unitCodeTextBox.Text.Trim();
            string unitText = unitTextBox.Text.Trim();
            int isMainUnit = isMainUnitCheckBox.Checked ? 1 : 0; // Checkbox durumunu belirle
            string mainUnitCode = mainUnitCodeTextBox.Text.Trim();


            if (string.IsNullOrEmpty(comCode) || string.IsNullOrEmpty(unitCode) || string.IsNullOrEmpty(unitText))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN005 WHERE UNITCODE = @UNITCODE";
                    using (cmd = new SqlCommand(checkQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@UNITCODE", unitCode);

                        int unitCodeExists = (int)cmd.ExecuteScalar();

                        if (unitCodeExists > 0)
                        {
                            MessageBox.Show("Bu Birim Kodu zaten mevcut. Lütfen başka bir Birim Kodu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO BSMGRTRTGEN005 (COMCODE, UNITCODE, UNITTEXT, ISMAINUNIT,MAINUNITCODE) VALUES (@COMCODE, @UNITCODE, @UNITTEXT, @ISMAINUNIT, @MAINUNITCODE)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", comCode);
                        cmd.Parameters.AddWithValue("@UNITCODE", unitCode);
                        cmd.Parameters.AddWithValue("@UNITTEXT", unitText);
                        cmd.Parameters.AddWithValue("@ISMAINUNIT", isMainUnit);
                        cmd.Parameters.AddWithValue("@MAINUNITCODE", mainUnitCode);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            firmCodeTextBox.Clear();
                            unitCodeTextBox.Clear();
                            unitTextBox.Clear();
                            isMainUnitCheckBox.Checked = false;
                            mainUnitCodeTextBox.Clear();
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

        private void btnDel_Click(object sender, EventArgs e)
        {
            string unitCode = unitCodeTextBox.Text.Trim();

            if (string.IsNullOrEmpty(unitCode))
            {
                MessageBox.Show("Lütfen bir UNITCODE giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTGEN005 WHERE UNITCODE = @UNITCODE";
                    cmd = new SqlCommand(checkQuery, con);
                    cmd.Parameters.AddWithValue("@UNITCODE", unitCode);

                    int recordExists = (int)cmd.ExecuteScalar();

                    if (recordExists == 0)
                    {
                        MessageBox.Show("Belirtilen Birim Kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string deleteQuery = "DELETE FROM BSMGRTRTGEN005 WHERE UNITCODE = @UNITCODE";
                    cmd = new SqlCommand(deleteQuery, con);
                    cmd.Parameters.AddWithValue("@UNITCODE", unitCode);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        unitCodeTextBox.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt silinemedi. Lütfen tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
