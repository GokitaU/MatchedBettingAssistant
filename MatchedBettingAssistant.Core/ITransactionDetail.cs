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

        double Payback { get; }

        void AddTransaction(ITransaction transaction);
    }
}