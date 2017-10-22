using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class BankDto : AccountDto, IBank
    {
        private readonly DataModel.Bank bank;

        public BankDto(Bank bank) : base(bank)
        {
            this.bank = bank;
        }

        public double PointValue
        {
            get => this.bank.PointValue;
            set => this.bank.PointValue = value;
        }

    }
}