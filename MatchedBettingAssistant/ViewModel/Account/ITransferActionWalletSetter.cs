using MatchedBettingAssistant.Model;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public interface ITransferActionWalletSetter
    {
        string ActionDescription { get;  }

        string AccountName { get; }

        void SetWallet(IAccount account);
    }
}