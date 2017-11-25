using DevExpress.Mvvm;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountManagerViewModel : ViewModelBase, IAddsEntity, IDeletesEntity, IRollsBack
    {
        private AccountNavigationViewModel navigationViewModel;
        private AccountViewModel accountViewModel;

        public AccountManagerViewModel()
        {
            this.AccountViewModel = new AccountViewModel();
        }

        public AccountManagerViewModel(AccountViewModel account)
        {
            this.AccountViewModel = account;
        }

        public AccountNavigationViewModel NavigationViewModel
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

        public EditAccountViewModel EditViewModel
        {
            get => this.AccountViewModel?.EditViewModel;
            set
            {
                this.AccountViewModel.EditViewModel = value;

                RaisePropertyChanged(()=>EditViewModel);
            }
        }

        public void Unregister()
        {
            this.AccountViewModel.Unregister();
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

        public void SelectFirst()
        {
            this.NavigationViewModel.SelectFirst();
        }
    }
}