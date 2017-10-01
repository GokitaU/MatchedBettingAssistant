using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class SelectedBookmakerChangedMessage
    {
        public SelectedBookmakerChangedMessage(IBettingAccount bookmaker)
        {
            this.Bookmaker = bookmaker;
        }
        public IBettingAccount Bookmaker { get; }

    }
}