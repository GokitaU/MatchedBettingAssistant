using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Core.Repositories;
using MatchedBettingAssistant.ViewModel.Messages;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BankNavigationViewModel : ViewModelBase
    {
        private readonly IBankRepository bankRepository;

        private ObservableCollection<BankLookupItem> lookupItems;

        private BankLookupItem selectedAccount;

        public BankNavigationViewModel(IBankRepository bankRepository)
        {
            this.bankRepository = bankRepository;

            CreateBanks();

            RegisterMessages();
        }

        public BankLookupItem SelectedAccount
        {
            get => selectedAccount;
            set
            {
                selectedAccount = value;

                if (selectedAccount != null)
                {
                    Messenger.Default.Send(new SelectedBankChangedMessage(this.selectedAccount.Account));
                }
                RaisePropertyChanged(() => SelectedAccount);
            }
        }

        public ObservableCollection<BankLookupItem> Accounts => this.lookupItems;


        public void Add(IBank account)
        {
            var wallet = new BankLookupItem(account);
            this.Accounts.Add(wallet);

            this.SelectedAccount = wallet;
        }

        private void CreateBanks()
        {
            var bookies = this.bankRepository.GetAccounts();

            this.lookupItems = new ObservableCollection<BankLookupItem>(bookies.Select(x => new BankLookupItem(x)));

            this.SelectedAccount = this.lookupItems.FirstOrDefault();
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<WalletNameChangedMessage>(this, BankNameChanged);
        }

        private void BankNameChanged(WalletNameChangedMessage message)
        {
            var account = this.Accounts.FirstOrDefault(x => ReferenceEquals(x.Account, message.Account));

            account?.Refresh();
        }

    }
}