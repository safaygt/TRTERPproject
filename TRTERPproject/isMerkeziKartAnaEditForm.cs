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


	public partial class isMerkeziKartAnaEditForm : Form
	{
		SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
		SqlDataReader reader;
		SqlCommand cmd;

		public string Firma { get; set; }
		public string IsMerkeziTipi { get; set; }
		public string IsMerkeziNumarasi { get; set; }
		public string OperasyonKodu { get; set; }
		public string MaliMerTip { get; set; }
		public string MaliMerKod { get; set; }
		public DateTime GecerliBaslangic { get; set; }
		public DateTime GecerliBitis { get; set; }
		public string AnaIsMerkeziTipi { get; set; }
		public string AnaIsMerkeziNumarasi { get; set; }
		public string Dil { get; set; }
		public string IMKA { get; set; }
		public string IMUA { get; set; }
		public string Worktime { get; set; }
		public bool? IsDeleted { get; set; }
		public bool? IsPassive { get; set; }


		public isMerkeziKartAnaEditForm()
		{
			InitializeComponent();
			this.Load += (s, e) => LoadComboBoxData();

			firmbox.Leave += (s, e) => ValidateAndAddData(firmbox, "COMCODE");
			comboBoxIsMerTip.Leave += (s, e) => ValidateAndAddData(comboBoxIsMerTip, "DOCTYPE");
			comboBoxOprCode.Leave += (s, e) => ValidateAndAddData(comboBoxOprCode, "DOCTYPE");
			dilBox.Leave += (s, e) => ValidateAndAddData(dilBox, "LANCODE");
			comboBoxMaliMerk.Leave += (s, e) => ValidateAndAddData(comboBoxMaliMerk, "DOCTYPE");
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

					// Seçilen değeri doğru şekilde ata
					if (comboBox.SelectedValue == null && dt.Rows.Count > 0)
					{
						comboBox.SelectedValue = dt.Rows[0][columnName]; // Varsayılan değeri ilk satır olarak ayarlayın
					}
				}
			}
		}


		private void ValidateAndAddData(ComboBox comboBox, string columnName)
		{
			string userInput = comboBox.Text.Trim();
			if (string.IsNullOrEmpty(userInput))
				return;

			string checkQuery = $"SELECT COUNT(*) FROM BSMGRTRTWCMHEAD WHERE {columnName} = @userInput";

			try
			{
				using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
				{
					using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
					{
						checkCmd.Parameters.AddWithValue("@userInput", userInput);
						con.Open();

						int count = (int)checkCmd.ExecuteScalar();
						if (count == 0)
						{
							MessageBox.Show($"'{userInput}' değeri {columnName} sütunu için geçerli değil.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							comboBox.Text = string.Empty;
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Hata: {ex.Message}");
			}
		}



		private void isMerkeziKartAnaEditForm_Load(object sender, EventArgs e)
		{
			LoadComboBoxData();

			firmbox.SelectedValue = Firma;
			comboBoxIsMerTip.SelectedValue = IsMerkeziTipi;
			ismerkodtxtBox.Text = IsMerkeziNumarasi;
			comboBoxOprCode.SelectedValue = OperasyonKodu;
			comboBoxMaliMerk.SelectedValue = MaliMerTip;
			maliyMerkTxtBox.Text = MaliMerKod;
			dateTimeBas.Value = GecerliBaslangic;
			dateTimeBit.Value = GecerliBitis;
			anaismertip.Text = AnaIsMerkeziTipi;
			anaismerkod.Text = AnaIsMerkeziNumarasi;
			dilBox.SelectedValue = Dil;
			ismerkKATxtBox.Text = IMKA;
			ismerkUAtextBox.Text = IMUA;
			textBoxGunlukCal.Text = Worktime;
			checkboxpas.Checked = IsDeleted ?? false;
			deletedlbl.Checked = IsPassive ?? false;

		}


		private void saveBut_Click(object sender, EventArgs e)
		{
			decimal worktime;
			if (decimal.TryParse(textBoxGunlukCal.Text, out worktime))
			{
				// cmd nesnesinin başlatıldığından emin olun.
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.Connection = new SqlConnection(ConnectionHelper.ConnectionString);
					cmd.Parameters.AddWithValue("@WORKTIME", worktime);
					try
					{
						using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
						{
							con.Open();

							// BSMGRTRTWCMHEAD Tablosu Güncelleme
							string query1 = @"
							UPDATE BSMGRTRTWCMHEAD
							SET 
							    COMCODE = @COMCODE,
							    WCMDOCTYPE = @WCMDOCTYPE,
							    WCMDOCNUM = @WCMDOCNUM,
							    CCMDOCTYPE = @CCMDOCTYPE,
							    CCMDOCNUM = @CCMDOCNUM,
							    WCMDOCFROM = @WCMDOCFROM,
							    WCMDOCUNTIL = @WCMDOCUNTIL,
							    MAINWCMDOCTYPE = @MAINWCMDOCTYPE,
							    MAINWCMDOCNUM = @MAINWCMDOCNUM,
							    WORKTIME = @WORKTIME,
							    ISDELETED = @ISDELETED,
							    ISPASSIVE = @ISPASSIVE
							WHERE 
							    WCMDOCNUM = @WCMDOCNUM;";

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
								command1.Parameters.AddWithValue("@WORKTIME", worktime);
								command1.Parameters.AddWithValue("@ISDELETED", checkboxpas.Checked ? 1 : (object)DBNull.Value);
								command1.Parameters.AddWithValue("@ISPASSIVE", deletedlbl.Checked ? 1 : (object)DBNull.Value);
								command1.ExecuteNonQuery();
							}

							// BSMGRTRTWCMTEXT Tablosu Güncelleme
							string query2 = @"
                UPDATE BSMGRTRTWCMTEXT
                SET 
                    WCMSTEXT = @WCMSTEXT,
                    WCMLTEXT = @WCMLTEXT
                FROM 
                    BSMGRTRTWCMTEXT WT
                INNER JOIN 
                    BSMGRTRTWCMHEAD WH ON WH.WCMDOCNUM = WT.WCMDOCNUM
                WHERE 
                    WH.WCMDOCNUM = @WCMDOCNUM;";

							using (SqlCommand command2 = new SqlCommand(query2, con))
							{
								command2.Parameters.AddWithValue("@WCMSTEXT", ismerkKATxtBox.Text ?? (object)DBNull.Value);
								command2.Parameters.AddWithValue("@WCMLTEXT", ismerkUAtextBox.Text ?? (object)DBNull.Value);
								command2.Parameters.AddWithValue("@WCMDOCNUM", ismerkodtxtBox.Text ?? (object)DBNull.Value);
								command2.ExecuteNonQuery();
							}

							// BSMGRTRTGEN002 Tablosu Güncelleme
							string query3 = @"
                UPDATE BSMGRTRTGEN002
                SET 
                    LANCODE = @LANCODE
                FROM 
                    BSMGRTRTGEN002 G2
                INNER JOIN 
                    BSMGRTRTWCMTEXT WT ON WT.LANCODE = G2.LANCODE
                INNER JOIN 
                    BSMGRTRTWCMHEAD WH ON WH.WCMDOCNUM = WT.WCMDOCNUM
                WHERE 
                    WH.WCMDOCNUM = @WCMDOCNUM;";

							using (SqlCommand command3 = new SqlCommand(query3, con))
							{
								command3.Parameters.AddWithValue("@LANCODE", dilBox.Text ?? (object)DBNull.Value);
								command3.Parameters.AddWithValue("@WCMDOCNUM", ismerkodtxtBox.Text ?? (object)DBNull.Value);
								command3.ExecuteNonQuery();
							}

							// BSMGRTRTWCMOPR Tablosu Güncelleme
							string query4 = @"
                UPDATE BSMGRTRTWCMOPR
                SET 
                    OPRDOCTYPE = @OPRDOCTYPE
                FROM 
                    BSMGRTRTWCMOPR WO
                INNER JOIN 
                    BSMGRTRTWCMHEAD WH ON WH.WCMDOCNUM = WO.WCMDOCNUM
                WHERE 
                    WH.WCMDOCNUM = @WCMDOCNUM;";

							using (SqlCommand command4 = new SqlCommand(query4, con))
							{
								command4.Parameters.AddWithValue("@OPRDOCTYPE", comboBoxOprCode.Text ?? (object)DBNull.Value);
								command4.Parameters.AddWithValue("@WCMDOCNUM", ismerkodtxtBox.Text ?? (object)DBNull.Value);
								command4.ExecuteNonQuery();
							}

							MessageBox.Show("Kayıt başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}


	}
}
