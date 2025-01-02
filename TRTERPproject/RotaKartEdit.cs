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
	public partial class RotaKartEdit : Form
	{
		public string Firma { get; set; }

		public string Rotdoctype { get; set; }
		public string Rotdocnum { get; set; }
		public string Doctype { get; set; }
		public string Bomdocnum { get; set; }
		public string Matdoctype { get; set; }

		public string Matdocnum { get; set; }
		public string Wcmdoctype { get; set; }

		public string Wcmdocnum { get; set; }
		public string Oprdoctype { get; set; }

		public string Oprnum { get; set; }
		public string Setuptime { get; set; }
		public string Machinetime { get; set; }

		public string Labourtime { get; set; }
		public string Contentnum { get; set; }

		public string Component { get; set; }
		public string Quantity { get; set; }
		public string Quantity2 { get; set; }
		public string Drawnum { get; set; }

		public DateTime GecerliBaslangic { get; set; }
		public DateTime GecerliBitis { get; set; }
		public string Dil { get; set; }

		public bool IsDeleted { get; set; }
		public bool IsPassive { get; set; }


		SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
		SqlDataReader reader;
		SqlCommand cmd;
		public RotaKartEdit()
		{
			InitializeComponent();
			this.Load += (s, e) => LoadComboBoxData();


			firmbox.Leave += (s, e) => ValidateAndAddData(firmbox, "COMCODE", "BSMGRTRTGEN001");
			comboBoxRotTip.Leave += (s, e) => ValidateAndAddData(comboBoxRotTip, "DOCTYPE", "BSMGRTRTROT001");
			dilBox.Leave += (s, e) => ValidateAndAddData(dilBox, "LANCODE", "BSMGRTRTGEN002");
			comboBoxMalzTip.Leave += (s, e) => ValidateAndAddData(comboBoxMalzTip, "DOCTYPE", "BSMGRTRTMAT001");
			comboBoxurnAgaTip.Leave += (s, e) => ValidateAndAddData(comboBoxurnAgaTip, "DOCTYPE", "BSMGRTRTBOM001");
			comboBoxIsmerkTip.Leave += (s, e) => ValidateAndAddData(comboBoxIsmerkTip, "DOCTYPE", "BSMGRTRTWCM001");
			OprKodBox.Leave += (s, e) => ValidateAndAddData(OprKodBox, "DOCTYPE", "BSMGRTRTOPR001");
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

		private void ValidateAndAddData(ComboBox comboBox, string columnName, string tableName)
		{
			string checkQuery = $@"
            SELECT COUNT(*) 
            FROM {tableName} 
            WHERE {columnName} = @userInput";

			if (string.IsNullOrEmpty(comboBox.Text)) return;

			try
			{
				using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
				{
					con.Open();
					using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
					{
						checkCmd.Parameters.AddWithValue("@userInput", comboBox.Text);
						int count = (int)checkCmd.ExecuteScalar();

						if (count == 0)
						{
							MessageBox.Show($"{columnName} '{comboBox.Text}' tablodaki verilerle uyuşmuyor.");
							comboBox.Text = string.Empty; // Kullanıcının yanlış girişini temizler
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Hata: {ex.Message}");
			}
		}


		private void LoadComboBoxData()
		{
			try
			{
				LoadComboBox(firmbox, "SELECT DISTINCT COMCODE FROM BSMGRTRTGEN001", "COMCODE");
				LoadComboBox(comboBoxRotTip, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTROT001", "DOCTYPE");
				LoadComboBox(dilBox, "SELECT DISTINCT LANCODE FROM BSMGRTRTGEN002", "LANCODE");
				LoadComboBox(comboBoxMalzTip, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTMAT001", "DOCTYPE");
				LoadComboBox(comboBoxurnAgaTip, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTBOM001", "DOCTYPE");
				LoadComboBox(comboBoxIsmerkTip, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTWCM001", "DOCTYPE");
				LoadComboBox(OprKodBox, "SELECT DISTINCT DOCTYPE FROM BSMGRTRTOPR001", "DOCTYPE");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}");
			}
		}

		private void SaveBut_Click(object sender, EventArgs e)
		{


			using (SqlCommand cmd = new SqlCommand())
			{
				cmd.Connection = new SqlConnection(ConnectionHelper.ConnectionString);

				try
				{
					using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
					{
						con.Open();

						// Transaction başlat
						using (SqlTransaction transaction = con.BeginTransaction())
						{
							cmd.Connection = con;
							cmd.Transaction = transaction;

							// Başlangıç ve Bitiş tarihlerini al
							DateTime bomDocFrom = DateTime.TryParse(dateTimePickerBaslangic.Text, out DateTime fromDate) ? fromDate : DateTime.MinValue;
							DateTime bomDocUntil = DateTime.TryParse(dateTimePickerBitis.Text, out DateTime untilDate) ? untilDate : DateTime.MinValue;

							// Bitiş tarihinin, Başlangıç tarihinden önce olup olmadığını kontrol et
							if (bomDocFrom >= bomDocUntil)
							{
								MessageBox.Show("Başlangıç tarihi, bitiş tarihinden önce olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return; // İşlemi durdur
							}

							try
							{
								// Mathead Tablosu Güncelleme
								string query1 = @"
                    UPDATE BSMGRTRTROTHEAD
                    SET 
                        COMCODE = @COMCODE,
                        ROTDOCTYPE = @ROTDOCTYPE,
                        ROTDOCNUM = @ROTDOCNUM,
                        ROTDOCFROM = @ROTDOCFROM,
                        ROTDOCUNTIL = @ROTDOCUNTIL,
						MATDOCTYPE = @MATDOCTYPE,
						MATDOCNUM = @MATDOCNUM,
						QUANTITY = @QUANTITY,
						DRAWNUM = @DRAWNUM,
						ISDELETED = @ISDELETED,
                        ISPASSIVE = @ISPASSIVE
                    WHERE 
                        ROTDOCNUM = @ROTDOCNUM;";

								using (SqlCommand command1 = new SqlCommand(query1, con, transaction))
								{
									// Parametreler ekleme
									command1.Parameters.AddWithValue("@COMCODE", firmbox.Text ?? (object)DBNull.Value);
									command1.Parameters.AddWithValue("@ROTDOCTYPE", comboBoxRotTip.Text ?? (object)DBNull.Value);
									command1.Parameters.AddWithValue("@ROTDOCNUM", RotaNumBox.Text ?? (object)DBNull.Value);
									command1.Parameters.AddWithValue("@ROTDOCFROM", bomDocFrom);
									command1.Parameters.AddWithValue("@ROTDOCUNTIL", bomDocUntil);
									command1.Parameters.AddWithValue("@MATDOCTYPE", comboBoxMalzTip.Text ?? (object)DBNull.Value);
									command1.Parameters.AddWithValue("@MATDOCNUM", MalzKodBox.Text ?? (object)DBNull.Value);
									command1.Parameters.AddWithValue("@QUANTITY", decimal.TryParse(MiktrBox.Text, out decimal Quantity2) ? Quantity2 : (object)DBNull.Value);
									command1.Parameters.AddWithValue("@DRAWNUM", CizmNumBox.Text ?? (object)DBNull.Value);
									command1.Parameters.AddWithValue("@ISDELETED", deletedlbl.Checked ? 1 : 0);
									command1.Parameters.AddWithValue("@ISPASSIVE", checkboxpas.Checked ? 1 : 0);

									command1.ExecuteNonQuery();
								}

								// Mattext Tablosu Güncelleme
								string query2 = @"
                    UPDATE BSMGRTRTROTOPRCONTENT
                    SET 
                        COMCODE = @COMCODE,
                        ROTDOCTYPE = @ROTDOCTYPE,
                        ROTDOCNUM = @ROTDOCNUM,
                        BOMDOCFROM = @ROTDOCFROM,
                        BOMDOCUNTIL = @ROTDOCUNTIL,
						MATDOCTYPE = @MATDOCTYPE,
						MATDOCNUM = @MATDOCNUM,
						WCMDOCTYPE = @WCMDOCTYPE,
						WCMDOCNUM = @WCMDOCNUM,
						OPRNUM = @OPRNUM,
						OPRDOCTYPE = @OPRDOCTYPE,
						SETUPTIME = @SETUPTIME,
						MACHINETIME = @MACHINETIME,
						LABOURTIME = @LABOURTIME
                    WHERE ROTDOCNUM = @ROTDOCNUM;";

								using (SqlCommand command2 = new SqlCommand(query2, con, transaction))
								{
									// Parametreler ekleme
									command2.Parameters.AddWithValue("@COMCODE", firmbox.Text ?? (object)DBNull.Value);
									command2.Parameters.AddWithValue("@ROTDOCTYPE", comboBoxRotTip.Text ?? (object)DBNull.Value);
									command2.Parameters.AddWithValue("@ROTDOCNUM", RotaNumBox.Text ?? (object)DBNull.Value);
									command2.Parameters.AddWithValue("@ROTDOCFROM", bomDocFrom);
									command2.Parameters.AddWithValue("@ROTDOCUNTIL", bomDocUntil);
									command2.Parameters.AddWithValue("@MATDOCTYPE", comboBoxMalzTip.Text ?? (object)DBNull.Value);
									command2.Parameters.AddWithValue("@MATDOCNUM", MalzKodBox.Text ?? (object)DBNull.Value);
									command2.Parameters.AddWithValue("@WCMDOCTYPE", comboBoxIsmerkTip.Text ?? (object)DBNull.Value);
									command2.Parameters.AddWithValue("@WCMDOCNUM", IsMerkzKodBox.Text ?? (object)DBNull.Value);
									command2.Parameters.AddWithValue("@OPRNUM", int.TryParse(OprNumBox.Text, out int Oprnum) ? Oprnum : (object)DBNull.Value);
									command2.Parameters.AddWithValue("@OPRDOCTYPE", OprKodBox.Text ?? (object)DBNull.Value);
									command2.Parameters.AddWithValue("@MACHINETIME", decimal.TryParse(oprMakSurBox.Text, out decimal Machinetime) ? Machinetime : (object)DBNull.Value);
									command2.Parameters.AddWithValue("@LABOURTIME", decimal.TryParse(oprIsciSurBox.Text, out decimal Labourtime) ? Labourtime : (object)DBNull.Value);
									command2.Parameters.AddWithValue("@SETUPTIME", decimal.TryParse(oprHazSurBox.Text, out decimal Setuptime) ? Setuptime : (object)DBNull.Value);

									command2.ExecuteNonQuery();
								}

								string query3 = @"
                    UPDATE BSMGRTRTROTBOMCONTENT
                    SET 
                        COMCODE = @COMCODE,
                        ROTDOCTYPE = @ROTDOCTYPE,
                        ROTDOCNUM = @ROTDOCNUM,
                        ROTDOCFROM = @ROTDOCFROM,
                        ROTDOCUNTIL = @ROTDOCUNTIL,
						MATDOCTYPE = @MATDOCTYPE,
						MATDOCNUM = @MATDOCNUM,
						BOMDOCTYPE = @BOMDOCTYPE,
						BOMDOCNUM = @BOMDOCNUM,
						OPRNUM = @OPRNUM,
						CONTENTNUM = @CONTENTNUM,
						COMPONENT = @COMPONENT,
						QUANTITY = @QUANTITY
                    WHERE ROTDOCNUM = @ROTDOCNUM;";

								using (SqlCommand command3 = new SqlCommand(query3, con, transaction))
								{
									// Parametreler ekleme
									command3.Parameters.AddWithValue("@COMCODE", firmbox.Text ?? (object)DBNull.Value);
									command3.Parameters.AddWithValue("@ROTDOCTYPE", comboBoxRotTip.Text ?? (object)DBNull.Value);
									command3.Parameters.AddWithValue("@ROTDOCNUM", RotaNumBox.Text ?? (object)DBNull.Value);
									command3.Parameters.AddWithValue("@ROTDOCFROM", bomDocFrom);
									command3.Parameters.AddWithValue("@ROTDOCUNTIL", bomDocUntil);
									command3.Parameters.AddWithValue("@MATDOCTYPE", comboBoxMalzTip.Text ?? (object)DBNull.Value);
									command3.Parameters.AddWithValue("@MATDOCNUM", MalzKodBox.Text ?? (object)DBNull.Value);
									command3.Parameters.AddWithValue("@BOMDOCTYPE", comboBoxurnAgaTip.Text ?? (object)DBNull.Value);
									command3.Parameters.AddWithValue("@BOMDOCNUM", UrnAganum.Text ?? (object)DBNull.Value);
									command3.Parameters.AddWithValue("@OPRNUM", int.TryParse(OprNumBox.Text, out int Oprnum) ? Oprnum : (object)DBNull.Value);
									command3.Parameters.AddWithValue("@CONTENTNUM", int.TryParse(IcrNumBox.Text, out int Contentnum) ? Contentnum : (object)DBNull.Value);
									command3.Parameters.AddWithValue("@COMPONENT", BilesKodBox.Text ?? (object)DBNull.Value);
									command3.Parameters.AddWithValue("@QUANTITY",decimal.TryParse(BilesMiktrBox.Text, out decimal Quantity) ? Quantity : (object)DBNull.Value);

									command3.ExecuteNonQuery();
								}

								// İşlem başarılı ise transaction'ı tamamla
								transaction.Commit();
								MessageBox.Show("Kayıt başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							catch (SqlException sqlEx)
							{
								// SQL Hatası yakalama ve rollback
								transaction.Rollback();
								MessageBox.Show($"SQL Hatası: {sqlEx.Message} (Hata Kodu: {sqlEx.Number})", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							catch (Exception ex)
							{
								// Diğer hatalar için rollback
								transaction.Rollback();
								MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}


		}

		private void RotaKartEdit_Load(object sender, EventArgs e)
		{
			firmbox.Text = Firma;
			comboBoxRotTip.Text = Rotdoctype;
			RotaNumBox.Text = Rotdocnum;
			UrnAganum.Text = Bomdocnum;
			dilBox.Text = Dil;
			comboBoxMalzTip.Text = Matdoctype;
			dateTimePickerBaslangic.Value = GecerliBaslangic;
			dateTimePickerBitis.Value = GecerliBitis;
			comboBoxurnAgaTip.Text = Doctype;
			comboBoxIsmerkTip.Text = Wcmdoctype;
			IsMerkzKodBox.Text = Wcmdocnum;
			OprKodBox.Text = Oprdoctype;
			OprNumBox.Text = Oprnum;
			IcrNumBox.Text = Contentnum;
			MalzKodBox.Text = Matdocnum;
			BilesKodBox.Text = Component;
			BilesMiktrBox.Text = Quantity;
			MiktrBox.Text = Quantity2;
			deletedlbl.Checked = IsDeleted;
			checkboxpas.Checked = IsPassive;
			CizmNumBox.Text = Drawnum;
			oprHazSurBox.Text = Setuptime;
			oprMakSurBox.Text = Machinetime;
			oprIsciSurBox.Text = Labourtime;
		}
	}
}
