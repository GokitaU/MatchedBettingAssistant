using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.Repositories;
using MatchedBettingAssistant.ViewModel.Account;
using MatchedBettingAssistant.ViewModel.Messages;

namespace MatchedBettingAssistant.ViewModel
{

    public class MainWindowViewModel : ViewModelBase
    {
        private IRepository repository;
        private bool isConnected;

        private AccountManagerViewModel currentViewModel;

        private DelegateCommand connectCommand;
        private DelegateCommand connectAndCreateCommand;
        private DelegateCommand saveCommand;
        private DelegateCommand undoCommand;
        private DelegateCommand addCommand;
        private DelegateCommand deleteCommand;
        private DelegateCommand walletsCommand;
        private DelegateCommand bookmakersCommand;

        private DelegateCommand banksCommand;

        public MainWindowViewModel()
        {
            RegisterMessages();
        }

        public MainWindowViewModel(IRepository repository)
        {
            this.repository = repository;
        }

        public DelegateCommand ConnectCommand => this.connectCommand ?? (this.connectCommand = new DelegateCommand(Connect, IsNotConnected));
        public DelegateCommand ConnectAndCreateCommand => this.connectAndCreateCommand ??
                                                          (this.connectAndCreateCommand = new DelegateCommand(ConnectAndCreate, IsConnected));
        public DelegateCommand SaveCommand => this.saveCommand ?? (this.saveCommand = new DelegateCommand(Save, IsModified));
        public DelegateCommand UndoCommand => this.undoCommand ?? (this.undoCommand = new DelegateCommand(Undo, IsModified));
        public DelegateCommand DeleteCommand => this.deleteCommand ?? (this.deleteCommand = new DelegateCommand(Delete, false));
        public DelegateCommand AddCommand => this.addCommand ?? (this.addCommand = new DelegateCommand(Add, IsConnected));
        public DelegateCommand BookmakersCommand => this.bookmakersCommand ?? (this.bookmakersCommand = new DelegateCommand(Bookmakers, TypeEnabled));
        public DelegateCommand WalletsCommand => this.walletsCommand ?? (this.walletsCommand = new DelegateCommand(Wallets, TypeEnabled));
        public DelegateCommand BanksCommand => this.banksCommand ?? (this.banksCommand = new DelegateCommand(Banks, TypeEnabled));


        public AccountManagerViewModel CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel?.Unregister();

                currentViewModel = value;

                this.RaisePropertyChanged(()=>this.CurrentViewModel);
            }
        }

        private bool TypeEnabled()
        {
            return this.IsConnected() && !IsModified();
        }

        private bool IsModified()
        {
            return this.repository?.IsModified() ?? false;
        }

        private bool IsConnected()
        {
            return this.isConnected;
        }

        private bool IsNotConnected()
        {
            return !this.isConnected;
        }

        private void ConnectAndCreate()
        {
            this.Connect();
            this.repository.Create();

            CheckEnabledCommands();
        }

        private void CheckEnabledCommands()
        {
            this.ConnectCommand.RaiseCanExecuteChanged();
            this.SaveCommand.RaiseCanExecuteChanged();
            this.UndoCommand.RaiseCanExecuteChanged();
            this.AddCommand.RaiseCanExecuteChanged();
            this.DeleteCommand.RaiseCanExecuteChanged();
            this.BookmakersCommand.RaiseCanExecuteChanged();
            this.WalletsCommand.RaiseCanExecuteChanged();
            this.BanksCommand.RaiseCanExecuteChanged();
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<ModelSaveStatusChangedMessage>(this, ModelUpdated);
        }

        private void ModelUpdated(ModelSaveStatusChangedMessage message)
        {
            CheckEnabledCommands();
        }

        private void Connect()
        {
            this.repository = new Repository();
            this.isConnected = true;
            CheckEnabledCommands();
            Bookmakers();
        }

        private void Save()
        {
            this.repository?.Save();

            CheckEnabledCommands();
        }
        private void Undo()
        {
            this.repository?.Undo();

            if (this.CurrentViewModel is IRollsBack refreshable)
            {
                refreshable.Refresh();
                this.RaisePropertyChanged(()=>CurrentViewModel);     
            }
            CheckEnabledCommands();
        }

        private void Delete()
        {
            if (this.CurrentViewModel is IDeletesEntity deletableVm)
            {
                deletableVm.DeleteCurrent();
            }
        }

        private void Add()
        {
            if (this.CurrentViewModel is IAddsEntity addableVm)
            {
                addableVm.Add();
            }
        }

        private void Bookmakers()
        {
            var navigationViewModel = new AccountNavigationViewModel(this.repository.BookmakerRepository.GetAccounts());
            var editViewModel = new EditBookmakerViewModel(this.repository);
            var accountViewModel = new AccountManagerViewModel()
            {
                NavigationViewModel = navigationViewModel,
                EditViewModel = editViewModel
            };
            accountViewModel.SelectFirst();
            this.CurrentViewModel = accountViewModel;
        }

        private void Wallets()
        {
            var navigationViewModel = new AccountNavigationViewModel(this.repository.WalletRepository.GetWallets());
            var editViewModel = new EditWalletViewModel(this.repository);
            
            var accountViewModel = new AccountManagerViewModel()
            {
                NavigationViewModel = navigationViewModel,
                EditViewModel = editViewModel,
                
            };
            accountViewModel.SelectFirst();
            this.CurrentViewModel = accountViewModel;
        }

        private void Banks()
        {
            //this.CurrentViewModel = new BankManagerViewModel(this.repository);
        }
    }
}