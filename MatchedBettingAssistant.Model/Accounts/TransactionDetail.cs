using System;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Accounts
{
    public class TransactionDetail : ITransactionDetail
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public double Profit { get; set; }
        public IBetType BetType { get; set; }
        public IOfferType OfferType { get; set; }

        public ISport Sport { get; set; }

        public IMarket Market { get; set; }

        public double PaybackPercent { get; set; }
        public bool IsSettled { get; set; }

        public double Payback => this.Profit * this.PaybackPercent;
        public ITransaction BackTransaction { get; }
        public ITransaction LayTransaction { get; }
        public void AddTransaction(ITransaction transaction)
        {
            throw new NotImplementedException();
        }

        public ITransaction CreateBackTransaction()
        {
            throw new NotImplementedException();
        }

        public ITransaction CreateLayTransaction()
        {
            throw new NotImplementedException();
        }
    }
}