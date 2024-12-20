namespace TRTERPproject
{
	partial class loginForm
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
			label2 = new Label();
			LoginBtn = new Button();
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
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(325, 162);
			label1.Name = "label1";
			label1.Size = new Size(75, 20);
			label1.TabIndex = 2;
			label1.Text = "Username";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(325, 227);
			label2.Name = "label2";
			label2.Size = new Size(70, 20);
			label2.TabIndex = 3;
			label2.Text = "Password";
			// 
			// LoginBtn
			// 
			LoginBtn.Location = new Point(352, 306);
			LoginBtn.Name = "LoginBtn";
			LoginBtn.Size = new Size(94, 29);
			LoginBtn.TabIndex = 4;
			LoginBtn.Text = "Login";
			LoginBtn.UseVisualStyleBackColor = true;
			// 
			// loginForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(LoginBtn);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(pwdBox);
			Controls.Add(usernameBox);
			Name = "loginForm";
			Text = "loginForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox usernameBox;
		private TextBox pwdBox;
		private Label label1;
		private Label label2;
		private Button LoginBtn;
	}
}