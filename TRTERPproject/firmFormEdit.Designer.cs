namespace TRTERPproject
{
    partial class firmFormEdit
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
            address1TextBox = new TextBox();
            firmNameTextBox = new TextBox();
            firmCodeTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            btnSave = new Button();
            label4 = new Label();
            address2TextBox = new TextBox();
            label5 = new Label();
            cityCodeTextBox = new TextBox();
            label6 = new Label();
            countryCodeTextBox = new TextBox();
            SuspendLayout();
            // 
            // address1TextBox
            // 
            address1TextBox.Location = new Point(454, 108);
            address1TextBox.Name = "address1TextBox";
            address1TextBox.Size = new Size(127, 27);
            address1TextBox.TabIndex = 19;
            // 
            // firmNameTextBox
            // 
            firmNameTextBox.Location = new Point(261, 108);
            firmNameTextBox.Name = "firmNameTextBox";
            firmNameTextBox.Size = new Size(127, 27);
            firmNameTextBox.TabIndex = 20;
            // 
            // firmCodeTextBox
            // 
            firmCodeTextBox.Location = new Point(54, 108);
            firmCodeTextBox.Name = "firmCodeTextBox";
            firmCodeTextBox.Size = new Size(127, 27);
            firmCodeTextBox.TabIndex = 21;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(261, 56);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 16;
            label2.Text = "Firma Adı";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(456, 56);
            label3.Name = "label3";
            label3.Size = new Size(63, 20);
            label3.TabIndex = 17;
            label3.Text = "Adres 1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(54, 56);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 18;
            label1.Text = "Firma Kodu";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(454, 325);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(125, 29);
            btnSave.TabIndex = 15;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(56, 175);
            label4.Name = "label4";
            label4.Size = new Size(63, 20);
            label4.TabIndex = 17;
            label4.Text = "Adres 2";
            // 
            // address2TextBox
            // 
            address2TextBox.Location = new Point(54, 227);
            address2TextBox.Name = "address2TextBox";
            address2TextBox.Size = new Size(127, 27);
            address2TextBox.TabIndex = 19;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ControlLightLight;
            label5.Location = new Point(263, 175);
            label5.Name = "label5";
            label5.Size = new Size(85, 20);
            label5.TabIndex = 17;
            label5.Text = "Şehir Kodu";
            // 
            // cityCodeTextBox
            // 
            cityCodeTextBox.Location = new Point(261, 227);
            cityCodeTextBox.Name = "cityCodeTextBox";
            cityCodeTextBox.Size = new Size(127, 27);
            cityCodeTextBox.TabIndex = 19;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ControlLightLight;
            label6.Location = new Point(456, 175);
            label6.Name = "label6";
            label6.Size = new Size(81, 20);
            label6.TabIndex = 17;
            label6.Text = "Ülke Kodu";
            // 
            // countryCodeTextBox
            // 
            countryCodeTextBox.Location = new Point(454, 227);
            countryCodeTextBox.Name = "countryCodeTextBox";
            countryCodeTextBox.Size = new Size(127, 27);
            countryCodeTextBox.TabIndex = 19;
            // 
            // firmFormEdit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(800, 450);
            Controls.Add(address2TextBox);
            Controls.Add(cityCodeTextBox);
            Controls.Add(countryCodeTextBox);
            Controls.Add(address1TextBox);
            Controls.Add(firmNameTextBox);
            Controls.Add(firmCodeTextBox);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Name = "firmFormEdit";
            Text = "firmFormEdit";
            Load += firmFormEdit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox address1TextBox;
        private TextBox firmNameTextBox;
        private TextBox firmCodeTextBox;
        private Label label2;
        private Label label3;
        private Label label1;
        private Button btnSave;
        private Label label4;
        private TextBox address2TextBox;
        private Label label5;
        private TextBox cityCodeTextBox;
        private Label label6;
        private TextBox countryCodeTextBox;
    }
}