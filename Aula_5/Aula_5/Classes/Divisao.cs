namespace Aula_5.Classes;

public class Divisao : Operacao
{
    protected override double Calcular(double valor1, double valor2)
    {
        return valor2 != 0 ? valor1 / valor2 : double.NaN;
    }
}
