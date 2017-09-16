using System;

namespace MatchedBettingAssistant.Model
{
    public class FundsTransaction : ITransaction
    {
        public DateTime TransactionDate { get; set; }

        public double Amount { get; set; }
    }
}