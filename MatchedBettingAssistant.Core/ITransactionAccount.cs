using System.Collections.Generic;

namespace MatchedBettingAssistant.Core
{
    public interface ITransactionAccount : IAccount
    {
        /// <summary>
        /// Adds a transaction to the account
        /// </summary>
        /// <param name="transaction">the transaction to be applied</param>
        void AddTransaction(ITransaction transaction);

        IEnumerable<ITransaction> Transactions { get; }
    }
}