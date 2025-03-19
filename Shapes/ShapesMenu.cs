using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using MyClassLibrary.Models;
using Shapes.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class ShapesMenu
    {
        private readonly IShapesService _shapesService;

        public ShapesMenu(IShapesService dbContext)
        {
            _shapesService = dbContext;
        }

       public  void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                string[] options = {
                        "1. Calculate New Shape",
                        "2. View All Shapes",
                        "3. Update a Shape",
                        "4. Delete a Shape",
                        "5. Exit"
                    };

                int choice = DisplayMenu("Main Menu", options);

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("You chose to calculate a new shape.");
                        _shapesService.CalculateShape();
                        break;
                    case 2:
                        Console.WriteLine("You chose to view all shapes.");
                        _shapesService.ShowAllShapes();
                        break;
                    case 3:
                        Console.WriteLine("You chose to update a shape.");
                        _shapesService.UpdateShapeById();
                        break;
                    case 4:
                        Console.WriteLine("You chose to delete a shape.");
                        _shapesService.DeleteShapeById();
                        break;
                    case 5:
                        Console.WriteLine("Exiting... Goodbye!");
                        return;
                }
            }
        }

        int DisplayMenu(string title, string[] options)
        {
            int currentIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"--- {title} ---\n");

                // Display options with hover effect
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == currentIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {options[i]}");
                    }
                }

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        currentIndex = (currentIndex == 0) ? options.Length - 1 : currentIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        currentIndex = (currentIndex == options.Length - 1) ? 0 : currentIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        return currentIndex + 1;
                }
            }
        }
    }
}
