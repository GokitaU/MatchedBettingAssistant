using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class SelectedWalletChangedMessage
    {
        public SelectedWalletChangedMessage(IWallet account)
        {
            Account = account;
        }

        public IWallet Account { get; }
    }
}