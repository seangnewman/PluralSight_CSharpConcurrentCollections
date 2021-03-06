using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SellShirts
{
    public enum SelectResult { Success, NoStockLeft, ChosenShirtSold}
    public class StockController
    {

        private readonly ConcurrentDictionary<string, int> _stock = new ConcurrentDictionary<string, int>();
        int _totalQuantityBought;
        int _totalQuantitySold;

        public void BuyShirts(string code, int quantityToBuy)
        {
            _stock.AddOrUpdate(code, quantityToBuy, (key, oldValue) => oldValue + quantityToBuy);
            Interlocked.Add(ref _totalQuantityBought, quantityToBuy);
            
        }


        public bool TrySellShirt(string code)
        {
            bool success = false;
            int newStockLevel = _stock.AddOrUpdate(code,
                                                                                   (itemName) => { success = false; return 0; },        // set success value to false
                                                                                   (itemName, oldValue) =>
                                                                                   {
                                                                                       if (oldValue == 0)                   //
                                                                                       {
                                                                                           success = false;                     // We were not able to sell item
                                                                                           return 0;
                                                                                       }
                                                                                       else
                                                                                       {
                                                                                           success = true;
                                                                                           return oldValue - 1;             // We were  able to sell item
                                                                                       }
                                                                                   });
            if (success)
            {
                Interlocked.Increment(ref _totalQuantitySold);
            }

            return success;
        }

        public void DisplayStock()
        {
            Console.WriteLine($"Stock levels by item: ");

            foreach (var shirt in TShirtProvider.AllShirts)
            {
                //TryGetValue defaults a 0 if no value found
                int stockLevel = _stock.GetOrAdd(shirt.Code, 0);                            // GetOrAdd ensures item is in dictionary
                Console.WriteLine($"{shirt.Name, -30}: {stockLevel}");
            }

            int totalStock = _stock.Values.Sum();
            Console.WriteLine($"\r\nBought = {_totalQuantityBought}");
            Console.WriteLine($"Sold = {_totalQuantitySold}");
            Console.WriteLine($"Stock = {totalStock}");
            int error = totalStock + _totalQuantitySold - _totalQuantityBought;


            // Check to see if there are any corruption has occurred
            if (error == 0)
            {
                Console.WriteLine("Stock levels match");
            }
            else
            {
                Console.WriteLine($"Error in stock level: {error}");
            }
            
        }


    }
}