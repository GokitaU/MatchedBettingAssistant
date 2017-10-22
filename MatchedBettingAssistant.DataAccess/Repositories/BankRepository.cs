using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DTO;

namespace MatchedBettingAssistant.DataAccess.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly MatchedBettingAssistantDbContext dataContext;

        public BankRepository(MatchedBettingAssistantDbContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<IBank> GetAccounts()
        {
            var wallets = this.dataContext.Accounts.OfType<DataModel.Bank>()
                            .Include(x => x.Transactions).ToList();

            return new List<IBank>(wallets.Select(x => new BankDto(x)));
        }

        public IBank New()
        {
            var newAccount = new DataModel.Bank();
            this.dataContext.Accounts.Add(newAccount);

            return new BankDto(newAccount);
        }
    }
}