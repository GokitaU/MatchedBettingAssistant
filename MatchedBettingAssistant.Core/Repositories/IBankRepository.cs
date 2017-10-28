using System.Collections.Generic;

namespace MatchedBettingAssistant.Core.Repositories
{
    public interface IBankRepository
    {
        IEnumerable<IBank> GetAccounts();

        IBank New();
    }
}