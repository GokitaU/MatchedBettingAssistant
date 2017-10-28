using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Core.Repositories;
using MatchedBettingAssistant.DataAccess.DataModel;
using MatchedBettingAssistant.DataAccess.DTO;

namespace MatchedBettingAssistant.DataAccess.Repositories
{
    public class MarketRepository : IMarketRepository
    {
        private MatchedBettingAssistantDbContext dataContext;

        public MarketRepository(MatchedBettingAssistantDbContext dataContext)
        {
            this.dataContext = dataContext;
        }


        public IMarket GetMarket(int id)
        {
            var market = this.dataContext.Markets.FirstOrDefault(x => x.Id == id);

            if (market != null)
            {
                return new MarketDto(market);
            }
            return null;
        }

        public IEnumerable<IMarket> GetMarkets()
        {
            var markets = this.dataContext.Markets.ToList();

            return new List<IMarket>(markets.Select(x => new MarketDto(x)));
        }

        public IMarket New()
        {
            var market = new Market();
            
            return new MarketDto(market);
        }
    }
}