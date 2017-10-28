using System.Collections.Generic;

namespace MatchedBettingAssistant.Core.Repositories
{
    public interface IWalletRepository
    {
        IEnumerable<IWallet> GetWallets();

        IWallet New();
        int Count();

        int Count(int excluding);
    }
}