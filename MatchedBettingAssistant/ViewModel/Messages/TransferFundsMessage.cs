using MatchedBettingAssistant.ViewModel.Account;

namespace MatchedBettingAssistant.ViewModel.Messages
{
    public class TransferFundsMessage
    {
        public TransferFundsMessage(TransferFundsToAccountViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public TransferFundsToAccountViewModel ViewModel { get;  }
    }
}