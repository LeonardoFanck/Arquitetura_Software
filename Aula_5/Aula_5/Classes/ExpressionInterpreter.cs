namespace Aula_5.Classes;

public class ExpressionInterpreter
{
    public static double Interpret(string expression)
    {
        var tokens = expression.Split(' ');
        double valor1 = double.Parse(tokens[0]);
        string operacao = tokens[1];
        double valor2 = double.Parse(tokens[2]);

        return operacao switch
        {
            "+" => new Adicao().Executar(valor1, valor2),
            "-" => new Subtracao().Executar(valor1, valor2),
            "*" => new Multiplicacao().Executar(valor1, valor2),
            "/" => new Divisao().Executar(valor1, valor2),
            _ => throw new Exception("Operação inválida")
        };
    }
}
