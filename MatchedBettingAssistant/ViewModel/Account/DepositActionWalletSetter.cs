using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Account;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class DepositActionWalletSetter : ITransferActionWalletSetter
    {
        private TransferFundsAccountAction action;

        public DepositActionWalletSetter(TransferFundsAccountAction action)
        {
            this.action = action;
        }

        public string ActionDescription => "Deposit";

        public string AccountName
        {
            get { return this.action.Destination?.Name; }
        }

        public void SetWallet(IAccount account)
        {
            this.action.Source = account;
        }
    }
}