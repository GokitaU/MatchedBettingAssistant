using System.Collections.Generic;

namespace MatchedBettingAssistant.Core.Repositories
{
    public interface IBetTypeRepository
    {
        IBetType GetBetType(int id);

        IEnumerable<IBetType> GetBetTypes();

        IBetType New();

    }
}