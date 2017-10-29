using DevExpress.Mvvm;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public abstract class AccountManagerViewModel : ViewModelBase, IAddsEntity, IDeletesEntity, IRollsBack
    {
        public abstract void Add();

        public abstract void DeleteCurrent();

        public abstract void Refresh();

    }
}