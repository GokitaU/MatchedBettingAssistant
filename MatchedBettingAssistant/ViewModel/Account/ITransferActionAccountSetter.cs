using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public interface ITransferActionAccountSetter
    {
        string ActionDescription { get;  }

        string AccountName { get; }

        void SetAccount(ITransactionAccount account);
    }
}