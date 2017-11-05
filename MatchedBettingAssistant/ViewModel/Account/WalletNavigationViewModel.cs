using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Core.Repositories;
using MatchedBettingAssistant.ViewModel.Messages;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class WalletNavigationViewModel : ViewModelBase
    {
        private readonly IWalletRepository walletRepository;

        private ObservableCollection<WalletLookupItem> lookupItems;

        private WalletLookupItem selectedAccount;

        public WalletNavigationViewModel(IWalletRepository walletRepository)
        {
            this.walletRepository = walletRepository;

            CreateWallets();

            RegisterMessages();
        }

        public WalletLookupItem SelectedAccount
        {
            get => selectedAccount;
            set
            {
                selectedAccount = value;

                if (selectedAccount != null)
                {
                    Messenger.Default.Send(new SelectedWalletChangedMessage(this.selectedAccount.Account));
                }
                RaisePropertyChanged(()=>SelectedAccount);
            }
        }

        public ObservableCollection<WalletLookupItem> Accounts => this.lookupItems;


        public void Add(IWallet account)
        {
            var wallet = new WalletLookupItem(account);
            this.Accounts.Add(wallet);

            this.SelectedAccount = wallet;
        }

        private void CreateWallets()
        {
            var bookies = this.walletRepository.GetWallets().OrderBy(x => x.Name);

            this.lookupItems = new ObservableCollection<WalletLookupItem>(bookies.Select(x => new WalletLookupItem(x)));

            this.SelectedAccount = this.lookupItems.FirstOrDefault();
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<WalletNameChangedMessage>(this, WalletNameChanged);
        }

        private void WalletNameChanged(WalletNameChangedMessage message)
        {
            var account = this.Accounts.FirstOrDefault(x => ReferenceEquals(x.Account, message.Account));

            account?.Refresh();
        }
    }
}