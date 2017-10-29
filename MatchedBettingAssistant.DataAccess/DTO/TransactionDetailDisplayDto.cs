using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class TransactionDetailDisplayDto : ITransactionDetailDisplay
    {
        private TransactionDetail detail;

        public TransactionDetailDisplayDto(TransactionDetail detail)
        {
            this.detail = detail;
        }

        public int Id => detail.Id;

        public string Description => detail.Description;
        public double Profit => detail.Profit;
        public string BetType => detail.BetType?.Name;
        public string OfferType => detail.OfferType?.Name;
        public string Sport => detail.Sport?.Name;
        public string Market => detail.Market?.Name;
    }
}