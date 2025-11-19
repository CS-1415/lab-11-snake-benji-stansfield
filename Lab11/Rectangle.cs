namespace Lab11;

public class Rectangle : AbstractGraphic2D
{

    public decimal Left { get; set; }
    public decimal Top { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }

    public Rectangle(decimal left, decimal top, decimal width, decimal height)
    {
        Left = left;
        Top = top;
        Width = width;
        Height = height;
        DisplayChar = '*';
        ForegroundColor = ConsoleColor.White;
    }

    public override decimal LowerBoundX => Left;

    public override decimal UpperBoundX => Left + Width;

    public override decimal LowerBoundY => Top;

    public override decimal UpperBoundY => Top + Height;

    public override bool ContainsPoint(decimal x, decimal y)
    {
        return (x >= LowerBoundX && x <= UpperBoundX) && (y >= LowerBoundY && y <= UpperBoundY);
    }

    public override string ToString()
    {
        return $"Rectangle: Left = {Left}, Top = {Top}, Width = {Width}, Height = {Height}";
    }
}
