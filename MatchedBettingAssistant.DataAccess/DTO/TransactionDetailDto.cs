using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class TransactionDetailDto : ITransactionDetail
    {
        private DataModel.TransactionDetail detail;

        public TransactionDetailDto(TransactionDetail detail)
        {
            this.detail = detail;
        }

        public double Profit
        {
            get => this.detail.Profit;
            set => this.detail.Profit = value;
        }
    }
}