using System;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Core.Repositories;

namespace MatchedBettingAssistant.Model.Accounts
{
    

    public class TransferFundsAccountAction : IAccountAction
    {
        private readonly ITransactionDetail detail;

        public TransferFundsAccountAction(ITransactionDetail detail)
        {
            this.detail = detail;
        }

        /// <summary>
        /// The date of the bet
        /// </summary>
        public DateTime Date
        {
            get => this.detail.Date;
            set
            {
                this.detail.Date = value;

                if (this.detail.BackTransaction != null)
                {
                    this.detail.BackTransaction.TransactionDate = value;
                }

                if (this.detail.LayTransaction != null)
                {
                    this.detail.LayTransaction.TransactionDate = value;
                }
            }
        }

        public ITransactionDetail Detail => this.detail;

        public ITransactionAccount Source { get; set; }

        public ITransactionAccount Destination { get; set; }

        /// <summary>
        /// The amount the bet returned
        /// </summary>
        public double Amount
        {
            get => this.detail.LayTransaction?.Amount ?? 0;
            set
            {
                if (this.detail.BackTransaction != null)
                {
                    this.detail.BackTransaction.Amount = value *-1;
                }

                if (this.detail.LayTransaction != null)
                {
                    this.detail.LayTransaction.Amount = value;
                }
            }
        }

        /// <summary>
        /// A description for this bet
        /// </summary>
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

        public void Apply()
        {
            if (string.IsNullOrEmpty(this.Description))
            {
                this.detail.BackTransaction.Description = this.GetWithdrawDescription();
                this.detail.LayTransaction.Description = this.GetDepositDescription();
            }
            this.detail.IsSettled = true;
            this.Source?.AddTransaction(this.detail.BackTransaction);
            this.Destination?.AddTransaction(this.detail.LayTransaction);
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