using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public abstract class BetViewModel : ViewModelBase
    {
        public abstract void Commit();

        internal abstract ITransactionDetail Detail { get; }
    }
}