using System;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Core.Repositories;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.Model.Bets
{
    public class SimpleMatchedBet
    {
        private ITransactionDetail detail;

        public SimpleMatchedBet(ITransactionDetail detail)
        {
            this.detail = detail;
        }

        public ITransactionDetail Detail => this.detail;

        public DateTime Date
        {
            get => this.detail.Date;
            set
            {
                this.detail.Date = value;

                this.detail.BackTransaction.TransactionDate = value;

                this.detail.LayTransaction.TransactionDate = value;
            }
        }

        public IBettingAccount BackAccount { get; set; }

        public double BackReturns
        {
            get => this.detail.BackTransaction.Amount;
            set => this.detail.BackTransaction.Amount = value;
        }

        public IBettingAccount LayAccount { get; set; }

        public double LayReturns
        {
            get => this.detail.LayTransaction.Amount;
            set => this.detail.LayTransaction.Amount = value;
        }

        public IOfferType OfferType
        {
            get => this.detail.OfferType;
            set => this.detail.OfferType = value;
        }

        public IBetType BetType
        {
            get => this.detail.BetType;
            set => this.detail.BetType = value;
        }

        public ISport Sport
        {
            get => this.detail.Sport;
            set => this.detail.Sport = value;
        }

        public IMarket Market
        {
            get => this.detail.Market;
            set => this.detail.Market = value;
        }

        public IBank Bank { get; set; }

        public bool IsSettled
        {
            get => this.detail.IsSettled;
            set => this.detail.IsSettled = value;
        }

        public string Description
        {
            get => this.detail.Description;
            set
            {
                this.detail.Description = value;
                this.detail.BackTransaction.Description = value;
                this.detail.BackTransaction.Description = value;
            }
        }

        public void Place()
        {
            if (string.IsNullOrEmpty(this.Description))
            {
                this.Description = this.CreateDescription();
            }
            this.BackAccount?.AddTransaction(this.detail.BackTransaction);
            this.LayAccount?.AddTransaction(this.detail.LayTransaction);

            Bank?.AddTransaction(detail);
        }

        private string CreateDescription()
        {
            if (this.BetType != null)
            {
                return $"{this.BetType?.Name} bet at {this.BackAccount?.Name}";
            }
            return $"{this.BackAccount?.Name} bet";
        }

        public void Complete()
        {

        }

        public void Update()
        {
        }
    }
}