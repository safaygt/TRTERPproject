namespace TRTERPproject
{
	partial class LoginForm
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
			usernameBox = new TextBox();
			pwdBox = new TextBox();
			label1 = new Label();
			LoginBtn = new Button();
			label3 = new Label();
			SuspendLayout();
			// 
			// usernameBox
			// 
			usernameBox.Location = new Point(325, 185);
			usernameBox.Name = "usernameBox";
			usernameBox.Size = new Size(159, 27);
			usernameBox.TabIndex = 0;
			// 
			// pwdBox
			// 
			pwdBox.Location = new Point(325, 250);
			pwdBox.Name = "pwdBox";
			pwdBox.Size = new Size(159, 27);
			pwdBox.TabIndex = 1;
			pwdBox.UseSystemPasswordChar = true;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
			label1.ForeColor = SystemColors.ControlLightLight;
			label1.Location = new Point(325, 151);
			label1.Name = "label1";
			label1.Size = new Size(106, 28);
			label1.TabIndex = 2;
			label1.Text = "Username";
			// 
			// LoginBtn
			// 
			LoginBtn.Location = new Point(352, 306);
			LoginBtn.Name = "LoginBtn";
			LoginBtn.Size = new Size(94, 29);
			LoginBtn.TabIndex = 4;
			LoginBtn.Text = "Login";
			LoginBtn.UseVisualStyleBackColor = true;
			LoginBtn.Click += LoginBtn_Click;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
			label3.ForeColor = SystemColors.ControlLightLight;
			label3.Location = new Point(325, 219);
			label3.Name = "label3";
			label3.Size = new Size(101, 28);
			label3.TabIndex = 5;
			label3.Text = "Password";
			// 
			// LoginForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(197, 110, 51);
			ClientSize = new Size(832, 491);
			Controls.Add(label3);
			Controls.Add(LoginBtn);
			Controls.Add(label1);
			Controls.Add(pwdBox);
			Controls.Add(usernameBox);
			Name = "LoginForm";
			Text = "loginForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox usernameBox;
		private TextBox pwdBox;
		private Label label1;
		private Button LoginBtn;
		private Label label3;
	}
}