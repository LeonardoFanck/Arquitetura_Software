namespace Aula_6.State;

public class SelectionState(Editor editor) : IEditorState
{
    private readonly Editor editor = editor;

    public void OnMouseDown(Point p)
    {
        editor.SelectAt(p);
    }

    public void OnMouseMove(Point p)
    {
        if (editor.IsDragging)
            editor.DragTo(p);
    }

    public void OnMouseUp(Point p)
    {
        editor.Release();
    }
}
