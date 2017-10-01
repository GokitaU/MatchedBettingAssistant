using System.Collections.Generic;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Accounts;


namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountViewModel : ViewModelBase
    {
        private IAccount account;
        private IRepository repository; 

        private readonly EditAccountViewModel editAccountViewModel;
        private readonly EditBookmakerViewModel editBookmakerViewModel;
        private readonly AccountTransactionListViewModel transactionListViewModel;

        public AccountViewModel(IBettingAccount account, IRepository repository)
        {
            this.account = account;
            this.repository = repository;
            this.editAccountViewModel = new EditAccountViewModel(account, this.repository);
            this.editBookmakerViewModel = new EditBookmakerViewModel(account, this.repository);
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
