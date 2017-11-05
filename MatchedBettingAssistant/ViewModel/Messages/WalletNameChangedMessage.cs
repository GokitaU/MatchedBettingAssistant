using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Messages
{
    public class WalletNameChangedMessage
    {
        public WalletNameChangedMessage(IAccount account)
        {
            Account = account;
        }

        public IAccount Account { get; }
    }
}