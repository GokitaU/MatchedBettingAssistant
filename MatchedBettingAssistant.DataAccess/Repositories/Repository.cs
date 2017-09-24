using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.DataAccess.Repositories
{
    public class Repository : IRepository
    {
        private readonly MatchedBettingAssistantDbContext dbContext;

        public Repository()
        {
            var initialiser = new MatchedBettingInitialiser();
            this.dbContext = new MatchedBettingAssistantDbContext(initialiser);
        }

        public void Create()
        {
            this.dbContext.Database.Initialize(true);
        }

        public IBookmakerRepository BookmakerRepository => new BookmakerRepository(dbContext);

        public IWalletRepository WalletRepository => new WalletRepository(dbContext);


    }
}