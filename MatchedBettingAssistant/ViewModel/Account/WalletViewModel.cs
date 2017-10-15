using System.Collections.Generic;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Accounts;


namespace MatchedBettingAssistant.ViewModel.Account
{
    public class WalletViewModel : ViewModelBase
    {
        private readonly IWallet wallet;
        private readonly IRepository repository;

        private EditAccountViewModel editViewModel;

        private AccountTransactionListViewModel transactions;


        public WalletViewModel(IWallet wallet, IRepository repository)
        {
            this.wallet = wallet;
            this.repository = repository;
            this.CreateEditViewModel();
            this.CreateTransactions();
        }

        public EditAccountViewModel EditViewModel
        {
            get => this.editViewModel;
            set
            {
                this.editViewModel = value;
                RaisePropertyChanged(() => EditViewModel);
            }
        }

        public AccountTransactionListViewModel Transactions
        {
            get => this.transactions;
            set
            {
                this.transactions = value;
                RaisePropertyChanged(() => Transactions);
            }
        }

        private void CreateEditViewModel()
        {
            this.EditViewModel = new EditAccountViewModel(wallet, this.repository);
        }

        private void CreateTransactions()
        {
            this.Transactions = new AccountTransactionListViewModel(this.wallet, false);
        }

    }
}
