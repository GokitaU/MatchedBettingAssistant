using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BookmakerNavigationViewModel : ViewModelBase
    {
        private readonly IBookmakerRepository bookmakerRepository;
        private BookmakerLookupItem selectedAccount;

        private ObservableCollection<BookmakerLookupItem> accounts;

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

        public BookmakerLookupItem SelectedAccount
        {
            get => this.selectedAccount;
            set
            {
                this.selectedAccount = value;
                if (selectedAccount != null)
                {
                    Messenger.Default.Send(new SelectedBookmakerChangedMessage(this.selectedAccount.Account));
                }
                RaisePropertyChanged(()=>SelectedAccount);
            }
        }

        public ObservableCollection<BookmakerLookupItem> Accounts => this.accounts;

        public void Add(IBettingAccount account)
        {
            var bookmaker = new BookmakerLookupItem(account);
            this.Accounts.Add(bookmaker);

            this.SelectedAccount = bookmaker;
        }

        private void CreateBookmakers()
        {
            var bookies = this.bookmakerRepository.GetAccounts();

            this.accounts = new ObservableCollection<BookmakerLookupItem>(bookies.Select(x => new BookmakerLookupItem(x)));
        }

        private void BookmakerNameChanged(BookmakerNameChangedMessage message)
        {
            var account = this.accounts.FirstOrDefault(x => ReferenceEquals(x.Account, message.Account));

            account?.Refresh();
        } 
    }
}