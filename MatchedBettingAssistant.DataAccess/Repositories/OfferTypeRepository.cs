using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;
using MatchedBettingAssistant.DataAccess.DTO;

namespace MatchedBettingAssistant.DataAccess.Repositories
{
    public class OfferTypeRepository : IOfferTypeRepository
    {
        private readonly MatchedBettingAssistantDbContext dataContext;

        public OfferTypeRepository(MatchedBettingAssistantDbContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IOfferType GetOfferType(int id)
        {
            var offerType = dataContext.OfferTypes.FirstOrDefault(x => x.Id == id);

            if (offerType != null)
            {
                return new OfferTypeDto(offerType);
            }

            return null;
        }

        public IEnumerable<IOfferType> GetOfferTypes()
        {
            var offerTypes = dataContext.OfferTypes.ToList();

            return new List<IOfferType>(offerTypes.Select(x => new OfferTypeDto(x)));
        }

        public IOfferType New()
        {
            var type = new OfferType();
            this.dataContext.OfferTypes.Add(type);
            return new OfferTypeDto(type);
        }
    }

    public class TransactionRepository : ITransactionRepository
    {

        public ITransaction New()
        {
            return  new TransactionDto(new Transaction() );
        }

        public ITransactionDetail NewDetail()
        {
            return new TransactionDetailDto(new TransactionDetail());
        }
    }
}