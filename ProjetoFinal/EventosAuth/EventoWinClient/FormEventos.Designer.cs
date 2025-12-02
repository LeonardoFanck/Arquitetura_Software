namespace EventoWinClient
{
    partial class FormEventos
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
			GridEventos = new DataGridView();
			((System.ComponentModel.ISupportInitialize)GridEventos).BeginInit();
			SuspendLayout();
			// 
			// GridEventos
			// 
			GridEventos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			GridEventos.Location = new Point(12, 12);
			GridEventos.Name = "GridEventos";
			GridEventos.Size = new Size(776, 365);
			GridEventos.TabIndex = 0;
			// 
			// FormEventos
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 392);
			Controls.Add(GridEventos);
			Name = "FormEventos";
			Text = "Eventos";
			((System.ComponentModel.ISupportInitialize)GridEventos).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private DataGridView GridEventos;
    }
}