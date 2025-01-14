using ClassLiberry;
using MyClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Strategy
{
    public class ShapeService : IShapesService
    {


        public ApplicationDbContext _dbContext { get; set; }
        public ShapeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CalculateShape()
        {
            int choice = DisplayMenuWithHoverArrow();

            // Strategy selection based on user's choice
            IStrategy selectedStrategy;
            switch (choice)
            {
                case 1:
                    selectedStrategy = new RectangleStrategy();
                    break;
                case 2:
                    selectedStrategy = new TriangelStrategy();
                    break;
                case 3:
                    selectedStrategy = new RhombusStrategy();
                    break;
                case 4:
                    selectedStrategy = new ParallelogramStrategy();
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    return;
            }

            var context = new Context();
            context.SetStrategy(selectedStrategy);

            // Collect inputs for the chosen shape
            Console.WriteLine("Ange den första parametern (t.ex. bredd, bas):");
            var input1 = GetValidIntegerInput();

            Console.WriteLine("Ange den andra parametern (t.ex. höjd):");
            var input2 = GetValidIntegerInput();

            int input3 = 0;
            if (choice == 2) // For Triangle, optionally ask for a third input
            {
                Console.WriteLine("Ange den tredje parametern (t.ex. längd på en sida):");
                input3 = GetValidIntegerInput();
            }

            var showResult = context.ExecuteStrategy(input1, input2, input3);

            Console.WriteLine($"Form: {selectedStrategy.GetType().Name}");
            Console.WriteLine($"Input1: {input1}, Input2: {input2}, Input3: {input3}");
            Console.WriteLine($"Area: {showResult.Area}");
            Console.WriteLine($"Omkrets: {showResult.Perimiter}");

            // Save data to the database
            _dbContext.shapeDatas.Add(new ShapeData
            {
                Input1 = input1,
                Input2 = input2,
                Input3 = input3,
                Area = showResult.Area,
                Perimeter = showResult.Perimiter,
                Date = DateTime.Now,
            });
            _dbContext.SaveChanges();

            Console.WriteLine("Data sparat i databasen. Tryck på valfri tangent för att fortsätta.");
            Console.ReadLine();
        }

       
        private int DisplayMenuWithHoverArrow()
        {
            string[] options = { "Rektangel", "Triangel", "Romb", "Parallellogram" };
            int selectedIndex = 0;

            ConsoleKey key;
            do
            {
                Console.Clear();
                Console.WriteLine("Välj vilken form du vill beräkna:");
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                        Console.WriteLine($"> {options[i]}"); // Hover arrow
                    else
                        Console.WriteLine($"  {options[i]}");
                }

                key = Console.ReadKey(true).Key;

                // Navigate through options
                if (key == ConsoleKey.UpArrow)
                    selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                else if (key == ConsoleKey.DownArrow)
                    selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;

            } while (key != ConsoleKey.Enter);

            return selectedIndex + 1; // Convert index to choice (1-based)
        }

        /// <summary>
        /// Gets a valid integer input from the user, prompting until valid.
        /// </summary>
        private int GetValidIntegerInput()
        {
            int result;
            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out result) && result > 0)
                    return result;

                Console.WriteLine("Ogiltig inmatning. Ange ett giltigt heltal större än 0:");
            }
        }

        public void ShowAllShapes()
        {
            var shapes = _dbContext.shapeDatas.ToList();
            foreach (var shape in shapes)
            {
                Console.WriteLine($"ID: {shape.Id}, Bredd: {shape.Input1}, Höjd: {shape.Input2}, Area: {shape.Area}, Omkrets: {shape.Perimeter}, Datum: {shape.Date}");
            }
        }

        public void DeleteShapeById()
        {
            ShowAllShapes();
            Console.WriteLine("Ange ID för formen du vill ta bort:");
            var id = Convert.ToInt32(Console.ReadLine());
            var shape = FindShapeById(id);
            if (shape != null)
            {
                _dbContext.shapeDatas.Remove(shape);
                _dbContext.SaveChanges();
                Console.WriteLine("Formen har tagits bort.");
            }
            else
            {
                Console.WriteLine("Formen hittades inte.");
            }
        }

        public void UpdateShapeById()
        {
            ShowAllShapes();
            Console.WriteLine("Ange ID för formen du vill ta bort:");
            var id = Convert.ToInt32(Console.ReadLine());
            var shape = FindShapeById(id);
            if (shape != null)
            {
                Console.WriteLine("Ange ny bredd:");
                shape.Input1 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Ange ny höjd:");
                shape.Input2 = Convert.ToDouble(Console.ReadLine());
                shape.Area = shape.Input1 * shape.Input2; // Assuming rectangle
                shape.Perimeter = 2 * (shape.Input1 + shape.Input2); // Assuming rectangle
                _dbContext.SaveChanges();
                Console.WriteLine("Formen har uppdaterats.");
            }
            else
            {
                Console.WriteLine("Formen hittades inte.");
            }
        }
        public ShapeData FindShapeById(int id)
        {
            var shape = _dbContext.shapeDatas.Find(id);
            if (shape != null)
            {
                Console.WriteLine($"ID: {shape.Id}, Bredd: {shape.Input1}, Höjd: {shape.Input2}, Area: {shape.Area}, Omkrets: {shape.Perimeter}, Datum: {shape.Date}");
            }
            else
            {
                Console.WriteLine("Formen hittades inte.");
            }
            return shape;
        }

    }
}

