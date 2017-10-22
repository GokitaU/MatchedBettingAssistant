using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class SelectedBankChangedMessage
    {
        public SelectedBankChangedMessage(IBank account)
        {
            Account = account;
        }

        public IBank Account { get; }
    }
}