namespace Lab11;

public static class Program
{
    public static void Main()
    {
        // make sure window/buffer can fit
        int width = 40;
        int height = 20;
        try
        {
            Console.CursorVisible = false;
            if (Console.BufferWidth < width || Console.BufferHeight < height + 3)
            {
                // try to set size (may fail in some consoles)
                Console.SetWindowSize(Math.Min(width + 2, Console.LargestWindowWidth), Math.Min(height + 5, Console.LargestWindowHeight));
                Console.SetBufferSize(Math.Max(width + 2, Console.BufferWidth), Math.Max(height + 5, Console.BufferHeight));
            }
        }
        catch(Exception) { };

        var board = new Board(width, height, '@');

        // create two snakes
        var s1 = new Snake("Player 1 (WASD)", new Cell(height / 2, 5), Direction.Right, 'O', board);
        var s2 = new Snake("Player 2 (Arrows)", new Cell(height / 2, width - 6), Direction.Left, 'X', board);
        board.AddSnake(s1);
        board.AddSnake(s2);

        /*Intro Screen*/
        Console.Clear();
        Console.WriteLine("=== Two-Player Snake ===");
        Console.WriteLine("Controls:");
        Console.WriteLine(" Player 1: W (up), A (left), S (down), D (right)");
        Console.WriteLine(" Player 2: Arrow keys");
        Console.WriteLine("Press any key to advance both snakes. If you hit your opponent's tail or a wall, your opponent wins.");
        Console.WriteLine("Eat the apple (@) to grow. Press Esc to quit.");
        Console.WriteLine("Press any key to start...");
        Console.ReadKey(true);

        var finished = false;
        string winner = null;

        board.Draw();
        while (!finished)
        {
            ConsoleKeyInfo keyInfo;
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape) break;
                var result = board.Step(keyInfo.Key);
                if (result.Finished)
                {
                    finished = true;
                    winner = result.Winner;
                }
            }
            else
            {
                Thread.Sleep(50);
                continue;
            }

            board.Draw();
        }

        Console.SetCursorPosition(0, Math.Min(board.Height + 1, Console.BufferHeight - 1));
        if (winner == null)
        {
            Console.WriteLine("Game ended in a tie.");
        }
        else
        {
            Console.WriteLine($"Game over. Winner: {winner}");
        }

        // Show final snake lengths if multi-player optional not done
        foreach (var s in board.Snakes)
        {
            Console.WriteLine($"{s.Name} length: {s.GetCells().Count}");
        }

        Console.WriteLine("Press any key to exit.");
        Console.CursorVisible = true;
        Console.ReadKey(true);
    }
}
