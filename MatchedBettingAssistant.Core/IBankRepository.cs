using System.Collections.Generic;

namespace MatchedBettingAssistant.Core
{
    public interface IBankRepository
    {
        IEnumerable<IBank> GetAccounts();

        IBank New();
    }
}