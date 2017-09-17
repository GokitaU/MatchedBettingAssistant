using DevExpress.Mvvm;
using MatchedBettingAssistant.Model;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountViewModel : ViewModelBase
    {
        private IAccount account;

        private readonly EditAccountViewModel editAccountViewModel;
        private readonly EditBookmakerViewModel editBookmakerViewModel;
        private readonly AccountTransactionListViewModel transactionListViewModel;

        public AccountViewModel(IBettingAccount account)
        {
            this.account = account;

            this.editAccountViewModel = new EditAccountViewModel(account);
            this.editBookmakerViewModel = new EditBookmakerViewModel(account);
            this.transactionListViewModel = new AccountTransactionListViewModel(account);
        }

        public EditAccountViewModel EditAccountViewModel
        {
            get { return editAccountViewModel; }
        }

        public AccountTransactionListViewModel TransactionListViewModel
        {
            get { return transactionListViewModel; }
        }

        public EditBookmakerViewModel BookmakerViewModel
        {
            get { return editBookmakerViewModel; }
        }
    }
}
