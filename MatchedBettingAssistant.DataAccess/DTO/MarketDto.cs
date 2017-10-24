using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class MarketDto : IMarket
    {
        private readonly Market market;

        public MarketDto(Market market)
        {
            this.market = market;
        }

        public int Id
        {
            get => this.market.Id;
            set => this.market.Id = value;
        }

        public string Name
        {
            get => this.market.Name;
            set => this.market.Name = value;
        }

        internal Market Market => this.market;
    }
}