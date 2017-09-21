using DevExpress.Mvvm;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public abstract class BetViewModel : ViewModelBase
    {
        public abstract void Commit();
    }
}