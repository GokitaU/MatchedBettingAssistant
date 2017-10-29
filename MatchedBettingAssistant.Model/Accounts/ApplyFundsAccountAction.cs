using System;
using System.Configuration;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Core.Repositories;

namespace MatchedBettingAssistant.Model.Accounts
{
    /// <summary>
    /// A way to apply additional funds.
    /// </summary>
    public class ApplyFundsAccountAction : IAccountAction
    {
        private readonly ITransactionDetail detail;

        public ApplyFundsAccountAction(ITransactionDetail detail)
        {
            this.detail = detail;
        }

        public ITransactionDetail Detail => this.detail;

        public ITransactionAccount Destination { get; set; }

        public DateTime Date
        {
            get => detail.Date;
            set
            {
                this.detail.Date = value;

                if (this.detail.BackTransaction != null)
                {
                    this.detail.BackTransaction.TransactionDate = value;
                }
            }
        }

        public string Description
        {
            get => this.detail.Description;
            set
            {
                this.detail.Description = value;
                if (this.detail.BackTransaction != null)
                {
                    this.detail.BackTransaction.Description = value;
                }
            }
        }

        public double Amount
        {
            get => this.detail.BackTransaction?.Amount ?? 0;
            set
            {
                if (this.detail.BackTransaction != null)
                {
                    this.detail.BackTransaction.Amount = value;
                }
            }
        }

        public void Apply()
        {
            this.detail.IsSettled = true;

            this.Destination.AddTransaction(this.detail.BackTransaction);
        }


    }
}