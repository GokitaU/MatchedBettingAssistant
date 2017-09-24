using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class WithdrawActionWalletSetter : ITransferActionWalletSetter
    {
        private TransferFundsAccountAction action;

        public WithdrawActionWalletSetter(TransferFundsAccountAction action)
        {
            this.action = action;
        }

        public string ActionDescription => "Withdraw";

        public string AccountName
        {
            get { return this.action.Source?.Name; }
        }

        public void SetWallet(IAccount account)
        {
            this.action.Destination = account;
        }
    }
}