using System.Runtime.CompilerServices;

namespace Lab11;

public class Board
{
    public int Width { get; }
    public int Height { get; }
    public Cell Apple { get; set; }
    public char AppleChar {get;}
    private List<Snake> snakes = new List<Snake>();

    public Board(int width, int height, char appleChar)
    {
        Width = width;
        Height = height;
        AppleChar = appleChar;
        MoveApple();
    }

    public void AddSnake(Snake s)
    {
        
    }

    public void MoveApple()
    {
        
    }
}