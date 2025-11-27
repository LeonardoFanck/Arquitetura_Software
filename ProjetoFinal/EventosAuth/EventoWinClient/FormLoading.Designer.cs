namespace EventoWinClient
{
	partial class FormLoading
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
			groupBox1 = new GroupBox();
			LblCarregando = new Label();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(LblCarregando);
			groupBox1.Location = new Point(1, -7);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(232, 54);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			// 
			// LblCarregando
			// 
			LblCarregando.AutoSize = true;
			LblCarregando.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			LblCarregando.Location = new Point(47, 10);
			LblCarregando.Name = "LblCarregando";
			LblCarregando.Size = new Size(127, 30);
			LblCarregando.TabIndex = 1;
			LblCarregando.Text = "Carregando";
			// 
			// FormLoading
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(234, 47);
			Controls.Add(groupBox1);
			Name = "FormLoading";
			Text = "FormLoading";
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private GroupBox groupBox1;
		private Label LblCarregando;
	}
}