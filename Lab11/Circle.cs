namespace Lab11;

public class Circle : AbstractGraphic2D
{
    public decimal CenterX { get; set; }
    public decimal CenterY { get; set; }
    public decimal Radius { get; set; }
    
    public Circle(decimal x, decimal y, decimal radius)
    {
        CenterX = x;
        CenterY = y;
        Radius = radius;
        DisplayChar = '*';
        ForegroundColor = ConsoleColor.White;
    }
    
    public override decimal LowerBoundX => CenterX - Radius;

    public override decimal UpperBoundX => CenterX + Radius;

    public override decimal LowerBoundY => CenterY - Radius;

    public override decimal UpperBoundY => CenterY + Radius;

    public override bool ContainsPoint(decimal x, decimal y)
    {
        // using the equation (x-h)^2 + (y-k)^2 <= r^2
        decimal dx = x - CenterX;
        decimal dy = y - CenterY;
        return (dx * dx) + (dy * dy) <= Radius * Radius;
    }

    public override string ToString()
    {
        return $"Circle: Center X = {CenterX}, Center Y = {CenterY}, Radius = {Radius}";
    }
}
