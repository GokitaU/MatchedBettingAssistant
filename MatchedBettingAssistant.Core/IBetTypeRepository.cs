using System.Collections.Generic;

namespace MatchedBettingAssistant.Core
{
    public interface IBetTypeRepository
    {
        IBetType GetBetType(int id);

        IEnumerable<IBetType> GetBetTypes();

        IBetType New();

    }
}