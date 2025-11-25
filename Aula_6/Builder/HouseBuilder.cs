using Aula_6.Patterns;

namespace Aula_6.Builder;

// Example concrete builder: HouseBuilder
public class HouseBuilder(IRenderer renderer) : DrawingBuilder(renderer, new FlyweightFactory())
{

    // Adds a simple house composed of rectangle (body) and triangle (roof simulated by rectangle rotated - simplified)
    public HouseBuilder AddHouse()
    {
        // Use flyweight shapes
        var circleKey = "circle|" + renderer.GetType().Name;
        var rectKey = "rect|" + renderer.GetType().Name;

        var rect = factory.GetShape(rectKey, () => new ShapeRectangle(renderer));
        var smallRect = factory.GetShape(rectKey + "_small", () => new ShapeRectangle(renderer));

        // Body
        drawing.Add(new PlacedShape(rect, new Point(0, 20), 80, Color.BurlyWood));
        // Door
        drawing.Add(new PlacedShape(smallRect, new Point(0, 40), 20, Color.SaddleBrown));
        // Window
        drawing.Add(new PlacedShape(smallRect, new Point(20, 20), 15, Color.LightBlue));

        return this;
    }
}
