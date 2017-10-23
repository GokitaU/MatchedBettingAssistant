using DevExpress.Data.Helpers;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BetTypeLookup : Lookup<IBetType>
    {
        public BetTypeLookup(IBetType lookup) : base(lookup)
        {
        }
    }
}