using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BookmakerLookupItem : ViewModelBase
    {
        public BookmakerLookupItem(IBettingAccount account)
        {
            this.Account = account;
        }

        public int Id => this.Account.Id;

        public string Name => this.Account.Name;

        internal IBettingAccount Account { get; }
    }
}