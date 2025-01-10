﻿
using Shapes;
using Shapes.Strategy;

namespace MainMenu
{
    public class app
    {
        // Oh no!! The attack of the concretions!!
        // A Factory sure would be nice here :)
        public IShapesService CalcServices { get; set; } = new ShapesServerice();
        

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
                case "1": // Calculator
                    var gotoProject1 = new ShapesMenu(CalcServices);
                    gotoProject1.MainMenu();
                    break;
                //case "2": // Shapes
                //    var gotoProject2 = new Project2App(FormsServices);
                //    gotoProject2.Project2ShowMenu();
                //    break;
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
