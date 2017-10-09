using System.Collections.Generic;

namespace MatchedBettingAssistant.Core
{
    public interface IOfferTypeRepository
    {
        IOfferType GetOfferType(int id);

        IEnumerable<IOfferType> GetOfferTypes();

        IOfferType New();
    }
}