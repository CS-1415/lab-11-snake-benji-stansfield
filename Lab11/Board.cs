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
    public IEnumerable<Snake> Snakes => snakes;

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

    public GameResult Step(ConsoleKey? key) // this converts user input into movement
    {
        if (key.HasValue)
        {
            switch(key.Value)
            {
                // WASD -> snake 1
                case ConsoleKey.W: snakes.FirstOrDefault()?.TurnUp(); break;
                case ConsoleKey.S: snakes.FirstOrDefault()?.TurnDown(); break;
                case ConsoleKey.A: snakes.FirstOrDefault()?.TurnLeft(); break;
                case ConsoleKey.D: snakes.FirstOrDefault()?.TurnRight(); break;

                // Arrows -> snake 2 (if exists)
                case ConsoleKey.UpArrow: if (snakes.Count > 1) snakes[1].TurnUp(); break;
                case ConsoleKey.DownArrow: if (snakes.Count > 1) snakes[1].TurnDown(); break;
                case ConsoleKey.LeftArrow: if (snakes.Count > 1) snakes[1].TurnLeft(); break;
                case ConsoleKey.RightArrow: if (snakes.Count > 1) snakes[1].TurnRight(); break;
            }
        }

        var nextHeads = snakes.Select(s => s.PeekNextHead()).ToList(); // decide the next head

        /*Checks for collisions (wall or other snake)*/
        var dead = new bool[snakes.Count];
        for (int i = 0; i < snakes.Count; i++)
        {
            var nh = nextHeads[i];
            if (!IsInside(nh))
            {
                dead[i] = true;
                continue;
            }
            // check collision with opponent's occupied cells (before they move)
            for (int j = 0; j < snakes.Count; j++)
            {
                if (i == j) continue; // spec says opponent's tail; skip self
                if (snakes[j].IsOccupied(nh))
                {
                    dead[i] = true;
                    break;
                }
            }
        }

        // Game ties if both snakes die
        if (dead.All(d => d))
            return new GameResult(true, null);
        
        for (int i = 0; i < snakes.Count; i++)
        {
            if (dead[i])
            {
                // winner is any other snake that is alive; if none, tie
                var alive = snakes.Where((s, idx) => !dead[idx]).ToList();
                string winner = alive.Count > 0 ? alive[0].Name : null;
                return new GameResult(true, winner);
            }
        }

        var appleEatenBy = -1;
        for (int i = 0; i < snakes.Count; i++)
        {
            if (nextHeads[i].Equals(Apple))
            {
                appleEatenBy = i;
                break; // if both somehow move onto apple, first in list wins the apple
            }
        }

        // If a snake's next head is apple it grows, else normal move (append head, remove tail)
        for (int i = 0; i < snakes.Count; i++)
        {
            bool grow = (i == appleEatenBy);
            snakes[i].MoveForward(grow);
        }

        if (appleEatenBy >= 0)
        {
            MoveApple();
        }

        return new GameResult(false, null); // not finished
    }

    // Draw board to console
    public void Draw()
    {
        Console.Clear();

        int w = Width;
        int h = Height;

        // Draw corners
        Console.SetCursorPosition(0, 0);               Console.Write("┌");
        Console.SetCursorPosition(w + 1, 0);           Console.Write("┐");
        Console.SetCursorPosition(0, h + 1);           Console.Write("└");
        Console.SetCursorPosition(w + 1, h + 1);       Console.Write("┘");

        // Top and bottom borders
        for (int c = 1; c <= w; c++)
        {
            Console.SetCursorPosition(c, 0);
            Console.Write("─");

            Console.SetCursorPosition(c, h + 1);
            Console.Write("─");
        }

        // Left and right borders
        for (int r = 1; r <= h; r++)
        {
            Console.SetCursorPosition(0, r);
            Console.Write("│");

            Console.SetCursorPosition(w + 1, r);
            Console.Write("│");
        }

        // Draw apple
        Console.SetCursorPosition(Apple.Column + 1, Apple.Row + 1);
        Console.Write(AppleChar);

        // Draw snakes
        foreach (var s in snakes)
        {
            foreach (var cell in s.GetCells())
            {
                Console.SetCursorPosition(cell.Column + 1, cell.Row + 1);
                Console.Write(s.DisplayChar);
            }
        }

        // Move cursor below board
        Console.SetCursorPosition(0, h + 2);
    }
}