namespace Aula_6.State;

// STATE pattern - different behaviors for mouse interactions
public interface IEditorState
{
    void OnMouseDown(Point p);
    void OnMouseMove(Point p);
    void OnMouseUp(Point p);
}
