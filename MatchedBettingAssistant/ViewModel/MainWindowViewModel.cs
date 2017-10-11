using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.Repositories;
using MatchedBettingAssistant.ViewModel.Account;

namespace MatchedBettingAssistant.ViewModel
{

    public class MainWindowViewModel : ViewModelBase
    {
        private IRepository repository;

        private ViewModelBase currentViewModel;

        private DelegateCommand connectCommand;
        private DelegateCommand connectAndCreateCommand;
        private DelegateCommand saveCommand;
        private DelegateCommand addCommand;
        private DelegateCommand deleteCommand;

        public MainWindowViewModel()
        {
            
        }

        public MainWindowViewModel(IRepository repository)
        {
            this.repository = repository;
        }

        public DelegateCommand ConnectCommand => this.connectCommand ?? (this.connectCommand = new DelegateCommand(Connect));
        public DelegateCommand ConnectAndCreateCommand => this.connectAndCreateCommand ??
                                                          (this.connectAndCreateCommand = new DelegateCommand(ConnectAndCreate));
        public DelegateCommand SaveCommand => this.saveCommand ?? (this.saveCommand = new DelegateCommand(Save));
        public DelegateCommand DeleteCommand => this.deleteCommand ?? (this.deleteCommand = new DelegateCommand(Delete));
        public DelegateCommand AddCommand => this.addCommand ?? (this.addCommand = new DelegateCommand(Add));

        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;

                this.RaisePropertyChanged(()=>this.CurrentViewModel);
            }
        }

        private void ConnectAndCreate()
        {
            this.Connect();
            this.repository.Create();
        }

        private void Connect()
        {
            this.repository = new Repository();
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
    }
}