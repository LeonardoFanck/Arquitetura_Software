namespace Aula_5.Classes;

public abstract class Operacao
{
    public double Executar(double valor1, double valor2)
    {
        Console.WriteLine("Iniciando operação...");
        double result = Calcular(valor1, valor2);
        Console.WriteLine("Resultado: " + result);
        return result;
    }

    protected abstract double Calcular(double valor1, double valor2);
}
