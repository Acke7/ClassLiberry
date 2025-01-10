using ClassLiberry;
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
        
      
        public void ShowShapesMenu()
        {
            _shapesService.CalculateShape();
            //Console.Clear();
            //Console.WriteLine("Shapes Menu");
            //Console.WriteLine("===========");
            //Console.WriteLine("1: Rectangle");
            //Console.WriteLine("2: Triangel");
            //Console.WriteLine("0: Huvudmenyn");

            //var shapesMenuAnswer = Console.ReadLine();
            //if (shapesMenuAnswer == "0") return;
            
            //checkShapesMenuAnswer(shapesMenuAnswer);
        }

        //private void checkShapesMenuAnswer(string? menuChoice)
        //{
        //    var context = new Context();

        //    switch (menuChoice)
        //    {
        //        case "1":
        //            context.SetStrategy(new RectangleStrategy());
        //            Console.WriteLine("Vad är bredden på din rektangel?");
        //            var input1 = Convert.ToInt32(Console.ReadLine());
        //            Console.WriteLine("... och vad är höjden på din rektangel?");
        //            var input2 = Convert.ToInt32(Console.ReadLine());
        //            var input3 = 0; // Används EJ!
        //            var showResult = context.ExecuteStrategy(input1, input2, input3);
        //            Console.WriteLine($"Rektangel: Bredd: {input1}, Höjd: {input2} Area = {showResult.Area}");
        //            Console.WriteLine($"Rektangel: Bredd: {input1}, Höjd: {input2} Omkrets = {showResult.Perimiter}");

        //            // Nu sparar vi all data till Db :)
        //            _shapesService.shapeDatas.Add(new ShapeData
        //            {
        //                Input1 = input1,
        //                Input2 = input2,
        //                Input3 = input3,
        //                Area = showResult.Area,
        //                Perimeter = showResult.Perimiter,
        //                Date = DateTime.Now,
        //            });
        //            _shapesService.SaveChanges();
        //            Console.WriteLine("Tryck på valfri tangent för att gå vidare.");
        //            Console.ReadLine();
        //            break;
        //        case "2":
        //            Console.WriteLine("Inte ska ni få ALL kod! :)");
        //            Console.ReadLine();
        //            break;
        //        case "0":
        //        default:
        //            break;
        //    }
        //}
    }
}

