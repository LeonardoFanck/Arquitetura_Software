using Aula_6.Builder;
using Aula_6.Patterns;
using Aula_6.State;

namespace Aula_6;

public partial class Form1 : Form
{
    private bool _isMouseDown = false;
    private readonly Editor _editor;
    private readonly IRenderer _renderer;
    private Point _lastLocation = new(0, 0);

    public Form1()
    {
        InitializeComponent();

        ComboCores.SelectedIndex = 0;

        _renderer = new GdiRenderer();
        _editor = new(_renderer);

        RadioAdicionar.Select();
        RadioCirculo.Select();
    }

    private void PanelCanvas_MouseDown(object sender, MouseEventArgs e)
    {
        _isMouseDown = true;
        _editor.OnMouseDown(e.Location, GetSelectedColor());
        PanelCanvas.Invalidate();

        _lastLocation = e.Location;
    }

    private void PanelCanvas_MouseUp(object sender, MouseEventArgs e)
    {
        _isMouseDown = false;
        _editor.OnMouseUp(e.Location);
        PanelCanvas.Invalidate();
    }

    private void PanelCanvas_Paint(object sender, PaintEventArgs e)
    {
        _editor.RenderAll(e.Graphics);
    }

    private Color GetSelectedColor()
    {
        return ComboCores.SelectedItem?.ToString() switch
        {
            "Vermelho" => Color.Red,
            "Azul" => Color.Blue,
            "Verde" => Color.Green,
            _ => Color.Black
        };
    }

    private void RadioCirculo_CheckedChanged(object sender, EventArgs e)
    {
        _editor.CurrentShapeType = ShapeType.Circle;
    }

    private void RadioRetangulo_CheckedChanged(object sender, EventArgs e)
    {
        _editor.CurrentShapeType = ShapeType.Rectangle;
    }

    private void BtnCasa_Click(object sender, EventArgs e)
    {
        var builder = new HouseBuilder(_renderer);
        var drawing = builder.AddHouse().Build();
        _editor.AddDrawingAt(drawing, _lastLocation);
        PanelCanvas.Invalidate();
    }

    private void RadioAdicionar_CheckedChanged(object sender, EventArgs e)
    {
        _editor.SetState(new DrawingState(_editor));
    }

    private void RadioMover_CheckedChanged(object sender, EventArgs e)
    {
        _editor.SetState(new SelectionState(_editor));
    }

    private void PanelCanvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isMouseDown)
        {
            _editor.OnMouseMove(e.Location);
            PanelCanvas.Invalidate();
        }
    }
}
