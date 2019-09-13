using System;
using System.Globalization;

namespace BankingSystem
{
    public class transHelper
    {
        public string amount { get; set; }
        public DateTime timestamp { get; set; }

        public transHelper(string amount, DateTime timestamp)
        {
            this.amount = amount;
            this.timestamp = timestamp;
        }

    }
}
