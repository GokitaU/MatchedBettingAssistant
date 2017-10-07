using System;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Bets
{
    public class SimpleMatchedBet
    {
        private ISimpleBet backBet;

        private ISimpleBet layBet;

        private DateTime date;

        public SimpleMatchedBet()
        {
            this.backBet = new SimpleBet();
            this.layBet = new SimpleBet();
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

        public void Place()
        {
            this.backBet.Place();
            this.layBet.Place();

            //create transaction detail bet

        }
    }
}