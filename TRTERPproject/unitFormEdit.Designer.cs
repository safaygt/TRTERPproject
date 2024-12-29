namespace TRTERPproject
{
    partial class unitFormEdit
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
            unitTextBox = new TextBox();
            unitCodeTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            btnSave = new Button();
            isMainUnitCheckBox = new CheckBox();
            label4 = new Label();
            label5 = new Label();
            mainUnitCodeTextBox = new TextBox();
            comboBoxFirmCode = new ComboBox();
            SuspendLayout();
            // 
            // unitTextBox
            // 
            unitTextBox.Location = new Point(385, 140);
            unitTextBox.Name = "unitTextBox";
            unitTextBox.Size = new Size(127, 27);
            unitTextBox.TabIndex = 19;
            // 
            // unitCodeTextBox
            // 
            unitCodeTextBox.Location = new Point(213, 140);
            unitCodeTextBox.Name = "unitCodeTextBox";
            unitCodeTextBox.Size = new Size(127, 27);
            unitCodeTextBox.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(213, 88);
            label2.Name = "label2";
            label2.Size = new Size(88, 20);
            label2.TabIndex = 16;
            label2.Text = "Birim Kodu";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(385, 88);
            label3.Name = "label3";
            label3.Size = new Size(47, 20);
            label3.TabIndex = 17;
            label3.Text = "Birim";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(28, 87);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 18;
            label1.Text = "Firma Kodu";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(512, 333);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(125, 29);
            btnSave.TabIndex = 15;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // isMainUnitCheckBox
            // 
            isMainUnitCheckBox.AutoSize = true;
            isMainUnitCheckBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            isMainUnitCheckBox.ForeColor = SystemColors.ControlLightLight;
            isMainUnitCheckBox.Location = new Point(552, 142);
            isMainUnitCheckBox.Name = "isMainUnitCheckBox";
            isMainUnitCheckBox.Size = new Size(130, 24);
            isMainUnitCheckBox.TabIndex = 22;
            isMainUnitCheckBox.Text = "Ana Birim mi?";
            isMainUnitCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(552, 88);
            label4.Name = "label4";
            label4.Size = new Size(0, 20);
            label4.TabIndex = 17;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ControlLightLight;
            label5.Location = new Point(28, 213);
            label5.Name = "label5";
            label5.Size = new Size(120, 20);
            label5.TabIndex = 18;
            label5.Text = "Ana Birim Kodu";
            // 
            // mainUnitCodeTextBox
            // 
            mainUnitCodeTextBox.Location = new Point(28, 266);
            mainUnitCodeTextBox.Name = "mainUnitCodeTextBox";
            mainUnitCodeTextBox.Size = new Size(127, 27);
            mainUnitCodeTextBox.TabIndex = 21;
            // 
            // comboBoxFirmCode
            // 
            comboBoxFirmCode.FormattingEnabled = true;
            comboBoxFirmCode.Location = new Point(28, 140);
            comboBoxFirmCode.Name = "comboBoxFirmCode";
            comboBoxFirmCode.Size = new Size(137, 28);
            comboBoxFirmCode.TabIndex = 23;
            // 
            // unitFormEdit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(800, 450);
            Controls.Add(comboBoxFirmCode);
            Controls.Add(isMainUnitCheckBox);
            Controls.Add(unitTextBox);
            Controls.Add(unitCodeTextBox);
            Controls.Add(mainUnitCodeTextBox);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Name = "unitFormEdit";
            Text = "Birim Düzenle";
            Load += unitFormEdit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox unitTextBox;
        private TextBox unitCodeTextBox;
        private Label label2;
        private Label label3;
        private Label label1;
        private Button btnSave;
        private CheckBox isMainUnitCheckBox;
        private Label label4;
        private Label label5;
        private TextBox mainUnitCodeTextBox;
        private ComboBox comboBoxFirmCode;
    }
}