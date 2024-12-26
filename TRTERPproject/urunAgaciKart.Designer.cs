namespace TRTERPproject
{
	partial class urunAgaciKart
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            urnAgaTipBox = new ComboBox();
            label2 = new Label();
            firmbox = new ComboBox();
            checkboxpas = new CheckBox();
            deletedlbl = new CheckBox();
            duzBut = new Button();
            DelBut = new Button();
            addBut = new Button();
            getBut = new Button();
            urnAgcData = new DataGridView();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            textBox1 = new TextBox();
            label7 = new Label();
            urnagamalztipbox = new ComboBox();
            temelmikBox = new TextBox();
            cizmikBox = new TextBox();
            label8 = new Label();
            label9 = new Label();
            dateTimePickerBaslangic = new DateTimePicker();
            dateTimePickerBitis = new DateTimePicker();
            getAll = new Button();
            urnmalzemenumBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)urnAgcData).BeginInit();
            SuspendLayout();
            // 
            // urnAgaTipBox
            // 
            urnAgaTipBox.FormattingEnabled = true;
            urnAgaTipBox.Location = new Point(18, 160);
            urnAgaTipBox.Name = "urnAgaTipBox";
            urnAgaTipBox.Size = new Size(186, 28);
            urnAgaTipBox.TabIndex = 96;
            urnAgaTipBox.SelectedIndexChanged += urnAgaTipBox_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.GhostWhite;
            label2.Location = new Point(33, 134);
            label2.Name = "label2";
            label2.Size = new Size(135, 23);
            label2.TabIndex = 95;
            label2.Text = "Ürün Ağacı Tipi";
            // 
            // firmbox
            // 
            firmbox.FormattingEnabled = true;
            firmbox.Location = new Point(18, 74);
            firmbox.Name = "firmbox";
            firmbox.Size = new Size(186, 28);
            firmbox.TabIndex = 93;
            firmbox.SelectedIndexChanged += firmbox_SelectedIndexChanged;
            // 
            // checkboxpas
            // 
            checkboxpas.AutoSize = true;
            checkboxpas.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            checkboxpas.ForeColor = SystemColors.Control;
            checkboxpas.Location = new Point(896, 136);
            checkboxpas.Name = "checkboxpas";
            checkboxpas.Size = new Size(109, 29);
            checkboxpas.TabIndex = 92;
            checkboxpas.Text = "Pasif mi?";
            checkboxpas.UseVisualStyleBackColor = true;
            // 
            // deletedlbl
            // 
            deletedlbl.AutoSize = true;
            deletedlbl.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            deletedlbl.ForeColor = SystemColors.Control;
            deletedlbl.Location = new Point(896, 171);
            deletedlbl.Name = "deletedlbl";
            deletedlbl.Size = new Size(120, 29);
            deletedlbl.TabIndex = 91;
            deletedlbl.Text = "Silindi mi?";
            deletedlbl.UseVisualStyleBackColor = true;
            // 
            // duzBut
            // 
            duzBut.Location = new Point(543, 251);
            duzBut.Name = "duzBut";
            duzBut.Size = new Size(125, 38);
            duzBut.TabIndex = 90;
            duzBut.Text = "Düzenle";
            duzBut.UseVisualStyleBackColor = true;
            duzBut.Click += duzBut_Click;
            // 
            // DelBut
            // 
            DelBut.Location = new Point(696, 251);
            DelBut.Name = "DelBut";
            DelBut.Size = new Size(125, 38);
            DelBut.TabIndex = 89;
            DelBut.Text = "Sil";
            DelBut.UseVisualStyleBackColor = true;
            DelBut.Click += DelBut_Click;
            // 
            // addBut
            // 
            addBut.Location = new Point(396, 251);
            addBut.Name = "addBut";
            addBut.Size = new Size(125, 38);
            addBut.TabIndex = 88;
            addBut.Text = "Ekle";
            addBut.UseVisualStyleBackColor = true;
            addBut.Click += addBut_Click;
            // 
            // getBut
            // 
            getBut.Location = new Point(233, 251);
            getBut.Name = "getBut";
            getBut.Size = new Size(125, 38);
            getBut.TabIndex = 87;
            getBut.Text = "Getir";
            getBut.UseVisualStyleBackColor = true;
            getBut.Click += getBut_Click;
            // 
            // urnAgcData
            // 
            urnAgcData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            urnAgcData.Location = new Point(24, 305);
            urnAgcData.Name = "urnAgcData";
            urnAgcData.RowHeadersWidth = 51;
            urnAgcData.RowTemplate.Height = 29;
            urnAgcData.Size = new Size(1024, 337);
            urnAgcData.TabIndex = 86;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.GhostWhite;
            label6.Location = new Point(263, 50);
            label6.Name = "label6";
            label6.Size = new Size(118, 23);
            label6.TabIndex = 79;
            label6.Text = "Malzeme Tipi";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.GhostWhite;
            label5.Location = new Point(880, 48);
            label5.Name = "label5";
            label5.Size = new Size(125, 23);
            label5.TabIndex = 78;
            label5.Text = "Geçerlilik Bitiş";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.GhostWhite;
            label4.Location = new Point(671, 48);
            label4.Name = "label4";
            label4.Size = new Size(165, 23);
            label4.TabIndex = 77;
            label4.Text = "Geçerlilik Başlangıç";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.GhostWhite;
            label3.Location = new Point(460, 51);
            label3.Name = "label3";
            label3.Size = new Size(163, 23);
            label3.TabIndex = 76;
            label3.Text = "Malzeme Numarası";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.GhostWhite;
            label1.Location = new Point(49, 48);
            label1.Name = "label1";
            label1.Size = new Size(103, 23);
            label1.TabIndex = 74;
            label1.Text = "Firma Kodu";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(233, 162);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(189, 27);
            textBox1.TabIndex = 98;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.GhostWhite;
            label7.Location = new Point(242, 134);
            label7.Name = "label7";
            label7.Size = new Size(180, 23);
            label7.TabIndex = 97;
            label7.Text = "Ürün Ağacı Numarası";
            // 
            // urnagamalztipbox
            // 
            urnagamalztipbox.FormattingEnabled = true;
            urnagamalztipbox.Location = new Point(233, 76);
            urnagamalztipbox.Name = "urnagamalztipbox";
            urnagamalztipbox.Size = new Size(186, 28);
            urnagamalztipbox.TabIndex = 99;
            urnagamalztipbox.SelectedIndexChanged += urnagamalztipbox_SelectedIndexChanged;
            // 
            // temelmikBox
            // 
            temelmikBox.Location = new Point(451, 162);
            temelmikBox.Name = "temelmikBox";
            temelmikBox.Size = new Size(186, 27);
            temelmikBox.TabIndex = 101;
            // 
            // cizmikBox
            // 
            cizmikBox.Location = new Point(655, 161);
            cizmikBox.Name = "cizmikBox";
            cizmikBox.Size = new Size(209, 27);
            cizmikBox.TabIndex = 102;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.GhostWhite;
            label8.Location = new Point(470, 136);
            label8.Name = "label8";
            label8.Size = new Size(116, 23);
            label8.TabIndex = 103;
            label8.Text = "Temel Miktar";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ForeColor = Color.GhostWhite;
            label9.Location = new Point(655, 136);
            label9.Name = "label9";
            label9.Size = new Size(136, 23);
            label9.TabIndex = 104;
            label9.Text = "Çizim Numarası";
            // 
            // dateTimePickerBaslangic
            // 
            dateTimePickerBaslangic.Location = new Point(655, 76);
            dateTimePickerBaslangic.Name = "dateTimePickerBaslangic";
            dateTimePickerBaslangic.Size = new Size(203, 27);
            dateTimePickerBaslangic.TabIndex = 105;
            // 
            // dateTimePickerBitis
            // 
            dateTimePickerBitis.Location = new Point(870, 76);
            dateTimePickerBitis.Name = "dateTimePickerBitis";
            dateTimePickerBitis.Size = new Size(193, 27);
            dateTimePickerBitis.TabIndex = 106;
            // 
            // getAll
            // 
            getAll.Location = new Point(64, 251);
            getAll.Name = "getAll";
            getAll.Size = new Size(140, 34);
            getAll.TabIndex = 107;
            getAll.Text = "Hepsini Getir";
            getAll.UseVisualStyleBackColor = true;
            getAll.Click += getAll_Click;
            // 
            // urnmalzemenumBox
            // 
            urnmalzemenumBox.FormattingEnabled = true;
            urnmalzemenumBox.Location = new Point(451, 78);
            urnmalzemenumBox.Name = "urnmalzemenumBox";
            urnmalzemenumBox.Size = new Size(186, 28);
            urnmalzemenumBox.TabIndex = 175;
            // 
            // urunAgaciKart
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(1071, 673);
            Controls.Add(urnmalzemenumBox);
            Controls.Add(getAll);
            Controls.Add(dateTimePickerBitis);
            Controls.Add(dateTimePickerBaslangic);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(cizmikBox);
            Controls.Add(temelmikBox);
            Controls.Add(urnagamalztipbox);
            Controls.Add(textBox1);
            Controls.Add(label7);
            Controls.Add(urnAgaTipBox);
            Controls.Add(label2);
            Controls.Add(firmbox);
            Controls.Add(checkboxpas);
            Controls.Add(deletedlbl);
            Controls.Add(duzBut);
            Controls.Add(DelBut);
            Controls.Add(addBut);
            Controls.Add(getBut);
            Controls.Add(urnAgcData);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "urunAgaciKart";
            Text = "urunAgaciKart";
            Load += urunAgaciKart_Load;
            ((System.ComponentModel.ISupportInitialize)urnAgcData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox urnAgaTipBox;
		private Label label2;
		private ComboBox firmbox;
		private CheckBox checkboxpas;
		private CheckBox deletedlbl;
		private Button duzBut;
		private Button DelBut;
		private Button addBut;
		private Button getBut;
		private DataGridView urnAgcData;
		private Label label6;
		private Label label5;
		private Label label4;
		private Label label3;
		private Label label1;
		private TextBox textBox1;
		private Label label7;
		private ComboBox urnagamalztipbox;
        private TextBox temelmikBox;
        private TextBox cizmikBox;
        private Label label8;
        private Label label9;
        private DateTimePicker dateTimePickerBaslangic;
        private DateTimePicker dateTimePickerBitis;
        private Button getAll;
        private ComboBox urnmalzemenumBox;
    }
}