using System;
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

	public partial class RotaKartAdd : Form
	{
		SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
		SqlDataReader reader;
		SqlCommand cmd;
		public RotaKartAdd()
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

		private void RotaKartAdd_Load(object sender, EventArgs e)
		{
			LoadComboBoxData();
		}

		private void SaveBut_Click(object sender, EventArgs e)
		{
			string Firma = firmbox.Text;
			string Rotdoctype = comboBoxRotTip.Text;
			string Rotdocnum = RotaNumBox.Text;
			string Doctype = comboBoxurnAgaTip.Text;
			string Bomdocnum = UrnAganum.Text;
			string Bomdoctype = comboBoxurnAgaTip.Text;
			string Matdoctype = comboBoxMalzTip.Text;
			string Matdocnum = MalzKodBox.Text;
			string Wcmdoctype = comboBoxIsmerkTip.Text;
			string Wcmdocnum  = IsMerkzKodBox.Text;
			string Oprdoctype = OprKodBox.Text;
			int Oprnum = int.Parse(OprNumBox.Text);
			decimal Setuptime = decimal.Parse(oprHazSurBox.Text);
			decimal Machinetime = decimal.Parse(oprMakSurBox.Text);
			decimal Labourtime = decimal.Parse(oprIsciSurBox.Text);
			int Contentnum = int.Parse(IcrNumBox.Text); 
			int Component = int.Parse(BilesKodBox.Text);
			decimal Quantity = decimal.Parse(BilesMiktrBox.Text);
			decimal Quantity2 = decimal.Parse(MiktrBox.Text);
			string Drawnum = CizmNumBox.Text;
			DateTime GecerliBaslangic = dateTimePickerBaslangic.Value;
			DateTime GecerliBitis = dateTimePickerBitis.Value;
			string Dil = dilBox.Text;
			bool IsDeleted = deletedlbl.Checked;
			bool IsPassive = checkboxpas.Checked;

			// Tüm alanların boş olup olmadığını kontrol et
			if (string.IsNullOrWhiteSpace(Firma) ||
				string.IsNullOrWhiteSpace(Rotdoctype) ||
				string.IsNullOrWhiteSpace(Rotdocnum) ||
				string.IsNullOrWhiteSpace(Doctype) ||
				string.IsNullOrWhiteSpace(Bomdocnum) ||
				string.IsNullOrWhiteSpace(Matdoctype) ||
				string.IsNullOrWhiteSpace(Matdocnum) ||
				string.IsNullOrWhiteSpace(Wcmdoctype) ||
				string.IsNullOrWhiteSpace(Wcmdocnum) ||
				string.IsNullOrWhiteSpace(Oprdoctype) ||
				string.IsNullOrWhiteSpace(Oprnum.ToString()) ||
				string.IsNullOrWhiteSpace(Setuptime.ToString()) ||
				string.IsNullOrWhiteSpace(Machinetime.ToString()) ||
				string.IsNullOrWhiteSpace(Labourtime.ToString()) ||
				string.IsNullOrWhiteSpace(Contentnum.ToString()) ||
				string.IsNullOrWhiteSpace(Component.ToString()) ||
				string.IsNullOrWhiteSpace(Quantity.ToString()) ||
				string.IsNullOrWhiteSpace(Quantity2.ToString()) ||
				string.IsNullOrWhiteSpace(Drawnum) ||
				string.IsNullOrWhiteSpace(Dil))
			{
				MessageBox.Show("Boş alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// Tarih mantık hatasını kontrol etme
			if (GecerliBaslangic >= GecerliBitis)
			{
				MessageBox.Show("Başlangıç tarihi, bitiş tarihinden önce olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
			{
				con.Open();
				SqlTransaction transaction = con.BeginTransaction();

				try
				{
					string query1 = @"
                INSERT INTO BSMGRTRTROTHEAD 
                (COMCODE, ROTDOCTYPE, ROTDOCNUM, ROTDOCFROM, ROTDOCUNTIL, MATDOCTYPE, MATDOCNUM, QUANTITY, DRAWNUM, ISDELETED, ISPASSIVE) 
                VALUES 
                (@COMCODE, @ROTDOCTYPE, @ROTDOCNUM, @ROTDOCFROM, @ROTDOCUNTIL, @MATDOCTYPE, @MATDOCNUM, @QUANTITY, @DRAWNUM, @ISDELETED, @ISPASSIVE)";

					using (SqlCommand cmd1 = new SqlCommand(query1, con, transaction))
					{
						cmd1.Parameters.AddWithValue("@COMCODE", Firma);
						cmd1.Parameters.AddWithValue("@ROTDOCTYPE", Rotdoctype);
						cmd1.Parameters.AddWithValue("@ROTDOCNUM", Rotdocnum);
						cmd1.Parameters.AddWithValue("@ROTDOCFROM", GecerliBaslangic);
						cmd1.Parameters.AddWithValue("@ROTDOCUNTIL", GecerliBitis);
						cmd1.Parameters.AddWithValue("@MATDOCTYPE", Matdoctype);
						cmd1.Parameters.AddWithValue("@MATDOCNUM", Matdocnum);
						cmd1.Parameters.AddWithValue("@QUANTITY", Quantity2);
						cmd1.Parameters.AddWithValue("@DRAWNUM", Drawnum);
						cmd1.Parameters.AddWithValue("@ISDELETED", IsDeleted ? 1 : 0);
						cmd1.Parameters.AddWithValue("@ISPASSIVE", IsPassive ? 1 : 0);
						cmd1.ExecuteNonQuery();
					}

					string query2 = @"
                INSERT INTO BSMGRTRTROTOPRCONTENT
                (COMCODE, ROTDOCTYPE, ROTDOCNUM, BOMDOCFROM, BOMDOCUNTIL, MATDOCTYPE, MATDOCNUM, OPRNUM, OPRDOCTYPE, WCMDOCTYPE, WCMDOCNUM, SETUPTIME, MACHINETIME, LABOURTIME) 
                VALUES 
                (@COMCODE, @ROTDOCTYPE, @ROTDOCNUM, @BOMDOCFROM, @BOMDOCUNTIL, @MATDOCTYPE, @MATDOCNUM, @OPRNUM, @OPRDOCTYPE, @WCMDOCTYPE, @WCMDOCNUM, @SETUPTIME, @MACHINETIME, @LABOURTIME)";

					using (SqlCommand cmd2 = new SqlCommand(query2, con, transaction))
					{
						cmd2.Parameters.AddWithValue("@COMCODE", Firma);
						cmd2.Parameters.AddWithValue("@ROTDOCTYPE", Rotdoctype);
						cmd2.Parameters.AddWithValue("@ROTDOCNUM", Rotdocnum);
						cmd2.Parameters.AddWithValue("@BOMDOCFROM", GecerliBaslangic);
						cmd2.Parameters.AddWithValue("@BOMDOCUNTIL", GecerliBitis);
						cmd2.Parameters.AddWithValue("@MATDOCTYPE", Matdoctype);
						cmd2.Parameters.AddWithValue("@MATDOCNUM", Matdocnum);
						cmd2.Parameters.AddWithValue("@OPRNUM", Oprnum);
						cmd2.Parameters.AddWithValue("@OPRDOCTYPE", Oprdoctype);
						cmd2.Parameters.AddWithValue("@WCMDOCTYPE", Wcmdoctype);
						cmd2.Parameters.AddWithValue("@WCMDOCNUM", Wcmdocnum);
						cmd2.Parameters.AddWithValue("@SETUPTIME", Setuptime);
						cmd2.Parameters.AddWithValue("@MACHINETIME", Machinetime);
						cmd2.Parameters.AddWithValue("@LABOURTIME", Labourtime);
						cmd2.ExecuteNonQuery();
					}

					string query3 = @"
                INSERT INTO BSMGRTRTROTBOMCONTENT
                (COMCODE, ROTDOCTYPE, ROTDOCNUM, ROTDOCFROM, ROTDOCUNTIL, MATDOCTYPE, MATDOCNUM, OPRNUM, BOMDOCTYPE, BOMDOCNUM, CONTENTNUM, COMPONENT, QUANTITY) 
                VALUES 
                (@COMCODE, @ROTDOCTYPE, @ROTDOCNUM, @ROTDOCFROM, @ROTDOCUNTIL, @MATDOCTYPE, @MATDOCNUM, @OPRNUM, @BOMDOCTYPE, @BOMDOCNUM, @CONTENTNUM, @COMPONENT, @QUANTITY)";

					using (SqlCommand cmd3 = new SqlCommand(query3, con, transaction))
					{
						cmd3.Parameters.AddWithValue("@COMCODE", Firma);
						cmd3.Parameters.AddWithValue("@ROTDOCTYPE", Rotdoctype);
						cmd3.Parameters.AddWithValue("@ROTDOCNUM", Rotdocnum);
						cmd3.Parameters.AddWithValue("@ROTDOCFROM", GecerliBaslangic);
						cmd3.Parameters.AddWithValue("@ROTDOCUNTIL", GecerliBitis);
						cmd3.Parameters.AddWithValue("@MATDOCTYPE", Matdoctype);
						cmd3.Parameters.AddWithValue("@MATDOCNUM", Matdocnum);
						cmd3.Parameters.AddWithValue("@OPRNUM", Oprnum);
						cmd3.Parameters.AddWithValue("@BOMDOCTYPE",Bomdoctype);
						cmd3.Parameters.AddWithValue("@BOMDOCNUM", Bomdocnum);
						cmd3.Parameters.AddWithValue("@CONTENTNUM", Contentnum);
						cmd3.Parameters.AddWithValue("@COMPONENT", Component);
						cmd3.Parameters.AddWithValue("@QUANTITY", Quantity);
						cmd3.ExecuteNonQuery();
					}

					transaction.Commit();
					MessageBox.Show("Kayıt başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (SqlException ex)
				{
					transaction.Rollback();

					if (ex.Message.Contains("UQ_ROTDOCFROM"))
					{
						MessageBox.Show("Bu tarih için zaten bir kayıt mevcut. Lütfen farklı bir tarih giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}
	}
	
}

