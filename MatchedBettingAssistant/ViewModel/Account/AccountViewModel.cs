using System.Collections.Generic;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Account;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountViewModel : ViewModelBase
    {
        private IAccount account;

        private readonly EditAccountViewModel editAccountViewModel;
        private readonly EditBookmakerViewModel editBookmakerViewModel;
        private readonly AccountTransactionListViewModel transactionListViewModel;

        public AccountViewModel(IBettingAccount account, IEnumerable<Wallet> wallets)
        {
            this.account = account;

            this.editAccountViewModel = new EditAccountViewModel(account, wallets);
            this.editBookmakerViewModel = new EditBookmakerViewModel(account, wallets);
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
