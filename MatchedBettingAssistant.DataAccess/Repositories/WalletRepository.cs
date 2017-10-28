using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Core.Repositories;
using MatchedBettingAssistant.DataAccess.DataModel;
using MatchedBettingAssistant.DataAccess.DTO;

namespace MatchedBettingAssistant.DataAccess.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly MatchedBettingAssistantDbContext dataContext;

        public WalletRepository(MatchedBettingAssistantDbContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public int Count()
        {
            return dataContext.Accounts.OfType<Wallet>().Count();
        }

        public int Count(int excluding)
        {
            return dataContext.Accounts.OfType<Wallet>().Count(x => x.Id != excluding);
        }

        public IEnumerable<IWallet> GetWallets()
        {
            var wallets = this.dataContext.Accounts.OfType<DataModel.Wallet>()
                .Include(x => x.Transactions)
                .Include(x => x.Transactions.Select(d => d.Detail)).ToList();
            return new List<IWallet>(wallets.Select(x => new WalletDto(x))); 
        }

        public IWallet New()
        {
            var newWallet = new DataModel.Wallet();
            this.dataContext.Accounts.Add(newWallet);

            return new WalletDto(newWallet);
        }
    }
}