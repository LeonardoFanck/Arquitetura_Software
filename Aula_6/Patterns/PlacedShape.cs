namespace Aula_6.Patterns
{
    // Represents a shape placed on canvas: uses a flyweight Shape + extrinsic state
    public class PlacedShape(Shape flyweight, Point position, int size, Color color)
    {
        public Shape ShapeFlyweight { get; } = flyweight;
        public Point Position { get; set; } = position;
        public int Size { get; set; } = size;
        public Color Color { get; set; } = color;

        public void Draw(Graphics g) => ShapeFlyweight.Draw(g, Position.X, Position.Y, Size, Color);
    }
}
