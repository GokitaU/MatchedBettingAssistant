using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;
using MatchedBettingAssistant.DataAccess.DTO;

namespace MatchedBettingAssistant.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private MatchedBettingAssistantDbContext context;

        public TransactionRepository(MatchedBettingAssistantDbContext context)
        {
            this.context = context;
        }

        public ITransaction New()
        {
            return  new TransactionDto(new Transaction() );
        }

        public ITransactionDetail NewDetail()
        {
            var detail = new TransactionDetail();

            this.context.TransactionDetails.Add(detail);

            return new TransactionDetailDto(detail);
        }
    }
}