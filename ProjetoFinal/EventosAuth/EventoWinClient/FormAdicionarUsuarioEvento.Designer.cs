namespace EventoWinClient
{
	partial class FormAdicionarUsuarioEvento
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
			GridUsuarios = new DataGridView();
			((System.ComponentModel.ISupportInitialize)GridUsuarios).BeginInit();
			SuspendLayout();
			// 
			// GridUsuarios
			// 
			GridUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			GridUsuarios.Location = new Point(12, 12);
			GridUsuarios.Name = "GridUsuarios";
			GridUsuarios.Size = new Size(530, 338);
			GridUsuarios.TabIndex = 0;
			// 
			// FormAdicionarUsuarioEvento
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(554, 362);
			Controls.Add(GridUsuarios);
			Name = "FormAdicionarUsuarioEvento";
			Text = "Adicionar Usuario ao Evento";
			((System.ComponentModel.ISupportInitialize)GridUsuarios).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private DataGridView GridUsuarios;
	}
}