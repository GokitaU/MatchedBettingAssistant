using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BankTransactionListViewModel : ViewModelBase
    {
        private IBank bank;
        private BankTransactionViewModel selectedItem;

        public BankTransactionListViewModel(IBank bank)
        {
            this.bank = bank;

            CreateTransactions();
        }

        public ObservableCollection<BankTransactionViewModel> Transactions { get; private set; }

        public BankTransactionViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisePropertyChanged(()=>SelectedItem);
            }
        }

        private void CreateTransactions()
        {
            if (this.bank.Transactions != null)
            {
                var transactions = this.bank.Transactions.Select(x => new BankTransactionViewModel(x));
                this.Transactions = new ObservableCollection<BankTransactionViewModel>(transactions);
                this.SelectedItem = this.Transactions.FirstOrDefault();
                RaisePropertyChanged(() => Transactions);
            }
        }

    }
}