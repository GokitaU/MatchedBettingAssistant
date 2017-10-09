using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.DataAccess.DataModel
{
    public class TransactionDetail
    {
        public TransactionDetail()
        {
        }

        public TransactionDetail(ITransactionDetail detail)
        {
            this.Profit = detail.Profit;
        }

        /// <summary>
        /// Identifier
        /// </summary>
        public int TransactionDetailId { get; set; }

        /// <summary>
        /// The profit of the bet when matching bets are accounted for
        /// </summary>
        public double Profit { get; set; }

        public BetType BetType { get; set; }

        public OfferType OfferType { get; set; }
    }
}