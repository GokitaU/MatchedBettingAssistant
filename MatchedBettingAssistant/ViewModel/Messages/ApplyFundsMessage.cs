using MatchedBettingAssistant.ViewModel.Account;

namespace MatchedBettingAssistant.ViewModel.Messages
{
    public class ApplyFundsMessage
    {
        public ApplyFundsMessage(ApplyFundsToAccountViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public ApplyFundsToAccountViewModel ViewModel { get; }
    }
}