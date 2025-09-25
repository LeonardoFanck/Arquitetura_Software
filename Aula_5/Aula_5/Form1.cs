using Aula_5.Classes;

namespace Aula_5;

public partial class Form1 : Form
{
    private readonly Calculadora _calculadora;

    public Form1()
    {
        InitializeComponent();
        _calculadora = new();
        AtualizarOperacao("+");
    }

    private void BtnAdicao_Click(object sender, EventArgs e)
    {
        AtualizarOperacao("+");
    }

    private void BtnSubtracao_Click(object sender, EventArgs e)
    {
        AtualizarOperacao("-");
    }

    private void BtnDivisao_Click(object sender, EventArgs e)
    {
        AtualizarOperacao("/");
    }

    private void BtnMultiplicacao_Click(object sender, EventArgs e)
    {
        AtualizarOperacao("*");
    }

    private void BtnVoltar_Click(object sender, EventArgs e)
    {
        _calculadora.Desfazer();
        LblResultado.Text = _calculadora.GetResultado().ToString();
    }

    private void BtnAvancar_Click(object sender, EventArgs e)
    {
        _calculadora.Fazer();
        LblResultado.Text = _calculadora.GetResultado().ToString();
    }

    private void BtnCalcular_Click(object sender, EventArgs e)
    {
        Calcular();
    }

    private void AtualizarOperacao(string operacao)
    {
        lblOperacao.Text = operacao;
    }

    private void Calcular()
    {
        var valor1 = TxtValor1.Text;
        var valor2 = TxtValor2.Text;

        if (string.IsNullOrEmpty(valor1))
        {
            MessageBox.Show("Valor 1 obrigatório");
            TxtValor1.Focus();
            return;
        }

        if (string.IsNullOrEmpty(valor2))
        {
            MessageBox.Show("Valor 2 obrigatório");
            TxtValor2.Focus();
            return;
        }

        try
        {
            _calculadora.Calcular($"{valor1} {lblOperacao.Text} {valor2}");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro, não foi possível calcular, detalhes: " + ex.Message);
        }

        LblResultado.Text = _calculadora.GetResultado().ToString();
    }
}
