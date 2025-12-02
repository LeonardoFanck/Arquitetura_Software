namespace EventoWinClient
{
	partial class FormPrincipal
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
			BtnEventos = new Button();
			BtnParticipante = new Button();
			BtnSincronizar = new Button();
			BtnSair = new Button();
			CheckBoxInternet = new CheckBox();
			SuspendLayout();
			// 
			// BtnEventos
			// 
			BtnEventos.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			BtnEventos.Location = new Point(77, 45);
			BtnEventos.Name = "BtnEventos";
			BtnEventos.Size = new Size(173, 70);
			BtnEventos.TabIndex = 1;
			BtnEventos.Text = "Listar Eventos";
			BtnEventos.UseVisualStyleBackColor = true;
			BtnEventos.Click += BtnEventos_Click;
			// 
			// BtnParticipante
			// 
			BtnParticipante.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			BtnParticipante.Location = new Point(77, 139);
			BtnParticipante.Name = "BtnParticipante";
			BtnParticipante.Size = new Size(173, 70);
			BtnParticipante.TabIndex = 2;
			BtnParticipante.Text = "Cadastrar participante";
			BtnParticipante.UseVisualStyleBackColor = true;
			BtnParticipante.Click += BtnParticipante_Click;
			// 
			// BtnSincronizar
			// 
			BtnSincronizar.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			BtnSincronizar.Location = new Point(77, 233);
			BtnSincronizar.Name = "BtnSincronizar";
			BtnSincronizar.Size = new Size(173, 70);
			BtnSincronizar.TabIndex = 3;
			BtnSincronizar.Text = "Sincronizar dados";
			BtnSincronizar.UseVisualStyleBackColor = true;
			BtnSincronizar.Click += BtnSincronizar_Click;
			// 
			// BtnSair
			// 
			BtnSair.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			BtnSair.Location = new Point(77, 358);
			BtnSair.Name = "BtnSair";
			BtnSair.Size = new Size(173, 70);
			BtnSair.TabIndex = 4;
			BtnSair.Text = "Sair do sistema";
			BtnSair.UseVisualStyleBackColor = true;
			BtnSair.Click += BtnSair_Click;
			// 
			// CheckBoxInternet
			// 
			CheckBoxInternet.AutoSize = true;
			CheckBoxInternet.Location = new Point(225, 444);
			CheckBoxInternet.Name = "CheckBoxInternet";
			CheckBoxInternet.Size = new Size(97, 19);
			CheckBoxInternet.TabIndex = 5;
			CheckBoxInternet.Text = "Internet Ativa";
			CheckBoxInternet.UseVisualStyleBackColor = true;
			// 
			// FormPrincipal
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(334, 475);
			Controls.Add(CheckBoxInternet);
			Controls.Add(BtnSair);
			Controls.Add(BtnSincronizar);
			Controls.Add(BtnParticipante);
			Controls.Add(BtnEventos);
			Name = "FormPrincipal";
			Text = "Sistema de Eventos";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Button BtnEventos;
        private Button BtnParticipante;
        private Button BtnSincronizar;
        private Button BtnSair;
		private CheckBox CheckBoxInternet;
	}
}