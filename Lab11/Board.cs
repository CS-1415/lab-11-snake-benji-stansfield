using System.Runtime.CompilerServices;

namespace Lab11;

public class Board
{
    public int Width { get; }
    public int Height { get; }
    public Cell Apple { get; set; }
    public char AppleChar {get;}
    private List<Snake> snakes = new List<Snake>();
    Random rand = new Random();

    public Board(int width, int height, char appleChar)
    {
        Width = width;
        Height = height;
        AppleChar = appleChar;
        MoveApple();
    }

    public void AddSnake(Snake s)
    {
        snakes.Add(s);
    }

    public void MoveApple()
    {
        var freeCell = new List<Cell>();

        for(int r = 0; r < Height; r++)
        {
            for (int c = 0; c < Width; c++)
            {
                var cell = new Cell(r,c);
                if (IsCellEmpty(cell)) freeCell.Add(cell); // adds to the free cell list if the cell is empty
            }

            if(freeCell.Count == 0) return; // returns if there are no more free cells

            Apple = freeCell[rand.Next(freeCell.Count)];
        }
    }

    public bool IsInside(Cell c) => c.Row >= 0 && c.Row < Height && c.Column >= 0 && c.Column < Width;

    public bool IsCellEmpty(Cell c)
    {
        foreach(var s in snakes)
            if (s.IsOccupied(c)) return false;
        return true;
    }
}