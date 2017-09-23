using System;

namespace MatchedBettingAssistant.Model.Account
{
    public interface ITransaction
    {
        DateTime TransactionDate { get; set; }

        double Amount { get; set; }

        string Description { get; set; }
    }
}