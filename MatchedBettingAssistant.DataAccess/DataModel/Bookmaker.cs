using System.ComponentModel.DataAnnotations.Schema;
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

        /// <summary>
        /// Gets or sets whether this account has been limited by the bookmaker
        /// </summary>
        public bool LimitedAccount { get; set; }

        /// <summary>
        /// the target percentage of profits to be paid back to the 
        /// bookmaker in non-bonus bets
        /// </summary>
        public double PayBackPercent { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double Profit => this.Transactions.Sum(x => x.Detail?.Profit ?? 0);

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
                    .Where(x => x.Detail?.OfferType?.IsBonusOffer ?? false)
                    .Sum(x => x.Detail?.Payback ?? 0);

                var totalPaid = this.Transactions
                    .Where(x => (x.Detail?.OfferType?.IsBonusOffer ?? false) == false)
                    .Sum(x => x.Detail?.Profit ?? 0);

                return totalOwed + totalPaid;
            }
        }
    }
}