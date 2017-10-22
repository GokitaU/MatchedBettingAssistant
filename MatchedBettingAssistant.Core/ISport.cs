using System.Collections.Generic;

namespace MatchedBettingAssistant.Core
{
    public interface ISport
    {
        int Id { get; set; }

        string Name { get; set; }

        IEnumerable<IMarket> Markets { get; }
    }
}