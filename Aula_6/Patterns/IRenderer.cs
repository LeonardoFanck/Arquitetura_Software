namespace Aula_6.Patterns;

// BRIDGE - Renderer interface (implementation side)
public interface IRenderer
{
    void DrawCircle(Graphics g, int x, int y, int radius, Color color);
    void DrawRectangle(Graphics g, int x, int y, int width, int height, Color color);
}
