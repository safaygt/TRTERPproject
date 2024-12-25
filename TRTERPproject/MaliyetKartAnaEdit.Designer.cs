namespace TRTERPproject
{
    partial class MaliyetKartAnaEdit
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
            dateTimePickerBitis = new DateTimePicker();
            dateTimePickerBaslangic = new DateTimePicker();
            comboBoxMalMerTip = new ComboBox();
            dilCombo = new ComboBox();
            firmComboBox = new ComboBox();
            checkboxpas = new CheckBox();
            deletedlbl = new CheckBox();
            maliyTxtBox = new TextBox();
            malNotxtBox = new TextBox();
            dillbl = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            maliylbl = new Label();
            label1 = new Label();
            btnSave = new Button();
            maliKodTxtBox = new TextBox();
            malikodlbl = new Label();
            SuspendLayout();
            // 
            // dateTimePickerBitis
            // 
            dateTimePickerBitis.Location = new Point(719, 204);
            dateTimePickerBitis.Name = "dateTimePickerBitis";
            dateTimePickerBitis.Size = new Size(203, 27);
            dateTimePickerBitis.TabIndex = 131;
            dateTimePickerBitis.Value = new DateTime(2024, 12, 1, 0, 0, 0, 0);
            // 
            // dateTimePickerBaslangic
            // 
            dateTimePickerBaslangic.Location = new Point(719, 117);
            dateTimePickerBaslangic.Name = "dateTimePickerBaslangic";
            dateTimePickerBaslangic.Size = new Size(203, 27);
            dateTimePickerBaslangic.TabIndex = 130;
            dateTimePickerBaslangic.Value = new DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // comboBoxMalMerTip
            // 
            comboBoxMalMerTip.FormattingEnabled = true;
            comboBoxMalMerTip.Location = new Point(259, 116);
            comboBoxMalMerTip.Name = "comboBoxMalMerTip";
            comboBoxMalMerTip.Size = new Size(175, 28);
            comboBoxMalMerTip.TabIndex = 128;
            // 
            // dilCombo
            // 
            dilCombo.FormattingEnabled = true;
            dilCombo.Location = new Point(341, 217);
            dilCombo.Name = "dilCombo";
            dilCombo.Size = new Size(174, 28);
            dilCombo.TabIndex = 127;
            // 
            // firmComboBox
            // 
            firmComboBox.FormattingEnabled = true;
            firmComboBox.Location = new Point(64, 116);
            firmComboBox.Name = "firmComboBox";
            firmComboBox.Size = new Size(167, 28);
            firmComboBox.TabIndex = 126;
            // 
            // checkboxpas
            // 
            checkboxpas.AutoSize = true;
            checkboxpas.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            checkboxpas.ForeColor = SystemColors.Control;
            checkboxpas.Location = new Point(742, 268);
            checkboxpas.Name = "checkboxpas";
            checkboxpas.Size = new Size(109, 29);
            checkboxpas.TabIndex = 125;
            checkboxpas.Text = "Pasif mi?";
            checkboxpas.UseVisualStyleBackColor = true;
            // 
            // deletedlbl
            // 
            deletedlbl.AutoSize = true;
            deletedlbl.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            deletedlbl.ForeColor = SystemColors.Control;
            deletedlbl.Location = new Point(742, 303);
            deletedlbl.Name = "deletedlbl";
            deletedlbl.Size = new Size(120, 29);
            deletedlbl.TabIndex = 124;
            deletedlbl.Text = "Silindi mi?";
            deletedlbl.UseVisualStyleBackColor = true;
            // 
            // maliyTxtBox
            // 
            maliyTxtBox.Location = new Point(64, 217);
            maliyTxtBox.Name = "maliyTxtBox";
            maliyTxtBox.Size = new Size(167, 27);
            maliyTxtBox.TabIndex = 119;
            // 
            // malNotxtBox
            // 
            malNotxtBox.Location = new Point(464, 117);
            malNotxtBox.Name = "malNotxtBox";
            malNotxtBox.Size = new Size(181, 27);
            malNotxtBox.TabIndex = 118;
            // 
            // dillbl
            // 
            dillbl.AutoSize = true;
            dillbl.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            dillbl.ForeColor = Color.GhostWhite;
            dillbl.Location = new Point(380, 192);
            dillbl.Name = "dillbl";
            dillbl.Size = new Size(80, 23);
            dillbl.TabIndex = 117;
            dillbl.Text = "Dil Kodu";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.GhostWhite;
            label6.Location = new Point(259, 91);
            label6.Name = "label6";
            label6.Size = new Size(175, 23);
            label6.TabIndex = 116;
            label6.Text = "Maliyet Merkezi Tipi";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.GhostWhite;
            label5.Location = new Point(763, 168);
            label5.Name = "label5";
            label5.Size = new Size(125, 23);
            label5.TabIndex = 115;
            label5.Text = "Geçerlilik Bitiş";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.GhostWhite;
            label4.Location = new Point(737, 91);
            label4.Name = "label4";
            label4.Size = new Size(165, 23);
            label4.TabIndex = 114;
            label4.Text = "Geçerlilik Başlangıç";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.GhostWhite;
            label3.Location = new Point(464, 91);
            label3.Name = "label3";
            label3.Size = new Size(186, 23);
            label3.TabIndex = 113;
            label3.Text = "Maliyet Merkezi Kodu";
            // 
            // maliylbl
            // 
            maliylbl.AutoSize = true;
            maliylbl.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            maliylbl.ForeColor = Color.GhostWhite;
            maliylbl.Location = new Point(44, 192);
            maliylbl.Name = "maliylbl";
            maliylbl.Size = new Size(216, 23);
            maliylbl.TabIndex = 112;
            maliylbl.Text = "Ana Maliyet Merkezi Tipi ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.GhostWhite;
            label1.Location = new Point(91, 91);
            label1.Name = "label1";
            label1.Size = new Size(103, 23);
            label1.TabIndex = 111;
            label1.Text = "Firma Kodu";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(464, 398);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(146, 53);
            btnSave.TabIndex = 132;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // maliKodTxtBox
            // 
            maliKodTxtBox.Location = new Point(64, 305);
            maliKodTxtBox.Name = "maliKodTxtBox";
            maliKodTxtBox.Size = new Size(167, 27);
            maliKodTxtBox.TabIndex = 134;
            // 
            // malikodlbl
            // 
            malikodlbl.AutoSize = true;
            malikodlbl.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            malikodlbl.ForeColor = Color.GhostWhite;
            malikodlbl.Location = new Point(44, 279);
            malikodlbl.Name = "malikodlbl";
            malikodlbl.Size = new Size(222, 23);
            malikodlbl.TabIndex = 135;
            malikodlbl.Text = "Ana Maliyet Merkezi Kodu";
            // 
            // MaliyetKartAnaEdit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(1041, 535);
            Controls.Add(malikodlbl);
            Controls.Add(maliKodTxtBox);
            Controls.Add(btnSave);
            Controls.Add(dateTimePickerBitis);
            Controls.Add(dateTimePickerBaslangic);
            Controls.Add(comboBoxMalMerTip);
            Controls.Add(dilCombo);
            Controls.Add(firmComboBox);
            Controls.Add(checkboxpas);
            Controls.Add(deletedlbl);
            Controls.Add(maliyTxtBox);
            Controls.Add(malNotxtBox);
            Controls.Add(dillbl);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(maliylbl);
            Controls.Add(label1);
            Name = "MaliyetKartAnaEdit";
            Text = "MaliyetKartAnaEdit";
            Load += MaliyetKartAnaEdit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dateTimePickerBitis;
        private DateTimePicker dateTimePickerBaslangic;
        private ComboBox comboBoxMalMerTip;
        private ComboBox dilCombo;
        private ComboBox firmComboBox;
        private CheckBox checkboxpas;
        private CheckBox deletedlbl;
        private TextBox maliyTxtBox;
        private TextBox malNotxtBox;
        private Label dillbl;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label maliylbl;
        private Label label1;
        private Button btnSave;
        private TextBox maliKodTxtBox;
        private Label malikodlbl;
    }
}