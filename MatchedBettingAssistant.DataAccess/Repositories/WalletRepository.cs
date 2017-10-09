using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MatchedBettingAssistant.Core;
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


        public IEnumerable<IWallet> GetWallets()
        {
            var betTypeRepository = new BetTypeRepository(this.dataContext);
            var offerTypeRepository = new OfferTypeRepository(this.dataContext);

            var wallets = this.dataContext.Accounts.OfType<DataModel.Wallet>()
                .Include(x => x.Transactions)
                .Include(x => x.Transactions.Select(d => d.Detail)).ToList();
            return new List<IWallet>(wallets.Select(x => new WalletDto(x, betTypeRepository, offerTypeRepository))); 
        }

        public IWallet New()
        {
            var betTypeRepository = new BetTypeRepository(this.dataContext);
            var offerTypeRepository = new OfferTypeRepository(this.dataContext);

            var newWallet = new DataModel.Wallet();
            this.dataContext.Accounts.Add(newWallet);

            return new WalletDto(newWallet, betTypeRepository, offerTypeRepository);

        }
    }
}