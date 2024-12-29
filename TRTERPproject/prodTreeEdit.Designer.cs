namespace TRTERPproject
{
    partial class prodTreeEdit
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
            prodDocispassiveBOX = new CheckBox();
            prodDoctypeTextTextBox = new TextBox();
            prodDoctypeTextBox = new TextBox();
            label2 = new Label();
            firmCodeTextBox = new TextBox();
            label3 = new Label();
            label1 = new Label();
            btnSave = new Button();
            SuspendLayout();
            // 
            // prodDocispassiveBOX
            // 
            prodDocispassiveBOX.AutoSize = true;
            prodDocispassiveBOX.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            prodDocispassiveBOX.ForeColor = SystemColors.ControlLightLight;
            prodDocispassiveBOX.Location = new Point(599, 98);
            prodDocispassiveBOX.Name = "prodDocispassiveBOX";
            prodDocispassiveBOX.Size = new Size(109, 29);
            prodDocispassiveBOX.TabIndex = 23;
            prodDocispassiveBOX.Text = "Pasif mi?";
            prodDocispassiveBOX.UseVisualStyleBackColor = true;
            // 
            // prodDoctypeTextTextBox
            // 
            prodDoctypeTextTextBox.Location = new Point(364, 100);
            prodDoctypeTextTextBox.Name = "prodDoctypeTextTextBox";
            prodDoctypeTextTextBox.Size = new Size(196, 27);
            prodDoctypeTextTextBox.TabIndex = 20;
            // 
            // prodDoctypeTextBox
            // 
            prodDoctypeTextBox.Location = new Point(217, 100);
            prodDoctypeTextBox.Name = "prodDoctypeTextBox";
            prodDoctypeTextBox.Size = new Size(127, 27);
            prodDoctypeTextBox.TabIndex = 21;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(217, 45);
            label2.Name = "label2";
            label2.Size = new Size(117, 20);
            label2.TabIndex = 17;
            label2.Text = "Ürün Ağacı Tipi";
            // 
            // firmCodeTextBox
            // 
            firmCodeTextBox.Location = new Point(66, 100);
            firmCodeTextBox.Name = "firmCodeTextBox";
            firmCodeTextBox.Size = new Size(127, 27);
            firmCodeTextBox.TabIndex = 22;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(364, 45);
            label3.Name = "label3";
            label3.Size = new Size(196, 20);
            label3.TabIndex = 18;
            label3.Text = "Ürün Ağacı Tipi Açıklaması";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(66, 45);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 19;
            label1.Text = "Firma Kodu";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(466, 186);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(107, 43);
            btnSave.TabIndex = 24;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // prodTreeEdit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(800, 450);
            Controls.Add(btnSave);
            Controls.Add(prodDocispassiveBOX);
            Controls.Add(prodDoctypeTextTextBox);
            Controls.Add(prodDoctypeTextBox);
            Controls.Add(label2);
            Controls.Add(firmCodeTextBox);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "prodTreeEdit";
            Text = "Ürün Ağacı Düzenle";
            Load += prodTreeEdit_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox prodDocispassiveBOX;
        private TextBox prodDoctypeTextTextBox;
        private TextBox prodDoctypeTextBox;
        private Label label2;
        private TextBox firmCodeTextBox;
        private Label label3;
        private Label label1;
        private Button btnSave;
    }
}