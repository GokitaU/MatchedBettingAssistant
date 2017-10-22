using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MatchedBettingAssistant.DataAccess.DataModel
{
    /// <summary>
    /// A bank keeps a record of bets made for a certain system or category to 
    /// keep track of its profitability
    /// </summary>
    public class Bank : Account
    {
        public Bank()
        {
            this.Transactions = new List<TransactionDetail>();
        }

        /// <summary>
        /// The amount each point is valued at
        /// </summary>
        public double PointValue { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double Balance => this.StartingBalance + this.Transactions.Sum(x => x.Profit);

        public ICollection<TransactionDetail> Transactions { get; set; }
    }
}