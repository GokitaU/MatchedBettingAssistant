using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class MarketLookup : Lookup<IMarket>
    {
        public MarketLookup(IMarket lookup) : base(lookup)
        {
        }
    }
}