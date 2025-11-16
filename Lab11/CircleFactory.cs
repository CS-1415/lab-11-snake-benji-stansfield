namespace Lab11;

public class CircleFactory : IGraphic2DFactory
{
    public string Name => "Circle";

    public IGraphic2D Create()
    {
        Console.Write("X Coordinate: ");
        int x = int.Parse(Console.ReadLine());

        Console.Write("Y Coordinate: ");
        int y = int.Parse(Console.ReadLine());

        Console.Write("Radius: ");
        int radius = int.Parse(Console.ReadLine());

        return new Circle(x, y, radius);
    }
}
