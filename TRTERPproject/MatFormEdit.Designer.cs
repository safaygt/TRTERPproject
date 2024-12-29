namespace TRTERPproject
{
    partial class MatFormEdit
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
            MALNAMEBOX = new TextBox();
            MALCODEBOX = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            btnSave = new Button();
            ispassiveBOX = new CheckBox();
            comboBoxFirmCode = new ComboBox();
            SuspendLayout();
            // 
            // MALNAMEBOX
            // 
            MALNAMEBOX.Location = new Point(450, 112);
            MALNAMEBOX.Name = "MALNAMEBOX";
            MALNAMEBOX.Size = new Size(127, 27);
            MALNAMEBOX.TabIndex = 12;
            // 
            // MALCODEBOX
            // 
            MALCODEBOX.Location = new Point(257, 112);
            MALCODEBOX.Name = "MALCODEBOX";
            MALCODEBOX.Size = new Size(127, 27);
            MALCODEBOX.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(257, 60);
            label2.Name = "label2";
            label2.Size = new Size(113, 20);
            label2.TabIndex = 9;
            label2.Text = "Malzeme Kodu";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(452, 60);
            label3.Name = "label3";
            label3.Size = new Size(100, 20);
            label3.TabIndex = 10;
            label3.Text = "Malzeme Adı";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(50, 60);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 11;
            label1.Text = "Firma Kodu";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(535, 189);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(165, 60);
            btnSave.TabIndex = 8;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // ispassiveBOX
            // 
            ispassiveBOX.AutoSize = true;
            ispassiveBOX.Cursor = Cursors.No;
            ispassiveBOX.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            ispassiveBOX.ForeColor = SystemColors.ControlLightLight;
            ispassiveBOX.Location = new Point(640, 112);
            ispassiveBOX.Name = "ispassiveBOX";
            ispassiveBOX.Size = new Size(109, 29);
            ispassiveBOX.TabIndex = 16;
            ispassiveBOX.Text = "Pasif mi?";
            ispassiveBOX.UseVisualStyleBackColor = true;
            // 
            // comboBoxFirmCode
            // 
            comboBoxFirmCode.FormattingEnabled = true;
            comboBoxFirmCode.Location = new Point(50, 113);
            comboBoxFirmCode.Name = "comboBoxFirmCode";
            comboBoxFirmCode.Size = new Size(140, 28);
            comboBoxFirmCode.TabIndex = 17;
            // 
            // MatFormEdit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(865, 450);
            Controls.Add(comboBoxFirmCode);
            Controls.Add(ispassiveBOX);
            Controls.Add(MALNAMEBOX);
            Controls.Add(MALCODEBOX);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Name = "MatFormEdit";
            Text = "Malzeme Destek Tablosu Düzenle";
            Load += MatFormEdit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox MALNAMEBOX;
        private TextBox MALCODEBOX;
        private Label label2;
        private Label label3;
        private Label label1;
        private Button btnSave;
        private CheckBox ispassiveBOX;
        private ComboBox comboBoxFirmCode;
    }
}