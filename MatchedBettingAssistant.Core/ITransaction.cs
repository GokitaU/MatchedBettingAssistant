using System;

namespace MatchedBettingAssistant.Core
{
    public interface ITransaction
    {
        DateTime TransactionDate { get; set; }

        double Amount { get; set; }

        string Description { get; set; }

        ITransactionDetail Detail { get; }

        void AddDetail(ITransactionDetail detail);

    }
}