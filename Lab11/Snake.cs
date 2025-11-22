namespace Lab11;

public class Snake
{
    public string Name {get;}
    private List<Cell> cells;
    public char DisplayChar {get;}
    public Direction Direction {get; set;}
    public Board BoardReference {get; set;}

    public Snake(string name, Cell startHead, Direction dir, Char displayChar, Board board)
    {
        Name = name;
        Direction = dir;
        DisplayChar = displayChar;
        BoardReference = board;

        var head = startHead; // creates a head for the snake
        var tail = startHead; // creates another cell - the snake's tail

        cells = new List<Cell> {tail, head};
    }

    // Methods to turn (do not allow direct 180 degree reversal)
    public void TurnUp() { if (Direction != Direction.Down) Direction = Direction.Up; }
    public void TurnDown() { if (Direction != Direction.Up) Direction = Direction.Down; }
    public void TurnLeft() { if (Direction != Direction.Right) Direction = Direction.Left; }
    public void TurnRight() { if (Direction != Direction.Left) Direction = Direction.Right; }

    // FOR TESTING
    public List<Cell> GetCells() => new List<Cell>(cells);

    public bool IsOccupied(Cell c) => cells.Any(x => x.Equals(c)); // checks if a cell is being used

}
