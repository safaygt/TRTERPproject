namespace TRTERPproject
{
	partial class RotaKartAna
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
            dilBox = new ComboBox();
            firmbox = new ComboBox();
            checkboxpas = new CheckBox();
            deletedlbl = new CheckBox();
            duzBut = new Button();
            DelBut = new Button();
            addBut = new Button();
            getBut = new Button();
            RotaData = new DataGridView();
            maliyTxtBox = new TextBox();
            malNotxtBox = new TextBox();
            dillbl = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            comboBoxRotTip = new ComboBox();
            getAllBut = new Button();
            dateTimePickerBaslangic = new DateTimePicker();
            dateTimePickerBitis = new DateTimePicker();
            malTypelabel = new Label();
            ((System.ComponentModel.ISupportInitialize)RotaData).BeginInit();
            SuspendLayout();
            // 
            // dilBox
            // 
            dilBox.FormattingEnabled = true;
            dilBox.Location = new Point(678, 84);
            dilBox.Name = "dilBox";
            dilBox.Size = new Size(174, 28);
            dilBox.TabIndex = 68;
            // 
            // firmbox
            // 
            firmbox.FormattingEnabled = true;
            firmbox.Location = new Point(57, 84);
            firmbox.Name = "firmbox";
            firmbox.Size = new Size(167, 28);
            firmbox.TabIndex = 67;
            // 
            // checkboxpas
            // 
            checkboxpas.AutoSize = true;
            checkboxpas.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            checkboxpas.ForeColor = SystemColors.Control;
            checkboxpas.Location = new Point(907, 236);
            checkboxpas.Name = "checkboxpas";
            checkboxpas.Size = new Size(109, 29);
            checkboxpas.TabIndex = 66;
            checkboxpas.Text = "Pasif mi?";
            checkboxpas.UseVisualStyleBackColor = true;
            // 
            // deletedlbl
            // 
            deletedlbl.AutoSize = true;
            deletedlbl.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            deletedlbl.ForeColor = SystemColors.Control;
            deletedlbl.Location = new Point(907, 271);
            deletedlbl.Name = "deletedlbl";
            deletedlbl.Size = new Size(120, 29);
            deletedlbl.TabIndex = 65;
            deletedlbl.Text = "Silindi mi?";
            deletedlbl.UseVisualStyleBackColor = true;
            // 
            // duzBut
            // 
            duzBut.Location = new Point(718, 267);
            duzBut.Name = "duzBut";
            duzBut.Size = new Size(125, 38);
            duzBut.TabIndex = 64;
            duzBut.Text = "Düzenle";
            duzBut.UseVisualStyleBackColor = true;
            // 
            // DelBut
            // 
            DelBut.Location = new Point(554, 267);
            DelBut.Name = "DelBut";
            DelBut.Size = new Size(125, 38);
            DelBut.TabIndex = 63;
            DelBut.Text = "Sil";
            DelBut.UseVisualStyleBackColor = true;
            // 
            // addBut
            // 
            addBut.Location = new Point(386, 267);
            addBut.Name = "addBut";
            addBut.Size = new Size(125, 38);
            addBut.TabIndex = 62;
            addBut.Text = "Ekle";
            addBut.UseVisualStyleBackColor = true;
            // 
            // getBut
            // 
            getBut.Location = new Point(218, 267);
            getBut.Name = "getBut";
            getBut.Size = new Size(125, 38);
            getBut.TabIndex = 61;
            getBut.Text = "Getir";
            getBut.UseVisualStyleBackColor = true;
            getBut.Click += getBut_Click;
            // 
            // RotaData
            // 
            RotaData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RotaData.Location = new Point(41, 325);
            RotaData.Name = "RotaData";
            RotaData.RowHeadersWidth = 51;
            RotaData.RowTemplate.Height = 29;
            RotaData.Size = new Size(1024, 337);
            RotaData.TabIndex = 60;
            // 
            // maliyTxtBox
            // 
            maliyTxtBox.Location = new Point(57, 172);
            maliyTxtBox.Name = "maliyTxtBox";
            maliyTxtBox.Size = new Size(213, 27);
            maliyTxtBox.TabIndex = 56;
            // 
            // malNotxtBox
            // 
            malNotxtBox.Location = new Point(457, 85);
            malNotxtBox.Name = "malNotxtBox";
            malNotxtBox.Size = new Size(181, 27);
            malNotxtBox.TabIndex = 55;
            // 
            // dillbl
            // 
            dillbl.AutoSize = true;
            dillbl.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            dillbl.ForeColor = Color.GhostWhite;
            dillbl.Location = new Point(717, 59);
            dillbl.Name = "dillbl";
            dillbl.Size = new Size(80, 23);
            dillbl.TabIndex = 54;
            dillbl.Text = "Dil Kodu";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.GhostWhite;
            label6.Location = new Point(301, 59);
            label6.Name = "label6";
            label6.Size = new Size(83, 23);
            label6.TabIndex = 53;
            label6.Text = "Rota Tipi";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.GhostWhite;
            label5.Location = new Point(940, 136);
            label5.Name = "label5";
            label5.Size = new Size(125, 23);
            label5.TabIndex = 52;
            label5.Text = "Geçerlilik Bitiş";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.GhostWhite;
            label4.Location = new Point(917, 59);
            label4.Name = "label4";
            label4.Size = new Size(165, 23);
            label4.TabIndex = 51;
            label4.Text = "Geçerlilik Başlangıç";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.GhostWhite;
            label3.Location = new Point(501, 59);
            label3.Name = "label3";
            label3.Size = new Size(94, 23);
            label3.TabIndex = 50;
            label3.Text = "Rota Kodu";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.GhostWhite;
            label1.Location = new Point(84, 59);
            label1.Name = "label1";
            label1.Size = new Size(103, 23);
            label1.TabIndex = 48;
            label1.Text = "Firma Kodu";
            // 
            // comboBoxRotTip
            // 
            comboBoxRotTip.FormattingEnabled = true;
            comboBoxRotTip.Location = new Point(253, 85);
            comboBoxRotTip.Name = "comboBoxRotTip";
            comboBoxRotTip.Size = new Size(174, 28);
            comboBoxRotTip.TabIndex = 69;
            // 
            // getAllBut
            // 
            getAllBut.Location = new Point(57, 267);
            getAllBut.Name = "getAllBut";
            getAllBut.Size = new Size(125, 38);
            getAllBut.TabIndex = 70;
            getAllBut.Text = "Hepsini Getir";
            getAllBut.UseVisualStyleBackColor = true;
            getAllBut.Click += getAllBut_Click;
            // 
            // dateTimePickerBaslangic
            // 
            dateTimePickerBaslangic.Location = new Point(902, 86);
            dateTimePickerBaslangic.Name = "dateTimePickerBaslangic";
            dateTimePickerBaslangic.Size = new Size(203, 27);
            dateTimePickerBaslangic.TabIndex = 131;
            dateTimePickerBaslangic.Value = new DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // dateTimePickerBitis
            // 
            dateTimePickerBitis.Location = new Point(902, 162);
            dateTimePickerBitis.Name = "dateTimePickerBitis";
            dateTimePickerBitis.Size = new Size(203, 27);
            dateTimePickerBitis.TabIndex = 132;
            dateTimePickerBitis.Value = new DateTime(2024, 12, 1, 0, 0, 0, 0);
            // 
            // malTypelabel
            // 
            malTypelabel.AutoSize = true;
            malTypelabel.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            malTypelabel.ForeColor = Color.GhostWhite;
            malTypelabel.Location = new Point(84, 146);
            malTypelabel.Name = "malTypelabel";
            malTypelabel.Size = new Size(163, 23);
            malTypelabel.TabIndex = 133;
            malTypelabel.Text = "Malzeme Numarası";
            // 
            // RotaKartAna
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(1144, 710);
            Controls.Add(malTypelabel);
            Controls.Add(dateTimePickerBitis);
            Controls.Add(dateTimePickerBaslangic);
            Controls.Add(getAllBut);
            Controls.Add(comboBoxRotTip);
            Controls.Add(dilBox);
            Controls.Add(firmbox);
            Controls.Add(checkboxpas);
            Controls.Add(deletedlbl);
            Controls.Add(duzBut);
            Controls.Add(DelBut);
            Controls.Add(addBut);
            Controls.Add(getBut);
            Controls.Add(RotaData);
            Controls.Add(maliyTxtBox);
            Controls.Add(malNotxtBox);
            Controls.Add(dillbl);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "RotaKartAna";
            Text = "RotaKartAna";
            ((System.ComponentModel.ISupportInitialize)RotaData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox dilBox;
		private ComboBox firmbox;
		private CheckBox checkboxpas;
		private CheckBox deletedlbl;
		private Button duzBut;
		private Button DelBut;
		private Button addBut;
		private Button getBut;
		private DataGridView RotaData;
		private TextBox maliyTxtBox;
		private TextBox malNotxtBox;
		private Label dillbl;
		private Label label6;
		private Label label5;
		private Label label4;
		private Label label3;
		private Label label1;
		private ComboBox comboBoxRotTip;
        private Button getAllBut;
        private DateTimePicker dateTimePickerBaslangic;
        private DateTimePicker dateTimePickerBitis;
        private Label malTypelabel;
    }
}