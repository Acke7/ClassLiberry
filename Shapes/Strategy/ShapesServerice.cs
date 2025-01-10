using ClassLiberry;
using MyClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Strategy
{
    public class ShapesServerice : IShapesService
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

