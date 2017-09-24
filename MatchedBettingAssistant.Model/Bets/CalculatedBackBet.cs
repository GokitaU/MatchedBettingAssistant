using System;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.Model.Bets
{


    public class CalculatedBackBet : ICalculatedBackBet
    {
        public DateTime Date { get; set; }

        public IBettingAccount Account { get; set; }

        public double Stake { get; set; }

        public double Odds { get; set; }

        public double Returns => this.Stake * (this.Odds - 1);

        public double Probability => 1 / this.Odds;

        public void Place()
        {
            var backTransaction = new FundsTransaction() { TransactionDate = Date, Amount = this.Stake * -1};

            this.Account.AddTransaction(backTransaction);
        }

        public void Won()
        {
            var profit = this.Returns;

            profit -= profit * this.Account.CommissionPercent;

            var stakeReturn = this.Stake;

            var backTransaction = new FundsTransaction() { TransactionDate = Date, Amount = stakeReturn + profit};

            this.Account.AddTransaction(backTransaction);
        }

        public void Lost()
        {
            
        }
    }
}