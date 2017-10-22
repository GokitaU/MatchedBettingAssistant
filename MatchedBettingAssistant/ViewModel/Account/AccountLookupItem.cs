using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountLookupItem : AccountLookupItem<IAccount>
    {
        public AccountLookupItem(IAccount account) : base(account)
        {

        }
    }

    public class AccountLookupItem<T> : ViewModelBase where T: IAccount
    {
        private readonly T account;

        public AccountLookupItem(T account)
        {
            this.account = account;
        }

        public int Id => this.account.Id;

        public string Name => this.account.Name;

        internal T Account => this.account;

        public void Refresh()
        {
            RaisePropertyChanged(() => Name);
        }
    }
}