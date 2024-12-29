namespace TRTERPproject
{
    partial class businessForm
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
            businessDataGridView = new DataGridView();
            businessTypeStatementTextBox = new TextBox();
            businessTypeTextBox = new TextBox();
            firmCodeTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)businessDataGridView).BeginInit();
            SuspendLayout();
            // 
            // isPassiveCheckBox
            // 
            isPassiveCheckBox.AutoSize = true;
            isPassiveCheckBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            isPassiveCheckBox.ForeColor = SystemColors.ControlLightLight;
            isPassiveCheckBox.Location = new Point(643, 74);
            isPassiveCheckBox.Name = "isPassiveCheckBox";
            isPassiveCheckBox.Size = new Size(94, 24);
            isPassiveCheckBox.TabIndex = 50;
            isPassiveCheckBox.Text = "Pasif mi?";
            isPassiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // btnDel
            // 
            btnDel.Location = new Point(481, 125);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(116, 29);
            btnDel.TabIndex = 46;
            btnDel.Text = "Sil";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(331, 125);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 29);
            btnAdd.TabIndex = 47;
            btnAdd.Text = "Ekle";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(184, 125);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(127, 29);
            btnEdit.TabIndex = 48;
            btnEdit.Text = "Düzenle";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnGet
            // 
            btnGet.Location = new Point(33, 125);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(127, 29);
            btnGet.TabIndex = 49;
            btnGet.Text = "Getir";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // businessDataGridView
            // 
            businessDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            businessDataGridView.Location = new Point(24, 197);
            businessDataGridView.Name = "businessDataGridView";
            businessDataGridView.RowHeadersWidth = 51;
            businessDataGridView.RowTemplate.Height = 29;
            businessDataGridView.Size = new Size(915, 239);
            businessDataGridView.TabIndex = 45;
            // 
            // businessTypeStatementTextBox
            // 
            businessTypeStatementTextBox.Location = new Point(368, 74);
            businessTypeStatementTextBox.Name = "businessTypeStatementTextBox";
            businessTypeStatementTextBox.Size = new Size(229, 27);
            businessTypeStatementTextBox.TabIndex = 42;
            // 
            // businessTypeTextBox
            // 
            businessTypeTextBox.Location = new Point(184, 74);
            businessTypeTextBox.Name = "businessTypeTextBox";
            businessTypeTextBox.Size = new Size(150, 27);
            businessTypeTextBox.TabIndex = 43;
            // 
            // firmCodeTextBox
            // 
            firmCodeTextBox.Location = new Point(33, 74);
            firmCodeTextBox.Name = "firmCodeTextBox";
            firmCodeTextBox.Size = new Size(127, 27);
            firmCodeTextBox.TabIndex = 44;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(184, 42);
            label2.Name = "label2";
            label2.Size = new Size(110, 20);
            label2.TabIndex = 39;
            label2.Text = "İş Merkezi Tipi";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(368, 42);
            label3.Name = "label3";
            label3.Size = new Size(189, 20);
            label3.TabIndex = 40;
            label3.Text = "İş Merkezi Tipi Açıklaması";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(33, 42);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 41;
            label1.Text = "Firma Kodu";
            // 
            // businessForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(1036, 533);
            Controls.Add(isPassiveCheckBox);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnGet);
            Controls.Add(businessDataGridView);
            Controls.Add(businessTypeStatementTextBox);
            Controls.Add(businessTypeTextBox);
            Controls.Add(firmCodeTextBox);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "businessForm";
            Text = "İş Merkezi Destek Tablosu";
            ((System.ComponentModel.ISupportInitialize)businessDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox isPassiveCheckBox;
        private Button btnDel;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnGet;
        private DataGridView businessDataGridView;
        private TextBox businessTypeStatementTextBox;
        private TextBox businessTypeTextBox;
        private TextBox firmCodeTextBox;
        private Label label2;
        private Label label3;
        private Label label1;
    }
}