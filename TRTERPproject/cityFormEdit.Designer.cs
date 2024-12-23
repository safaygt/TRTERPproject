namespace TRTERPproject
{
    partial class cityFormEdit
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
            cityNameTextBox = new TextBox();
            cityCodeTextBox = new TextBox();
            firmCodeTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            btnSave = new Button();
            countryCodeTextBox = new TextBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // cityNameTextBox
            // 
            cityNameTextBox.Location = new Point(386, 119);
            cityNameTextBox.Name = "cityNameTextBox";
            cityNameTextBox.Size = new Size(127, 27);
            cityNameTextBox.TabIndex = 12;
            // 
            // cityCodeTextBox
            // 
            cityCodeTextBox.Location = new Point(224, 119);
            cityCodeTextBox.Name = "cityCodeTextBox";
            cityCodeTextBox.Size = new Size(127, 27);
            cityCodeTextBox.TabIndex = 13;
            // 
            // firmCodeTextBox
            // 
            firmCodeTextBox.Location = new Point(64, 119);
            firmCodeTextBox.Name = "firmCodeTextBox";
            firmCodeTextBox.Size = new Size(127, 27);
            firmCodeTextBox.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(224, 67);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 9;
            label2.Text = "Şehir Kodu";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(386, 67);
            label3.Name = "label3";
            label3.Size = new Size(72, 20);
            label3.TabIndex = 10;
            label3.Text = "Şehir Adı";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(64, 67);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 11;
            label1.Text = "Firma Kodu";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(547, 212);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(125, 29);
            btnSave.TabIndex = 8;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click_1;
            // 
            // countryCodeTextBox
            // 
            countryCodeTextBox.Location = new Point(545, 119);
            countryCodeTextBox.Name = "countryCodeTextBox";
            countryCodeTextBox.Size = new Size(127, 27);
            countryCodeTextBox.TabIndex = 16;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(545, 67);
            label4.Name = "label4";
            label4.Size = new Size(81, 20);
            label4.TabIndex = 15;
            label4.Text = "Ülke Kodu";
            // 
            // cityFormEdit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(800, 450);
            Controls.Add(countryCodeTextBox);
            Controls.Add(label4);
            Controls.Add(cityNameTextBox);
            Controls.Add(cityCodeTextBox);
            Controls.Add(firmCodeTextBox);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Name = "cityFormEdit";
            Text = "Edit";
            Load += cityFormEdit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox cityNameTextBox;
        private TextBox cityCodeTextBox;
        private TextBox firmCodeTextBox;
        private Label label2;
        private Label label3;
        private Label label1;
        private Button btnSave;
        private TextBox countryCodeTextBox;
        private Label label4;
    }
}