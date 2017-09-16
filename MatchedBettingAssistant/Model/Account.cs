using System.Collections.Generic;
using System.Linq;

namespace MatchedBettingAssistant.Model
{
    public abstract class Account : IAccount
    {
        private readonly List<ITransaction> transactions;

        protected Account()
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

        /// <inheritdoc />
        /// <summary>
        /// Gets the current balance of this bookmaker
        /// </summary>
        public virtual double Balance
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
        public virtual void AddTransaction(ITransaction transaction)
        {
            this.transactions.Add(transaction);
        }
    }
}