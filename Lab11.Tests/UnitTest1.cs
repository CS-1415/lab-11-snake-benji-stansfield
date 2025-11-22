namespace Lab11.Tests;

// Circle Tests
public class CircleTests
{
    Circle circle;
    AbstractGraphic2D shape;

    [SetUp]
    public void Setup()
    {
        // should be x, y, and radius
        circle = new Circle(8, 10, 2);

        // should extend the abstract class
        shape = circle;
    }

    [Test]
    public void CircleHasCorrectDimensions()
    {
        Assert.AreEqual(8, circle.CenterX);
        Assert.AreEqual(10, circle.CenterY);
        Assert.AreEqual(2, circle.Radius);
    }

    [Test]
    public void HasCorrectBoundingBox()
    {
        Assert.AreEqual(8 - 2, shape.LowerBoundX);
        Assert.AreEqual(10 - 2, shape.LowerBoundY);
        Assert.AreEqual(8 + 2, shape.UpperBoundX);
        Assert.AreEqual(10 + 2, shape.UpperBoundY);
    }

    [Test]
    public void CenterIsIncluded()
    {
        Assert.IsTrue(shape.ContainsPoint(8, 10));
    }

    [Test]
    public void ContainsAllFourPointsOfTheCompass()
    {
        Assert.IsTrue(shape.ContainsPoint(8 - 2, 10));
        Assert.IsTrue(shape.ContainsPoint(8 + 2, 10));
        Assert.IsTrue(shape.ContainsPoint(8, 10 - 2));
        Assert.IsTrue(shape.ContainsPoint(8, 10 + 2));
    }

    [Test]
    public void ShouldNotContainFourCorners()
    {
        Assert.IsFalse(shape.ContainsPoint(8 - 2, 10 - 2));
        Assert.IsFalse(shape.ContainsPoint(8 + 2, 10 - 2));
        Assert.IsFalse(shape.ContainsPoint(8 - 2, 10 + 2));
        Assert.IsFalse(shape.ContainsPoint(8 + 2, 10 + 2));
    }
}

// Rectangle Tests
public class RectangleTests
{
    Rectangle rectangle;
    AbstractGraphic2D shape;

    [SetUp]
    public void Setup()
    {
        // should be top, left, width, and height
        rectangle = new Rectangle(3, 4, 5, 6);

        // should extend the abstract class
        shape = rectangle;
    }

    [Test]
    public void EnsurePropertiesAreCorrect()
    {
        Assert.AreEqual(3, rectangle.Left);
        Assert.AreEqual(4, rectangle.Top);
        Assert.AreEqual(5, rectangle.Width);
        Assert.AreEqual(6, rectangle.Height);
    }

    [Test]
    public void CheckLowerBounds()
    {
        // lower bound is the smallest x that needs to be checked when drawing the shape
        Assert.AreEqual(3, shape.LowerBoundX);
        Assert.AreEqual(4, shape.LowerBoundY);
    }

    [Test]
    public void CheckUpperBounds()
    {
        // upper bound is the largest x that needs to be checked when drawing the shape
        Assert.AreEqual(3 + 5, shape.UpperBoundX);
        Assert.AreEqual(4 + 6, shape.UpperBoundY);
    }

    [Test]
    public void MiddleOfShapeIsIncluded()
    {
        Assert.IsTrue(shape.ContainsPoint(5.5m, 7));
    }

    [Test]
    public void CornersIncluded()
    {
        Assert.IsTrue(shape.ContainsPoint(3, 4));
        Assert.IsTrue(shape.ContainsPoint(8, 4));
        Assert.IsTrue(shape.ContainsPoint(3, 10));
        Assert.IsTrue(shape.ContainsPoint(8, 10));
    }

    [Test]
    public void OutsideOfCornersNotIncludedInShape()
    {
        Assert.IsFalse(shape.ContainsPoint(3 - 0.1m, 4));
        Assert.IsFalse(shape.ContainsPoint(8, 4 - 0.1m));
        Assert.IsFalse(shape.ContainsPoint(3, 10 + 0.1m));
        Assert.IsFalse(shape.ContainsPoint(8 + 0.1m, 10));
    }
}

// Game Tests
public class SnakeGameTests
{
    [Test]
    public void CellEqualityAndConstruction()
    {
        var a = new Cell(1, 2);
        var b = new Cell(1, 2);
        Assert.AreEqual(a, b);
    }
}

// Snake Class Tests
public class SnakeTests
{
    [Test]
    public void TurnAndMoveForward()
    {
        var board = new Board(10, 10, 'A');
        var s = new Snake("P1", new Cell(5, 5), Direction.Right, 'O', board);
        board.AddSnake(s);

        s.TurnUp();
        Assert.AreEqual(Direction.Up, s.Direction);

        // Move forward one - should remove tail and add head
        var oldCells = s.GetCells().ToList();
        s.MoveForward(grow: false);
        Assert.AreEqual(oldCells.Count, s.GetCells().Count);
        Assert.AreNotEqual(oldCells.Last(), s.GetCells().Last());
    }

    [Test]
    public void EatAppleGrows()
    {
        var board = new Board(10, 10, 'A');
        var snake = new Snake("P1", new Cell(2, 2), Direction.Right, 'O', board);
        board.AddSnake(snake);

        board.Apple = new Cell(2, 3); // apple directly in front
        var result = board.Step(null); // press any key, no direction change

        // step returns GameResult; check snake grew
        Assert.Greater(snake.GetCells().Count, 1);
        Assert.AreNotEqual(board.Apple, new Cell(2,3)); // apple moved
    }
}

// Board Class Tests
public class BoardTests
{
    [Test]
    public void DetectCollisionWithOpponentTail()
    {
        var board = new Board(10, 10, 'A');
        var s1 = new Snake("P1", new Cell(5, 5), Direction.Right, 'O', board);
        var s2 = new Snake("P2", new Cell(5, 7), Direction.Left, 'X', board);
        board.AddSnake(s1);
        board.AddSnake(s2);

        s2.CellsSetForTest(new System.Collections.Generic.List<Cell> { new Cell(5, 6), new Cell(5,7) });

        var result = board.Step(null); // both move
        Assert.IsTrue(result.Finished && result.Winner == "P2");
    }

    [Test]
    public void BoardMoveAppleToEmptyCell()
    {
        var board = new Board(5, 5, 'A');
        var snake = new Snake("P1", new Cell(0, 0), Direction.Right, 'O', board);
        board.AddSnake(snake);
        board.MoveApple();
        Assert.IsTrue(board.IsInside(board.Apple));
        Assert.IsTrue(board.IsCellEmpty(board.Apple));
    }
}
