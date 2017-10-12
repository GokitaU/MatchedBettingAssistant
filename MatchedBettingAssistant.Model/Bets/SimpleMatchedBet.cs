using System;
using MatchedBettingAssistant.Core;
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

        public void Place()
        {
            this.backBet.Place();
            this.layBet.Place();

            //create transaction detail bet
            var detail = this.repository.NewDetail();
            detail.OfferType = this.OfferType;
            detail.BetType = this.BetType;
            detail.Profit = this.LayReturns + this.BackReturns;
            detail.PaybackPercent = this.BackAccount.PaybackPercent;
            this.backBet.Transaction.AddDetail(detail);
            this.layBet.Transaction.AddDetail(detail);
        }

        public void Complete()
        {
            this.backBet.Complete();
            this.layBet.Complete();
        }
    }
}