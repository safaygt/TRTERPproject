namespace TRTERPproject
{
    partial class lanForm
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
            CountryDataGridView = new DataGridView();
            lanTextBox = new TextBox();
            lanCodeTextBox = new TextBox();
            firmCodeTextBox = new TextBox();
            label3 = new Label();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)CountryDataGridView).BeginInit();
            SuspendLayout();
            // 
            // btnDel
            // 
            btnDel.Location = new Point(519, 135);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(127, 29);
            btnDel.TabIndex = 11;
            btnDel.Text = "Sil";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(369, 135);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 29);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "Ekle";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(222, 135);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(127, 29);
            btnEdit.TabIndex = 13;
            btnEdit.Text = "Düzenle";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnGet
            // 
            btnGet.Location = new Point(71, 135);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(127, 29);
            btnGet.TabIndex = 14;
            btnGet.Text = "Getir";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // CountryDataGridView
            // 
            CountryDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            CountryDataGridView.Location = new Point(26, 221);
            CountryDataGridView.Name = "CountryDataGridView";
            CountryDataGridView.RowHeadersWidth = 51;
            CountryDataGridView.RowTemplate.Height = 29;
            CountryDataGridView.Size = new Size(915, 239);
            CountryDataGridView.TabIndex = 10;
            CountryDataGridView.CellContentClick += CountryDataGridView_CellContentClick;
            // 
            // lanTextBox
            // 
            lanTextBox.Location = new Point(369, 69);
            lanTextBox.Name = "lanTextBox";
            lanTextBox.Size = new Size(127, 27);
            lanTextBox.TabIndex = 7;
            lanTextBox.TextChanged += lanTextBox_TextChanged;
            // 
            // lanCodeTextBox
            // 
            lanCodeTextBox.Location = new Point(222, 69);
            lanCodeTextBox.Name = "lanCodeTextBox";
            lanCodeTextBox.Size = new Size(127, 27);
            lanCodeTextBox.TabIndex = 8;
            lanCodeTextBox.TextChanged += lanCodeTextBox_TextChanged;
            // 
            // firmCodeTextBox
            // 
            firmCodeTextBox.Location = new Point(71, 69);
            firmCodeTextBox.Name = "firmCodeTextBox";
            firmCodeTextBox.Size = new Size(127, 27);
            firmCodeTextBox.TabIndex = 9;
            firmCodeTextBox.TextChanged += firmCodeTextBox_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(369, 37);
            label3.Name = "label3";
            label3.Size = new Size(28, 20);
            label3.TabIndex = 5;
            label3.Text = "Dil";
            label3.Click += label3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(71, 37);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 6;
            label1.Text = "Firma Kodu";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(222, 37);
            label2.Name = "label2";
            label2.Size = new Size(69, 20);
            label2.TabIndex = 5;
            label2.Text = "Dil Kodu";
            label2.Click += label2_Click;
            // 
            // lanForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(1016, 508);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnGet);
            Controls.Add(CountryDataGridView);
            Controls.Add(lanTextBox);
            Controls.Add(lanCodeTextBox);
            Controls.Add(firmCodeTextBox);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "lanForm";
            Text = "lanForm";
            ((System.ComponentModel.ISupportInitialize)CountryDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDel;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnGet;
        private DataGridView CountryDataGridView;
        private TextBox lanTextBox;
        private TextBox lanCodeTextBox;
        private TextBox firmCodeTextBox;
        private Label label3;
        private Label label1;
        private Label label2;
    }
}