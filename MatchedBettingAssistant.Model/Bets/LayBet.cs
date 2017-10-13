using System;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.Model.Bets
{
    public class LayBet : ICalculatedLayBet
    {
        public IBettingAccount Account { get; set; }
        public DateTime Date { get; set; }
        public double Odds { get; set; }
        public double Stake { get; set; }
        public double Liability => Math.Round(this.Stake * (this.Odds - 1.0),2);

        public double Returns => this.Stake;

        public string Description { get; set; }

        /// <summary>
        /// Bet has lost. The funds will already have been taken from the account
        /// </summary>
        public void Lost()
        {
            
        }

        public void Place()
        {
            var transaction = new FundsTransaction() { TransactionDate = Date, Amount = (Liability*-1)};

            this.Account.AddTransaction(transaction);
        }

        public void Complete()
        {
            throw new NotImplementedException();
        }

        public void Won()
        {
            var liability = Liability;
            var profit = this.Stake - (this.Stake * this.Account.CommissionPercent);
            var transaction = new FundsTransaction() {TransactionDate = Date, Amount = liability + profit};

            this.Account.AddTransaction(transaction);
        }

    }
}