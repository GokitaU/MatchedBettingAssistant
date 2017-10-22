using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class AccountDto : IAccount
    {

        private readonly DataModel.Account wallet;
        private readonly IList<ITransaction> transactions;

        public AccountDto(Account bank)
        {
            this.wallet = bank;
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
            if (transaction is TransactionDto transactionDto)
            {
                this.wallet.Transactions.Add(transactionDto.Transaction);

                this.transactions.Add(transactionDto);

            }
        }
    }
}