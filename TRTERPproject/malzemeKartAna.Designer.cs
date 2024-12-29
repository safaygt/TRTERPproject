namespace TRTERPproject
{
    partial class malzemeKartAna
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
            comboBoxTedTip = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            malNotxtBox = new TextBox();
            malacicTxtBox = new TextBox();
            malKartAna = new DataGridView();
            getBut = new Button();
            addBut = new Button();
            DelBut = new Button();
            duzBut = new Button();
            isDeletedCheckBox = new CheckBox();
            isPassiveCheckBox = new CheckBox();
            comboBoxMalzFirm = new ComboBox();
            malzTipcombo = new ComboBox();
            label8 = new Label();
            comboBoxDil = new ComboBox();
            dateTimePickerBaslangic = new DateTimePicker();
            dateTimePickerBitis = new DateTimePicker();
            getAll = new Button();
            ((System.ComponentModel.ISupportInitialize)malKartAna).BeginInit();
            SuspendLayout();
            // 
            // comboBoxTedTip
            // 
            comboBoxTedTip.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTedTip.FormattingEnabled = true;
            comboBoxTedTip.Location = new Point(244, 133);
            comboBoxTedTip.Name = "comboBoxTedTip";
            comboBoxTedTip.Size = new Size(151, 28);
            comboBoxTedTip.TabIndex = 27;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.GhostWhite;
            label1.Location = new Point(94, 30);
            label1.Name = "label1";
            label1.Size = new Size(103, 23);
            label1.TabIndex = 0;
            label1.Text = "Firma Kodu";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.GhostWhite;
            label2.Location = new Point(424, 30);
            label2.Name = "label2";
            label2.Size = new Size(173, 23);
            label2.TabIndex = 1;
            label2.Text = "Malzeme Açıklaması";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.GhostWhite;
            label3.Location = new Point(239, 30);
            label3.Name = "label3";
            label3.Size = new Size(163, 23);
            label3.TabIndex = 2;
            label3.Text = "Malzeme Numarası";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.GhostWhite;
            label4.Location = new Point(850, 105);
            label4.Name = "label4";
            label4.Size = new Size(125, 23);
            label4.TabIndex = 3;
            label4.Text = "Geçerlilik Bitiş";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.GhostWhite;
            label5.Location = new Point(850, 30);
            label5.Name = "label5";
            label5.Size = new Size(165, 23);
            label5.TabIndex = 4;
            label5.Text = "Geçerlilik Başlangıç";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.GhostWhite;
            label6.Location = new Point(94, 107);
            label6.Name = "label6";
            label6.Size = new Size(118, 23);
            label6.TabIndex = 5;
            label6.Text = "Malzeme Tipi";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.GhostWhite;
            label7.Location = new Point(265, 107);
            label7.Name = "label7";
            label7.Size = new Size(105, 23);
            label7.TabIndex = 6;
            label7.Text = "Tedarik Tipi";
            // 
            // malNotxtBox
            // 
            malNotxtBox.Location = new Point(244, 56);
            malNotxtBox.Name = "malNotxtBox";
            malNotxtBox.Size = new Size(158, 27);
            malNotxtBox.TabIndex = 9;
            // 
            // malacicTxtBox
            // 
            malacicTxtBox.Location = new Point(424, 56);
            malacicTxtBox.Name = "malacicTxtBox";
            malacicTxtBox.Size = new Size(173, 27);
            malacicTxtBox.TabIndex = 10;
            // 
            // malKartAna
            // 
            malKartAna.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            malKartAna.Location = new Point(12, 286);
            malKartAna.Name = "malKartAna";
            malKartAna.RowHeadersWidth = 51;
            malKartAna.RowTemplate.Height = 29;
            malKartAna.Size = new Size(1024, 337);
            malKartAna.TabIndex = 16;
            // 
            // getBut
            // 
            getBut.Location = new Point(184, 214);
            getBut.Name = "getBut";
            getBut.Size = new Size(125, 38);
            getBut.TabIndex = 17;
            getBut.Text = "Getir";
            getBut.UseVisualStyleBackColor = true;
            getBut.Click += getBut_Click;
            // 
            // addBut
            // 
            addBut.Location = new Point(352, 214);
            addBut.Name = "addBut";
            addBut.Size = new Size(125, 38);
            addBut.TabIndex = 18;
            addBut.Text = "Ekle";
            addBut.UseVisualStyleBackColor = true;
            addBut.Click += addBut_Click;
            // 
            // DelBut
            // 
            DelBut.Location = new Point(675, 214);
            DelBut.Name = "DelBut";
            DelBut.Size = new Size(125, 38);
            DelBut.TabIndex = 19;
            DelBut.Text = "Sil";
            DelBut.UseVisualStyleBackColor = true;
            DelBut.Click += DelBut_Click;
            // 
            // duzBut
            // 
            duzBut.Location = new Point(513, 214);
            duzBut.Name = "duzBut";
            duzBut.Size = new Size(125, 38);
            duzBut.TabIndex = 20;
            duzBut.Text = "Düzenle";
            duzBut.UseVisualStyleBackColor = true;
            duzBut.Click += duzBut_Click;
            // 
            // isDeletedCheckBox
            // 
            isDeletedCheckBox.AutoSize = true;
            isDeletedCheckBox.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            isDeletedCheckBox.ForeColor = SystemColors.Control;
            isDeletedCheckBox.Location = new Point(864, 214);
            isDeletedCheckBox.Name = "isDeletedCheckBox";
            isDeletedCheckBox.Size = new Size(120, 29);
            isDeletedCheckBox.TabIndex = 21;
            isDeletedCheckBox.Text = "Silindi mi?";
            isDeletedCheckBox.UseVisualStyleBackColor = true;
            // 
            // isPassiveCheckBox
            // 
            isPassiveCheckBox.AutoSize = true;
            isPassiveCheckBox.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            isPassiveCheckBox.ForeColor = SystemColors.Control;
            isPassiveCheckBox.Location = new Point(864, 179);
            isPassiveCheckBox.Name = "isPassiveCheckBox";
            isPassiveCheckBox.Size = new Size(109, 29);
            isPassiveCheckBox.TabIndex = 22;
            isPassiveCheckBox.Text = "Pasif mi?";
            isPassiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // comboBoxMalzFirm
            // 
            comboBoxMalzFirm.FormattingEnabled = true;
            comboBoxMalzFirm.Location = new Point(72, 55);
            comboBoxMalzFirm.Name = "comboBoxMalzFirm";
            comboBoxMalzFirm.Size = new Size(151, 28);
            comboBoxMalzFirm.TabIndex = 23;
            comboBoxMalzFirm.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // malzTipcombo
            // 
            malzTipcombo.FormattingEnabled = true;
            malzTipcombo.Location = new Point(72, 133);
            malzTipcombo.Name = "malzTipcombo";
            malzTipcombo.Size = new Size(151, 28);
            malzTipcombo.TabIndex = 24;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.GhostWhite;
            label8.Location = new Point(462, 107);
            label8.Name = "label8";
            label8.Size = new Size(80, 23);
            label8.TabIndex = 25;
            label8.Text = "Dil Kodu";
            // 
            // comboBoxDil
            // 
            comboBoxDil.FormattingEnabled = true;
            comboBoxDil.Location = new Point(424, 133);
            comboBoxDil.Name = "comboBoxDil";
            comboBoxDil.Size = new Size(173, 28);
            comboBoxDil.TabIndex = 26;
            // 
            // dateTimePickerBaslangic
            // 
            dateTimePickerBaslangic.Location = new Point(829, 57);
            dateTimePickerBaslangic.Name = "dateTimePickerBaslangic";
            dateTimePickerBaslangic.Size = new Size(207, 27);
            dateTimePickerBaslangic.TabIndex = 30;
            // 
            // dateTimePickerBitis
            // 
            dateTimePickerBitis.Location = new Point(829, 131);
            dateTimePickerBitis.Name = "dateTimePickerBitis";
            dateTimePickerBitis.Size = new Size(207, 27);
            dateTimePickerBitis.TabIndex = 31;
            // 
            // getAll
            // 
            getAll.Location = new Point(28, 214);
            getAll.Name = "getAll";
            getAll.Size = new Size(112, 38);
            getAll.TabIndex = 32;
            getAll.Text = "Hepsini Getir";
            getAll.UseVisualStyleBackColor = true;
            getAll.Click += getAll_Click;
            // 
            // malzemeKartAna
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(1048, 635);
            Controls.Add(getAll);
            Controls.Add(dateTimePickerBitis);
            Controls.Add(dateTimePickerBaslangic);
            Controls.Add(comboBoxTedTip);
            Controls.Add(comboBoxDil);
            Controls.Add(label8);
            Controls.Add(malzTipcombo);
            Controls.Add(comboBoxMalzFirm);
            Controls.Add(isPassiveCheckBox);
            Controls.Add(isDeletedCheckBox);
            Controls.Add(duzBut);
            Controls.Add(DelBut);
            Controls.Add(addBut);
            Controls.Add(getBut);
            Controls.Add(malKartAna);
            Controls.Add(malacicTxtBox);
            Controls.Add(malNotxtBox);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "malzemeKartAna";
            Text = "Malzeme Veri Tablosu";
            Load += malzemeKartAna_Load;
            ((System.ComponentModel.ISupportInitialize)malKartAna).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox malNotxtBox;
        private TextBox malacicTxtBox;
        private DataGridView malKartAna;
        private Button getBut;
        private Button addBut;
        private Button DelBut;
        private Button duzBut;
        private CheckBox isDeletedCheckBox;
        private CheckBox isPassiveCheckBox;
        private ComboBox comboBoxMalzFirm;
        private ComboBox malzTipcombo;
        private Label label8;
        private ComboBox comboBoxDil;
        private ComboBox comboBoxTedTip;
        private DateTimePicker dateTimePickerBaslangic;
        private DateTimePicker dateTimePickerBitis;
        private Button getAll;
    }
}