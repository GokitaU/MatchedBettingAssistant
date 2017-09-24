using System.Collections.Generic;

namespace MatchedBettingAssistant.Core
{
    public interface IWalletRepository
    {
        IEnumerable<IWallet> GetWallets();

        IWallet New();
    }
}