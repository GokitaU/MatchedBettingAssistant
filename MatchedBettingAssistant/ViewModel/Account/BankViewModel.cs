using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BankViewModel : ViewModelBase
    {
        private readonly IBank bank;
        private readonly IRepository repository;

        private EditBankViewModel editViewModel;

        private AccountTransactionListViewModel transactions;


        public BankViewModel(IBank bank, IRepository repository)
        {
            this.bank = bank;
            this.repository = repository;
            this.CreateEditViewModel();
            this.CreateTransactions();
        }

        public EditBankViewModel EditViewModel
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
            this.EditViewModel = new EditBankViewModel(bank, this.repository);
        }

        private void CreateTransactions()
        {
            this.Transactions = new AccountTransactionListViewModel(this.bank, false);
        }
    }
}