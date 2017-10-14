using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class WalletManagerViewModel : ViewModelBase, IAddsEntity, IDeletesEntity
    {
        private readonly IRepository repository;

        private WalletNavigationViewModel navigationViewModel;
        private WalletViewModel editViewModel;

        public WalletManagerViewModel(IRepository repository)
        {
            this.repository = repository;
            this.navigationViewModel = new WalletNavigationViewModel(this.repository.WalletRepository);

            this.RegisterMessages();
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

        public void Add()
        {
            var wallet = this.repository.WalletRepository.New();
            wallet.Name = "New Wallet";
            this.NavigationViewModel.Add(wallet);
        }

        public void DeleteCurrent()
        {
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