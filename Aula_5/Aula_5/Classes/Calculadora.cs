namespace Aula_5.Classes;

public class Calculadora
{
    private double valorAtual;
    private readonly Stack<Memento> historyUndo = new();
    private readonly Stack<Memento> historyDo = new();

    public void Calcular(string expression)
    {
        historyDo.Clear();
        historyUndo.Push(new Memento(valorAtual)); // salvar antes do cálculo
        valorAtual = ExpressionInterpreter.Interpret(expression);
    }

    public void Desfazer()
    {
        if (historyUndo.Count > 0)
        {
            var memento = historyUndo.Pop();
            historyDo.Push(memento);
            valorAtual = memento.Value;
            Console.WriteLine("Desfeito! Valor restaurado: " + valorAtual);
        }
    }

    public void Fazer()
    {
        if (historyDo.Count > 0)
        {
            var memento = historyDo.Pop();
            historyUndo.Push(memento);
            valorAtual = memento.Value;
            Console.WriteLine("Refeito! Valor restaurado: " + valorAtual);
        }
    }

    public double GetResultado() => valorAtual;

}
