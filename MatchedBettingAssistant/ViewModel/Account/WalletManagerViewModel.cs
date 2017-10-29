using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class WalletManagerViewModel : AccountManagerViewModel
    {
        private readonly IRepository repository;

        private WalletNavigationViewModel navigationViewModel;
        private WalletViewModel editViewModel;

        public WalletManagerViewModel(IRepository repository)
        {
            this.repository = repository;
            this.RegisterMessages();

            this.navigationViewModel = new WalletNavigationViewModel(this.repository.WalletRepository);

        }

        public WalletNavigationViewModel NavigationViewModel
        {
            get { return navigationViewModel; }
            set
            {
                this.navigationViewModel = value;
                
                RaisePropertyChanged(()=>NavigationViewModel);
            }
        }

        public WalletViewModel EditViewModel
        {
            get => this.editViewModel;
            set
            {
                this.editViewModel = value;
                RaisePropertyChanged(() => EditViewModel);
            }
        }

        public override void Add()
        {
            var wallet = this.repository.WalletRepository.New();
            wallet.Name = "New Wallet";
            this.NavigationViewModel.Add(wallet);
        }

        public override void DeleteCurrent()
        {
        }

        public override void Refresh()
        {
            this.NavigationViewModel = new WalletNavigationViewModel(this.repository.WalletRepository);
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<SelectedWalletChangedMessage>(this, WalletChanged);
        }

        private void WalletChanged(SelectedWalletChangedMessage message)
        {
            this.EditViewModel = new WalletViewModel(message.Account, this.repository);

        }
    }
}