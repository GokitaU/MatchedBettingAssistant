using System.Collections.Generic;

namespace MatchedBettingAssistant.Core.Repositories
{
    public interface ISportRepository
    {
        ISport GetSport(int id);

        IEnumerable<ISport> GetSports();

        ISport New();
    }
}