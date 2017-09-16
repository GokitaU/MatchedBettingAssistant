using MatchedBettingAssistant.Model;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountLookupItem
    {
        private readonly IAccount account;

        public AccountLookupItem(IAccount account)
        {
            this.account = account;
        }

        public string Name => this.account.Name;

        internal IAccount Account => this.account;
    }
}