namespace TRTERPproject
{
    partial class firmForm
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
            address1TextBox = new TextBox();
            firmNameTextBox = new TextBox();
            firmCodeTextBox = new TextBox();
            label3 = new Label();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            address2TextBox = new TextBox();
            label5 = new Label();
            cityCodeTextBox = new TextBox();
            label6 = new Label();
            countryCodeTextBox = new TextBox();
            firmFormDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)firmFormDataGridView).BeginInit();
            SuspendLayout();
            // 
            // btnDel
            // 
            btnDel.ForeColor = SystemColors.ActiveCaptionText;
            btnDel.Location = new Point(477, 223);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(127, 29);
            btnDel.TabIndex = 22;
            btnDel.Text = "Sil";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // btnAdd
            // 
            btnAdd.ForeColor = SystemColors.ActiveCaptionText;
            btnAdd.Location = new Point(335, 223);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 29);
            btnAdd.TabIndex = 23;
            btnAdd.Text = "Ekle";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.ForeColor = SystemColors.ActiveCaptionText;
            btnEdit.Location = new Point(188, 223);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(127, 29);
            btnEdit.TabIndex = 24;
            btnEdit.Text = "Düzenle";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnGet
            // 
            btnGet.ForeColor = SystemColors.ActiveCaptionText;
            btnGet.Location = new Point(37, 223);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(127, 29);
            btnGet.TabIndex = 25;
            btnGet.Text = "Getir";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // address1TextBox
            // 
            address1TextBox.Location = new Point(335, 48);
            address1TextBox.Name = "address1TextBox";
            address1TextBox.Size = new Size(127, 27);
            address1TextBox.TabIndex = 18;
            // 
            // firmNameTextBox
            // 
            firmNameTextBox.Location = new Point(188, 48);
            firmNameTextBox.Name = "firmNameTextBox";
            firmNameTextBox.Size = new Size(127, 27);
            firmNameTextBox.TabIndex = 19;
            // 
            // firmCodeTextBox
            // 
            firmCodeTextBox.Location = new Point(37, 48);
            firmCodeTextBox.Name = "firmCodeTextBox";
            firmCodeTextBox.Size = new Size(127, 27);
            firmCodeTextBox.TabIndex = 20;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(335, 16);
            label3.Name = "label3";
            label3.Size = new Size(63, 20);
            label3.TabIndex = 16;
            label3.Text = "Adres 1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(37, 16);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 17;
            label1.Text = "Firma Kodu";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(188, 16);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 16;
            label2.Text = "Firma Adı";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(37, 100);
            label4.Name = "label4";
            label4.Size = new Size(63, 20);
            label4.TabIndex = 16;
            label4.Text = "Adres 2";
            // 
            // address2TextBox
            // 
            address2TextBox.Location = new Point(37, 138);
            address2TextBox.Name = "address2TextBox";
            address2TextBox.Size = new Size(127, 27);
            address2TextBox.TabIndex = 18;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ControlLightLight;
            label5.Location = new Point(188, 100);
            label5.Name = "label5";
            label5.Size = new Size(85, 20);
            label5.TabIndex = 16;
            label5.Text = "Şehir Kodu";
            // 
            // cityCodeTextBox
            // 
            cityCodeTextBox.Location = new Point(188, 138);
            cityCodeTextBox.Name = "cityCodeTextBox";
            cityCodeTextBox.Size = new Size(127, 27);
            cityCodeTextBox.TabIndex = 18;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ControlLightLight;
            label6.Location = new Point(335, 100);
            label6.Name = "label6";
            label6.Size = new Size(81, 20);
            label6.TabIndex = 16;
            label6.Text = "Ülke Kodu";
            // 
            // countryCodeTextBox
            // 
            countryCodeTextBox.Location = new Point(335, 138);
            countryCodeTextBox.Name = "countryCodeTextBox";
            countryCodeTextBox.Size = new Size(127, 27);
            countryCodeTextBox.TabIndex = 18;
            // 
            // firmFormDataGridView
            // 
            firmFormDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            firmFormDataGridView.Location = new Point(25, 304);
            firmFormDataGridView.Name = "firmFormDataGridView";
            firmFormDataGridView.RowHeadersWidth = 51;
            firmFormDataGridView.RowTemplate.Height = 29;
            firmFormDataGridView.Size = new Size(962, 272);
            firmFormDataGridView.TabIndex = 26;
            // 
            // firmForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(1031, 588);
            Controls.Add(firmFormDataGridView);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnGet);
            Controls.Add(countryCodeTextBox);
            Controls.Add(cityCodeTextBox);
            Controls.Add(address2TextBox);
            Controls.Add(address1TextBox);
            Controls.Add(firmNameTextBox);
            Controls.Add(label6);
            Controls.Add(firmCodeTextBox);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            ForeColor = SystemColors.ControlLightLight;
            Name = "firmForm";
            Text = "firmForm";
            Load += firmForm_Load;
            ((System.ComponentModel.ISupportInitialize)firmFormDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDel;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnGet;
        private TextBox address1TextBox;
        private TextBox firmNameTextBox;
        private TextBox firmCodeTextBox;
        private Label label3;
        private Label label1;
        private Label label2;
        private Label label4;
        private TextBox address2TextBox;
        private Label label5;
        private TextBox cityCodeTextBox;
        private Label label6;
        private TextBox countryCodeTextBox;
        private DataGridView firmFormDataGridView;
    }
}