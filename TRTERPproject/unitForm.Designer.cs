namespace TRTERPproject
{
    partial class unitForm
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
            btnDel = new Button();
            btnAdd = new Button();
            btnEdit = new Button();
            btnGet = new Button();
            unitDataGridView = new DataGridView();
            unitTextBox = new TextBox();
            unitCodeTextBox = new TextBox();
            firmCodeTextBox = new TextBox();
            label3 = new Label();
            label1 = new Label();
            mainUnitCodeTextBox = new TextBox();
            isMainUnitCheckBox = new CheckBox();
            label2 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)unitDataGridView).BeginInit();
            SuspendLayout();
            // 
            // btnDel
            // 
            btnDel.Location = new Point(476, 165);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(127, 29);
            btnDel.TabIndex = 22;
            btnDel.Text = "Sil";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(326, 165);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(127, 29);
            btnAdd.TabIndex = 23;
            btnAdd.Text = "Ekle";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(179, 165);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(127, 29);
            btnEdit.TabIndex = 24;
            btnEdit.Text = "Düzenle";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnGet
            // 
            btnGet.Location = new Point(28, 165);
            btnGet.Name = "btnGet";
            btnGet.Size = new Size(127, 29);
            btnGet.TabIndex = 25;
            btnGet.Text = "Getir";
            btnGet.UseVisualStyleBackColor = true;
            btnGet.Click += btnGet_Click;
            // 
            // unitDataGridView
            // 
            unitDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            unitDataGridView.Location = new Point(22, 240);
            unitDataGridView.Name = "unitDataGridView";
            unitDataGridView.RowHeadersWidth = 51;
            unitDataGridView.RowTemplate.Height = 29;
            unitDataGridView.Size = new Size(915, 239);
            unitDataGridView.TabIndex = 21;
            // 
            // unitTextBox
            // 
            unitTextBox.Location = new Point(326, 101);
            unitTextBox.Name = "unitTextBox";
            unitTextBox.Size = new Size(127, 27);
            unitTextBox.TabIndex = 18;
            // 
            // unitCodeTextBox
            // 
            unitCodeTextBox.Location = new Point(179, 101);
            unitCodeTextBox.Name = "unitCodeTextBox";
            unitCodeTextBox.Size = new Size(127, 27);
            unitCodeTextBox.TabIndex = 19;
            // 
            // firmCodeTextBox
            // 
            firmCodeTextBox.Location = new Point(28, 101);
            firmCodeTextBox.Name = "firmCodeTextBox";
            firmCodeTextBox.Size = new Size(127, 27);
            firmCodeTextBox.TabIndex = 20;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(326, 69);
            label3.Name = "label3";
            label3.Size = new Size(47, 20);
            label3.TabIndex = 16;
            label3.Text = "Birim";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(28, 69);
            label1.Name = "label1";
            label1.Size = new Size(90, 20);
            label1.TabIndex = 17;
            label1.Text = "Firma Kodu";
            // 
            // mainUnitCodeTextBox
            // 
            mainUnitCodeTextBox.Location = new Point(476, 101);
            mainUnitCodeTextBox.Name = "mainUnitCodeTextBox";
            mainUnitCodeTextBox.Size = new Size(127, 27);
            mainUnitCodeTextBox.TabIndex = 18;
            // 
            // isMainUnitCheckBox
            // 
            isMainUnitCheckBox.AutoSize = true;
            isMainUnitCheckBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            isMainUnitCheckBox.ForeColor = SystemColors.ControlLightLight;
            isMainUnitCheckBox.Location = new Point(641, 101);
            isMainUnitCheckBox.Name = "isMainUnitCheckBox";
            isMainUnitCheckBox.Size = new Size(130, 24);
            isMainUnitCheckBox.TabIndex = 26;
            isMainUnitCheckBox.Text = "Ana Birim mi?";
            isMainUnitCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(179, 69);
            label2.Name = "label2";
            label2.Size = new Size(88, 20);
            label2.TabIndex = 16;
            label2.Text = "Birim Kodu";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(476, 69);
            label4.Name = "label4";
            label4.Size = new Size(88, 20);
            label4.TabIndex = 16;
            label4.Text = "Birim Kodu";
            // 
            // unitForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(1003, 555);
            Controls.Add(isMainUnitCheckBox);
            Controls.Add(btnDel);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnGet);
            Controls.Add(unitDataGridView);
            Controls.Add(mainUnitCodeTextBox);
            Controls.Add(unitTextBox);
            Controls.Add(unitCodeTextBox);
            Controls.Add(firmCodeTextBox);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "unitForm";
            Text = "Birim Destek Tablosu";
            ((System.ComponentModel.ISupportInitialize)unitDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDel;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnGet;
        private DataGridView unitDataGridView;
        private TextBox unitTextBox;
        private TextBox unitCodeTextBox;
        private TextBox firmCodeTextBox;
        private Label label3;
        private Label label1;
        private TextBox mainUnitCodeTextBox;
        private CheckBox isMainUnitCheckBox;
        private Label label2;
        private Label label4;
    }
}