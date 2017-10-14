using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BookmakerNameChangedMessage
    {
        public BookmakerNameChangedMessage(IBettingAccount account)
        {
            Account = account;
        }

        public IBettingAccount Account { get; }

    }

    public class WalletNameChangedMessage
    {
        public WalletNameChangedMessage(IAccount account)
        {
            Account = account;
        }

        public IAccount Account { get; }
    }
}