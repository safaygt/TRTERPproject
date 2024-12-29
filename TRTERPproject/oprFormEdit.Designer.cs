namespace TRTERPproject
{
    partial class oprFormEdit
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
            isPassiveOprCheckbox = new CheckBox();
            oprTypeDesTextBox = new TextBox();
            oprTypeTextBox = new TextBox();
            oprFirmCodeTextBox = new TextBox();
            oprTypeLabel = new Label();
            oprTypeDesLabel = new Label();
            oprFrimLabel = new Label();
            btnSave = new Button();
            SuspendLayout();
            // 
            // isPassiveOprCheckbox
            // 
            isPassiveOprCheckbox.AutoSize = true;
            isPassiveOprCheckbox.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            isPassiveOprCheckbox.ForeColor = SystemColors.Control;
            isPassiveOprCheckbox.Location = new Point(657, 137);
            isPassiveOprCheckbox.Name = "isPassiveOprCheckbox";
            isPassiveOprCheckbox.Size = new Size(109, 29);
            isPassiveOprCheckbox.TabIndex = 74;
            isPassiveOprCheckbox.Text = "Pasif mi?";
            isPassiveOprCheckbox.UseVisualStyleBackColor = true;
            // 
            // oprTypeDesTextBox
            // 
            oprTypeDesTextBox.Location = new Point(327, 137);
            oprTypeDesTextBox.Name = "oprTypeDesTextBox";
            oprTypeDesTextBox.Size = new Size(261, 27);
            oprTypeDesTextBox.TabIndex = 71;
            // 
            // oprTypeTextBox
            // 
            oprTypeTextBox.Location = new Point(180, 137);
            oprTypeTextBox.Name = "oprTypeTextBox";
            oprTypeTextBox.Size = new Size(127, 27);
            oprTypeTextBox.TabIndex = 72;
            // 
            // oprFirmCodeTextBox
            // 
            oprFirmCodeTextBox.Location = new Point(29, 135);
            oprFirmCodeTextBox.Name = "oprFirmCodeTextBox";
            oprFirmCodeTextBox.Size = new Size(127, 27);
            oprFirmCodeTextBox.TabIndex = 73;
            // 
            // oprTypeLabel
            // 
            oprTypeLabel.AutoSize = true;
            oprTypeLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            oprTypeLabel.ForeColor = SystemColors.ControlLightLight;
            oprTypeLabel.Location = new Point(180, 103);
            oprTypeLabel.Name = "oprTypeLabel";
            oprTypeLabel.Size = new Size(114, 20);
            oprTypeLabel.TabIndex = 68;
            oprTypeLabel.Text = "Operasyon Tipi";
            // 
            // oprTypeDesLabel
            // 
            oprTypeDesLabel.AutoSize = true;
            oprTypeDesLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            oprTypeDesLabel.ForeColor = SystemColors.ControlLightLight;
            oprTypeDesLabel.Location = new Point(327, 103);
            oprTypeDesLabel.Name = "oprTypeDesLabel";
            oprTypeDesLabel.Size = new Size(193, 20);
            oprTypeDesLabel.TabIndex = 69;
            oprTypeDesLabel.Text = "Operasyon Tipi Açıklaması";
            // 
            // oprFrimLabel
            // 
            oprFrimLabel.AutoSize = true;
            oprFrimLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            oprFrimLabel.ForeColor = SystemColors.ControlLightLight;
            oprFrimLabel.Location = new Point(29, 103);
            oprFrimLabel.Name = "oprFrimLabel";
            oprFrimLabel.Size = new Size(90, 20);
            oprFrimLabel.TabIndex = 70;
            oprFrimLabel.Text = "Firma Kodu";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(525, 325);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(125, 29);
            btnSave.TabIndex = 75;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // oprFormEdit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(800, 450);
            Controls.Add(btnSave);
            Controls.Add(isPassiveOprCheckbox);
            Controls.Add(oprTypeDesTextBox);
            Controls.Add(oprTypeTextBox);
            Controls.Add(oprFirmCodeTextBox);
            Controls.Add(oprTypeLabel);
            Controls.Add(oprTypeDesLabel);
            Controls.Add(oprFrimLabel);
            Name = "oprFormEdit";
            Text = "Operasyon Düzenleme Tablosu";
            Load += oprFormEdit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox isPassiveOprCheckbox;
        private TextBox oprTypeDesTextBox;
        private TextBox oprTypeTextBox;
        private TextBox oprFirmCodeTextBox;
        private Label oprTypeLabel;
        private Label oprTypeDesLabel;
        private Label oprFrimLabel;
        private Button btnSave;
    }
}