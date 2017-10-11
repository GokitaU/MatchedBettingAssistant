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

            RegisterMessages();
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<BookmakerNameChangedMessage>(this, BookmakerNameChanged);
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

        public void Add(IBettingAccount account)
        {
            var bookmaker = new BookmakerLookupItem(account);
            this.Bookmakers.Add(bookmaker);

            this.SelectedBookmaker = bookmaker;
        }

        private void CreateBookmakers()
        {
            var bookies = this.bookmakerRepository.GetAccounts();

            this.bookmakers = new ObservableCollection<BookmakerLookupItem>(bookies.Select(x => new BookmakerLookupItem(x)));
        }

        private void BookmakerNameChanged(BookmakerNameChangedMessage message)
        {
            var account = this.bookmakers.FirstOrDefault(x => ReferenceEquals(x.Account, message.Account));

            account?.Refresh();
        } 
    }
}