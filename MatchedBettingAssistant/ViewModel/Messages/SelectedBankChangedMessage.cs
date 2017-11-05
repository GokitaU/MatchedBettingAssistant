using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Messages
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