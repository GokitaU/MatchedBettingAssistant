using System.Collections.Generic;

namespace MatchedBettingAssistant.Core
{
    /// <summary>
    /// Interface for classes that represent methods of payment such as
    /// bank accounts and credit cards.
    /// </summary>
    public interface IAccount
    {
        int Id { get; set; }

        string Name { get; set; }

        double StartingBalance { get; set; }

        /// <summary>
        /// Gets the current balance of this bookmaker
        /// </summary>
        double Balance { get; }

        /// <summary>
        /// Adds a transaction to the account
        /// </summary>
        /// <param name="transaction">the transaction to be applied</param>
        void AddTransaction(ITransaction transaction);

        IEnumerable<ITransaction> Transactions { get; }
    }
}