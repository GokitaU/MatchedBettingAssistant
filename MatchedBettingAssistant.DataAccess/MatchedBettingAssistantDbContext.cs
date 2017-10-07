using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchedBettingAssistant.DataAccess
{
    public class MatchedBettingAssistantDbContext : DbContext
    {
        public MatchedBettingAssistantDbContext(IDatabaseInitializer<MatchedBettingAssistantDbContext> intialiser)
        {
            Database.SetInitializer(intialiser);
        }

        public DbSet<DataModel.Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataModel.Bookmaker>().ToTable("Bookmaker");
            modelBuilder.Entity<DataModel.Wallet>().ToTable("Wallet");
            modelBuilder.Entity<DataModel.Transaction>().HasOptional(a => a.TransactionDetail);

            base.OnModelCreating(modelBuilder);
        }
    }
}
