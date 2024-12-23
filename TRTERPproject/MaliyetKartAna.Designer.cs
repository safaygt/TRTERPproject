namespace TRTERPproject
{
	partial class MaliyetKartAna
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
			checkboxpas = new CheckBox();
			deletedlbl = new CheckBox();
			duzBut = new Button();
			DelBut = new Button();
			addBut = new Button();
			getBut = new Button();
			maliyetdata = new DataGridView();
			bitistarTxtBox = new TextBox();
			basTarTxtBox = new TextBox();
			maliyTxtBox = new TextBox();
			malNotxtBox = new TextBox();
			dillbl = new Label();
			label6 = new Label();
			label5 = new Label();
			label4 = new Label();
			label3 = new Label();
			maliylbl = new Label();
			label1 = new Label();
			firmComboBox = new ComboBox();
			dilCombo = new ComboBox();
			comboBoxMalMerTip = new ComboBox();
			((System.ComponentModel.ISupportInitialize)maliyetdata).BeginInit();
			SuspendLayout();
			// 
			// checkboxpas
			// 
			checkboxpas.AutoSize = true;
			checkboxpas.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
			checkboxpas.ForeColor = SystemColors.Control;
			checkboxpas.Location = new Point(864, 201);
			checkboxpas.Name = "checkboxpas";
			checkboxpas.Size = new Size(109, 29);
			checkboxpas.TabIndex = 45;
			checkboxpas.Text = "Pasif mi?";
			checkboxpas.UseVisualStyleBackColor = true;
			// 
			// deletedlbl
			// 
			deletedlbl.AutoSize = true;
			deletedlbl.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
			deletedlbl.ForeColor = SystemColors.Control;
			deletedlbl.Location = new Point(864, 236);
			deletedlbl.Name = "deletedlbl";
			deletedlbl.Size = new Size(120, 29);
			deletedlbl.TabIndex = 44;
			deletedlbl.Text = "Silindi mi?";
			deletedlbl.UseVisualStyleBackColor = true;
			// 
			// duzBut
			// 
			duzBut.Location = new Point(523, 236);
			duzBut.Name = "duzBut";
			duzBut.Size = new Size(125, 38);
			duzBut.TabIndex = 43;
			duzBut.Text = "Düzenle";
			duzBut.UseVisualStyleBackColor = true;
			// 
			// DelBut
			// 
			DelBut.Location = new Point(359, 236);
			DelBut.Name = "DelBut";
			DelBut.Size = new Size(125, 38);
			DelBut.TabIndex = 42;
			DelBut.Text = "Sil";
			DelBut.UseVisualStyleBackColor = true;
			// 
			// addBut
			// 
			addBut.Location = new Point(191, 236);
			addBut.Name = "addBut";
			addBut.Size = new Size(125, 38);
			addBut.TabIndex = 41;
			addBut.Text = "Ekle";
			addBut.UseVisualStyleBackColor = true;
			// 
			// getBut
			// 
			getBut.Location = new Point(23, 236);
			getBut.Name = "getBut";
			getBut.Size = new Size(125, 38);
			getBut.TabIndex = 40;
			getBut.Text = "Getir";
			getBut.UseVisualStyleBackColor = true;
			getBut.Click += getBut_Click;
			// 
			// maliyetdata
			// 
			maliyetdata.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			maliyetdata.Location = new Point(20, 280);
			maliyetdata.Name = "maliyetdata";
			maliyetdata.RowHeadersWidth = 51;
			maliyetdata.RowTemplate.Height = 29;
			maliyetdata.Size = new Size(1024, 337);
			maliyetdata.TabIndex = 39;
			// 
			// bitistarTxtBox
			// 
			bitistarTxtBox.Location = new Point(864, 127);
			bitistarTxtBox.Name = "bitistarTxtBox";
			bitistarTxtBox.Size = new Size(158, 27);
			bitistarTxtBox.TabIndex = 35;
			// 
			// basTarTxtBox
			// 
			basTarTxtBox.Location = new Point(859, 50);
			basTarTxtBox.Name = "basTarTxtBox";
			basTarTxtBox.Size = new Size(163, 27);
			basTarTxtBox.TabIndex = 34;
			// 
			// maliyTxtBox
			// 
			maliyTxtBox.Location = new Point(14, 137);
			maliyTxtBox.Name = "maliyTxtBox";
			maliyTxtBox.Size = new Size(401, 27);
			maliyTxtBox.TabIndex = 33;
			// 
			// malNotxtBox
			// 
			malNotxtBox.Location = new Point(414, 50);
			malNotxtBox.Name = "malNotxtBox";
			malNotxtBox.Size = new Size(181, 27);
			malNotxtBox.TabIndex = 32;
			// 
			// dillbl
			// 
			dillbl.AutoSize = true;
			dillbl.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
			dillbl.ForeColor = Color.GhostWhite;
			dillbl.Location = new Point(674, 24);
			dillbl.Name = "dillbl";
			dillbl.Size = new Size(80, 23);
			dillbl.TabIndex = 30;
			dillbl.Text = "Dil Kodu";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
			label6.ForeColor = Color.GhostWhite;
			label6.Location = new Point(209, 24);
			label6.Name = "label6";
			label6.Size = new Size(175, 23);
			label6.TabIndex = 28;
			label6.Text = "Maliyet Merkezi Tipi";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
			label5.ForeColor = Color.GhostWhite;
			label5.Location = new Point(885, 101);
			label5.Name = "label5";
			label5.Size = new Size(125, 23);
			label5.TabIndex = 27;
			label5.Text = "Geçerlilik Bitiş";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
			label4.ForeColor = Color.GhostWhite;
			label4.Location = new Point(859, 24);
			label4.Name = "label4";
			label4.Size = new Size(165, 23);
			label4.TabIndex = 26;
			label4.Text = "Geçerlilik Başlangıç";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
			label3.ForeColor = Color.GhostWhite;
			label3.Location = new Point(414, 24);
			label3.Name = "label3";
			label3.Size = new Size(186, 23);
			label3.TabIndex = 25;
			label3.Text = "Maliyet Merkezi Kodu";
			// 
			// maliylbl
			// 
			maliylbl.AutoSize = true;
			maliylbl.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
			maliylbl.ForeColor = Color.GhostWhite;
			maliylbl.Location = new Point(123, 111);
			maliylbl.Name = "maliylbl";
			maliylbl.Size = new Size(161, 23);
			maliylbl.TabIndex = 24;
			maliylbl.Text = "Maliyet Açıklaması";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
			label1.ForeColor = Color.GhostWhite;
			label1.Location = new Point(41, 24);
			label1.Name = "label1";
			label1.Size = new Size(103, 23);
			label1.TabIndex = 23;
			label1.Text = "Firma Kodu";
			// 
			// firmComboBox
			// 
			firmComboBox.FormattingEnabled = true;
			firmComboBox.Location = new Point(14, 49);
			firmComboBox.Name = "firmComboBox";
			firmComboBox.Size = new Size(167, 28);
			firmComboBox.TabIndex = 46;
			// 
			// dilCombo
			// 
			dilCombo.FormattingEnabled = true;
			dilCombo.Location = new Point(635, 49);
			dilCombo.Name = "dilCombo";
			dilCombo.Size = new Size(174, 28);
			dilCombo.TabIndex = 47;
			// 
			// comboBoxMalMerTip
			// 
			comboBoxMalMerTip.FormattingEnabled = true;
			comboBoxMalMerTip.Location = new Point(209, 49);
			comboBoxMalMerTip.Name = "comboBoxMalMerTip";
			comboBoxMalMerTip.Size = new Size(175, 28);
			comboBoxMalMerTip.TabIndex = 48;
			// 
			// MaliyetKartAna
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(197, 110, 51);
			ClientSize = new Size(1059, 637);
			Controls.Add(comboBoxMalMerTip);
			Controls.Add(dilCombo);
			Controls.Add(firmComboBox);
			Controls.Add(checkboxpas);
			Controls.Add(deletedlbl);
			Controls.Add(duzBut);
			Controls.Add(DelBut);
			Controls.Add(addBut);
			Controls.Add(getBut);
			Controls.Add(maliyetdata);
			Controls.Add(bitistarTxtBox);
			Controls.Add(basTarTxtBox);
			Controls.Add(maliyTxtBox);
			Controls.Add(malNotxtBox);
			Controls.Add(dillbl);
			Controls.Add(label6);
			Controls.Add(label5);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(maliylbl);
			Controls.Add(label1);
			Name = "MaliyetKartAna";
			Text = "MaliyetKartAna";
			((System.ComponentModel.ISupportInitialize)maliyetdata).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private CheckBox checkboxpas;
		private CheckBox deletedlbl;
		private Button duzBut;
		private Button DelBut;
		private Button addBut;
		private Button getBut;
		private DataGridView maliyetdata;
		private TextBox bitistarTxtBox;
		private TextBox basTarTxtBox;
		private TextBox maliyTxtBox;
		private TextBox malNotxtBox;
		private Label dillbl;
		private Label label6;
		private Label label5;
		private Label label4;
		private Label label3;
		private Label maliylbl;
		private Label label1;
		private ComboBox firmComboBox;
		private ComboBox dilCombo;
		private ComboBox comboBoxMalMerTip;
	}
}