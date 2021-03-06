using System;
using System.Threading;

namespace SellShirts
{ 
    public class SalesPerson
    {
        public string Name { get; }

        public SalesPerson(string name)
        {
            this.Name = name;
        }

        internal void Work(TimeSpan workday, StockController controller, LogTradeQueue tradesQueue)
        {
            DateTime start = DateTime.Now;

            while (DateTime.Now - start < workday)
            {
                string msg = ServeCustomer(controller, tradesQueue);
                if(msg != null)
                    Console.WriteLine($"{Name}: {msg} ");
            }
        }

        public string ServeCustomer(StockController controller, LogTradeQueue tradesQueue)
        {
            Thread.Sleep(Rnd.NextInt(20));
            TShirt shirt = TShirtProvider.SelectRandomShirt();
            string code = shirt.Code;

            bool custSells = Rnd.TrueWithProb(1.0 / 6.0);

            if (custSells)
            {
                int quantity = Rnd.NextInt(9) + 1;
                controller.BuyShirts(code, quantity);
                tradesQueue.QueueTradeForLogging(new Trade(this, shirt, TradeType.Purchase, quantity));
                return $"Bought {quantity} of {shirt}";
            }
            else
            {
                bool success = controller.TrySellShirt(code);
                if (success)
                {
                    tradesQueue.QueueTradeForLogging(new Trade(this, shirt, TradeType.Sale, 1));
                    return $"Sold {shirt}";
                }
                else
                {
                    return $"Couldn't sell {shirt}: Out of stock";
                }
            }
        }
    }
}