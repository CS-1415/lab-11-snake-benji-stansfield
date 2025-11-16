using System.Drawing;

namespace Lab10;

public class RectangleFactory : IGraphic2DFactory
{
    public string Name => "Rectangle";

    public IGraphic2D Create()
    {
        Console.Write("Left coordinate: ");
        int left = int.Parse(Console.ReadLine());

        Console.Write("Top coordinate: ");
        int top = int.Parse(Console.ReadLine());

        Console.Write("Width: ");
        int width = int.Parse(Console.ReadLine());

        Console.Write("Height: ");
        int height = int.Parse(Console.ReadLine());

        return new Rectangle(left, top, width, height);
    }
}
