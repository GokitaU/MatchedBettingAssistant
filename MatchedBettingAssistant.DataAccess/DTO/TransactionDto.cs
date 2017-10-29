using System;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class TransactionDto : ITransaction
    {
        private readonly DataModel.Transaction transaction;
        private ITransactionDetail detail;


        public TransactionDto(Transaction transaction)
        {
            this.transaction = transaction;

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

        public ITransactionDetail Detail
        {
            get => this.detail;
            set
            {
                AddDetail(value);
                this.detail = value;
            }
        }

        public void AddDetail(ITransactionDetail detail)
        {
            if (detail is TransactionDetailDto transactionDetailDto)
            {
                this.transaction.Detail = transactionDetailDto.TransactionDetail;
                this.detail = transactionDetailDto;
            }
        }

        internal Transaction Transaction => this.transaction;
    }
}