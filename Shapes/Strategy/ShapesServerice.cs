using ClassLiberry;
using MyClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Strategy
{
    internal class ShapesServerice : IShapesService
    {


        public ApplicationDbContext _dbContext { get; set; }
        public ShapesServerice()
        {
            _dbContext = new ApplicationDbContext();
        }
        public void CalculateShape()
        {
            var context = new Context();
                    context.SetStrategy(new RectangleStrategy());
                    Console.WriteLine("Vad är bredden på din rektangel?");
                    var input1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("... och vad är höjden på din rektangel?");
                    var input2 = Convert.ToInt32(Console.ReadLine());
                    var input3 = 0; // Används EJ!
                    var showResult = context.ExecuteStrategy(input1, input2, input3);
                    Console.WriteLine($"Rektangel: Bredd: {input1}, Höjd: {input2} Area = {showResult.Area}");
                    Console.WriteLine($"Rektangel: Bredd: {input1}, Höjd: {input2} Omkrets = {showResult.Perimiter}");

                    // Nu sparar vi all data till Db :)
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
                    Console.WriteLine("Tryck på valfri tangent för att gå vidare.");
                    Console.ReadLine();
                
                    Console.WriteLine("Inte ska ni få ALL kod! :)");
                    Console.ReadLine();
                
            }
        }
    }

