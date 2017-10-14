using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BookmakerLookupItem : AccountLookupItem<IBettingAccount>
    {
        public BookmakerLookupItem(IBettingAccount account) : base(account)
        {
        }
    }

    public class WalletLookupItem : AccountLookupItem<IWallet>
    {
        public WalletLookupItem(IWallet account) : base(account)
        {
        }
    }
}