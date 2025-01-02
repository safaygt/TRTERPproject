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
    public partial class urunPatlatmaEdit : Form
    {
        private string bomDocNum;
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString);

        // Özellikler
        public string Firma { get; set; }
        public string UrunAgaciTipi { get; set; }
        public string UrunAgaciNumarasi { get; set; }
        public string MalzemeTipi { get; set; }
        public string MalzemeNumarasi { get; set; }
        public decimal TemelMiktar { get; set; }
        public string CizimNumarasi { get; set; }
        public DateTime GecerlilikBaslangic { get; set; }
        public DateTime GecerlilikBitis { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsPassive { get; set; }
        public string BilesenKodu { get; set; }
        public string KalemUrunAgaciTipi { get; set; }
        public string KalemUrunAgaciNumarasi { get; set; }
        public string? IcerikNumarasi { get; internal set; }


        public urunPatlatmaEdit()
        {
            InitializeComponent();
        }

        private void urunPatlatmaEdit_Load(object sender, EventArgs e)
        {
            // Formdaki alanları özelliklerle doldurma
            firmbox.Text = Firma;
            urnAgaTipBox.Text = UrunAgaciTipi;
            textBox1.Text = UrunAgaciNumarasi;
            urnagamalztipbox.Text = MalzemeTipi;
            urnmalzemenumBox.Text = MalzemeNumarasi;
            icerikNumBox.Text = IcerikNumarasi;
            kalemAgUrnTipBox.Text = KalemUrunAgaciTipi;
            kalemAgUrnKodBox.Text = KalemUrunAgaciNumarasi;
            bilesenKodBox.Text = BilesenKodu;
            temelmikBox.Text = TemelMiktar.ToString();
            baslangicDateTimePicker.Value = GecerlilikBaslangic;
            bitisDateTimePicker.Value = GecerlilikBitis;

            // Combobox verilerini doldur
            LoadComboBoxData();
        }



        private void LoadComboBoxData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    con.Open();

                    // Firma verileri
                    LoadComboBoxData("SELECT DISTINCT COMCODE FROM BSMGRTRTGEN001", firmbox, "COMCODE");

                    // Malzeme Tipi
                    LoadComboBoxData("SELECT DISTINCT DOCTYPE FROM BSMGRTRTMAT001", urnagamalztipbox, "DOCTYPE");

                    // Ürün Ağacı Tipi
                    LoadComboBoxData("SELECT DISTINCT DOCTYPE FROM BSMGRTRTBOM001", urnAgaTipBox, "DOCTYPE");

                    LoadComboBoxData("SELECT DISTINCT BOMDOCNUM FROM BSMGRTRTBOMHEAD", textBox1, "BOMDOCNUM");

                    LoadComboBoxData("SELECT DISTINCT COMPONENT FROM BSMGRTRTBOMCONTENT", bilesenKodBox, "COMPONENT");

                    LoadComboBoxData("SELECT DISTINCT COMPBOMDOCNUM FROM BSMGRTRTBOMCONTENT", kalemAgUrnTipBox, "COMPBOMDOCNUM");

                    LoadComboBoxData("SELECT DISTINCT COMPBOMDOCTYPE FROM BSMGRTRTBOMCONTENT", kalemAgUrnKodBox, "COMPBOMDOCTYPE");

                    LoadComboBoxData("SELECT DISTINCT MATDOCNUM FROM BSMGRTRTMATHEAD", urnmalzemenumBox, "MATDOCNUM");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veriler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxData(string query, ComboBox comboBox, string displayMember)
        {
            using (SqlConnection con = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox.DataSource = dt;
                comboBox.DisplayMember = displayMember;
                comboBox.ValueMember = displayMember;
            }
        }

       




        private void contentbtnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
