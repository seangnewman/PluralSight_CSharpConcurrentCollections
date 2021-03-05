using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellShirts
{
    class Program
    {
        static void Main(string[] args)
        {
            StockController controller = new StockController(TShirtProvider.AllShirts);
            TimeSpan workday = new TimeSpan(0, 0, 0, 0, 500);

            Task task1 = Task.Run( () =>  new SalesPerson("Kim").Work(workday, controller));
            Task task2 = Task.Run(() =>  new SalesPerson("Sahil").Work(workday, controller));
            Task task3 = Task.Run(() => new SalesPerson("Chuck").Work(workday, controller));

            Task.WaitAll(task1, task2, task3);

            controller.DisplayStock();
        }
    }
}
