using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BankManagerViewModel : AccountManagerViewModel
    {
        private readonly IRepository repository;

        private BankNavigationViewModel navigationViewModel;
        private BankViewModel editViewModel;

        public BankManagerViewModel(IRepository repository)
        {
            this.repository = repository;

            this.navigationViewModel = new BankNavigationViewModel(this.repository.BankRepository);

            RegisterMessages();
        }

        public BankNavigationViewModel NavigationViewModel
        {
            get => navigationViewModel;
            set
            {
                this.navigationViewModel = value;

                RaisePropertyChanged(() => NavigationViewModel);
            }
        }

        public BankViewModel EditViewModel
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
            var bank = this.repository.BankRepository.New();
            bank.Name = "New Bank";
            this.NavigationViewModel.Add(bank);
        }

        public override void DeleteCurrent()
        {
        }

        public override void Refresh()
        {
            this.NavigationViewModel = new BankNavigationViewModel(this.repository.BankRepository);
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<SelectedBankChangedMessage>(this, BankChanged);
        }

        private void BankChanged(SelectedBankChangedMessage message)
        {
            this.EditViewModel = new BankViewModel(message.Account, this.repository);

        }
    }
}