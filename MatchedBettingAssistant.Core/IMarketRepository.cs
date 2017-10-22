using System.Collections.Generic;

namespace MatchedBettingAssistant.Core
{
    public interface IMarketRepository
    {
        IMarket GetMarket(int id);

        IEnumerable<IMarket> GetMarkets();

        IMarket New();
    }
}