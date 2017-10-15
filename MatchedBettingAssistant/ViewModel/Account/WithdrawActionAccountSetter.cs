using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class WithdrawActionAccountSetter : ITransferActionAccountSetter
    {
        private TransferFundsAccountAction action;

        public WithdrawActionAccountSetter(TransferFundsAccountAction action)
        {
            this.action = action;
        }

        public string ActionDescription => "Withdraw";

        public string AccountName
        {
            get { return this.action.Source?.Name; }
        }

        public void SetAccount(IAccount account)
        {
            this.action.Destination = account;
        }
    }
}