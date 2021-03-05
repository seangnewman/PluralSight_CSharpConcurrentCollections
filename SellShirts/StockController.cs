using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SellShirts
{
    public enum SelectResult { Success, NoStockLeft, ChosenShirtSold}
    internal class StockController
    {
        private readonly ConcurrentDictionary<string, TShirt> _stock;

        public StockController(IEnumerable<TShirt> shirts)
        {
            _stock = new ConcurrentDictionary<string, TShirt>(shirts.ToDictionary(x => x.Code));
        }

        public bool Sell(string code)
        {
            return _stock.TryRemove(code, out TShirt shirtRemoved);
        }

        public void DisplayStock()
        {
            Console.WriteLine($"\r\n{_stock.Count} items left in stock: ");
            foreach (var shirt in _stock.Values)
            {
                Console.WriteLine(shirt);
            }
        }

        public (SelectResult Result, TShirt Shirt) SelectRandomShirt()
        {
            var keys = _stock.Keys.ToList();
            if (keys.Count == 0)
            {
                return (SelectResult.NoStockLeft, null);                                // All shirts sold
            }
            Thread.Sleep(Rnd.NextInt(10));
            string selectedCode = keys[Rnd.NextInt(keys.Count)];
            bool found = _stock.TryGetValue(selectedCode, out TShirt shirt);
            return found ? (SelectResult.Success, shirt) : (SelectResult.ChosenShirtSold, null);
            //return _stock[selectedCode];
        }


    }
}