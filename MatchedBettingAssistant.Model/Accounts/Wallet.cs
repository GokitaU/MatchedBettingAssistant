using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Accounts
{
    public class Wallet : Account, IWallet
    {
        public Wallet(IWallet baseAccount) : base(baseAccount)
        {
        }
    }
}