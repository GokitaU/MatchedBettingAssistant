using System;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Accounts
{
    /// <summary>
    /// A way to apply additional funds.
    /// </summary>
    public class ApplyFundsAccountAction : IAccountAction
    {
        public IAccount Destination { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public void Apply()
        {
            var transaction = new FundsTransaction()
            {
                TransactionDate = this.Date,
                Amount = this.Amount,
                Description = this.Description
            };

            this.Destination.AddTransaction(transaction);
        }
    }
}