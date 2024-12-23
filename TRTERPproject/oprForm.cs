﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
    public partial class oprForm : Form
    {

        SqlDataReader reader;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);


        public oprForm()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            string query = "Select * from BSMGRTRTOPR001";
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
                oprDataGridView.DataSource = ds.Tables[0];  // Veritabanından çekilen ilk tabloyu DataGridView'e bağla
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

        private void firmCodeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string operationCode = oprTypeTextBox.Text;

            if (string.IsNullOrEmpty(operationCode))
            {
                MessageBox.Show("Lütfen bir Operasyon Tipi giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM BSMGRTRTOPR001 WHERE DOCTYPE = @DOCTYPE";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DOCTYPE", operationCode);

                try
                {
                    con.Open();
                    int recordExists = (int)cmd.ExecuteScalar();

                    if (recordExists > 0)
                    {
                        oprFormEdit OperationFormEdit = new oprFormEdit(operationCode);
                        OperationFormEdit.Show();
                    }
                    else
                    {
                        MessageBox.Show("Belirtilen Operasyon Kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string firmCode = oprFirmCodeTextBox.Text.Trim();
            string typeTex = oprTypeTextBox.Text.Trim();
            string typeDes = oprTypeDesTextBox.Text.Trim();
            int oprPascheck = oprPascheckbox.Checked ? 1 : 0;


            if (string.IsNullOrEmpty(firmCode) || string.IsNullOrEmpty(typeTex) || string.IsNullOrEmpty(typeDes))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTOPR001 WHERE DOCTYPE = @DOCTYPE";
                    using (cmd = new SqlCommand(checkQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@DOCTYPE", typeTex);

                        int unitCodeExists = (int)cmd.ExecuteScalar();

                        if (unitCodeExists > 0)
                        {
                            MessageBox.Show("Bu Birim Kodu zaten mevcut. Lütfen başka bir Operasyon Tipini giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO BSMGRTRTOPR001 (COMCODE,DOCTYPE,DOCTYPETEXT,ISPASSIVE) VALUES(@COMCODE,@DOCTYPE,@DOCTYPETEXT,@ISPASSIVE)";
                    using (cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@COMCODE", firmCode);
                        cmd.Parameters.AddWithValue("@DOCTYPE", typeTex);
                        cmd.Parameters.AddWithValue("@DOCTYPETEXT", typeDes);
                        cmd.Parameters.AddWithValue("@ISPASSIVE", oprPascheck);
                        

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            oprFirmCodeTextBox.Clear();
                            oprTypeTextBox.Clear();
                            oprTypeDesTextBox.Clear();
                            oprPascheckbox.Checked = false;
                            
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

            string unitCode = oprTypeTextBox.Text.Trim();

            if (string.IsNullOrEmpty(unitCode))
            {
                MessageBox.Show("Lütfen bir DOCTYPE giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                try
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM BSMGRTRTOPR001 WHERE DOCTYPE = @DOCTYPE";
                    cmd = new SqlCommand(checkQuery, con);
                    cmd.Parameters.AddWithValue("@DOCTYPE", unitCode);

                    int recordExists = (int)cmd.ExecuteScalar();

                    if (recordExists == 0)
                    {
                        MessageBox.Show("Belirtilen Birim Kodu için bir kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string deleteQuery = "DELETE FROM BSMGRTRTOPR001 WHERE DOCTYPE = @DOCTYPE";
                    cmd = new SqlCommand(deleteQuery, con);
                    cmd.Parameters.AddWithValue("@DOCTYPE", unitCode);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        oprTypeTextBox.Clear();
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

        private void oprTypeDesLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
