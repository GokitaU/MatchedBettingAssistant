using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DTO;

namespace MatchedBettingAssistant.DataAccess.Repositories
{
    public class BookmakerRepository : IBookmakerRepository
    {
        private readonly MatchedBettingAssistantDbContext dataContext;

        public BookmakerRepository(MatchedBettingAssistantDbContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<IBettingAccount> GetAccounts()
        {
            var accounts = this.dataContext.Accounts.OfType<DataModel.Bookmaker>()
                .Include(x => x.Transactions)
                .Include(x => x.Transactions.Select(d => d.Detail))
                .Include(x => x.Transactions.Select(d => d.Detail.BetType))
                .Include(x => x.Transactions.Select(d => d.Detail.OfferType)).ToList();

            return new List<IBettingAccount>(accounts.Select(x => new BookmakerDto(x)));

        }

        public IBettingAccount New()
        {
            var newAccount = new DataModel.Bookmaker();
            this.dataContext.Accounts.Add(newAccount);

            return new BookmakerDto(newAccount);
        }


    }
}