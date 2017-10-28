using System.Collections.Generic;

namespace MatchedBettingAssistant.Core.Repositories
{
    public interface IBookmakerRepository
    {
        IEnumerable<IBettingAccount> GetAccounts();

        IBettingAccount New();

        int Count();

        int Count(int excluding);
    }

    
}
