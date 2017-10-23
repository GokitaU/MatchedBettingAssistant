using System.Collections.Generic;

namespace MatchedBettingAssistant.Core
{
    public interface ILookup
    {
        int Id { get; set; }

        string Name { get; set; }
    }

    public interface ISport : ILookup
    {
        IEnumerable<IMarket> Markets { get; }
    }
}