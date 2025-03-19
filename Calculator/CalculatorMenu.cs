using Calculator.Service;
using System;

namespace Calculator
{
    public class CalculatorMenu
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorMenu(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                string[] options = {
                    "1. Perform a Calculation",
                    "2. View All Calculations",
                    "3. Update a Calculation",
                    "4. Delete a Calculation",
                    "5. Exit"
                };

                int choice = DisplayMenu("Calculator Menu", options);

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("You chose to perform a new calculation.");
                        _calculatorService.PerformCalculation();
                        break;
                    case 2:
                        Console.WriteLine("You chose to view all calculations.");
                        _calculatorService.ShowAllCalculations();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("You chose to update a calculation.");
                        _calculatorService.UpdateCalculationById();
                        break;
                    case 4:
                        Console.WriteLine("You chose to delete a calculation.");
                        _calculatorService.DeleteCalculationById();
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
                        currentIndex = currentIndex == 0 ? options.Length - 1 : currentIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        currentIndex = currentIndex == options.Length - 1 ? 0 : currentIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        return currentIndex + 1;
                }
            }
        }
    }
}
