using System;

namespace MatchedBettingAssistant.Model
{
    public class TransferFundsAccountAction : IAccountAction
    {
        public IAccount Source { get; set; }
        public IAccount Destination { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public void Apply()
        {
            var withdraw = new FundsTransaction() {Amount = this.Amount * -1};

            var deposit = new FundsTransaction() {Amount = this.Amount};

            this.Source.AddTransaction(withdraw);

            this.Destination.AddTransaction(deposit);
        }
    }
}