using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellShirts
{
    class Program
    {
        static void Main()
        {
            StockController controller = new StockController();
            TimeSpan workday = new TimeSpan(0, 0, 0, 0, 500);

            StaffRecords staffLogs = new StaffRecords();
            LogTradeQueue tradesQueue = new LogTradeQueue(staffLogs);

            SalesPerson[] staff =
            {
                new SalesPerson("Sahil"),
                new SalesPerson("Julie"),
                new SalesPerson("Kim"),
                new SalesPerson("Chuck")

            };
            List<Task> salesTasks = new List<Task>();

            foreach (var person in staff)
            {
                salesTasks.Add(Task.Run( ()=>
                        person.Work(workday, controller, tradesQueue
                    ))); ;
            }

            Task[] loggingTasks =
            {
                Task.Run(() => tradesQueue.MonitorAndLogTrades()),
                Task.Run(() => tradesQueue.MonitorAndLogTrades()),
            };

            Task.WaitAll(salesTasks.ToArray());   // Wait for all trades to complete
            tradesQueue.SetNoMoreTrades();      // Indicate no more trades to be completed
            Task.WaitAll(loggingTasks);               // Wait for logging to complete


            controller.DisplayStock();
            staffLogs.DisplayCommissions(staff);
        }
    }
}
