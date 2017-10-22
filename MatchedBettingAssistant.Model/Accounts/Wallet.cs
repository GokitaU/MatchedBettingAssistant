using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Accounts
{
    public class Wallet : TransactionAccount, IWallet
    {
        public Wallet(IWallet baseAccount) : base(baseAccount)
        {
        }
    }
}