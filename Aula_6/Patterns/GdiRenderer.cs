namespace Aula_6.Patterns;

// Concrete Renderer for WinForms GDI+
public class GdiRenderer() : IRenderer
{
    public void DrawCircle(Graphics g, int x, int y, int radius, Color color)
    {
        var pen = Pens.Black;
        var brush = new SolidBrush(color);
        g.FillEllipse(brush, x - radius, y - radius, radius * 2, radius * 2);
        brush.Dispose();
    }

    public void DrawRectangle(Graphics g, int x, int y, int width, int height, Color color)
    {
        var brush = new SolidBrush(color);
        g.FillRectangle(brush, x, y, width, height);
        brush.Dispose();
    }
}
