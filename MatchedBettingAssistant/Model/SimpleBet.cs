using System;

namespace MatchedBettingAssistant.Model
{
    /// <summary>
    /// A simple back bet where the return is entered by the user
    /// </summary>
    public class SimpleBet : ISimpleBet
    {
        public IBettingAccount Account { get; set; }
        public DateTime Date { get; set; }
        public double Returns { get; set; }

        public string Description { get; set; }
        public void Place()
        {
            var backTransaction = new FundsTransaction() { TransactionDate = Date, Amount = this.Returns, Description = this.Description};

            this.Account.AddTransaction(backTransaction);
        }
    }
}