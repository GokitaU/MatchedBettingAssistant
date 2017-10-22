using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class TransactionAccountDto : AccountDto, ITransactionAccount
    {
        private readonly DataModel.TransactionAccount account;
        private readonly IList<ITransaction> transactions;

        public TransactionAccountDto(TransactionAccount account) : base(account)
        {
            this.account = account;
            this.transactions = new List<ITransaction>(this.account.Transactions.Select(x => new TransactionDto(x)));
        }

        public override double Balance => this.account.Balance;

        public IEnumerable<ITransaction> Transactions => transactions;

        public void AddTransaction(ITransaction transaction)
        {
            if (transaction is TransactionDto transactionDto)
            {
                this.account.Transactions.Add(transactionDto.Transaction);

                this.transactions.Add(transactionDto);

            }
        }
    }
}