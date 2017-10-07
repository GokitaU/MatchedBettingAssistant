using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{ 

    public class BookmakerDto : IBettingAccount
    {
        private readonly DataModel.Bookmaker bookmaker;

        private IList<ITransaction> transactions;

        public BookmakerDto(Bookmaker bookmaker)
        {
            this.bookmaker = bookmaker;

            this.transactions = new List<ITransaction>(this.bookmaker.Transactions.Select(x => new TransactionDto(x)));
        }

        public int Id
        {
            get => this.bookmaker.Id;
            set => this.bookmaker.Id = value;
        }

        public string Name
        {
            get => this.bookmaker.Name;
            set => this.bookmaker.Name = value;
        }

        public double StartingBalance
        {
            get => this.bookmaker.StartingBalance;
            set => this.bookmaker.StartingBalance = value;
        }

        public double Balance => this.bookmaker.Balance;

        public double CommissionPercent
        {
            get => this.bookmaker.CommissionPercent;
            set => this.bookmaker.CommissionPercent = value;
        }

        public bool IsExchange
        {
            get => this.bookmaker.IsExchange;
            set => this.bookmaker.IsExchange = value;
        }

        public double Profit => this.bookmaker.Profit;

        public IEnumerable<ITransaction> Transactions => transactions;

        public void AddTransaction(ITransaction transaction)
        {
            var transactionDataObject = new Transaction(transaction);
            this.bookmaker.Transactions.Add(transactionDataObject);

            var dto = new TransactionDto(transactionDataObject);
            this.transactions.Add(dto);
        }
    }
}