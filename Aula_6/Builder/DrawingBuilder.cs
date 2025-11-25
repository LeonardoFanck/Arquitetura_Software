using Aula_6.Patterns;

namespace Aula_6.Builder;

// BUILDER - build complex drawing step by step
public class DrawingBuilder(IRenderer renderer, FlyweightFactory factory)
{
    protected Drawing drawing = new ();
    protected IRenderer renderer = renderer;
    protected FlyweightFactory factory = factory;

    public Drawing Build() => drawing;
}
