using System;
using System.Collections.Concurrent;
using System.Threading;

namespace SellShirts
{
    public  class LogTradeQueue
    {
        private readonly ConcurrentStack<Trade> _tradesToLog = new ConcurrentStack<Trade>();
        private readonly StaffRecords _staffLogs;
        private bool _workingDayComplete;


        public LogTradeQueue(StaffRecords staffLogs)
        {
            _staffLogs = staffLogs;
        }

        public StaffRecords StaffLogs { get; }

        public void MonitorAndLogTrades()
        {
            while (true)
            {
                Trade nextTrade;
                bool done = _tradesToLog.TryPop(out nextTrade);

                if (done)
                {
                    _staffLogs.LogTrades(nextTrade);
                    Console.WriteLine($"Processing transaction from {nextTrade.Person.Name}");
                }else if(_workingDayComplete)
                {
                    Console.WriteLine("No more sales to log - exiting");
                    return;
                }
                else
                {
                    Console.WriteLine("No transactions available");
                    Thread.Sleep(500);                                                          // polling the queue *** bad practice ****
                }
            }
        }

        public void SetNoMoreTrades() => _workingDayComplete = true;
        public void QueueTradeForLogging(Trade trade) => _tradesToLog.Push(trade);
         
    }
}