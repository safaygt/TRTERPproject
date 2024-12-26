using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TRTERPproject.Helpers;

namespace TRTERPproject
{
	public partial class isMerkeziAddForm : Form
	{
		SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

		public isMerkeziAddForm()
		{
			InitializeComponent();
			this.Load += (s, e) => LoadComboBoxData();
		}

		private void LoadComboBoxData()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
				{
					con.Open();

					// Firma verileri
					LoadComboBox(firmbox, "SELECT DISTINCT COMCODE FROM BSMGRTRTGEN001", "COMCODE");

					// İş Merkezi Tipi
					LoadComboBox(comboBoxIsMerTip, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTWCM001", "DOCTYPE");

					// Dil Kodları
					LoadComboBox(dilBox, "SELECT DISTINCT LANCODE FROM BSMGRTRTGEN002", "LANCODE");

					// Operasyon Kodu
					LoadComboBox(comboBoxOprCode, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTOPR001", "DOCTYPE");

					// Maliyet Merkezi
					LoadComboBox(comboBoxMaliMerk, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTCCM001", "DOCTYPE");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}");
			}
		}

		private void LoadComboBox(ComboBox comboBox, string query, string columnName)
		{
			using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
			{
				using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
				{
					DataTable dt = new DataTable();
					adapter.Fill(dt);
					comboBox.DataSource = dt;
					comboBox.DisplayMember = columnName;
					comboBox.ValueMember = columnName;
					comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
				}
			}
		}


		private void saveBut_Click_1(object sender, EventArgs e)
		{

			try
			{
				using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
				{
					con.Open();

					// BSMGRTRTWCMHEAD Tablosuna Ekleme
					string query1 = @"
                    INSERT INTO BSMGRTRTWCMHEAD (
                        COMCODE, WCMDOCTYPE, WCMDOCNUM, CCMDOCTYPE, CCMDOCNUM, 
                        WCMDOCFROM, WCMDOCUNTIL, MAINWCMDOCTYPE, MAINWCMDOCNUM, 
                        WORKTIME, ISDELETED, ISPASSIVE)
                    VALUES (
                        @COMCODE, @WCMDOCTYPE, @WCMDOCNUM, @CCMDOCTYPE, @CCMDOCNUM, 
                        @WCMDOCFROM, @WCMDOCUNTIL, @MAINWCMDOCTYPE, @MAINWCMDOCNUM, 
                        @WORKTIME, @ISDELETED, @ISPASSIVE);";

					using (SqlCommand command1 = new SqlCommand(query1, con))
					{
						command1.Parameters.AddWithValue("@COMCODE", firmbox.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@WCMDOCTYPE", comboBoxIsMerTip.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@WCMDOCNUM", ismerkodtxtBox.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@CCMDOCTYPE", comboBoxMaliMerk.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@CCMDOCNUM", maliyMerkTxtBox.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@WCMDOCFROM", DateTime.TryParse(dateTimeBas.Text, out DateTime fromDate) ? fromDate : (object)DBNull.Value);
						command1.Parameters.AddWithValue("@WCMDOCUNTIL", DateTime.TryParse(dateTimeBit.Text, out DateTime untilDate) ? untilDate : (object)DBNull.Value);
						command1.Parameters.AddWithValue("@MAINWCMDOCTYPE", anaismertip.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@MAINWCMDOCNUM", anaismerkod.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@WORKTIME", textBoxGunlukCal.Text ?? (object)DBNull.Value);
						command1.Parameters.AddWithValue("@ISDELETED", checkboxpas.Checked ? 1 : 0);
						command1.Parameters.AddWithValue("@ISPASSIVE", deletedlbl.Checked ? 1 : 0);

						command1.ExecuteNonQuery();
					}

					// BSMGRTRTWCMTEXT Tablosuna Ekleme
					string query2 = @"
                    INSERT INTO BSMGRTRTWCMTEXT (WCMDOCNUM, WCMSTEXT, WCMLTEXT)
                    VALUES (@WCMDOCNUM, @WCMSTEXT, @WCMLTEXT);";

					using (SqlCommand command2 = new SqlCommand(query2, con))
					{
						command2.Parameters.AddWithValue("@WCMDOCNUM", ismerkodtxtBox.Text ?? (object)DBNull.Value);
						command2.Parameters.AddWithValue("@WCMSTEXT", ismerkKATxtBox.Text ?? (object)DBNull.Value);
						command2.Parameters.AddWithValue("@WCMLTEXT", ismerkUAtextBox.Text ?? (object)DBNull.Value);

						command2.ExecuteNonQuery();
					}

					// BSMGRTRTWCMOPR Tablosuna Ekleme
					string query3 = @"
                    INSERT INTO BSMGRTRTWCMOPR (WCMDOCNUM, OPRDOCTYPE)
                    VALUES (@WCMDOCNUM, @OPRDOCTYPE);";

					using (SqlCommand command3 = new SqlCommand(query3, con))
					{
						command3.Parameters.AddWithValue("@WCMDOCNUM", ismerkodtxtBox.Text ?? (object)DBNull.Value);
						command3.Parameters.AddWithValue("@OPRDOCTYPE", comboBoxOprCode.Text ?? (object)DBNull.Value);

						command3.ExecuteNonQuery();
					}

					MessageBox.Show("Yeni kayıt başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
