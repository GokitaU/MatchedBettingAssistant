using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class DepositActionAccountSetter : ITransferActionAccountSetter
    {
        private TransferFundsAccountAction action;

        public DepositActionAccountSetter(TransferFundsAccountAction action)
        {
            this.action = action;
        }

        public string ActionDescription => "Deposit";

        public string AccountName => this.action.Destination?.Name;

        public void SetAccount(ITransactionAccount account)
        {
            this.action.Source = account;
        }
    }
}