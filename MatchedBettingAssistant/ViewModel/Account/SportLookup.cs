using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class SportLookup : Lookup<ISport>
    {
        public SportLookup(ISport lookup) : base(lookup)
        {
        }
    }
}