namespace EventoWinClient
{
	partial class FormEventoDetalhes
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
			GridInscricoes = new DataGridView();
			BtnAdicionar = new Button();
			((System.ComponentModel.ISupportInitialize)GridInscricoes).BeginInit();
			SuspendLayout();
			// 
			// GridInscricoes
			// 
			GridInscricoes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			GridInscricoes.Location = new Point(12, 50);
			GridInscricoes.Name = "GridInscricoes";
			GridInscricoes.Size = new Size(711, 406);
			GridInscricoes.TabIndex = 0;
			// 
			// BtnAdicionar
			// 
			BtnAdicionar.Location = new Point(539, 12);
			BtnAdicionar.Name = "BtnAdicionar";
			BtnAdicionar.Size = new Size(184, 32);
			BtnAdicionar.TabIndex = 1;
			BtnAdicionar.Text = "Adicionar ao evento";
			BtnAdicionar.UseVisualStyleBackColor = true;
			BtnAdicionar.Click += BtnAdicionar_Click;
			// 
			// FormEventoDetalhes
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(735, 467);
			Controls.Add(BtnAdicionar);
			Controls.Add(GridInscricoes);
			Name = "FormEventoDetalhes";
			Text = "Detalhes do Evento";
			((System.ComponentModel.ISupportInitialize)GridInscricoes).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private DataGridView GridInscricoes;
		private Button BtnAdicionar;
	}
}