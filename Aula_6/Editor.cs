using Aula_6.Builder;
using Aula_6.Patterns;
using Aula_6.State;

namespace Aula_6;

public enum ShapeType
{
    Circle, 
    Rectangle
}

public class Editor
{
    private readonly IRenderer renderer;
    private readonly FlyweightFactory flyFactory = new ();
    private readonly List<PlacedShape> placed = [];
    private readonly Drawing? currentDrawing = null;
    private Point dragStart;
    private PlacedShape? selected = null;
    private IEditorState state;

    public ShapeType CurrentShapeType { get; set; } = ShapeType.Circle;
    public int DefaultSize { get; set; } = 30;
    public bool IsDragging { get; private set; } = false;

    public Editor(IRenderer renderer)
    {
        this.renderer = renderer;
        state = new DrawingState(this);
    }

    public void SetState(IEditorState newState) => state = newState;

    public void RenderAll(Graphics g)
    {
        // draw placed shapes
        foreach (var p in placed) p.Draw(g);
        // draw current drawing (builder result) if any
        if (currentDrawing != null)
        {
            foreach (var s in currentDrawing.Shapes) s.Draw(g);
        }
        // highlight selection
        if (selected != null)
        {
            var rect = new Rectangle(selected.Position.X - selected.Size / 2 - 2, selected.Position.Y - selected.Size / 2 - 2, selected.Size + 4, selected.Size + 4);
            g.DrawRectangle(Pens.Black, rect);
        }
    }

    public void OnMouseDown(Point p, Color color)
    {
        // store desired color and pass to state behavior
        TempColor = color;
        state.OnMouseDown(p);
    }

    public void OnMouseMove(Point p) => state.OnMouseMove(p);

    public void OnMouseUp(Point p) => state.OnMouseUp(p);

    // Called by DrawingState
    public void CreateAt(Point p)
    {
        string key = (CurrentShapeType == ShapeType.Circle ? "circle" : "rect") + "|" + renderer.GetType().Name;
        Shape fly = flyFactory.GetShape(key, () =>
        {
            return CurrentShapeType == ShapeType.Circle ? new ShapeCircle(renderer) : new ShapeRectangle(renderer);
        });
        var ps = new PlacedShape(fly, p, DefaultSize, TempColor);
        placed.Add(ps);
    }

    public Color TempColor { get; set; } = Color.Black;

    // Selection behavior
    public void SelectAt(Point p)
    {
        // simple hit test by distance
        selected = placed.LastOrDefault(z => Distance(z.Position, p) <= z.Size / 2);
        if (selected != null)
        {
            IsDragging = true;
            dragStart = p;
        }
    }

    public void DragTo(Point p)
    {
        if (selected == null) return;
        selected.Position = new Point(selected.Position.X + (p.X - dragStart.X), selected.Position.Y + (p.Y - dragStart.Y));
        dragStart = p;
    }

    public void Release()
    {
        IsDragging = false;
        selected = null;
    }

    private static double Distance(Point a, Point b)
    {
        var dx = a.X - b.X;
        var dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    // Builder support: add drawing at a location (used when builder creates already-positioned shapes with offsets)
    public void AddDrawingAt(Drawing drawing, Point origin)
    {
        foreach (var s in drawing.Shapes)
        {
            // each placed shape in drawing has positions relative to 0,0 - move by origin
            var fly = flyFactory.GetShape(GetKeyForShape(s.ShapeFlyweight), () => s.ShapeFlyweight);
            var p = new PlacedShape(fly, new Point(origin.X + s.Position.X, origin.Y + s.Position.Y), s.Size, s.Color);
            placed.Add(p);
        }
    }

    private string GetKeyForShape(Shape shape)
    {
        if (shape is ShapeCircle) return "circle|" + renderer.GetType().Name;
        return "rect|" + renderer.GetType().Name;
    }
}
