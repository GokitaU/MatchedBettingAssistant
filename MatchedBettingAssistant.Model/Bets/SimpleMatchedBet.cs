using System;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Core.Repositories;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.Model.Bets
{
    public class SimpleMatchedBet
    {
        private ITransactionRepository repository;

        private ISimpleBet backBet;

        private ISimpleBet layBet;

        private DateTime date;

        public SimpleMatchedBet(ITransactionRepository repository)
        {
            this.repository = repository;
            this.backBet = new SimpleBet(repository);
            this.layBet = new SimpleBet(repository);
        }

        public DateTime Date
        {
            get => this.date;
            set
            {
                this.date = value;

                this.backBet.Date = value;

                this.layBet.Date = value;
            }
        }

        public IBettingAccount BackAccount
        {
            get => this.backBet.Account;
            set => this.backBet.Account = value;
        }

        public double BackReturns
        {
            get => this.backBet.Returns;
            set => this.backBet.Returns = value;
        }

        public IBettingAccount LayAccount
        {
            get => this.layBet.Account;
            set => this.layBet.Account = value;
        }

        public double LayReturns
        {
            get => this.layBet.Returns;
            set => this.layBet.Returns = value;
        }

        public IOfferType OfferType { get; set; }

        public IBetType BetType { get; set; }

        public ISport Sport { get; set; }

        public IMarket Market { get; set; }

        public IBank Bank { get; set; }

        public bool IsSettled { get; set; }

        public string Description
        {
            get => this.backBet.Description;
            set
            {
                this.backBet.Description = value;
                this.layBet.Description = value;
            }
        }

        public void Place()
        {
            this.backBet.Place();
            this.layBet.Place();

            //create transaction detail bet
            var detail = this.repository.NewDetail();
            SetDetailProperties(detail);
            detail.AddTransaction(this.layBet.Transaction);
            detail.AddTransaction(this.backBet.Transaction);

            Bank?.AddTransaction(detail);
        }

        private void SetDetailProperties(ITransactionDetail detail)
        {
            detail.Date = this.Date;
            detail.Description = CreateDescription();
            detail.OfferType = this.OfferType;
            detail.BetType = this.BetType;
            detail.Sport = this.Sport;
            detail.Market = this.Market;
            detail.Profit = this.LayReturns + this.BackReturns;
            detail.PaybackPercent = this.BackAccount.PaybackPercent;
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
            this.backBet.Complete();
            this.layBet.Complete();
        }

        public void Update()
        {
        }
    }
}