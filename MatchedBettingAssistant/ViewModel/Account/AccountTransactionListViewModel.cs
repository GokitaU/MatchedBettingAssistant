using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Model;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountTransactionListViewModel : ViewModelBase
    {
        private readonly IAccount account;

        public AccountTransactionListViewModel(IAccount account)
        {
            this.account = account;
            CreateTransactions();
            this.RegisterMessages();
        }

        public ObservableCollection<AccountTransactionViewModel> Transactions { get; private set; }

        private void RegisterMessages()
        {
            Messenger.Default.Register<TransactionsUpdatedMessage>(this, RefreshTransactions);
        }

        private void RefreshTransactions(TransactionsUpdatedMessage message)
        {
            CreateTransactions();
        }

        private void CreateTransactions()
        {
            var transactions = this.account.Transactions.Select(x => new AccountTransactionViewModel(x));
            this.Transactions = new ObservableCollection<AccountTransactionViewModel>(transactions);
            RaisePropertyChanged(()=>Transactions);
        }
    }
}