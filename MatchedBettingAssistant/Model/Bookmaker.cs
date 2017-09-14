using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace MatchedBettingAssistant.Model
{
    public class Bookmaker : IAccount
    {
        private readonly List<ITransaction> transactions;

        public Bookmaker()
        {
            this.transactions = new List<ITransaction>();
        }

        /// <summary>
        /// Gets or sets the name of the bookmaker
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the starting balance at this bookmaker
        /// </summary>
        public double StartingBalance { get; set; }

        public double CommissionPercent { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the current balance of this bookmaker
        /// </summary>
        public double Balance
        {
            get
            {         
                return this.StartingBalance + this.transactions.Sum(x => x.Amount);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds a transaction to the account
        /// </summary>
        /// <param name="transaction">the transaction to be applied</param>
        public void AddTransaction(ITransaction transaction)
        {
            this.transactions.Add(transaction);
        }
    }

    public class FundsTransaction : ITransaction
    {
        public double Amount { get; set; }
    }

}