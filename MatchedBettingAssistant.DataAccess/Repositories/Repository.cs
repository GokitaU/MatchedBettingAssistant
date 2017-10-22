using System;
using System.Data.Entity.Infrastructure;
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

        public IBankRepository BankRepository => new BankRepository(dbContext);

        public IBetTypeRepository BetTypeRepository => new BetTypeRepository(dbContext);

        public IOfferTypeRepository OfferTypeRepository => new OfferTypeRepository(dbContext);

        public ITransactionRepository TransactionRepository => new TransactionRepository(dbContext);


        public void Save()
        {
            DisplayTrackedEntities(this.dbContext.ChangeTracker);
            this.dbContext.SaveChanges();
        }

        private static void DisplayTrackedEntities(DbChangeTracker changeTracker)
        {
            Console.WriteLine("");

            var entries = changeTracker.Entries();
            foreach (var entry in entries)
            {
                Console.WriteLine("Entity Name: {0}", entry.Entity.GetType().FullName);
                Console.WriteLine("Status: {0}", entry.State);
            }
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------");
        }
    }
}