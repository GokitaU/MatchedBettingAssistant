using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Messages
{
    public class SelectedBookmakerChangedMessage
    {
        public SelectedBookmakerChangedMessage(IBettingAccount bookmaker)
        {
            this.Bookmaker = bookmaker;
        }
        public IBettingAccount Bookmaker { get; }

    }

    public class SelectedAccountChangedMessage
    {
        public SelectedAccountChangedMessage(IAccount account)
        {
            Account = account;
        }

        public IAccount Account { get; }
    }
}