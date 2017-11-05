using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Messages
{
    public class AccountNameChangedMessage
    {
        public AccountNameChangedMessage(ITransactionAccount account)
        {
            Account = account;
        }

        public ITransactionAccount Account { get; }
    }
}