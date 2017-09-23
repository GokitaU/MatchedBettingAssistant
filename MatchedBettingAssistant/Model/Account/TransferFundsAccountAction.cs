using System;

namespace MatchedBettingAssistant.Model.Account
{
    

    public class TransferFundsAccountAction : IAccountAction
    {
        public IAccount Source { get; set; }
        public IAccount Destination { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public void Apply()
        {
            var withdraw = new FundsTransaction() {TransactionDate = this.Date, Amount = this.Amount * -1, Description = GetWithdrawDescription()};

            var deposit = new FundsTransaction() {TransactionDate = this.Date, Amount = this.Amount, Description = GetDepositDescription()};

            this.Source.AddTransaction(withdraw);

            this.Destination.AddTransaction(deposit);
        }

        private string GetWithdrawDescription()
        {
            if (string.IsNullOrEmpty(this.Description))
            {
                return $"Withdrawal to {this.Destination.Name}";
            }
            return this.Description;
        }

        private string GetDepositDescription()
        {
            if (string.IsNullOrEmpty(this.Description))
            {
                return $"Deposit from {this.Source.Name}";
            }
            return this.Description;
        }
    }
}