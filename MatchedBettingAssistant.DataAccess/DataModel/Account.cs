using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.DataAccess.DataModel
{
    public abstract class Account
    {
        protected Account()
        {
            this.Transactions = new List<Transaction>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public double StartingBalance { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double Balance => this.StartingBalance + this.Transactions.Sum(x=>x.Amount);

        public void AddTransaction(ITransaction transaction)
        {
            this.Transactions.Add(new Transaction(transaction));
        }
        public ICollection<Transaction> Transactions { get; set; }


    }
}