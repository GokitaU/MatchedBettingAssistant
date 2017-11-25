using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class EditWalletViewModel : EditAccountViewModel
    {
        public EditWalletViewModel(IRepository repository) : base(repository)
        {

        }

        protected override void New()
        {
            this.Account = this.Repository.WalletRepository.New();
            this.Name = "New Wallet";
        }


    }
}