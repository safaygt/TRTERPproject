namespace TRTERPproject
{
    partial class rotForm
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
            isPassiveBOX = new CheckBox();
            btnDel = new Button();
            btnAdd = new Button();
            btnEdit = new Button();
            btnGet = new Button();
            RotDataGridWiew = new DataGridView();
            rotTypeStatementTextBox = new TextBox();
            rotTypeTextBox = new TextBox();
            label2 = new Label();
            firmCodeTextBox = new TextBox();
            label3 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)RotDataGridWiew).BeginInit();
            SuspendLayout();
            // 
            // isPassiveBOX
            // 
            isPassiveBOX.AutoSize = true;
            isPassiveBOX.Cursor = Cursors.No;
            isPassiveBOX.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            isPassiveBOX.ForeColor = SystemColors.ControlLightLight;
            isPassiveBOX.Location = new Point(543, 57);
            isPassiveBOX.Name = "isPassiveBOX";
            isPassiveBOX.Size = new Size(109, 29);
            isPassiveBOX.TabIndex = 27;
            isPassiveBOX.Text = "Pasif mi?";
            isPassiveBOX.UseVisualStyleBackColor = true;
            // 
            // btnDel
            // 
            btnDel.Location = new Point(496, 143);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(127, 29);
            btnDel.TabIndex = 23;
            btnDel.Text = "Sil";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(346, 143);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 29);
            btnAdd.TabIndex = 24;
            btnAdd.Text = "Ekle";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(199, 143);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(127, 29);
            btnEdit.TabIndex = 25;
            btnEdit.Text = "Düzenle";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnGet
            // 
            btnGet.Location = new Point(48, 143);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(127, 29);
            btnGet.TabIndex = 26;
            btnGet.Text = "Getir";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // RotDataGridWiew
            // 
            RotDataGridWiew.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RotDataGridWiew.Location = new Point(22, 223);
            RotDataGridWiew.Name = "RotDataGridWiew";
            RotDataGridWiew.RowHeadersWidth = 51;
            RotDataGridWiew.RowTemplate.Height = 29;
            RotDataGridWiew.Size = new Size(915, 239);
            RotDataGridWiew.TabIndex = 22;
            // 
            // rotTypeStatementTextBox
            // 
            rotTypeStatementTextBox.Location = new Point(346, 59);
            rotTypeStatementTextBox.Name = "rotTypeStatementTextBox";
            rotTypeStatementTextBox.Size = new Size(151, 27);
            rotTypeStatementTextBox.TabIndex = 19;
            // 
            // rotTypeTextBox
            // 
            rotTypeTextBox.Location = new Point(199, 59);
            rotTypeTextBox.Name = "rotTypeTextBox";
            rotTypeTextBox.Size = new Size(127, 27);
            rotTypeTextBox.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(199, 27);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 16;
            label2.Text = "Rota Tipi";
            // 
            // firmCodeTextBox
            // 
            firmCodeTextBox.Location = new Point(48, 59);
            firmCodeTextBox.Name = "firmCodeTextBox";
            firmCodeTextBox.Size = new Size(127, 27);
            firmCodeTextBox.TabIndex = 21;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(346, 27);
            label3.Name = "label3";
            label3.Size = new Size(151, 20);
            label3.TabIndex = 17;
            label3.Text = "Rota Tipi Açıklaması";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(48, 27);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 18;
            label1.Text = "Firma Kodu";
            // 
            // rotForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(969, 500);
            Controls.Add(isPassiveBOX);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnGet);
            Controls.Add(RotDataGridWiew);
            Controls.Add(rotTypeStatementTextBox);
            Controls.Add(rotTypeTextBox);
            Controls.Add(label2);
            Controls.Add(firmCodeTextBox);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "rotForm";
            Text = "Rota Destek Tablosu";
            ((System.ComponentModel.ISupportInitialize)RotDataGridWiew).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox isPassiveBOX;
        private Button btnDel;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnGet;
        private DataGridView RotDataGridWiew;
        private TextBox rotTypeStatementTextBox;
        private TextBox rotTypeTextBox;
        private Label label2;
        private TextBox firmCodeTextBox;
        private Label label3;
        private Label label1;
    }
}