using Shapes;
using Shapes.Strategy;
using ClassLibrary;
using MyClassLibrary;
using Calculator.Service;
using Calculator;
using Calculator;
using Rps.Service;
using Rps;
using Rps.Service;
using System.ComponentModel;
using Autofac;
using ClassLibrary;



namespace MainMenu
{
    public class App
    {
        private Autofac.IContainer _container;
        public Build Build;
    
        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Build = new Build();
            Build.BuildDb();

            var builder = new ContainerBuilder();
            builder.RegisterType<ApplicationDbContext>().AsSelf().SingleInstance();
            builder.RegisterType<ShapeService>().As<IShapesService>();
            builder.RegisterType<CalculatorService>().As<ICalculatorService>();
            builder.RegisterType<RPCService>().As<IRPCService>();

            _container = builder.Build();

            using (var scope = _container.BeginLifetimeScope())
            {
                var shapesService = scope.Resolve<IShapesService>();
                var calculatorService = scope.Resolve<ICalculatorService>();
                var rPCService = scope.Resolve<IRPCService>();

                ShowMainMenu(shapesService, calculatorService, rPCService);
            }
        }

        public void ShowMainMenu(IShapesService shapesService, ICalculatorService calculatorService, IRPCService rPCService)
        {
            int selectedIndex = 0;
            string[] options = { "Shape Calculation ", "Numbers Calculator ", "Rps Game ", "Exit" };

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Welcome");
                Console.WriteLine("===============");
                Console.ResetColor();

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
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
                        ReadResponse((selectedIndex + 1).ToString(), shapesService, calculatorService, rPCService);
                        break;
                }
            }
        }

        public void ReadResponse(string resp, IShapesService shapesService, ICalculatorService calculatorService, IRPCService rPCService)
        {
            switch (resp)
            {
                case "1":
                    var shapesMenu = new ShapesMenu(shapesService);
                    shapesMenu.MainMenu();
                    break;
                case "2":
                    var calculatorMenu = new CalculatorMenu(calculatorService);
                    calculatorMenu.MainMenu();
                    break;
                case "3":
                    var rpcMenu = new RPCMenu(rPCService);
                    rpcMenu.MainMenu();
                    break;
                default:
                    break;
            }
        }
    }
}
