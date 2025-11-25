namespace Aula_6.Patterns;

// BRIDGE - Abstraction side: Shape
public abstract class Shape(IRenderer renderer)
{
    protected IRenderer renderer = renderer;

    // Draw with extrinsic state (position, size, color)
    public abstract void Draw(Graphics g, int x, int y, int size, Color color);
}
