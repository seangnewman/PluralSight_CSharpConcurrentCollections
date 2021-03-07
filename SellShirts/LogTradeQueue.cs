using System;
using System.Collections.Concurrent;
using System.Threading;

namespace SellShirts
{
    public  class LogTradeQueue 
    {
        // BlockingCollection wraps (encapsulates) all producer consumer collections (ConcurrentQueue, ConcurrentStack, ConcurrentBag, etc)
        // Defaults to ConcurrentQueue
        private readonly BlockingCollection<Trade> _tradesToLog = new BlockingCollection<Trade>();
        private readonly StaffRecords _staffLogs;
       // private bool _workingDayComplete;


        public LogTradeQueue(StaffRecords staffLogs)
        {
            _staffLogs = staffLogs;
        }

        public StaffRecords StaffLogs { get; }

        //public void MonitorAndLogTrades()
        //{
        //    while (true)
        //    {
        //        try
        //        {
                    
        //            Trade nextTrade = _tradesToLog.Take();   // BlockingCollections waits and blocks until there is an item in queue
        //            _staffLogs.LogTrades(nextTrade);
        //            Console.WriteLine($"Processing transaction from {nextTrade.Person.Name}");
        //        }
        //        catch (InvalidOperationException ex)            // If no more items are expected BlockingException throws exception
        //        {
        //            Console.WriteLine(ex.Message);
        //            return;
        //        }
        //    }
        //}

        public void MonitorAndLogTrades()
        {
            // GetConsumingEnumerable will make it look like enumerate the next item in collection,  
            // Note that we are consuming and circumventing read only of foreach  .. we cannot enumerate the underlying collection
            // It will enumerate the copy of the queue, not the queue
            foreach (var nextTrade in _tradesToLog.GetConsumingEnumerable())
            {
                _staffLogs.LogTrades(nextTrade);
                Console.WriteLine($"Processing transaction from {nextTrade.Person.Name}");
            }
        }

        //public void SetNoMoreTrades() => _workingDayComplete = true;
        public void SetNoMoreTrades() => _tradesToLog.CompleteAdding();
        public void QueueTradeForLogging(Trade trade) => _tradesToLog.TryAdd(trade);
         
    }
}