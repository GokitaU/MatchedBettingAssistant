using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class BankDto : AccountDto, IBank
    {
        private readonly DataModel.Bank bank;
        private readonly IList<ITransactionDetail> transactions;

        public BankDto(Bank bank) : base(bank)
        {
            this.bank = bank;
            this.transactions = new List<ITransactionDetail>(this.bank.Transactions.Select(x => new TransactionDetailDto(x)));

        }

        public double PointValue
        {
            get => this.bank.PointValue;
            set => this.bank.PointValue = value;
        }

        public IList<ITransactionDetail> Transactions => transactions;

        public override double Balance => bank.Balance;

        public void AddTransaction(ITransactionDetail detail)
        {
            if (detail is TransactionDetailDto detailDto)
            {

                this.bank.Transactions.Add(detailDto.TransactionDetail);
                this.transactions.Add(detailDto);
            }
        }
    }
}