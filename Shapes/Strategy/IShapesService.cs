using MyClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Strategy
{
    public interface IShapesService
    {
        public void CalculateShape();
        public void ShowAllShapes();
        public void DeleteShapeById();
        public void UpdateShapeById();
        public ShapeData FindShapeById(int id);
    }
}
