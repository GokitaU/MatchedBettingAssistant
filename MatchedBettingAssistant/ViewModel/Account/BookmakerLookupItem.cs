using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BookmakerLookupItem : ViewModelBase
    {
        private IBettingAccount account;

        public BookmakerLookupItem(IBettingAccount account)
        {
            this.account = account;
        }

        public int Id => this.account.Id;

        public string Name => this.account.Name;
    }
}