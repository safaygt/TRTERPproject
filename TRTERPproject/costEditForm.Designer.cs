namespace TRTERPproject
{
    partial class costEditForm
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
            isPassiveCheckBox = new CheckBox();
            costTypeStatementTextBox = new TextBox();
            costTypeTextBox = new TextBox();
            label2 = new Label();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            btnSave = new Button();
            comboBoxFirmCode = new ComboBox();
            SuspendLayout();
            // 
            // isPassiveCheckBox
            // 
            isPassiveCheckBox.AutoSize = true;
            isPassiveCheckBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            isPassiveCheckBox.ForeColor = SystemColors.ControlLightLight;
            isPassiveCheckBox.Location = new Point(705, 148);
            isPassiveCheckBox.Name = "isPassiveCheckBox";
            isPassiveCheckBox.Size = new Size(94, 24);
            isPassiveCheckBox.TabIndex = 30;
            isPassiveCheckBox.Text = "Pasif mi?";
            isPassiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // costTypeStatementTextBox
            // 
            costTypeStatementTextBox.Location = new Point(424, 148);
            costTypeStatementTextBox.Name = "costTypeStatementTextBox";
            costTypeStatementTextBox.Size = new Size(229, 27);
            costTypeStatementTextBox.TabIndex = 27;
            // 
            // costTypeTextBox
            // 
            costTypeTextBox.Location = new Point(231, 148);
            costTypeTextBox.Name = "costTypeTextBox";
            costTypeTextBox.Size = new Size(150, 27);
            costTypeTextBox.TabIndex = 28;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(231, 96);
            label2.Name = "label2";
            label2.Size = new Size(150, 20);
            label2.TabIndex = 23;
            label2.Text = "Maliyet Merkezi Tipi";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(591, 97);
            label4.Name = "label4";
            label4.Size = new Size(0, 20);
            label4.TabIndex = 24;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(424, 96);
            label3.Name = "label3";
            label3.Size = new Size(229, 20);
            label3.TabIndex = 25;
            label3.Text = "Maliyet Merkezi Tipi Açıklaması";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(51, 96);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 26;
            label1.Text = "Firma Kodu";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(528, 276);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(125, 29);
            btnSave.TabIndex = 31;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // comboBoxFirmCode
            // 
            comboBoxFirmCode.FormattingEnabled = true;
            comboBoxFirmCode.Location = new Point(51, 148);
            comboBoxFirmCode.Name = "comboBoxFirmCode";
            comboBoxFirmCode.Size = new Size(129, 28);
            comboBoxFirmCode.TabIndex = 32;
            // 
            // costEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(860, 553);
            Controls.Add(comboBoxFirmCode);
            Controls.Add(btnSave);
            Controls.Add(isPassiveCheckBox);
            Controls.Add(costTypeStatementTextBox);
            Controls.Add(costTypeTextBox);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "costEditForm";
            Text = "Maliyet Düzenle";
            Load += costEditForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox isPassiveCheckBox;
        private TextBox costTypeStatementTextBox;
        private TextBox costTypeTextBox;
        private Label label2;
        private Label label4;
        private Label label3;
        private Label label1;
        private Button btnSave;
        private ComboBox comboBoxFirmCode;
    }
}