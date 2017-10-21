using System;
using System.ComponentModel.DataAnnotations.Schema;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.DataAccess.DataModel
{
    public class Transaction
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

        public int TransactionDetailId { get; set; }

        public virtual TransactionDetail Detail { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }

    }
}