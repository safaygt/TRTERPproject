namespace TRTERPproject
{
    partial class MatForm
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
            MatDataGridWiew = new DataGridView();
            MatNameBox = new TextBox();
            MatCodeBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            ispassiveBOX = new CheckBox();
            btnFiltreliGetir = new Button();
            comboBoxFirmCode = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)MatDataGridWiew).BeginInit();
            SuspendLayout();
            // 
            // btnDel
            // 
            btnDel.Location = new Point(643, 159);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(127, 29);
            btnDel.TabIndex = 11;
            btnDel.Text = "Sil";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(493, 159);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 29);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "Ekle";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(346, 159);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(127, 29);
            btnEdit.TabIndex = 13;
            btnEdit.Text = "Düzenle";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnGet
            // 
            btnGet.Location = new Point(41, 159);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(127, 29);
            btnGet.TabIndex = 14;
            btnGet.Text = "Hepsini Getir";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // MatDataGridWiew
            // 
            MatDataGridWiew.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MatDataGridWiew.Location = new Point(31, 245);
            MatDataGridWiew.Name = "MatDataGridWiew";
            MatDataGridWiew.RowHeadersWidth = 51;
            MatDataGridWiew.RowTemplate.Height = 29;
            MatDataGridWiew.Size = new Size(915, 239);
            MatDataGridWiew.TabIndex = 10;
            // 
            // MatNameBox
            // 
            MatNameBox.Location = new Point(339, 76);
            MatNameBox.Name = "MatNameBox";
            MatNameBox.Size = new Size(127, 27);
            MatNameBox.TabIndex = 7;
            // 
            // MatCodeBox
            // 
            MatCodeBox.Location = new Point(192, 76);
            MatCodeBox.Name = "MatCodeBox";
            MatCodeBox.Size = new Size(127, 27);
            MatCodeBox.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(192, 44);
            label2.Name = "label2";
            label2.Size = new Size(102, 20);
            label2.TabIndex = 4;
            label2.Text = "Malzeme Tipi";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(339, 44);
            label3.Name = "label3";
            label3.Size = new Size(100, 20);
            label3.TabIndex = 5;
            label3.Text = "Malzeme Adı";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(41, 44);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 6;
            label1.Text = "Firma Kodu";
            // 
            // ispassiveBOX
            // 
            ispassiveBOX.AutoSize = true;
            ispassiveBOX.Cursor = Cursors.No;
            ispassiveBOX.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            ispassiveBOX.ForeColor = SystemColors.ControlLightLight;
            ispassiveBOX.Location = new Point(507, 74);
            ispassiveBOX.Name = "ispassiveBOX";
            ispassiveBOX.Size = new Size(109, 29);
            ispassiveBOX.TabIndex = 15;
            ispassiveBOX.Text = "Pasif mi?";
            ispassiveBOX.UseVisualStyleBackColor = true;
            // 
            // btnFiltreliGetir
            // 
            btnFiltreliGetir.Location = new Point(192, 159);
            btnFiltreliGetir.Name = "btnFiltreliGetir";
            btnFiltreliGetir.Size = new Size(127, 29);
            btnFiltreliGetir.TabIndex = 16;
            btnFiltreliGetir.Text = "Getir";
            btnFiltreliGetir.UseVisualStyleBackColor = true;
            btnFiltreliGetir.Click += btnFiltreliGetir_Click;
            // 
            // comboBoxFirmCode
            // 
            comboBoxFirmCode.FormattingEnabled = true;
            comboBoxFirmCode.Location = new Point(41, 76);
            comboBoxFirmCode.Name = "comboBoxFirmCode";
            comboBoxFirmCode.Size = new Size(127, 28);
            comboBoxFirmCode.TabIndex = 17;
            // 
            // MatForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(981, 520);
            Controls.Add(comboBoxFirmCode);
            Controls.Add(btnFiltreliGetir);
            Controls.Add(ispassiveBOX);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnGet);
            Controls.Add(MatDataGridWiew);
            Controls.Add(MatNameBox);
            Controls.Add(MatCodeBox);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "MatForm";
            Text = "Malzeme Destek Tablosu";
            ((System.ComponentModel.ISupportInitialize)MatDataGridWiew).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDel;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnGet;
        private DataGridView MatDataGridWiew;
        private TextBox MatNameBox;
        private TextBox MatCodeBox;
        private Label label2;
        private Label label3;
        private Label label1;
        private CheckBox ispassiveBOX;
        private Button btnFiltreliGetir;
        private ComboBox comboBoxFirmCode;
    }
}