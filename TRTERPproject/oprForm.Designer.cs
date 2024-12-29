namespace TRTERPproject
{
    partial class oprForm
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
            oprDataGridView = new DataGridView();
            oprTypeDesTextBox = new TextBox();
            oprTypeTextBox = new TextBox();
            oprTypeLabel = new Label();
            oprTypeDesLabel = new Label();
            oprFrimLabel = new Label();
            oprPascheckbox = new CheckBox();
            comboBoxFirmCode = new ComboBox();
            btnFiltreliGetir = new Button();
            ((System.ComponentModel.ISupportInitialize)oprDataGridView).BeginInit();
            SuspendLayout();
            // 
            // btnDel
            // 
            btnDel.Location = new Point(629, 166);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(127, 29);
            btnDel.TabIndex = 36;
            btnDel.Text = "Sil";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(479, 166);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 29);
            btnAdd.TabIndex = 37;
            btnAdd.Text = "Ekle";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(332, 166);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(127, 29);
            btnEdit.TabIndex = 38;
            btnEdit.Text = "Düzenle";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnGet
            // 
            btnGet.Location = new Point(28, 166);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(127, 29);
            btnGet.TabIndex = 39;
            btnGet.Text = "Hepsini Getir";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // oprDataGridView
            // 
            oprDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            oprDataGridView.Location = new Point(22, 238);
            oprDataGridView.Name = "oprDataGridView";
            oprDataGridView.RowHeadersWidth = 51;
            oprDataGridView.RowTemplate.Height = 29;
            oprDataGridView.Size = new Size(915, 239);
            oprDataGridView.TabIndex = 35;
            // 
            // oprTypeDesTextBox
            // 
            oprTypeDesTextBox.Location = new Point(326, 101);
            oprTypeDesTextBox.Name = "oprTypeDesTextBox";
            oprTypeDesTextBox.Size = new Size(261, 27);
            oprTypeDesTextBox.TabIndex = 32;
            // 
            // oprTypeTextBox
            // 
            oprTypeTextBox.Location = new Point(179, 101);
            oprTypeTextBox.Name = "oprTypeTextBox";
            oprTypeTextBox.Size = new Size(127, 27);
            oprTypeTextBox.TabIndex = 33;
            // 
            // oprTypeLabel
            // 
            oprTypeLabel.AutoSize = true;
            oprTypeLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            oprTypeLabel.ForeColor = SystemColors.ControlLightLight;
            oprTypeLabel.Location = new Point(179, 67);
            oprTypeLabel.Name = "oprTypeLabel";
            oprTypeLabel.Size = new Size(114, 20);
            oprTypeLabel.TabIndex = 28;
            oprTypeLabel.Text = "Operasyon Tipi";
            // 
            // oprTypeDesLabel
            // 
            oprTypeDesLabel.AutoSize = true;
            oprTypeDesLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            oprTypeDesLabel.ForeColor = SystemColors.ControlLightLight;
            oprTypeDesLabel.Location = new Point(326, 67);
            oprTypeDesLabel.Name = "oprTypeDesLabel";
            oprTypeDesLabel.Size = new Size(193, 20);
            oprTypeDesLabel.TabIndex = 29;
            oprTypeDesLabel.Text = "Operasyon Tipi Açıklaması";
            oprTypeDesLabel.Click += oprTypeDesLabel_Click;
            // 
            // oprFrimLabel
            // 
            oprFrimLabel.AutoSize = true;
            oprFrimLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            oprFrimLabel.ForeColor = SystemColors.ControlLightLight;
            oprFrimLabel.Location = new Point(28, 67);
            oprFrimLabel.Name = "oprFrimLabel";
            oprFrimLabel.Size = new Size(90, 20);
            oprFrimLabel.TabIndex = 30;
            oprFrimLabel.Text = "Firma Kodu";
            // 
            // oprPascheckbox
            // 
            oprPascheckbox.AutoSize = true;
            oprPascheckbox.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            oprPascheckbox.ForeColor = SystemColors.Control;
            oprPascheckbox.Location = new Point(656, 101);
            oprPascheckbox.Name = "oprPascheckbox";
            oprPascheckbox.Size = new Size(109, 29);
            oprPascheckbox.TabIndex = 67;
            oprPascheckbox.Text = "Pasif mi?";
            oprPascheckbox.UseVisualStyleBackColor = true;
            // 
            // comboBoxFirmCode
            // 
            comboBoxFirmCode.FormattingEnabled = true;
            comboBoxFirmCode.Location = new Point(28, 102);
            comboBoxFirmCode.Name = "comboBoxFirmCode";
            comboBoxFirmCode.Size = new Size(127, 28);
            comboBoxFirmCode.TabIndex = 68;
            // 
            // btnFiltreliGetir
            // 
            btnFiltreliGetir.Location = new Point(179, 166);
            btnFiltreliGetir.Name = "btnFiltreliGetir";
            btnFiltreliGetir.Size = new Size(127, 29);
            btnFiltreliGetir.TabIndex = 69;
            btnFiltreliGetir.Text = "Getir";
            btnFiltreliGetir.UseVisualStyleBackColor = true;
            btnFiltreliGetir.Click += btnFiltreliGetir_Click;
            // 
            // oprForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(1003, 555);
            Controls.Add(btnFiltreliGetir);
            Controls.Add(comboBoxFirmCode);
            Controls.Add(oprPascheckbox);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnGet);
            Controls.Add(oprDataGridView);
            Controls.Add(oprTypeDesTextBox);
            Controls.Add(oprTypeTextBox);
            Controls.Add(oprTypeLabel);
            Controls.Add(oprTypeDesLabel);
            Controls.Add(oprFrimLabel);
            Name = "oprForm";
            Text = "Operasyon Destek Tablosu";
            ((System.ComponentModel.ISupportInitialize)oprDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnDel;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnGet;
        private DataGridView oprDataGridView;
        private TextBox oprTypeDesTextBox;
        private TextBox oprTypeTextBox;
        private Label oprTypeLabel;
        private Label oprTypeDesLabel;
        private Label oprFrimLabel;
        private CheckBox oprPascheckbox;
        private ComboBox comboBoxFirmCode;
        private Button btnFiltreliGetir;
    }
}