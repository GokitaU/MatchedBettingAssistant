using System;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class TransactionDto : ITransaction
    {
        private readonly DataModel.Transaction transaction;
        private ITransactionDetailDisplay detail;


        public TransactionDto(Transaction transaction)
            : this(transaction, transaction.Detail)
        {
        }

        public TransactionDto(Transaction transaction, TransactionDetail detailDisplay)
        {
            this.transaction = transaction;

            this.detail = new TransactionDetailDisplayDto(detailDisplay); ;
        }

        public DateTime TransactionDate
        {
            get => this.transaction.TransactionDate;
            set => this.transaction.TransactionDate = value;
        }

        public double Amount
        {
            get => this.transaction.Amount;
            set => this.transaction.Amount = value;
        }

        public string Description
        {
            get => this.transaction.Description;
            set => this.transaction.Description = value;
        }

        public bool IncludeInProfit
        {
            get => this.transaction.IncludeInProfit;
            set => this.transaction.IncludeInProfit = value;
        }

        public ITransactionDetailDisplay Detail
        {
            get => this.detail;
            private set => this.detail = value;
        }

        internal Transaction Transaction => this.transaction;
    }
}