using DevExpress.Mvvm;
using MatchedBettingAssistant.Model;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountViewModel : ViewModelBase
    {
        private IAccount account;

        private readonly EditAccountViewModel editAccountViewModel;

        private readonly AccountTransactionListViewModel transactionListViewModel;

        public AccountViewModel(IAccount account)
        {
            this.account = account;

            this.editAccountViewModel = new EditAccountViewModel(account);

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
    }
}
