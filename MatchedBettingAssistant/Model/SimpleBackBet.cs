using System;

namespace MatchedBettingAssistant.Model
{
    /// <summary>
    /// A simple back bet where the return is entered by the user
    /// </summary>
    public class SimpleBackBet : IBackBet
    {
        public IBettingAccount Account { get; set; }
        public DateTime Date { get; set; }
        public double Stake { get; set; }
        public double Returns { get; set; }
        public bool StakeNotReturned { get; set; }
        public bool IsBonus { get; set; }
        public void Place()
        {
            var backTransaction = new FundsTransaction() { TransactionDate = Date, Amount = this.Returns };

            this.Account.AddTransaction(backTransaction);
        }
    }
}