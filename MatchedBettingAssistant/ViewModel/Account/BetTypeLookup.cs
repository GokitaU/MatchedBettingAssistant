using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BetTypeLookup
    {
        private IBetType betType;

        public BetTypeLookup(IBetType betType)
        {
            this.betType = betType;
        }

        public string Name => this.betType.Name;

        internal IBetType BetType => this.betType;
    }
}