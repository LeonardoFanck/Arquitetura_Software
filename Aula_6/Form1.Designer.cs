namespace Aula_6
{
    partial class Form1
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
            PanelCanvas = new Panel();
            ComboCores = new ComboBox();
            groupBox1 = new GroupBox();
            BtnCasa = new Button();
            RadioRetangulo = new RadioButton();
            RadioCirculo = new RadioButton();
            groupBox2 = new GroupBox();
            RadioMover = new RadioButton();
            RadioAdicionar = new RadioButton();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // PanelCanvas
            // 
            PanelCanvas.Location = new Point(12, 96);
            PanelCanvas.Name = "PanelCanvas";
            PanelCanvas.Size = new Size(965, 499);
            PanelCanvas.TabIndex = 0;
            PanelCanvas.Paint += PanelCanvas_Paint;
            PanelCanvas.MouseDown += PanelCanvas_MouseDown;
            PanelCanvas.MouseMove += PanelCanvas_MouseMove;
            PanelCanvas.MouseUp += PanelCanvas_MouseUp;
            // 
            // ComboCores
            // 
            ComboCores.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboCores.FormattingEnabled = true;
            ComboCores.Items.AddRange(new object[] { "Preto", "Vermelho", "Verde", "Azul" });
            ComboCores.Location = new Point(784, 26);
            ComboCores.Name = "ComboCores";
            ComboCores.Size = new Size(172, 23);
            ComboCores.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(BtnCasa);
            groupBox1.Controls.Add(RadioRetangulo);
            groupBox1.Controls.Add(RadioCirculo);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(287, 72);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Desenho";
            // 
            // BtnCasa
            // 
            BtnCasa.Location = new Point(202, 33);
            BtnCasa.Name = "BtnCasa";
            BtnCasa.Size = new Size(75, 23);
            BtnCasa.TabIndex = 5;
            BtnCasa.Text = "Casa";
            BtnCasa.UseVisualStyleBackColor = true;
            BtnCasa.Click += BtnCasa_Click;
            // 
            // RadioRetangulo
            // 
            RadioRetangulo.AutoSize = true;
            RadioRetangulo.Location = new Point(99, 35);
            RadioRetangulo.Name = "RadioRetangulo";
            RadioRetangulo.Size = new Size(79, 19);
            RadioRetangulo.TabIndex = 3;
            RadioRetangulo.TabStop = true;
            RadioRetangulo.Text = "Retangulo";
            RadioRetangulo.UseVisualStyleBackColor = true;
            RadioRetangulo.CheckedChanged += RadioRetangulo_CheckedChanged;
            // 
            // RadioCirculo
            // 
            RadioCirculo.AutoSize = true;
            RadioCirculo.Location = new Point(19, 35);
            RadioCirculo.Name = "RadioCirculo";
            RadioCirculo.Size = new Size(63, 19);
            RadioCirculo.TabIndex = 2;
            RadioCirculo.TabStop = true;
            RadioCirculo.Text = "Circulo";
            RadioCirculo.UseVisualStyleBackColor = true;
            RadioCirculo.CheckedChanged += RadioCirculo_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(RadioMover);
            groupBox2.Controls.Add(RadioAdicionar);
            groupBox2.Location = new Point(618, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(150, 72);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Modo";
            // 
            // RadioMover
            // 
            RadioMover.AutoSize = true;
            RadioMover.Location = new Point(20, 43);
            RadioMover.Name = "RadioMover";
            RadioMover.Size = new Size(59, 19);
            RadioMover.TabIndex = 1;
            RadioMover.TabStop = true;
            RadioMover.Text = "Mover";
            RadioMover.UseVisualStyleBackColor = true;
            RadioMover.CheckedChanged += RadioMover_CheckedChanged;
            // 
            // RadioAdicionar
            // 
            RadioAdicionar.AutoSize = true;
            RadioAdicionar.Location = new Point(20, 18);
            RadioAdicionar.Name = "RadioAdicionar";
            RadioAdicionar.Size = new Size(76, 19);
            RadioAdicionar.TabIndex = 0;
            RadioAdicionar.TabStop = true;
            RadioAdicionar.Text = "Adicionar";
            RadioAdicionar.UseVisualStyleBackColor = true;
            RadioAdicionar.CheckedChanged += RadioAdicionar_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(989, 607);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(ComboCores);
            Controls.Add(PanelCanvas);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelCanvas;
        private ComboBox ComboCores;
        private GroupBox groupBox1;
        private RadioButton RadioRetangulo;
        private RadioButton RadioCirculo;
        private GroupBox groupBox2;
        private RadioButton RadioMover;
        private RadioButton RadioAdicionar;
        private Button BtnCasa;
    }
}