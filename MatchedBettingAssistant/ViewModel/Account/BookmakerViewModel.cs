using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BookmakerViewModel : ViewModelBase
    {
        private readonly IBettingAccount bookmaker;
        private readonly IRepository repository;

        private EditBookmakerViewModel bookmakerEdit;

        private AccountTransactionListViewModel transactions;


        public BookmakerViewModel(IBettingAccount bookmaker, IRepository repository)
        {
            this.bookmaker = bookmaker;
            this.repository = repository;
            this.CreateEditViewModel();
            this.CreateTransactions();
        }

        public EditBookmakerViewModel BookmakerEdit
        {
            get => this.bookmakerEdit;
            set
            {
                this.bookmakerEdit = value;
                RaisePropertyChanged(()=>BookmakerEdit);
            }
        }

        public AccountTransactionListViewModel Transactions
        {
            get => this.transactions;
            set
            {
                this.transactions = value;
                RaisePropertyChanged(()=>Transactions);
            }
        }

        private void CreateEditViewModel()
        {
            this.BookmakerEdit = new EditBookmakerViewModel(bookmaker, this.repository);
        }

        private void CreateTransactions()
        {
            this.Transactions = new AccountTransactionListViewModel(this.bookmaker);
        }
    }
}