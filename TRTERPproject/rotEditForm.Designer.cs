namespace TRTERPproject
{
    partial class rotEditForm
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
            isPassiveCheckBox = new CheckBox();
            rotTypeStatementTextBox = new TextBox();
            rotTypeTextBox = new TextBox();
            label2 = new Label();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            comboBoxFirmCode = new ComboBox();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Location = new Point(489, 261);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(125, 29);
            btnSave.TabIndex = 40;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // isPassiveCheckBox
            // 
            isPassiveCheckBox.AutoSize = true;
            isPassiveCheckBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            isPassiveCheckBox.ForeColor = SystemColors.ControlLightLight;
            isPassiveCheckBox.Location = new Point(666, 132);
            isPassiveCheckBox.Name = "isPassiveCheckBox";
            isPassiveCheckBox.Size = new Size(87, 24);
            isPassiveCheckBox.TabIndex = 39;
            isPassiveCheckBox.Text = "Pasif mi";
            isPassiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // rotTypeStatementTextBox
            // 
            rotTypeStatementTextBox.Location = new Point(385, 132);
            rotTypeStatementTextBox.Name = "rotTypeStatementTextBox";
            rotTypeStatementTextBox.Size = new Size(229, 27);
            rotTypeStatementTextBox.TabIndex = 36;
            // 
            // rotTypeTextBox
            // 
            rotTypeTextBox.Location = new Point(192, 132);
            rotTypeTextBox.Name = "rotTypeTextBox";
            rotTypeTextBox.Size = new Size(150, 27);
            rotTypeTextBox.TabIndex = 37;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(192, 80);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 32;
            label2.Text = "Rota Tipi";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(552, 81);
            label4.Name = "label4";
            label4.Size = new Size(0, 20);
            label4.TabIndex = 33;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(385, 80);
            label3.Name = "label3";
            label3.Size = new Size(151, 20);
            label3.TabIndex = 34;
            label3.Text = "Rota Tipi Açıklaması";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(31, 80);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 35;
            label1.Text = "Firma Kodu";
            // 
            // comboBoxFirmCode
            // 
            comboBoxFirmCode.FormattingEnabled = true;
            comboBoxFirmCode.Location = new Point(31, 131);
            comboBoxFirmCode.Name = "comboBoxFirmCode";
            comboBoxFirmCode.Size = new Size(122, 28);
            comboBoxFirmCode.TabIndex = 41;
            // 
            // rotEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(800, 450);
            Controls.Add(comboBoxFirmCode);
            Controls.Add(btnSave);
            Controls.Add(isPassiveCheckBox);
            Controls.Add(rotTypeStatementTextBox);
            Controls.Add(rotTypeTextBox);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "rotEditForm";
            Text = "Rota Düzenleme ";
            Load += rotEditForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSave;
        private CheckBox isPassiveCheckBox;
        private TextBox rotTypeStatementTextBox;
        private TextBox rotTypeTextBox;
        private Label label2;
        private Label label4;
        private Label label3;
        private Label label1;
        private ComboBox comboBoxFirmCode;
    }
}