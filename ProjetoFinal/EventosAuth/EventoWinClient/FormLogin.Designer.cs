namespace EventoWinClient
{
	partial class FormLogin
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			buttonEntrar = new Button();
			buttonSair = new Button();
			textBoxEmail = new TextBox();
			textBoxSenha = new TextBox();
			label1 = new Label();
			label2 = new Label();
			SuspendLayout();
			// 
			// buttonEntrar
			// 
			buttonEntrar.Location = new Point(56, 136);
			buttonEntrar.Name = "buttonEntrar";
			buttonEntrar.Size = new Size(75, 23);
			buttonEntrar.TabIndex = 0;
			buttonEntrar.Text = "Entrar";
			buttonEntrar.UseVisualStyleBackColor = true;
			buttonEntrar.Click += ButtonEntrar_Click;
			// 
			// buttonSair
			// 
			buttonSair.Location = new Point(200, 136);
			buttonSair.Name = "buttonSair";
			buttonSair.Size = new Size(75, 23);
			buttonSair.TabIndex = 1;
			buttonSair.Text = "Sair";
			buttonSair.UseVisualStyleBackColor = true;
			// 
			// textBoxEmail
			// 
			textBoxEmail.Location = new Point(56, 38);
			textBoxEmail.Name = "textBoxEmail";
			textBoxEmail.Size = new Size(219, 23);
			textBoxEmail.TabIndex = 2;
			// 
			// textBoxSenha
			// 
			textBoxSenha.Location = new Point(56, 92);
			textBoxSenha.Name = "textBoxSenha";
			textBoxSenha.Size = new Size(219, 23);
			textBoxSenha.TabIndex = 3;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(56, 20);
			label1.Name = "label1";
			label1.Size = new Size(36, 15);
			label1.TabIndex = 4;
			label1.Text = "Email";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(56, 74);
			label2.Name = "label2";
			label2.Size = new Size(39, 15);
			label2.TabIndex = 5;
			label2.Text = "Senha";
			// 
			// FormLogin
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(340, 188);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(textBoxSenha);
			Controls.Add(textBoxEmail);
			Controls.Add(buttonSair);
			Controls.Add(buttonEntrar);
			Name = "FormLogin";
			Text = "Login";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button buttonEntrar;
		private Button buttonSair;
		private TextBox textBoxEmail;
		private TextBox textBoxSenha;
		private Label label1;
		private Label label2;
	}
}
