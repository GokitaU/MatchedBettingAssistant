using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.Repositories;
using MatchedBettingAssistant.ViewModel.Account;

namespace MatchedBettingAssistant.ViewModel
{

    public class MainWindowViewModel : ViewModelBase
    {
        private IRepository repository;
        private bool isConnected;

        private ViewModelBase currentViewModel;

        private DelegateCommand connectCommand;
        private DelegateCommand connectAndCreateCommand;
        private DelegateCommand saveCommand;
        private DelegateCommand addCommand;
        private DelegateCommand deleteCommand;
        private DelegateCommand walletsCommand;
        private DelegateCommand bookmakersCommand;

        private DelegateCommand banksCommand;


        public MainWindowViewModel()
        {
            
        }

        public MainWindowViewModel(IRepository repository)
        {
            this.repository = repository;
        }

        public DelegateCommand ConnectCommand => this.connectCommand ?? (this.connectCommand = new DelegateCommand(Connect, IsNotConnected));
        public DelegateCommand ConnectAndCreateCommand => this.connectAndCreateCommand ??
                                                          (this.connectAndCreateCommand = new DelegateCommand(ConnectAndCreate, IsConnected));
        public DelegateCommand SaveCommand => this.saveCommand ?? (this.saveCommand = new DelegateCommand(Save, IsConnected));
        public DelegateCommand DeleteCommand => this.deleteCommand ?? (this.deleteCommand = new DelegateCommand(Delete, IsConnected));
        public DelegateCommand AddCommand => this.addCommand ?? (this.addCommand = new DelegateCommand(Add, IsConnected));
        public DelegateCommand BookmakersCommand => this.bookmakersCommand ?? (this.bookmakersCommand = new DelegateCommand(Bookmakers, IsConnected));
        public DelegateCommand WalletsCommand => this.walletsCommand ?? (this.walletsCommand = new DelegateCommand(Wallets, IsConnected));
        public DelegateCommand BanksCommand => this.banksCommand ?? (this.banksCommand = new DelegateCommand(Banks, IsConnected));

        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;

                this.RaisePropertyChanged(()=>this.CurrentViewModel);
            }
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
            this.AddCommand.RaiseCanExecuteChanged();
            this.DeleteCommand.RaiseCanExecuteChanged();
            this.BookmakersCommand.RaiseCanExecuteChanged();
            this.WalletsCommand.RaiseCanExecuteChanged();
            this.BanksCommand.RaiseCanExecuteChanged();
        }

        private void Connect()
        {
            this.repository = new Repository();
            this.isConnected = true;
            CheckEnabledCommands();
            this.CurrentViewModel = new BookmakerManagerViewModel(this.repository);
        }

        private void Save()
        {
            this.repository?.Save();
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
            this.CurrentViewModel = new BookmakerManagerViewModel(this.repository);
        }

        private void Wallets()
        {
            this.CurrentViewModel = new WalletManagerViewModel(this.repository);
        }

        private void Banks()
        {
            this.CurrentViewModel = new BankManagerViewModel(this.repository);
        }
    }
}