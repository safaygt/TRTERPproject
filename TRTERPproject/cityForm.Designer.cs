﻿namespace TRTERPproject
{
    partial class cityForm
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
            btnDel = new Button();
            btnAdd = new Button();
            btnEdit = new Button();
            btnGet = new Button();
            cityDataGridView = new DataGridView();
            cityNameTextBox = new TextBox();
            cityCodeTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            label4 = new Label();
            comboBoxFirmCode = new ComboBox();
            comboBoxCountryCode = new ComboBox();
            btnFiltreliGetir = new Button();
            ((System.ComponentModel.ISupportInitialize)cityDataGridView).BeginInit();
            SuspendLayout();
            // 
            // btnDel
            // 
            btnDel.Location = new Point(625, 132);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(127, 29);
            btnDel.TabIndex = 11;
            btnDel.Text = "Sil";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(475, 132);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 29);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "Ekle";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(328, 132);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(127, 29);
            btnEdit.TabIndex = 13;
            btnEdit.Text = "Düzenle";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnGet
            // 
            btnGet.Location = new Point(27, 132);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(127, 29);
            btnGet.TabIndex = 14;
            btnGet.Text = "Hepsini Getir";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // cityDataGridView
            // 
            cityDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            cityDataGridView.Location = new Point(12, 209);
            cityDataGridView.Name = "cityDataGridView";
            cityDataGridView.RowHeadersWidth = 51;
            cityDataGridView.RowTemplate.Height = 29;
            cityDataGridView.Size = new Size(915, 239);
            cityDataGridView.TabIndex = 10;
            // 
            // cityNameTextBox
            // 
            cityNameTextBox.Location = new Point(325, 81);
            cityNameTextBox.Name = "cityNameTextBox";
            cityNameTextBox.Size = new Size(127, 27);
            cityNameTextBox.TabIndex = 7;
            // 
            // cityCodeTextBox
            // 
            cityCodeTextBox.Location = new Point(178, 81);
            cityCodeTextBox.Name = "cityCodeTextBox";
            cityCodeTextBox.Size = new Size(127, 27);
            cityCodeTextBox.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(178, 39);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 4;
            label2.Text = "Şehir Kodu";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(325, 39);
            label3.Name = "label3";
            label3.Size = new Size(72, 20);
            label3.TabIndex = 5;
            label3.Text = "Şehir Adı";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(27, 39);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 6;
            label1.Text = "Firma Kodu";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(475, 39);
            label4.Name = "label4";
            label4.Size = new Size(81, 20);
            label4.TabIndex = 5;
            label4.Text = "Ülke Kodu";
            // 
            // comboBoxFirmCode
            // 
            comboBoxFirmCode.FormattingEnabled = true;
            comboBoxFirmCode.Location = new Point(27, 81);
            comboBoxFirmCode.Name = "comboBoxFirmCode";
            comboBoxFirmCode.Size = new Size(127, 28);
            comboBoxFirmCode.TabIndex = 15;
            // 
            // comboBoxCountryCode
            // 
            comboBoxCountryCode.FormattingEnabled = true;
            comboBoxCountryCode.Location = new Point(475, 80);
            comboBoxCountryCode.Name = "comboBoxCountryCode";
            comboBoxCountryCode.Size = new Size(127, 28);
            comboBoxCountryCode.TabIndex = 16;
            // 
            // btnFiltreliGetir
            // 
            btnFiltreliGetir.Location = new Point(178, 133);
            btnFiltreliGetir.Name = "btnFiltreliGetir";
            btnFiltreliGetir.Size = new Size(127, 29);
            btnFiltreliGetir.TabIndex = 17;
            btnFiltreliGetir.Text = "Getir";
            btnFiltreliGetir.UseVisualStyleBackColor = true;
            btnFiltreliGetir.Click += btnFiltreliGetir_Click;
            // 
            // cityForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(967, 474);
            Controls.Add(btnFiltreliGetir);
            Controls.Add(comboBoxCountryCode);
            Controls.Add(comboBoxFirmCode);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnGet);
            Controls.Add(cityDataGridView);
            Controls.Add(cityNameTextBox);
            Controls.Add(cityCodeTextBox);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "cityForm";
            Text = "Şehir Destek Tablosu";
            ((System.ComponentModel.ISupportInitialize)cityDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDel;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnGet;
        private DataGridView cityDataGridView;
        private TextBox cityNameTextBox;
        private TextBox cityCodeTextBox;
        private Label label2;
        private Label label3;
        private Label label1;
        private Label label4;
        private ComboBox comboBoxFirmCode;
        private ComboBox comboBoxCountryCode;
        private Button btnFiltreliGetir;
    }
}