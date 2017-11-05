using MatchedBettingAssistant.ViewModel.Account;

namespace MatchedBettingAssistant.ViewModel.Messages
{
    public class PlaceBetMessage
    {
        public PlaceBetMessage(PlaceBetViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public PlaceBetViewModel ViewModel { get; }
    }
}