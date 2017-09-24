using System.Collections.Generic;
using System.Data.Entity;
using MatchedBettingAssistant.Model.Account;

namespace MatchedBettingAssistant.DataAccess
{
    public class MatchedBettingInitialiser : DropCreateDatabaseAlways<MatchedBettingAssistantDbContext>
    {
        protected override void Seed(MatchedBettingAssistantDbContext context)
        {
            var bookmakers = CreateBookmakers();
            context.Accounts.AddRange(bookmakers);

            var wallets = CreateWallets();
            context.Accounts.AddRange(wallets);

            context.SaveChanges();
            base.Seed(context);
        }

        private IEnumerable<Wallet> CreateWallets()
        {
            var skrill = new Wallet()
            {
                Name = "Skrill",
                StartingBalance = 521.00d
            };

            var wallets = new List<Wallet>() { skrill };

            return wallets;
        }

        private IEnumerable<Bookmaker> CreateBookmakers()
        {
            var bookmakers = new List<Bookmaker>();

            var betfair = new Bookmaker()
            {
                Name = "Betfair",
                StartingBalance = 525.00d,
                IsExchange = true,
                CommissionPercent = 0.05
            };

            bookmakers.Add(betfair);

            var paddyPower = new Bookmaker()
            {
                Name = "Paddy Power",
                StartingBalance = 77.98,
                IsExchange = false,
                CommissionPercent = 0.0d
            };

            bookmakers.Add(paddyPower);

            return bookmakers;
        }
    }
}