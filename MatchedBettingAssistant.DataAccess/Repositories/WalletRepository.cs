using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;

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
            var wallets = this.dataContext.Accounts.OfType<DataModel.Wallet>();
            return new List<IWallet>(wallets).Select(x => new Model.Accounts.Wallet(x)); 
        }

        public IWallet New()
        {
            var newWallet = new DataModel.Wallet();
            this.dataContext.Accounts.Add(newWallet);

            return new Model.Accounts.Wallet(newWallet);

        }
    }
}