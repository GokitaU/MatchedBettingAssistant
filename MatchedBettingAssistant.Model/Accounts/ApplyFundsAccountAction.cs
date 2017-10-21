using System;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Accounts
{
    /// <summary>
    /// A way to apply additional funds.
    /// </summary>
    public class ApplyFundsAccountAction : IAccountAction
    {
        private readonly ITransactionRepository repository;

        public ApplyFundsAccountAction(ITransactionRepository repository)
        {
            this.repository = repository;
        }

        public IAccount Destination { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public void Apply()
        {
            var transaction = this.repository.New();
            transaction.TransactionDate = this.Date;
            transaction.Amount = this.Amount;
            transaction.Description = this.Description;

            var detail = this.repository.NewDetail();
            detail.Profit = 0;
            detail.AddTransaction(transaction);
            this.Destination.AddTransaction(transaction);
        }
    }
}