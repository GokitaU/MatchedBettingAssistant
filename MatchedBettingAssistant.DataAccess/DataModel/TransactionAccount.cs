using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MatchedBettingAssistant.DataAccess.DataModel
{
    public abstract class TransactionAccount : Account
    {
        protected TransactionAccount()
        {
            this.Transactions = new List<Transaction>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double Balance => this.StartingBalance + this.Transactions.Sum(x => x.Amount);

        public ICollection<Transaction> Transactions { get; set; }
    }
}