using System.Collections.Generic;

namespace MatchedBettingAssistant.Core.Repositories
{
    public interface IOfferTypeRepository
    {
        IOfferType GetOfferType(int id);

        IEnumerable<IOfferType> GetOfferTypes();

        IOfferType New();
    }
}