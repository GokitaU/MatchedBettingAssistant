﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.DataAccess.DataModel
{
    public class Bookmaker : TransactionAccount
    {
        public Bookmaker()
        {
            this.PayBackPercent = 0.10;
        }

        public double CommissionPercent { get; set; }
        public bool IsExchange { get; set; }

        public bool CompletedNewAccountOffer { get; set; }

        /// <summary>
        /// Gets or sets whether this account has been limited by the bookmaker
        /// </summary>
        public bool LimitedAccount { get; set; }

        /// <summary>
        /// the target percentage of profits to be paid back to the 
        /// bookmaker in non-bonus bets
        /// </summary>
        public double PayBackPercent { get; set; }

        /// <summary>
        /// The actual profit made from this bookmaker including the income 
        /// from matched bets which may have lost from the bookmaker but won
        /// a profit by laying off. 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double Profit => this.Transactions.Sum(x => x.Detail?.Profit ?? 0);

        /// <summary>
        /// The profit made from this bookmaker. This figure is the total 
        /// profit or loss made from the bookmaker's point of view and does
        /// not factor in the profit gained by laying off.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double AccountProfit => this.Transactions.Where(x => x.IncludeInProfit).Sum(y => y.Amount);

        /// <summary>
        /// Total payback is the total payback of all bonus transactions, plus
        /// the profit of all non-bonus transactions (which will nearly all be
        /// losses and therefore subtracted).
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double PaybackDue
        {
            get
            {
                var totalOwed = this.Transactions
                    .Where(x => (x.Detail?.OfferType?.IsBonusOffer ?? false)
                                && (x.Detail?.OfferType?.ExcludeFromPayback ?? false) == false)
                    .Sum(x => x.Detail.Profit  * this.PayBackPercent);

                var totalPaid = this.Transactions
                    .Where(x => (x.Detail?.OfferType?.IsBonusOffer ?? true) == false  && 
                                (x.Detail?.OfferType.ExcludeFromPayback?? true)== false)
                    .Sum(x => x.Detail?.Profit ?? 0);

                return totalOwed + totalPaid;
            }
        }
    }
}