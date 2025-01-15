using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace EasyCalculator.Service
{
    public interface ICalculatorService
    {
        void PerformCalculation();         // Utför en ny beräkning och sparar den
        void ShowAllCalculations();        // Visar alla sparade beräkningar
        void DeleteCalculationById();      // Tar bort en beräkning baserat på ID
        void UpdateCalculationById();      // Uppdaterar en befintlig beräkning
        void FindCalculationById();        // Hittar och visar en specifik beräkning
    }
}
