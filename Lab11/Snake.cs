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

    public bool IsOccupied(Cell c) => cells.Any(x => x.Equals(c)); // checks if a cell is being used

    /*Looks at the next cell but doesn't modify*/
    public Cell PeekNextHead()
    {
        var head = cells.Last();
        switch (Direction)
        {
            case Direction.Up: return new Cell(head.Row - 1, head.Column);
            case Direction.Down: return new Cell(head.Row + 1, head.Column);
            case Direction.Left: return new Cell(head.Row, head.Column - 1);
            case Direction.Right: return new Cell(head.Row, head.Column + 1);
            default: return head;
        }
    }

    // Move forward by one. If grow==true, append new head and do not remove tail.
    public void MoveForward(bool grow)
    {
        var nh = PeekNextHead();
        cells.Add(nh);
        if (!grow)
        {
            // remove first element (tail end)
            if (cells.Count > 0) cells.RemoveAt(0);
        }
    }

    // FOR TESTING
    public List<Cell> GetCells() => new List<Cell>(cells);
    public void CellsSetForTest(List<Cell> newCells)
    {
        cells = new List<Cell>(newCells);
    }
}
