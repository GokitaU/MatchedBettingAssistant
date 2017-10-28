using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.DataAccess.DataModel
{
    public class TransactionDetail
    {
        public TransactionDetail()
        {
            this.Transactions = new Collection<Transaction>();
        }

        public TransactionDetail(ITransactionDetail detail) : this()
        {
            this.Profit = detail.Profit;
        }

        /// <summary>
        /// Identifier
        /// </summary>
        public int Id { get; set; }

        public DateTime Date { get; set; }

        /// <summary>
        /// The profit of the bet when matching bets are accounted for
        /// </summary>
        public double Profit { get; set; }

        public BetType BetType { get; set; }

        public OfferType OfferType { get; set; }

        public Sport Sport { get; set; }

        public Market Market { get; set; }

        /// <summary>
        /// Description to cover the entire transaction
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Notes for the transaction
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// The transactions associated with this detail
        /// </summary>
        public ICollection<Transaction> Transactions { get; set; }

        /// <summary>
        /// The percentage of the profit that should be paid back to the bookie
        /// for this transaction
        /// </summary>
        public double PaybackPercent { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double Payback => this.Profit * this.PaybackPercent;

        /// <summary>
        /// Whether the transaction is completed.
        /// </summary>
        public bool IsSettled { get; set; }
    }
}