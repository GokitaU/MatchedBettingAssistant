using DevExpress.Mvvm;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountManagerViewModel : ViewModelBase, IAddsEntity, IDeletesEntity, IRollsBack
    {
        private ViewModelBase navigationViewModel;
        private AccountViewModel accountViewModel;

        public AccountManagerViewModel()
        {
            this.AccountViewModel = new AccountViewModel();
        }

        public ViewModelBase NavigationViewModel
        {
            get => navigationViewModel;
            set
            {
                this.navigationViewModel = value;
                RaisePropertyChanged(() => NavigationViewModel);
            }
        }

        public AccountViewModel AccountViewModel
        {
            get => this.accountViewModel;
            set
            {
                this.accountViewModel = value;
                RaisePropertyChanged(() => AccountViewModel);
            }
        }

        public ViewModelBase EditViewModel
        {
            get => this.AccountViewModel?.EditViewModel;
            set
            {
                this.AccountViewModel.EditViewModel = value;

                RaisePropertyChanged(()=>EditViewModel);
            }
        }

        public void Add()
        {
            this.AccountViewModel?.Add();
        }

        public void DeleteCurrent()
        {
            
        }

        public void Refresh()
        {
            
        }

    }
}