using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubmitOrders
{
    class Program
    {
        static void Main()
        {
           
            #region Adding Concurrency
            //var ordersQueue = new Queue<string>();
            //PlaceOrders(ordersQueue, "Cody", 5);
            //PlaceOrders(ordersQueue, "Connor", 5);
            var ordersQueue = new ConcurrentQueue<string>();
            Task task1 = Task.Run( () => PlaceOrders(ordersQueue, "Cody", 5)); 
            Task task2 = Task.Run( () => PlaceOrders(ordersQueue, "Connor", 5));

            Task.WaitAll(task1, task2);
            #endregion
            foreach (var order in ordersQueue)
            {
                Console.WriteLine("ORDER : " + order);
            }
        }

        private static void PlaceOrders(ConcurrentQueue<string> orders, string customerName, int nOrders)
        {
            for (int i = 0; i < nOrders; i++)
            {
                Thread.Sleep(new TimeSpan(1));
                string orderName = $"{customerName} wants t-shirt {i}";
                orders.Enqueue(orderName);
            }
        }
    }
}
