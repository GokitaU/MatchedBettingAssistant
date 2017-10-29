using System;

namespace MatchedBettingAssistant.Core
{
    public interface ITransactionDetail
    {
        DateTime Date { get; set; }

        string Description { get; set; }

        double Profit { get; set; }

        IBetType BetType { get; set; }

        IOfferType OfferType { get; set; }

        ISport Sport { get; set; }

        IMarket Market { get; set; }

        double PaybackPercent { get; set; }

        bool IsSettled { get; set; }

        double Payback { get; }
        ITransaction BackTransaction { get; }

        ITransaction LayTransaction { get; }

        /// <summary>
        /// Adds an existing transaction
        /// </summary>
        /// <param name="transaction"></param>
        void AddTransaction(ITransaction transaction);

        /// <summary>
        /// Creates a new transaction and adds it
        /// </summary>
        /// <returns></returns>
        ITransaction CreateBackTransaction();

        ITransaction CreateLayTransaction();


    }
}