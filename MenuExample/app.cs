using Shapes;
using Shapes.Strategy;
using ClassLiberry;
using MyClassLibrary;
using EasyCalculator.Service;
using Calculator;
using EasyCalculator;
using RPC.Service;
using Rps;
using Rps.Service;

namespace MainMenu
{
    public class App
    {
        private ApplicationDbContext _dbContext;
        private IShapesService shapesService;
        private ICalculatorService calculatorService;
        private IRPCService rPCService;

        public void Run()
        {
            var build = new Build();
            _dbContext = build.BuildDb();
            shapesService = new ShapeService(_dbContext);
            calculatorService = new CalculatorService(_dbContext);
            rPCService = new RPCService(_dbContext);
            ShowMainMenu();
        }

        public void ShowMainMenu()
        {
            int selectedIndex = 0;
            string[] options = { "Shape Calculation →", "Numbers Calculator →", "RPC Game →", "Exit" };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome");
                Console.WriteLine("===============");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.WriteLine($"> {options[i]}");
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
                        selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
                        break;
                    case ConsoleKey.Enter:
                        if (selectedIndex == options.Length - 1)
                        {
                            return;
                        }
                        ReadResponse((selectedIndex + 1).ToString());
                        break;
                }
            }
        }

        public void ReadResponse(string resp)
        {
            switch (resp)
            {
                case "1":
                    var shapesMenu = new ShapesMenu(shapesService);
                    shapesMenu.MainMenu();
                    break;
                case "2": // Shapes
                    var calculatorMenu = new CalculatorMenu(calculatorService);
                    calculatorMenu.MainMenu();
                    break;
                case "3": // Rock Paper Scissors
                    var rpcMenu = new RPCMenu(rPCService);
                    rpcMenu.MainMenu();
                    break;
                default:
                    break;
            }
        }
    }
}
