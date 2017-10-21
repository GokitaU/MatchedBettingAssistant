using System;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Accounts
{
    

    public class TransferFundsAccountAction : IAccountAction
    {
        private readonly ITransactionRepository repository;

        public TransferFundsAccountAction(ITransactionRepository repository)
        {
            this.repository = repository;
        }

        public IAccount Source { get; set; }
        public IAccount Destination { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public void Apply()
        {
            var withdraw = this.repository.New();
            withdraw.TransactionDate = this.Date;
            withdraw.Amount = this.Amount * -1;
            withdraw.Description = GetWithdrawDescription();

            var deposit = this.repository.New();
            deposit.TransactionDate = this.Date;
            deposit.Amount = this.Amount;
            deposit.Description = GetDepositDescription();

            var detail = this.repository.NewDetail();
            detail.AddTransaction(withdraw);
            detail.AddTransaction(deposit);

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