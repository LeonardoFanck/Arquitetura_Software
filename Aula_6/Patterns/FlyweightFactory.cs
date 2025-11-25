namespace Aula_6.Patterns;

// FLYWEIGHT - shares intrinsic state (shape type + renderer instance)
public class FlyweightFactory
{
    private readonly Dictionary<string, Shape> pool = [];

    // key example: "circle|GdiRenderer"
    public Shape GetShape(string key, Func<Shape> createShape)
    {
        if (pool.TryGetValue(key, out Shape? value)) return value;
        var shape = createShape();
        pool[key] = shape;
        return shape;
    }
}
