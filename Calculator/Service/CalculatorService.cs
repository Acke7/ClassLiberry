using ClassLiberry;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCalculator.Strategy;
using MyClassLibrary.Models;

namespace EasyCalculator.Service
{
    public class CalculatorService : ICalculatorService
    {
        public ApplicationDbContext _dbContext { get; set; }

        public CalculatorService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void PerformCalculation()
        {
            var context = new Context();

            Console.WriteLine("Välj operation: (+, -, *, /, √, %)");
            string operation = Console.ReadLine();

            switch (operation)
            {
                case "+":
                    context.SetStrategy(new AdditionStrategy());
                    break;
                case "-":
                    context.SetStrategy(new SubtractionStrategy());
                    break;
                case "*":
                    context.SetStrategy(new MultiplicationStrategy());
                    break;
                case "/":
                    context.SetStrategy(new DivisionStrategy());
                    break;
                case "√":
                    context.SetStrategy(new SquareRootStrategy());
                    break;
                case "%":
                    context.SetStrategy(new ModulusStrategy());
                    break;
                default:
                    Console.WriteLine("Ogiltig operation.");
                    return;
            }

            Console.WriteLine("Ange första talet:");
            var operand1 = Convert.ToDouble(Console.ReadLine());

            double operand2 = 0;
            if (operation != "√")
            {
                Console.WriteLine("Ange andra talet:");
                operand2 = Convert.ToDouble(Console.ReadLine());
            }

            var result = context.ExecuteStrategy(operand1, operand2);
            Console.WriteLine($"Resultat: {operand1} {operation} {operand2} = {result}");


            _dbContext.Calculations.Add(new CalculationData
            {
                Operand1 = operand1,
                Operand2 = operand2,
                Operator = operation,
                Result = result,
                Date = DateTime.Now
            });


            _dbContext.SaveChanges();
            Console.WriteLine("Beräkningen har sparats!");
            Console.WriteLine("Tryck på valfri tangent för att gå vidare.");
            Console.ReadLine();
        }


        public void ShowAllCalculations()
        {
            var calculations = _dbContext.Calculations.ToList();
            foreach (var calc in calculations)
            {
                Console.WriteLine($"ID: {calc.Id}, {calc.Operand1} {calc.Operator} {calc.Operand2} = {calc.Result}, Datum: {calc.Date}");
            }
        }


        public void DeleteCalculationById()
        {
            ShowAllCalculations();
            Console.WriteLine("Ange ID för beräkningen du vill ta bort:");
            var id = Convert.ToInt32(Console.ReadLine());
            var calc = FindCalculationById(id);
            if (calc != null)
            {
                _dbContext.Calculations.Remove(calc);
                _dbContext.SaveChanges();
                Console.WriteLine("Beräkningen har tagits bort.");
            }
            else
            {
                Console.WriteLine("Beräkningen hittades inte.");
            }
        }


        public void UpdateCalculationById()
        {
            ShowAllCalculations();
            Console.WriteLine("Ange ID för beräkningen du vill uppdatera:");
            var id = Convert.ToInt32(Console.ReadLine());
            var calc = FindCalculationById(id);
            if (calc != null)
            {
                Console.WriteLine("Ange nytt första tal:");
                calc.Operand1 = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Ange nytt andra tal:");
                calc.Operand2 = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Välj ny operation: (+, -, *, /, √, %)");
                string operation = Console.ReadLine();

                var context = new Context();
                switch (operation)
                {
                    case "+":
                        context.SetStrategy(new AdditionStrategy());
                        break;
                    case "-":
                        context.SetStrategy(new SubtractionStrategy());
                        break;
                    case "*":
                        context.SetStrategy(new MultiplicationStrategy());
                        break;
                    case "/":
                        context.SetStrategy(new DivisionStrategy());
                        break;
                    case "√":
                        context.SetStrategy(new SquareRootStrategy());
                        break;
                    case "%":
                        context.SetStrategy(new ModulusStrategy());
                        break;
                    default:
                        Console.WriteLine("Ogiltig operation.");
                        return;
                }

                calc.Operator = operation;
                calc.Result = context.ExecuteStrategy(calc.Operand1, calc.Operand2);
                calc.Date = DateTime.Now;

                _dbContext.SaveChanges();
                Console.WriteLine("Beräkningen har uppdaterats.");
            }
            else
            {
                Console.WriteLine("Beräkningen hittades inte.");
            }
        }


        public CalculationData FindCalculationById(int id)
        {
            var calc = _dbContext.Calculations.Find(id);
            if (calc != null)
            {
                Console.WriteLine($"ID: {calc.Id}, {calc.Operand1} {calc.Operator} {calc.Operand2} = {calc.Result}, Datum: {calc.Date}");
            }
            else
            {
                Console.WriteLine("Beräkningen hittades inte.");
            }
            return calc;
        }

        public void FindCalculationById()
        {
            throw new NotImplementedException();
        }


        


      
    }
}
