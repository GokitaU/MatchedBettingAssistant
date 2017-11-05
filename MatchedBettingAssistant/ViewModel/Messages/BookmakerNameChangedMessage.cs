using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Messages
{
    public class BookmakerNameChangedMessage
    {
        public BookmakerNameChangedMessage(IBettingAccount account)
        {
            Account = account;
        }

        public IBettingAccount Account { get; }

    }
}