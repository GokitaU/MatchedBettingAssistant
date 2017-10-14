using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BookmakerManagerViewModel : ViewModelBase, IAddsEntity, IDeletesEntity
    {
        private readonly IRepository repository;

        private BookmakerNavigationViewModel navigationViewModel;
        private BookmakerViewModel editViewModel;

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

        public BookmakerViewModel EditViewModel
        {
            get => this.editViewModel;
            set
            {
                this.editViewModel = value;
                RaisePropertyChanged(()=>EditViewModel);
            }
        }

        /// <summary>
        /// Adds a new bookmaker
        /// </summary>
        public void Add()
        {
            var bookmaker = this.repository.BookmakerRepository.New();
            bookmaker.Name = "New Bookmaker";
            this.navigationViewModel.Add(bookmaker);
        }

        public void DeleteCurrent()
        {
            
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register< SelectedBookmakerChangedMessage>(this, BookmakerChanged);
        }

        private void BookmakerChanged(SelectedBookmakerChangedMessage message)
        {
            this.EditViewModel = new BookmakerViewModel(message.Bookmaker, this.repository);
        }
    }
}