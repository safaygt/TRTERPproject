namespace TRTERPproject
{
    partial class urunPatlatma
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
            urunPatlatmaData = new DataGridView();
            secilenPatlatma = new DataGridView();
            label6 = new Label();
            label1 = new Label();
            duzBut = new Button();
            DelBut = new Button();
            getBut = new Button();
            ((System.ComponentModel.ISupportInitialize)urunPatlatmaData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)secilenPatlatma).BeginInit();
            SuspendLayout();
            // 
            // urunPatlatmaData
            // 
            urunPatlatmaData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            urunPatlatmaData.Location = new Point(12, 215);
            urunPatlatmaData.Name = "urunPatlatmaData";
            urunPatlatmaData.RowHeadersWidth = 51;
            urunPatlatmaData.RowTemplate.Height = 29;
            urunPatlatmaData.Size = new Size(1214, 392);
            urunPatlatmaData.TabIndex = 0;
            // 
            // secilenPatlatma
            // 
            secilenPatlatma.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            secilenPatlatma.Location = new Point(21, 35);
            secilenPatlatma.Name = "secilenPatlatma";
            secilenPatlatma.RowHeadersWidth = 51;
            secilenPatlatma.RowTemplate.Height = 29;
            secilenPatlatma.Size = new Size(1205, 110);
            secilenPatlatma.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.GhostWhite;
            label6.Location = new Point(526, 9);
            label6.Name = "label6";
            label6.Size = new Size(160, 23);
            label6.TabIndex = 80;
            label6.Text = "Seçilen Ürün Ağacı";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.GhostWhite;
            label1.Location = new Point(554, 171);
            label1.Name = "label1";
            label1.Size = new Size(132, 23);
            label1.TabIndex = 81;
            label1.Text = "Alt Malzemeler";
            // 
            // duzBut
            // 
            duzBut.Location = new Point(152, 171);
            duzBut.Name = "duzBut";
            duzBut.Size = new Size(125, 38);
            duzBut.TabIndex = 180;
            duzBut.Text = "Düzenle";
            duzBut.UseVisualStyleBackColor = true;
            duzBut.Click += duzBut_Click;
            // 
            // DelBut
            // 
            DelBut.Location = new Point(283, 171);
            DelBut.Name = "DelBut";
            DelBut.Size = new Size(125, 38);
            DelBut.TabIndex = 179;
            DelBut.Text = "Sil";
            DelBut.UseVisualStyleBackColor = true;
            DelBut.Click += DelBut_Click;
            // 
            // getBut
            // 
            getBut.Location = new Point(21, 171);
            getBut.Name = "getBut";
            getBut.Size = new Size(125, 38);
            getBut.TabIndex = 178;
            getBut.Text = "Getir";
            getBut.UseVisualStyleBackColor = true;
            getBut.Click += getBut_Click;
            // 
            // urunPatlatma
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 110, 51);
            ClientSize = new Size(1238, 619);
            Controls.Add(duzBut);
            Controls.Add(DelBut);
            Controls.Add(getBut);
            Controls.Add(label1);
            Controls.Add(label6);
            Controls.Add(secilenPatlatma);
            Controls.Add(urunPatlatmaData);
            Name = "urunPatlatma";
            Text = "urunPatlatma";
            Load += urunPatlatma_Load;
            ((System.ComponentModel.ISupportInitialize)urunPatlatmaData).EndInit();
            ((System.ComponentModel.ISupportInitialize)secilenPatlatma).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView urunPatlatmaData;
        private DataGridView secilenPatlatma;
        private Label label6;
        private Label label1;
        private Button duzBut;
        private Button DelBut;
        private Button getBut;
    }
}