using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Core.Repositories;

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

        public ISportRepository SportRepository => new SportRepository(dbContext);

        public IMarketRepository MarketRepository => new MarketRepository(dbContext);

        public bool IsModified()
        {
            return this.dbContext.ChangeTracker.HasChanges();
        }

        public void Save()
        {
            DisplayTrackedEntities(this.dbContext.ChangeTracker);
            this.dbContext.SaveChanges();
        }

        public void Undo()
        {
            var changedEntries = dbContext.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
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