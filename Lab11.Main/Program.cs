namespace Lab11;

public class Program
{
    static void Main()
    {
        Console.Clear();
        List<IGraphic2DFactory> availableShapeTypes = new List<IGraphic2DFactory>()
        {
            new CircleFactory(),
            new RectangleFactory()
        };

        List<IGraphic2D> builtShapes = new List<IGraphic2D>();
        bool running = true;

        Console.WriteLine("Welcome to the drawing factory!\n");

        // Main menu
        while (running)
        {
            Console.WriteLine(@"1 - Display Drawing
2 - Add Graphic
3 - Remove Graphic
4 - Exit Program");
            Console.Write("Please select an option from above: ");

            string input = Console.ReadLine();
            if (!int.TryParse(input, out int choice)) // turns the string entered into an int for the switch
            {
                Console.WriteLine("Please enter a valid option.");
                continue;
            }

            switch (choice)
            {
                case 1: // Display drawing
                    Console.Clear();
                    AbstractGraphic2D.Display(builtShapes);
                    Console.WriteLine("\nPress any key to return to the menu.");
                    Console.ReadKey(true);
                    break;

                case 2: // Add graphic
                    Console.Clear();

                    // Checks if there are any current factories in availableShapeTypes
                    if (availableShapeTypes.Count == 0)
                    {
                        Console.WriteLine("Cannot find any shape factories.");
                        Console.WriteLine("Press any key to return to the menu.");
                        Console.ReadKey(true);
                        break;
                    }

                    // Lists each factory by name
                    Console.WriteLine("Available shapes:");
                    for (int i = 0; i < availableShapeTypes.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}) {availableShapeTypes[i].Name}");
                    }

                    Console.Write("Enter the index of the shape you want to create: ");
                    string factoryIndexInput = Console.ReadLine()?.Trim();

                    // Checks if the user inputs a correct number
                    if (!int.TryParse(factoryIndexInput, out int factoryIndex) || factoryIndex < 1 || factoryIndex > availableShapeTypes.Count)
                    {
                        Console.WriteLine("Invalid index.");
                        Console.WriteLine("Press any key to return to the menu.");
                        Console.ReadKey(true);
                        break;
                    }

                    try
                    {
                        IGraphic2D newShape = availableShapeTypes[factoryIndex - 1].Create();
                        if (newShape != null)
                        {
                            builtShapes.Add(newShape);
                            Console.WriteLine("Shape added to drawing.");
                        }
                        else
                        {
                            Console.WriteLine("Factory returned null; nothing was added.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"An error occurred while creating the shape: {e.Message}");
                    }

                    Console.WriteLine("Press any key to return to the menu.");
                    Console.ReadKey(true);
                    break;

                case 3: // Remove graphic
                    Console.Clear();
                    int count = builtShapes.Count;
                    Console.WriteLine($"There are {builtShapes.Count} graphic objects in the drawing.");

                    // Checks if there are any shapes in the list
                    if (builtShapes.Count == 0)
                    {
                        Console.WriteLine("Press any key to return to the main menu.");
                        Console.ReadKey(true);
                        break;
                    }

                    // Show name, size, and location of each shape
                    Console.WriteLine("Current objects:");
                    for (int i = 0; i < builtShapes.Count; i++)
                    {
                        // If IGraphic2D has a meaningful ToString() or a Name property show it; otherwise show type
                        var obj = builtShapes[i];
                        string description = obj?.ToString() ?? "(null)";
                        Console.WriteLine($"{i + 1}) {description}");
                    }

                    Console.Write("Enter the index of the graphic to remove: ");
                    string removeIndexInput = Console.ReadLine()?.Trim();

                    // Checks if the user put in a correct number
                    if (!int.TryParse(removeIndexInput, out int removeIndex) || removeIndex < 1 || removeIndex > builtShapes.Count)
                    {
                        Console.WriteLine("Invalid index.");
                        Console.WriteLine("Press any key to return to the menu.");
                        Console.ReadKey(true);
                        break;
                    }

                    builtShapes.RemoveAt(removeIndex - 1);
                    Console.WriteLine("Graphic removed.");
                    Console.WriteLine("Press any key to return to the menu.");
                    Console.ReadKey(true);
                    break;

                case 4:
                    running = false;
                    break;

                default:
                    Console.WriteLine("Sorry, that is not an acceptable option.");
                    break;
            }

            Console.Clear();
        }

    }
}
