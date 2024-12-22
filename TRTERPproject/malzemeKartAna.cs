﻿using System.Data;
using System.Data.SqlClient;
using TRTERPproject.Helpers;
namespace TRTERPproject
{
	public partial class malzemeKartAna : Form
	{
		SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);
		SqlDataReader reader;
		SqlCommand cmd;


		public malzemeKartAna()
		{
			InitializeComponent();
		}

		private void getBut_Click(object sender, EventArgs e)
		{

			try
			{
				using (con)
				{
					con.Open();

					// SQL sorgusu
					string query = @"
                        SELECT 
							COMCODE AS 'Firma', 
                            MATDOCTYPE AS 'Malzeme Tipi', 
                            MATDOCNUM AS 'Malzeme Numarası', 
                            MATDOCFROM AS 'Geçerlilik Başlangıç',
							MATDOCUNTIL	AS 'Geçerlilik Bitiş',
							SUPPLYTYPE AS 'Tedarik Tipi',
							STUNIT AS 'Stok Birimi',
							NETWEIGHT AS 'Net Ağırlık',
							NWUNIT AS 'Net Ağırlık Birimi',
							BRUTWEIGHT AS 'Brüt Ağırlık',
							BWUNIT AS 'Brüt Ağırlık Brimi',
							ISBOM AS 'Ürün Ağacı Var mı?',
							BOMDOCTYPE AS 'Ürün Ağacı Tipi',
							BOMDOCNUM AS 'Ürün Ağacı Kodu',
							ISROUTE AS 'Rota Var mı?',
							ROTDOCTYPE AS 'Rota Tipi',
							ROTDOCNUM AS 'Rota Kodu',
							ISDELETED AS 'Silindi mi?',
							ISPASSIVE AS 'Pasif mi?'
                        FROM BSMGRTRTMATHEAD";

					// Verileri çekmek için SqlDataAdapter kullanımı
					SqlDataAdapter da = new SqlDataAdapter(query, con);
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
