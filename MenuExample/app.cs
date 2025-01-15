
using Shapes;
using Shapes.Strategy;
using ClassLiberry;
using MyClassLibrary;
using EasyCalculator.Service;
using Calculator;
using EasyCalculator;

namespace MainMenu
{
    public class app
    {
        // Oh no!! The attack of the concretions!!
        // A Factory sure would be nice here :)
     
        private ApplicationDbContext _dbContext;
        public void Run()
        {
            var build = new Build();
            _dbContext = build.BuildDb();
            ShowMainMenu();

        }
        public void ShowMainMenu()
        {
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome");
                Console.WriteLine("===============");
                Console.WriteLine("1: Shape Calculation 1");
                Console.WriteLine("2: Project 2");
                Console.WriteLine("3: Project 3");
                Console.WriteLine("0: Exit");

                var response = Console.ReadLine();
                if (response == "0")
                {
                    break;
                }
                else
                {
                    ReadResponse(response);
                }
            }
        }

        public void ReadResponse(string resp)
        {
            switch (resp)
            {
                case "1": 
                    var shapeService = new ShapeService(_dbContext);
                    var shapesMenu = new ShapesMenu(shapeService);
                    shapesMenu.MainMenu();
                    break;
                case "2": // Shapes
                    var calculatorService = new CalculatorService(_dbContext);
                    var calculatorMenu = new CalculatorMenu(calculatorService);
                    calculatorMenu.MainMenu();
                    break;
                //case "3": // Rock Paper Scissors
                //    var gotoProject3 = new Project3App(RPSServices);
                //    gotoProject3.Project3ShowMenu();
                //    break;
                default:
                    break;
            }
        }
    }
}
