using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BankTransactionViewModel : ViewModelBase
    {
        private readonly ITransactionDetail detail;

        public BankTransactionViewModel(ITransactionDetail detail)
        {
            this.detail = detail;
        }

        public double Profit => this.detail.Profit;
    }
}