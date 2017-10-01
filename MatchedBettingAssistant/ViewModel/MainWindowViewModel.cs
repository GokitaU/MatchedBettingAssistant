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
            this.CurrentViewModel = new BookmakerManagerViewModel(this.repository.BookmakerRepository);
        }
    }
}