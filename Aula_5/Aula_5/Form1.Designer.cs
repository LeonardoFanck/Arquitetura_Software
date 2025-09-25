namespace Aula_5
{
    partial class Form1
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
            TxtValor1 = new TextBox();
            TxtValor2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            BtnAdicao = new Button();
            BtnSubtracao = new Button();
            BtnMultiplicacao = new Button();
            BtnDivisao = new Button();
            BtnVoltar = new Button();
            BtnAvancar = new Button();
            BtnCalcular = new Button();
            LblResultado = new Label();
            lblOperacao = new Label();
            SuspendLayout();
            // 
            // TxtValor1
            // 
            TxtValor1.Location = new Point(39, 48);
            TxtValor1.Name = "TxtValor1";
            TxtValor1.Size = new Size(117, 23);
            TxtValor1.TabIndex = 0;
            // 
            // TxtValor2
            // 
            TxtValor2.Location = new Point(204, 48);
            TxtValor2.Name = "TxtValor2";
            TxtValor2.Size = new Size(117, 23);
            TxtValor2.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 30);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 2;
            label1.Text = "Valor 1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(204, 30);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 3;
            label2.Text = "Valor 2";
            // 
            // BtnAdicao
            // 
            BtnAdicao.Location = new Point(81, 106);
            BtnAdicao.Name = "BtnAdicao";
            BtnAdicao.Size = new Size(75, 23);
            BtnAdicao.TabIndex = 4;
            BtnAdicao.Text = "+";
            BtnAdicao.UseVisualStyleBackColor = true;
            BtnAdicao.Click += BtnAdicao_Click;
            // 
            // BtnSubtracao
            // 
            BtnSubtracao.Location = new Point(204, 106);
            BtnSubtracao.Name = "BtnSubtracao";
            BtnSubtracao.Size = new Size(75, 23);
            BtnSubtracao.TabIndex = 5;
            BtnSubtracao.Text = "-";
            BtnSubtracao.UseVisualStyleBackColor = true;
            BtnSubtracao.Click += BtnSubtracao_Click;
            // 
            // BtnMultiplicacao
            // 
            BtnMultiplicacao.Location = new Point(204, 135);
            BtnMultiplicacao.Name = "BtnMultiplicacao";
            BtnMultiplicacao.Size = new Size(75, 23);
            BtnMultiplicacao.TabIndex = 6;
            BtnMultiplicacao.Text = "X";
            BtnMultiplicacao.UseVisualStyleBackColor = true;
            BtnMultiplicacao.Click += BtnMultiplicacao_Click;
            // 
            // BtnDivisao
            // 
            BtnDivisao.Location = new Point(81, 135);
            BtnDivisao.Name = "BtnDivisao";
            BtnDivisao.Size = new Size(75, 23);
            BtnDivisao.TabIndex = 7;
            BtnDivisao.Text = "/";
            BtnDivisao.UseVisualStyleBackColor = true;
            BtnDivisao.Click += BtnDivisao_Click;
            // 
            // BtnVoltar
            // 
            BtnVoltar.Location = new Point(15, 212);
            BtnVoltar.Name = "BtnVoltar";
            BtnVoltar.Size = new Size(75, 23);
            BtnVoltar.TabIndex = 8;
            BtnVoltar.Text = "Voltar";
            BtnVoltar.UseVisualStyleBackColor = true;
            BtnVoltar.Click += BtnVoltar_Click;
            // 
            // BtnAvancar
            // 
            BtnAvancar.Location = new Point(96, 212);
            BtnAvancar.Name = "BtnAvancar";
            BtnAvancar.Size = new Size(75, 23);
            BtnAvancar.TabIndex = 9;
            BtnAvancar.Text = "Avançar";
            BtnAvancar.UseVisualStyleBackColor = true;
            BtnAvancar.Click += BtnAvancar_Click;
            // 
            // BtnCalcular
            // 
            BtnCalcular.Location = new Point(204, 212);
            BtnCalcular.Name = "BtnCalcular";
            BtnCalcular.Size = new Size(75, 23);
            BtnCalcular.TabIndex = 10;
            BtnCalcular.Text = "Calcular";
            BtnCalcular.UseVisualStyleBackColor = true;
            BtnCalcular.Click += BtnCalcular_Click;
            // 
            // LblResultado
            // 
            LblResultado.AutoSize = true;
            LblResultado.Location = new Point(204, 251);
            LblResultado.Name = "LblResultado";
            LblResultado.Size = new Size(0, 15);
            LblResultado.TabIndex = 11;
            // 
            // lblOperacao
            // 
            lblOperacao.AutoSize = true;
            lblOperacao.Location = new Point(178, 51);
            lblOperacao.Name = "lblOperacao";
            lblOperacao.Size = new Size(0, 15);
            lblOperacao.TabIndex = 12;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(378, 299);
            Controls.Add(lblOperacao);
            Controls.Add(LblResultado);
            Controls.Add(BtnCalcular);
            Controls.Add(BtnAvancar);
            Controls.Add(BtnVoltar);
            Controls.Add(BtnDivisao);
            Controls.Add(BtnMultiplicacao);
            Controls.Add(BtnSubtracao);
            Controls.Add(BtnAdicao);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TxtValor2);
            Controls.Add(TxtValor1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TxtValor1;
        private TextBox TxtValor2;
        private Label label1;
        private Label label2;
        private Button BtnAdicao;
        private Button BtnSubtracao;
        private Button BtnMultiplicacao;
        private Button BtnDivisao;
        private Button BtnVoltar;
        private Button BtnAvancar;
        private Button BtnCalcular;
        private Label LblResultado;
        private Label lblOperacao;
    }
}
