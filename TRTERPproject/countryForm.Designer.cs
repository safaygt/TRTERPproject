namespace TRTERPproject
{
    partial class countryForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            label1 = new Label();
            firmCodeTextBox = new TextBox();
            countryCodeTextBox = new TextBox();
            label2 = new Label();
            countryNameTextBox = new TextBox();
            label3 = new Label();
            btnGet = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            btnDel = new Button();
            countryDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)countryDataGridView).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(36, 38);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 0;
            label1.Text = "Firma Kodu";
            // 
            // firmCodeTextBox
            // 
            firmCodeTextBox.Location = new Point(36, 70);
            firmCodeTextBox.Name = "firmCodeTextBox";
            firmCodeTextBox.Size = new Size(127, 27);
            firmCodeTextBox.TabIndex = 1;
            // 
            // countryCodeTextBox
            // 
            countryCodeTextBox.Location = new Point(187, 70);
            countryCodeTextBox.Name = "countryCodeTextBox";
            countryCodeTextBox.Size = new Size(127, 27);
            countryCodeTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(187, 38);
            label2.Name = "label2";
            label2.Size = new Size(81, 20);
            label2.TabIndex = 0;
            label2.Text = "Ülke Kodu";
            // 
            // countryNameTextBox
            // 
            countryNameTextBox.Location = new Point(334, 70);
            countryNameTextBox.Name = "countryNameTextBox";
            countryNameTextBox.Size = new Size(127, 27);
            countryNameTextBox.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(334, 38);
            label3.Name = "label3";
            label3.Size = new Size(68, 20);
            label3.TabIndex = 0;
            label3.Text = "Ülke Adı";
            // 
            // btnGet
            // 
            btnGet.ForeColor = SystemColors.ActiveCaptionText;
            btnGet.Location = new Point(36, 154);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(127, 29);
            btnGet.TabIndex = 3;
            btnGet.Text = "Getir";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // btnEdit
            // 
            btnEdit.ForeColor = SystemColors.ActiveCaptionText;
            btnEdit.Location = new Point(187, 154);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(127, 29);
            btnEdit.TabIndex = 3;
            btnEdit.Text = "Düzenle";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.ForeColor = SystemColors.ActiveCaptionText;
            btnAdd.Location = new Point(334, 154);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 29);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Ekle";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDel
            // 
            btnDel.ForeColor = SystemColors.ActiveCaptionText;
            btnDel.Location = new Point(484, 154);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(127, 29);
            btnDel.TabIndex = 3;
            btnDel.Text = "Sil";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // countryDataGridView
            // 
            countryDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            countryDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            countryDataGridView.Location = new Point(36, 246);
            countryDataGridView.Name = "countryDataGridView";
            countryDataGridView.RowHeadersWidth = 51;
            countryDataGridView.RowTemplate.Height = 29;
            countryDataGridView.Size = new Size(914, 272);
            countryDataGridView.TabIndex = 11;
            // 
            // countryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(981, 530);
            Controls.Add(countryDataGridView);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnGet);
            Controls.Add(countryNameTextBox);
            Controls.Add(countryCodeTextBox);
            Controls.Add(label2);
            Controls.Add(firmCodeTextBox);
            Controls.Add(label3);
            Controls.Add(label1);
            ForeColor = SystemColors.ControlLightLight;
            Name = "countryForm";
            Text = "Country Form";
            Load += countryForm_Load;
            ((System.ComponentModel.ISupportInitialize)countryDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox firmCodeTextBox;
        private TextBox countryCodeTextBox;
        private Label label2;
        private TextBox countryNameTextBox;
        private Label label3;
        private Button btnGet;
        private Button btnEdit;
        private Button btnAdd;
        private Button btnDel;
        private DataGridView countryDataGridView;
    }
}