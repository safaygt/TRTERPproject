namespace TRTERPproject
{
    partial class lanFormEdit
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
            lanTextBox = new TextBox();
            lanCodeTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            btnSave = new Button();
            comboBoxFirmCode = new ComboBox();
            SuspendLayout();
            // 
            // lanTextBox
            // 
            lanTextBox.Location = new Point(485, 123);
            lanTextBox.Name = "lanTextBox";
            lanTextBox.Size = new Size(127, 27);
            lanTextBox.TabIndex = 12;
            // 
            // lanCodeTextBox
            // 
            lanCodeTextBox.Location = new Point(292, 123);
            lanCodeTextBox.Name = "lanCodeTextBox";
            lanCodeTextBox.Size = new Size(127, 27);
            lanCodeTextBox.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(292, 71);
            label2.Name = "label2";
            label2.Size = new Size(69, 20);
            label2.TabIndex = 9;
            label2.Text = "Dil Kodu";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(487, 71);
            label3.Name = "label3";
            label3.Size = new Size(28, 20);
            label3.TabIndex = 10;
            label3.Text = "Dil";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(85, 71);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 11;
            label1.Text = "Firma Kodu";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(487, 198);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(125, 29);
            btnSave.TabIndex = 8;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // comboBoxFirmCode
            // 
            comboBoxFirmCode.FormattingEnabled = true;
            comboBoxFirmCode.Location = new Point(85, 123);
            comboBoxFirmCode.Name = "comboBoxFirmCode";
            comboBoxFirmCode.Size = new Size(139, 28);
            comboBoxFirmCode.TabIndex = 14;
            // 
            // lanFormEdit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(800, 450);
            Controls.Add(comboBoxFirmCode);
            Controls.Add(lanTextBox);
            Controls.Add(lanCodeTextBox);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Name = "lanFormEdit";
            Text = "Dil Düzenle";
            Load += lanFormEdit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox lanTextBox;
        private TextBox lanCodeTextBox;
        private Label label2;
        private Label label3;
        private Label label1;
        private Button btnSave;
        private ComboBox comboBoxFirmCode;
    }
}