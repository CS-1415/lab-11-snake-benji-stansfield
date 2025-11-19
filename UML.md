class Cell
{
  +int Row
  +int Col
  +Cell(int row, int col)
}

class Board
{
  -int Width
  -int Height
  -Cell Apple
  -List<Snake> Snakes
  -Random Rand
  +Board(int width, int height, char appleChar)
  +AddSnake(Snake s) : void
  +MoveApple() : void
  +IsInside(Cell c) : bool
  +IsCellEmpty(Cell c) : bool
  +Step(ConsoleKey? key) : GameResult
  +Draw() : void
}

enum Direction
{
  Up
  Down
  Left
  Right
}

class Snake
{
  -string Name
  -List<Cell> Cells
  -Direction Dir
  -Board BoardRef
  -char DisplayChar
  -ConsoleColor Foreground
  +Snake(string name, Cell start, Direction dir, char displayChar, Board board)
  +TurnUp() : void
  +TurnDown() : void
  +TurnLeft() : void
  +TurnRight() : void
  +PeekNextHead() : Cell
  +MoveForward(bool grow) : void
  +GetCells() : List<Cell>
}

class GameResult
{
  +bool Finished
  +string WinnerName
  +GameResult(bool finished, string winnerName)
}

Board "1" o-- "*" Snake
Snake --> Board : Reference
