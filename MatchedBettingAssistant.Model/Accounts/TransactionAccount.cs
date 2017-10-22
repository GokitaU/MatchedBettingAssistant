using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Accounts
{
    public abstract class TransactionAccount : Account, ITransactionAccount
    {
        private ITransactionAccount baseAccount;

        protected TransactionAccount(ITransactionAccount baseAccount) : base(baseAccount)
        {
            this.baseAccount = baseAccount;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the current balance of this bookmaker
        /// </summary>
        public override double Balance
        {
            get
            {
                return this.StartingBalance + (baseAccount.Transactions?.Sum(x => x.Amount) ?? 0);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds a transaction to the account
        /// </summary>
        /// <param name="transaction">the transaction to be applied</param>
        public virtual void AddTransaction(ITransaction transaction)
        {
            this.baseAccount.AddTransaction(transaction);
        }

        public IEnumerable<ITransaction> Transactions
        {
            get { return this.baseAccount.Transactions; }
        }

  
    }
}