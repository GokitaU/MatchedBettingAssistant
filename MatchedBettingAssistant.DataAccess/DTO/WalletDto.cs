using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class WalletDto : IWallet
    {
        private readonly DataModel.Wallet wallet;
        private readonly IBetTypeRepository betTypeRepository;
        private readonly IOfferTypeRepository offerTypeRepository;
        private IList<ITransaction> transactions;

        public WalletDto(Wallet wallet, 
                        IBetTypeRepository betTypeRepository,
                        IOfferTypeRepository offerTypeRepository)
        {
            this.wallet = wallet;
            this.betTypeRepository = betTypeRepository;
            this.offerTypeRepository = offerTypeRepository;

            this.transactions = new List<ITransaction>(this.wallet.Transactions.Select(x => new TransactionDto(x)));
        }

        public int Id
        {
            get => this.wallet.Id;
            set => this.wallet.Id = value;
        }

        public string Name
        {
            get => this.wallet.Name;
            set => this.wallet.Name = value;
        }

        public double StartingBalance
        {
            get => this.wallet.StartingBalance;
            set => this.wallet.StartingBalance = value;
        }

        public double Balance => this.wallet.Balance;

        public IEnumerable<ITransaction> Transactions => transactions;

        public void AddTransaction(ITransaction transaction)
        {
            var transactionDataObject = new Transaction(transaction);
            this.wallet.Transactions.Add(transactionDataObject);

            var dto = new TransactionDto(transactionDataObject);
            this.transactions.Add(dto);
        }
    }
}