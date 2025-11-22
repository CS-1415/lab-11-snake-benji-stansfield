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
    
}
