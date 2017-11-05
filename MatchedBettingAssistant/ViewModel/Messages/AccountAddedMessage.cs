using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Messages
{
    public class AccountAddedMessage
    {
        public AccountAddedMessage(ITransactionAccount account)
        {
            Account = account;
        }

        public ITransactionAccount Account { get; }
    }
}