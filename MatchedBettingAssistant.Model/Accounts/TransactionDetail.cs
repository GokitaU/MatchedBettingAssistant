using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Accounts
{
    public class TransactionDetail : ITransactionDetail
    {
        public double Profit { get; set; }
        public IBetType BetType { get; set; }
        public IOfferType OfferType { get; set; }
    }
}