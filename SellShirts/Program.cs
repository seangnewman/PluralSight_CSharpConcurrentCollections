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
            StockController controller = new StockController();
            TimeSpan workday = new TimeSpan(0, 0, 0, 0, 500);

            Task task1 = Task.Run(() => new SalesPerson("Tim").Work(workday, controller));
            Task task2 = Task.Run(() => new SalesPerson("Koffi").Work(workday, controller));
            Task task3 = Task.Run(() => new SalesPerson("Julie").Work(workday, controller));
            Task task4 = Task.Run(() => new SalesPerson("Michael").Work(workday, controller));

            Task.WaitAll(task1, task2, task3, task4);

            controller.DisplayStock();
        }
    }
}
