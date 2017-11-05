using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Messages
{
    public class BookmakerAddedMessage
    {
        public BookmakerAddedMessage(IBettingAccount account)
        {
            Account = account;
        }

        public IBettingAccount Account { get; }
    }
}