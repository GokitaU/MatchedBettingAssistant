using System;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Accounts
{
    public class TransactionDetail : ITransactionDetail
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Profit { get; set; }
        public IBetType BetType { get; set; }
        public IOfferType OfferType { get; set; }

        public ISport Sport { get; set; }

        public IMarket Market { get; set; }

        public double PaybackPercent { get; set; }

        public double Payback => this.Profit * this.PaybackPercent;
        public void AddTransaction(ITransaction transaction)
        {
            throw new System.NotImplementedException();
        }
    }
}