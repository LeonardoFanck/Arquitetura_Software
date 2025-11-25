namespace Aula_6.Patterns
{
    public class ShapeRectangle(IRenderer renderer) : Shape(renderer)
    {
        public override void Draw(Graphics g, int x, int y, int size, Color color)
        {
            renderer.DrawRectangle(g, x - size / 2, y - size / 2, size, size, color);
        }
    }
}
