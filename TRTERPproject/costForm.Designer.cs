namespace TRTERPproject
{
    partial class costForm
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
            isPassiveCheckBox = new CheckBox();
            btnDel = new Button();
            btnAdd = new Button();
            btnEdit = new Button();
            btnGet = new Button();
            costDataGridView = new DataGridView();
            costTypeStatementTextBox = new TextBox();
            costTypeTextBox = new TextBox();
            firmCodeTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)costDataGridView).BeginInit();
            SuspendLayout();
            // 
            // isPassiveCheckBox
            // 
            isPassiveCheckBox.AutoSize = true;
            isPassiveCheckBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            isPassiveCheckBox.ForeColor = SystemColors.ControlLightLight;
            isPassiveCheckBox.Location = new Point(631, 125);
            isPassiveCheckBox.Name = "isPassiveCheckBox";
            isPassiveCheckBox.Size = new Size(130, 24);
            isPassiveCheckBox.TabIndex = 38;
            isPassiveCheckBox.Text = "Ana Birim mi?";
            isPassiveCheckBox.UseVisualStyleBackColor = true;
            isPassiveCheckBox.CheckedChanged += isPassiveCheckBox_CheckedChanged;
            // 
            // btnDel
            // 
            btnDel.Location = new Point(469, 176);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(116, 29);
            btnDel.TabIndex = 34;
            btnDel.Text = "Sil";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(319, 176);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 29);
            btnAdd.TabIndex = 35;
            btnAdd.Text = "Ekle";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(172, 176);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(127, 29);
            btnEdit.TabIndex = 36;
            btnEdit.Text = "Düzenle";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnGet
            // 
            btnGet.Location = new Point(21, 176);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(127, 29);
            btnGet.TabIndex = 37;
            btnGet.Text = "Getir";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // costDataGridView
            // 
            costDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            costDataGridView.Location = new Point(12, 248);
            costDataGridView.Name = "costDataGridView";
            costDataGridView.RowHeadersWidth = 51;
            costDataGridView.RowTemplate.Height = 29;
            costDataGridView.Size = new Size(915, 239);
            costDataGridView.TabIndex = 33;
            costDataGridView.CellContentClick += CountryDataGridView_CellContentClick;
            // 
            // costTypeStatementTextBox
            // 
            costTypeStatementTextBox.Location = new Point(356, 125);
            costTypeStatementTextBox.Name = "costTypeStatementTextBox";
            costTypeStatementTextBox.Size = new Size(229, 27);
            costTypeStatementTextBox.TabIndex = 30;
            costTypeStatementTextBox.TextChanged += costTypeStatementTextBox_TextChanged;
            // 
            // costTypeTextBox
            // 
            costTypeTextBox.Location = new Point(172, 125);
            costTypeTextBox.Name = "costTypeTextBox";
            costTypeTextBox.Size = new Size(150, 27);
            costTypeTextBox.TabIndex = 31;
            costTypeTextBox.TextChanged += costTypeTextBox_TextChanged;
            // 
            // firmCodeTextBox
            // 
            firmCodeTextBox.Location = new Point(21, 125);
            firmCodeTextBox.Name = "firmCodeTextBox";
            firmCodeTextBox.Size = new Size(127, 27);
            firmCodeTextBox.TabIndex = 32;
            firmCodeTextBox.TextChanged += firmCodeTextBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(172, 93);
            label2.Name = "label2";
            label2.Size = new Size(150, 20);
            label2.TabIndex = 27;
            label2.Text = "Maliyet Merkezi Tipi";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(356, 93);
            label3.Name = "label3";
            label3.Size = new Size(229, 20);
            label3.TabIndex = 28;
            label3.Text = "Maliyet Merkezi Tipi Açıklaması";
            label3.Click += label3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(21, 93);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 29;
            label1.Text = "Firma Kodu";
            label1.Click += label1_Click;
            // 
            // costForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(941, 540);
            Controls.Add(isPassiveCheckBox);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnGet);
            Controls.Add(costDataGridView);
            Controls.Add(costTypeStatementTextBox);
            Controls.Add(costTypeTextBox);
            Controls.Add(firmCodeTextBox);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "costForm";
            Text = "Maliyet Merkezi";
            ((System.ComponentModel.ISupportInitialize)costDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox isPassiveCheckBox;
        private Button btnDel;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnGet;
        private DataGridView costDataGridView;
        private TextBox costTypeStatementTextBox;
        private TextBox costTypeTextBox;
        private TextBox firmCodeTextBox;
        private Label label2;
        private Label label3;
        private Label label1;
    }
}