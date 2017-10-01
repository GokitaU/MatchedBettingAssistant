using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BookmakerManagerViewModel : ViewModelBase
    {
        private IBookmakerRepository bookmakerRepository;

        private BookmakerNavigationViewModel navigationViewModel;
        private BookmakerViewModel bookmakerEditViewModel;

        public BookmakerManagerViewModel(IBookmakerRepository bookmakerRepository)
        {
            this.bookmakerRepository = bookmakerRepository;
            this.NavigationViewModel = new BookmakerNavigationViewModel(this.bookmakerRepository);
            this.CreateEditViewModel();
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

        private void CreateEditViewModel()
        {
            var first = this.bookmakerRepository.GetAccounts().OfType<Model.Accounts.Bookmaker>().FirstOrDefault();

            this.BookmakerEditViewModel = new BookmakerViewModel(first, this.bookmakerRepository);
        }
    }
}