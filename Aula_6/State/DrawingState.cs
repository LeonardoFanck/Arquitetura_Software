namespace Aula_6.State;

public class DrawingState(Editor editor) : IEditorState
{
    private readonly Editor editor = editor;

    public void OnMouseDown(Point p)
    {
        // create a new shape at position
        editor.CreateAt(p);
    }

    public void OnMouseMove(Point p) { /* while drawing could show preview - omitted for simplicity */ }

    public void OnMouseUp(Point p) { /* finalize if needed */ }
}
