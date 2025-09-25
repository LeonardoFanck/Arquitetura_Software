namespace Aula_5.Classes;

public class Multiplicacao : Operacao
{
    protected override double Calcular(double valor1, double valor2)
    {
        return valor1 * valor2;
    }
}
