namespace TRTERPproject
{
    partial class countryFormEdit
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
            btnSave = new Button();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            countryNameTextBox = new TextBox();
            countryCodeTextBox = new TextBox();
            firmCodeTextBox = new TextBox();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Location = new Point(470, 179);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(125, 29);
            btnSave.TabIndex = 1;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(275, 52);
            label2.Name = "label2";
            label2.Size = new Size(81, 20);
            label2.TabIndex = 2;
            label2.Text = "Ülke Kodu";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(470, 52);
            label3.Name = "label3";
            label3.Size = new Size(68, 20);
            label3.TabIndex = 3;
            label3.Text = "Ülke Adı";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(68, 52);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 4;
            label1.Text = "Firma Kodu";
            // 
            // countryNameTextBox
            // 
            countryNameTextBox.Location = new Point(468, 104);
            countryNameTextBox.Name = "countryNameTextBox";
            countryNameTextBox.Size = new Size(127, 27);
            countryNameTextBox.TabIndex = 5;
            // 
            // countryCodeTextBox
            // 
            countryCodeTextBox.Location = new Point(275, 104);
            countryCodeTextBox.Name = "countryCodeTextBox";
            countryCodeTextBox.Size = new Size(127, 27);
            countryCodeTextBox.TabIndex = 6;
            // 
            // firmCodeTextBox
            // 
            firmCodeTextBox.Location = new Point(68, 104);
            firmCodeTextBox.Name = "firmCodeTextBox";
            firmCodeTextBox.Size = new Size(127, 27);
            firmCodeTextBox.TabIndex = 7;
            // 
            // countryFormEdit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(800, 450);
            Controls.Add(countryNameTextBox);
            Controls.Add(countryCodeTextBox);
            Controls.Add(firmCodeTextBox);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Name = "countryFormEdit";
            Text = "Edit";
            Load += countryFormEdit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnSave;
        private Label label2;
        private Label label3;
        private Label label1;
        private TextBox countryNameTextBox;
        private TextBox countryCodeTextBox;
        private TextBox firmCodeTextBox;
    }
}