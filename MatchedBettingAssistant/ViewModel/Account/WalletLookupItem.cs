using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class WalletLookupItem : AccountLookupItem<IWallet>
    {
        public WalletLookupItem(IWallet account) : base(account)
        {
        }
    }
}