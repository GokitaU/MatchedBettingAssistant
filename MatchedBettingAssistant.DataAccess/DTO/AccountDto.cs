using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public abstract class AccountDto : IAccount
    {

        private readonly DataModel.Account wallet;

        protected AccountDto(Account bank)
        {
            this.wallet = bank;
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

        public abstract double Balance { get; }
    }
}