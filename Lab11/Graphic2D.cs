namespace Lab11;

// Represents a graphical element (i.e. a shape) that can be printed to the console.
public interface IGraphic2D
{
    // draws the graphic on the screen; return true if successful
    bool Display();
}


// Implements the display char and color properties.
public abstract class AbstractGraphic2D : IGraphic2D
{
    // returns true if the given point is within (including the border) of the shape
    public abstract bool ContainsPoint(decimal x, decimal y);

    // the following indicate a (possibly loose) bounding box for the element
    public abstract decimal LowerBoundX { get; }
    public abstract decimal UpperBoundX { get; }
    public abstract decimal LowerBoundY { get; }
    public abstract decimal UpperBoundY { get; }

    // the character to be displayed on cells within the image
    public char DisplayChar { get; set; }
    // the color of character to be displayed on cells within the image
    public ConsoleColor ForegroundColor { get; set; }
    // the background color of character to be displayed on cells within the image
    public ConsoleColor BackgroundColor { get; set; }

    // displays the given list of shapes
    public static void Display(List<IGraphic2D> shapes)
    {
        bool skippedSome = false;
        foreach (IGraphic2D shape in shapes)
        {
            if (!shape.Display())
            {
                skippedSome = true;
            }
        }
        if (skippedSome)
        {
            Console.WriteLine("warning: some cells skipped because of too small buffer");
        }
    }

    public bool Display()
    {
        Console.ForegroundColor = ForegroundColor;
        Console.BackgroundColor = BackgroundColor;
        int lowX = (int)decimal.Floor(LowerBoundX);
        int lowY = (int)decimal.Floor(LowerBoundY);
        int highX = (int)decimal.Floor(UpperBoundX);
        int highY = (int)decimal.Floor(UpperBoundY);
        bool skippedSome = false;
        for (int row = lowY; row <= highY; row++)
        {
            for (int column = lowX; column <= highX; column++)
            {
                if (ContainsPoint(column, row))
                {
                    if (column < Console.BufferWidth && row < Console.BufferHeight) 
                    {
                        Console.SetCursorPosition(column, row);
                        Console.Write(DisplayChar);
                    }
                    else
                    {
                        skippedSome = true;
                    }
                }
            }
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.SetCursorPosition(0, 0);
        return !skippedSome;
    }
}
