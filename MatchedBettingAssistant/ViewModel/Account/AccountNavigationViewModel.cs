using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.ViewModel.Messages;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountNavigationViewModel : ViewModelBase
    {
        private ObservableCollection<AccountLookupItem> accounts;

        private AccountLookupItem selectedAccount;

        public AccountNavigationViewModel(IEnumerable<ITransactionAccount> transactionAccounts)
        {
            CreateAccounts(transactionAccounts);

            RegisterMessages();
        }

        public AccountLookupItem SelectedAccount
        {
            get => selectedAccount;
            set
            {
                selectedAccount = value;
                RaisePropertyChanged(()=>SelectedAccount);
            }
        }

        public ObservableCollection<AccountLookupItem> Accounts => this.accounts;

        private void CreateAccounts(IEnumerable<ITransactionAccount> transactionAccounts)
        {
            this.accounts = new ObservableCollection<AccountLookupItem>(transactionAccounts.Select(x => new AccountLookupItem(x)));

            this.SelectedAccount = this.accounts.FirstOrDefault();
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<AccountNameChangedMessage>(this, AccountNameChanged);
            Messenger.Default.Register<AccountAddedMessage>(this, AccountAdded);
        }

        private void AccountAdded(AccountAddedMessage obj)
        {
            var account = new AccountLookupItem(obj.Account);

            this.Accounts.Add(account);

            this.SelectedAccount = account;
        }

        private void AccountNameChanged(AccountNameChangedMessage message)
        {
            var account = this.accounts.FirstOrDefault(x => ReferenceEquals(x.Account, message.Account));

            account?.Refresh();
        }


    }
}