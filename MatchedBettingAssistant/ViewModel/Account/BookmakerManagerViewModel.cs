using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BookmakerManagerViewModel : ViewModelBase
    {
        private IRepository repository;

        private BookmakerNavigationViewModel navigationViewModel;
        private BookmakerViewModel bookmakerEditViewModel;

        public BookmakerManagerViewModel(IRepository repository)
        {
            this.repository = repository;
            this.NavigationViewModel = new BookmakerNavigationViewModel(this.repository.BookmakerRepository);      
            RegisterMessages();
        }

        public BookmakerNavigationViewModel NavigationViewModel
        {
            get { return navigationViewModel; }
            private set
            {
                this.navigationViewModel = value;
                RaisePropertyChanged(()=>NavigationViewModel);
            }
        }

        public BookmakerViewModel BookmakerEditViewModel
        {
            get => this.bookmakerEditViewModel;
            set
            {
                this.bookmakerEditViewModel = value;
                RaisePropertyChanged(()=>BookmakerEditViewModel);
            }
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register< SelectedBookmakerChangedMessage>(this, BookmakerChanged);
        }

        private void BookmakerChanged(SelectedBookmakerChangedMessage message)
        {
            this.BookmakerEditViewModel = new BookmakerViewModel(message.Bookmaker, this.repository);
        }
    }
}