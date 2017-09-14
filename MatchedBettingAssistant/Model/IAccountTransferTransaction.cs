using System;
using DevExpress.Mvvm.DataAnnotations;

namespace MatchedBettingAssistant.Model
{
    public interface ITransaction
    {
        double Amount { get; set; }
    }

    public interface IAccountTransferTransaction : ITransaction
    {
        /// <summary>
        /// Gets or sets the transaction date
        /// </summary>
        DateTime TransactionDate { get; set; }

        /// <summary>
        /// Gets or sets the source of thr transaction (where the money
        /// is coming from)
        /// </summary>
        IAccount Source { get; set; }

        /// <summary>
        /// Gets or sets the target of the transaction (where the money
        /// is going to)
        /// </summary>
        IAccount Target { get; set; }

    }


}