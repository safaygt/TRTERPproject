namespace TRTERPproject
{
    partial class isMerkeziKart
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
            ismerkData = new DataGridView();
            ismerkodtxtBox = new TextBox();
            dillbl = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            comboBoxOprCode = new ComboBox();
            label2 = new Label();
            comboBoxIsMerTip = new ComboBox();
            geAllBut = new Button();
            dateTimeBas = new DateTimePicker();
            dateTimeBit = new DateTimePicker();
            comboBoxMalytMer = new ComboBox();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)ismerkData).BeginInit();
            SuspendLayout();
            // 
            // dilBox
            // 
            dilBox.DropDownStyle = ComboBoxStyle.DropDownList;
            dilBox.FormattingEnabled = true;
            dilBox.Location = new Point(412, 137);
            dilBox.Name = "dilBox";
            dilBox.Size = new Size(174, 28);
            dilBox.TabIndex = 68;
            // 
            // firmbox
            // 
            firmbox.DropDownStyle = ComboBoxStyle.DropDownList;
            firmbox.FormattingEnabled = true;
            firmbox.Location = new Point(35, 50);
            firmbox.Name = "firmbox";
            firmbox.Size = new Size(167, 28);
            firmbox.TabIndex = 67;
            // 
            // checkboxpas
            // 
            checkboxpas.AutoSize = true;
            checkboxpas.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            checkboxpas.ForeColor = SystemColors.Control;
            checkboxpas.Location = new Point(676, 160);
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
            deletedlbl.Location = new Point(676, 105);
            deletedlbl.Name = "deletedlbl";
            deletedlbl.Size = new Size(120, 29);
            deletedlbl.TabIndex = 65;
            deletedlbl.Text = "Silindi mi?";
            deletedlbl.UseVisualStyleBackColor = true;
            // 
            // duzBut
            // 
            duzBut.Location = new Point(520, 233);
            duzBut.Name = "duzBut";
            duzBut.Size = new Size(125, 38);
            duzBut.TabIndex = 64;
            duzBut.Text = "Düzenle";
            duzBut.UseVisualStyleBackColor = true;
            duzBut.Click += duzBut_Click;
            // 
            // DelBut
            // 
            DelBut.Location = new Point(676, 233);
            DelBut.Name = "DelBut";
            DelBut.Size = new Size(125, 38);
            DelBut.TabIndex = 63;
            DelBut.Text = "Sil";
            DelBut.UseVisualStyleBackColor = true;
            DelBut.Click += DelBut_Click;
            // 
            // addBut
            // 
            addBut.Location = new Point(361, 233);
            addBut.Name = "addBut";
            addBut.Size = new Size(125, 38);
            addBut.TabIndex = 62;
            addBut.Text = "Ekle";
            addBut.UseVisualStyleBackColor = true;
            addBut.Click += addBut_Click;
            // 
            // getBut
            // 
            getBut.Location = new Point(201, 233);
            getBut.Name = "getBut";
            getBut.Size = new Size(125, 38);
            getBut.TabIndex = 61;
            getBut.Text = "Getir";
            getBut.UseVisualStyleBackColor = true;
            getBut.Click += getBut_Click_1;
            // 
            // ismerkData
            // 
            ismerkData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ismerkData.Location = new Point(41, 281);
            ismerkData.Name = "ismerkData";
            ismerkData.RowHeadersWidth = 51;
            ismerkData.RowTemplate.Height = 29;
            ismerkData.Size = new Size(1024, 337);
            ismerkData.TabIndex = 60;
            // 
            // ismerkodtxtBox
            // 
            ismerkodtxtBox.Location = new Point(426, 50);
            ismerkodtxtBox.Name = "ismerkodtxtBox";
            ismerkodtxtBox.Size = new Size(181, 27);
            ismerkodtxtBox.TabIndex = 55;
            // 
            // dillbl
            // 
            dillbl.AutoSize = true;
            dillbl.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            dillbl.ForeColor = Color.GhostWhite;
            dillbl.Location = new Point(451, 112);
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
            label6.Location = new Point(246, 23);
            label6.Name = "label6";
            label6.Size = new Size(127, 23);
            label6.TabIndex = 53;
            label6.Text = "İş Merkezi Tipi";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.GhostWhite;
            label5.Location = new Point(895, 111);
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
            label4.Location = new Point(880, 25);
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
            label3.Location = new Point(435, 25);
            label3.Name = "label3";
            label3.Size = new Size(138, 23);
            label3.TabIndex = 50;
            label3.Text = "İş Merkezi Kodu";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.GhostWhite;
            label1.Location = new Point(62, 25);
            label1.Name = "label1";
            label1.Size = new Size(103, 23);
            label1.TabIndex = 48;
            label1.Text = "Firma Kodu";
            // 
            // comboBoxOprCode
            // 
            comboBoxOprCode.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxOprCode.FormattingEnabled = true;
            comboBoxOprCode.Location = new Point(644, 51);
            comboBoxOprCode.Name = "comboBoxOprCode";
            comboBoxOprCode.Size = new Size(186, 28);
            comboBoxOprCode.TabIndex = 70;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.GhostWhite;
            label2.Location = new Point(659, 25);
            label2.Name = "label2";
            label2.Size = new Size(142, 23);
            label2.TabIndex = 69;
            label2.Text = "Operasyon Kodu";
            // 
            // comboBoxIsMerTip
            // 
            comboBoxIsMerTip.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxIsMerTip.FormattingEnabled = true;
            comboBoxIsMerTip.Location = new Point(230, 49);
            comboBoxIsMerTip.Name = "comboBoxIsMerTip";
            comboBoxIsMerTip.Size = new Size(167, 28);
            comboBoxIsMerTip.TabIndex = 74;
            // 
            // geAllBut
            // 
            geAllBut.Location = new Point(41, 233);
            geAllBut.Name = "geAllBut";
            geAllBut.Size = new Size(125, 38);
            geAllBut.TabIndex = 75;
            geAllBut.Text = "Hepsini Getir";
            geAllBut.UseVisualStyleBackColor = true;
            geAllBut.Click += geAllBut_Click;
            // 
            // dateTimeBas
            // 
            dateTimeBas.Location = new Point(864, 52);
            dateTimeBas.Name = "dateTimeBas";
            dateTimeBas.Size = new Size(210, 27);
            dateTimeBas.TabIndex = 76;
            dateTimeBas.Value = new DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // dateTimeBit
            // 
            dateTimeBit.Location = new Point(864, 136);
            dateTimeBit.Name = "dateTimeBit";
            dateTimeBit.Size = new Size(210, 27);
            dateTimeBit.TabIndex = 77;
            // 
            // comboBoxMalytMer
            // 
            comboBoxMalytMer.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMalytMer.FormattingEnabled = true;
            comboBoxMalytMer.Location = new Point(62, 138);
            comboBoxMalytMer.Name = "comboBoxMalytMer";
            comboBoxMalytMer.Size = new Size(255, 28);
            comboBoxMalytMer.TabIndex = 79;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.GhostWhite;
            label8.Location = new Point(105, 111);
            label8.Name = "label8";
            label8.Size = new Size(175, 23);
            label8.TabIndex = 78;
            label8.Text = "Maliyet Merkezi Tipi";
            // 
            // isMerkeziKart
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(1102, 654);
            Controls.Add(comboBoxMalytMer);
            Controls.Add(label8);
            Controls.Add(dateTimeBit);
            Controls.Add(dateTimeBas);
            Controls.Add(geAllBut);
            Controls.Add(comboBoxIsMerTip);
            Controls.Add(comboBoxOprCode);
            Controls.Add(label2);
            Controls.Add(dilBox);
            Controls.Add(firmbox);
            Controls.Add(checkboxpas);
            Controls.Add(deletedlbl);
            Controls.Add(duzBut);
            Controls.Add(DelBut);
            Controls.Add(addBut);
            Controls.Add(getBut);
            Controls.Add(ismerkData);
            Controls.Add(ismerkodtxtBox);
            Controls.Add(dillbl);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "isMerkeziKart";
            Text = "İş Merkezi Veri Tablosu";
            ((System.ComponentModel.ISupportInitialize)ismerkData).EndInit();
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
        private DataGridView ismerkData;
        private TextBox ismerkodtxtBox;
        private Label dillbl;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label1;
        private ComboBox comboBoxOprCode;
        private Label label2;
        private ComboBox comboBoxIsMerTip;
        private Button geAllBut;
        private DateTimePicker dateTimeBas;
        private DateTimePicker dateTimeBit;
        private ComboBox comboBoxMalytMer;
        private Label label8;
    }
}