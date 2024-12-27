namespace TRTERPproject
{
    partial class prodTree
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
            prodTreeDataGridView = new DataGridView();
            prodDoctypeTextTextBox = new TextBox();
            prodDoctypeTextBox = new TextBox();
            label2 = new Label();
            firmCodeTextBox = new TextBox();
            label3 = new Label();
            label1 = new Label();
            prodDocispassiveBOX = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)prodTreeDataGridView).BeginInit();
            SuspendLayout();
            // 
            // btnDel
            // 
            btnDel.Location = new Point(482, 137);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(127, 29);
            btnDel.TabIndex = 11;
            btnDel.Text = "Sil";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(332, 137);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 29);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "Ekle";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(185, 137);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(127, 29);
            btnEdit.TabIndex = 13;
            btnEdit.Text = "Düzenle";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnGet
            // 
            btnGet.Location = new Point(34, 137);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(127, 29);
            btnGet.TabIndex = 14;
            btnGet.Text = "Getir";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // prodTreeDataGridView
            // 
            prodTreeDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            prodTreeDataGridView.Location = new Point(34, 224);
            prodTreeDataGridView.Name = "prodTreeDataGridView";
            prodTreeDataGridView.RowHeadersWidth = 51;
            prodTreeDataGridView.RowTemplate.Height = 29;
            prodTreeDataGridView.Size = new Size(718, 239);
            prodTreeDataGridView.TabIndex = 10;
            // 
            // prodDoctypeTextTextBox
            // 
            prodDoctypeTextTextBox.BackColor = SystemColors.ControlLightLight;
            prodDoctypeTextTextBox.Location = new Point(332, 70);
            prodDoctypeTextTextBox.Name = "prodDoctypeTextTextBox";
            prodDoctypeTextTextBox.Size = new Size(196, 27);
            prodDoctypeTextTextBox.TabIndex = 7;
            // 
            // prodDoctypeTextBox
            // 
            prodDoctypeTextBox.BackColor = SystemColors.ControlLightLight;
            prodDoctypeTextBox.Location = new Point(185, 70);
            prodDoctypeTextBox.Name = "prodDoctypeTextBox";
            prodDoctypeTextBox.Size = new Size(127, 27);
            prodDoctypeTextBox.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(185, 38);
            label2.Name = "label2";
            label2.Size = new Size(117, 20);
            label2.TabIndex = 4;
            label2.Text = "Ürün Ağacı Tipi";
            // 
            // firmCodeTextBox
            // 
            firmCodeTextBox.BackColor = SystemColors.ControlLightLight;
            firmCodeTextBox.Location = new Point(34, 70);
            firmCodeTextBox.Name = "firmCodeTextBox";
            firmCodeTextBox.Size = new Size(127, 27);
            firmCodeTextBox.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(332, 38);
            label3.Name = "label3";
            label3.Size = new Size(196, 20);
            label3.TabIndex = 5;
            label3.Text = "Ürün Ağacı Tipi Açıklaması";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(34, 38);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 6;
            label1.Text = "Firma Kodu";
            // 
            // prodDocispassiveBOX
            // 
            prodDocispassiveBOX.AutoSize = true;
            prodDocispassiveBOX.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            prodDocispassiveBOX.ForeColor = SystemColors.ControlLightLight;
            prodDocispassiveBOX.Location = new Point(567, 51);
            prodDocispassiveBOX.Name = "prodDocispassiveBOX";
            prodDocispassiveBOX.Size = new Size(109, 29);
            prodDocispassiveBOX.TabIndex = 16;
            prodDocispassiveBOX.Text = "Pasif mi?";
            prodDocispassiveBOX.UseVisualStyleBackColor = true;
            // 
            // prodTree
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(869, 489);
            Controls.Add(prodDocispassiveBOX);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnGet);
            Controls.Add(prodTreeDataGridView);
            Controls.Add(prodDoctypeTextTextBox);
            Controls.Add(prodDoctypeTextBox);
            Controls.Add(label2);
            Controls.Add(firmCodeTextBox);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "prodTree";
            Text = "prodTree";
            Load += prodTree_Load;
            ((System.ComponentModel.ISupportInitialize)prodTreeDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDel;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnGet;
        private DataGridView prodTreeDataGridView;
        private TextBox prodDoctypeTextTextBox;
        private TextBox prodDoctypeTextBox;
        private Label label2;
        private TextBox firmCodeTextBox;
        private Label label3;
        private Label label1;
        private CheckBox prodDocispassiveBOX;
    }
}