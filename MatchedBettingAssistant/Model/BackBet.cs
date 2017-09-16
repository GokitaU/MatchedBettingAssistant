using System;
using DevExpress.Xpf.Editors.Internal;

namespace MatchedBettingAssistant.Model
{
    public class BackBet : IBackBet
    {
        public DateTime Date { get; set; }

        public IBettingAccount Account { get; set; }

        public double Stake { get; set; }

        public double Odds { get; set; }

        public double Returns => this.Stake * (this.Odds - 1);

        /// <summary>
        /// Whether the bet returns the stake when won. Bonuses often do 
        /// not return the stake.
        /// </summary>
        public bool StakeNotReturned { get; set; }

        /// <summary>
        /// Whether this bet is a bonus bet. 
        /// </summary>
        public bool IsBonus { get; set; }

        public double Probability => 1 / this.Odds;

        public void Place()
        {
            if (!this.IsBonus)
            {
                var backTransaction = new FundsTransaction() { TransactionDate = Date, Amount = this.Stake * -1};

                this.Account.AddTransaction(backTransaction);
            }
        }

        public void Won()
        {
            var profit = this.Returns;

            profit -= profit * this.Account.CommissionPercent;

            var stakeReturn = this.StakeNotReturned ? 0 : this.Stake;

            var backTransaction = new FundsTransaction() { TransactionDate = Date, Amount = stakeReturn + profit};

            this.Account.AddTransaction(backTransaction);
        }

        public void Lost()
        {
            
        }
    }
}