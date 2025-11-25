using Aula_6.Patterns;

namespace Aula_6.Builder;

// A Drawing is a collection of placed shapes
public class Drawing
{
    private readonly List<PlacedShape> shapes = [];
    public IEnumerable<PlacedShape> Shapes => shapes;
    public void Add(PlacedShape s) => shapes.Add(s);
}
