using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchedBettingAssistant.Model.Account;

namespace MatchedBettingAssistant.DataAccess
{
    public class MatchedBettingAssistantDbContext : DbContext
    {
        public MatchedBettingAssistantDbContext(IDatabaseInitializer<MatchedBettingAssistantDbContext> intialiser)
        {
            Database.SetInitializer(intialiser);
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookmaker>().ToTable("Bookmaker");
            modelBuilder.Entity<Wallet>().ToTable("Wallet");

            base.OnModelCreating(modelBuilder);
        }
    }
}
