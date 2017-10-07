using System;
using System.ComponentModel.DataAnnotations.Schema;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.DataAccess.DataModel
{
    public class Transaction : ITransaction
    {
        public Transaction()
        {
        }

        public Transaction(ITransaction transaction)
        {
            this.TransactionDate = transaction.TransactionDate;
            this.Amount = transaction.Amount;
            this.Description = transaction.Description;
        }

        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

        public virtual TransactionDetail TransactionDetail { get; set; }

        [NotMapped]
        public ITransactionDetail Detail { get { return TransactionDetail; } }

        public void AddDetail(ITransactionDetail detail)
        {
            this.TransactionDetail = new TransactionDetail(detail);
        }
    }
}