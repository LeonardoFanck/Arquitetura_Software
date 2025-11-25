namespace Aula_6.Patterns;

public class ShapeCircle(IRenderer renderer) : Shape(renderer)
{
    public override void Draw(Graphics g, int x, int y, int size, Color color)
    {
        renderer.DrawCircle(g, x, y, size, color);
    }
}
