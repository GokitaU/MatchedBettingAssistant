using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BankLookupItem : AccountLookupItem<IBank>
    {
        public BankLookupItem(IBank account) : base(account)
        {
        }
    }
}