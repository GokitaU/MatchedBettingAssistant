using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BookmakerNavigationViewModel : ViewModelBase
    {
        private readonly IBookmakerRepository bookmakerRepository;
        private BookmakerLookupItem selectedBookmaker;

        private ObservableCollection<BookmakerLookupItem> bookmakers;

        public BookmakerNavigationViewModel(IBookmakerRepository bookmakerRepository)
        {
            this.bookmakerRepository = bookmakerRepository;

            CreateBookmakers();
        }

        public BookmakerLookupItem SelectedBookmaker
        {
            get => this.selectedBookmaker;
            set
            {
                this.selectedBookmaker = value;
                Messenger.Default.Send(new SelectedBookmakerChangedMessage(this.selectedBookmaker.Account));
                RaisePropertyChanged(()=>SelectedBookmaker);
            }
        }

        public ObservableCollection<BookmakerLookupItem> Bookmakers
        {
            get { return this.bookmakers; }
        }

        private void CreateBookmakers()
        {
            var bookies = this.bookmakerRepository.GetAccounts();

            this.bookmakers = new ObservableCollection<BookmakerLookupItem>(bookies.Select(x => new BookmakerLookupItem(x)));
        }        
    }
}