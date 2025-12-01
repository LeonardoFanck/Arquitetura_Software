namespace EventoWinClient
{
    partial class FormParticipante
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
            label1 = new Label();
            label2 = new Label();
            TxtEmail = new TextBox();
            TxtSenha = new TextBox();
            ButtonGravar = new Button();
            ButtonCancelar = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 60);
            label1.Name = "label1";
            label1.Size = new Size(36, 15);
            label1.TabIndex = 0;
            label1.Text = "Email";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(39, 124);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 1;
            label2.Text = "Senha";
            // 
            // TxtEmail
            // 
            TxtEmail.Location = new Point(39, 78);
            TxtEmail.Name = "TxtEmail";
            TxtEmail.Size = new Size(230, 23);
            TxtEmail.TabIndex = 2;
            // 
            // TxtSenha
            // 
            TxtSenha.Location = new Point(39, 142);
            TxtSenha.Name = "TxtSenha";
            TxtSenha.PasswordChar = '*';
            TxtSenha.Size = new Size(230, 23);
            TxtSenha.TabIndex = 3;
            // 
            // ButtonGravar
            // 
            ButtonGravar.Location = new Point(39, 206);
            ButtonGravar.Name = "ButtonGravar";
            ButtonGravar.Size = new Size(75, 23);
            ButtonGravar.TabIndex = 4;
            ButtonGravar.Text = "Gravar";
            ButtonGravar.UseVisualStyleBackColor = true;
            ButtonGravar.Click += ButtonGravar_Click;
            // 
            // ButtonCancelar
            // 
            ButtonCancelar.Location = new Point(194, 206);
            ButtonCancelar.Name = "ButtonCancelar";
            ButtonCancelar.Size = new Size(75, 23);
            ButtonCancelar.TabIndex = 5;
            ButtonCancelar.Text = "Cancelar";
            ButtonCancelar.UseVisualStyleBackColor = true;
            ButtonCancelar.Click += ButtonCancelar_Click;
            // 
            // FormParticipante
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(331, 259);
            Controls.Add(ButtonCancelar);
            Controls.Add(ButtonGravar);
            Controls.Add(TxtSenha);
            Controls.Add(TxtEmail);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormParticipante";
            Text = "Cadastro Simplificado";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
		private Label label2;
		private TextBox TxtEmail;
		private TextBox TxtSenha;
		private Button ButtonGravar;
		private Button ButtonCancelar;
	}
}