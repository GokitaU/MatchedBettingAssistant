using System.Collections.Generic;

namespace MatchedBettingAssistant.Core.Repositories
{
    public interface IMarketRepository
    {
        IMarket GetMarket(int id);

        IEnumerable<IMarket> GetMarkets();

        IMarket New();
    }
}