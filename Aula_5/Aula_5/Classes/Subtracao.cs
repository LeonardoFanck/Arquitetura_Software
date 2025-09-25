namespace Aula_5.Classes;

internal class Subtracao : Operacao
{
    protected override double Calcular(double valor1, double valor2)
    {
        return valor1 - valor2;
    }
}
