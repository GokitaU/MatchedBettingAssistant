using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Accounts
{
    public class Bookmaker : TransactionAccount, IBettingAccount
    {
        private readonly IBettingAccount baseAccount;

        public Bookmaker(IBettingAccount baseAccount) : base(baseAccount)
        {
            this.baseAccount = baseAccount;
        }

        public double CommissionPercent
        {
            get => this.baseAccount.CommissionPercent;
            set => this.baseAccount.CommissionPercent = value;
        }

        public bool IsExchange
        {
            get => this.baseAccount.IsExchange;
            set => this.baseAccount.IsExchange = value;
        }

        public double Profit
        {
            get => this.baseAccount.Profit;
        }

        public double AccountProfit
        {
            get => this.baseAccount.AccountProfit;
        }

        public double PaybackPercent
        {
            get => this.baseAccount.PaybackPercent;
            set => this.baseAccount.PaybackPercent = value;
        }

        public double PaybackDue
        {
            get => this.baseAccount.PaybackDue;
        }

        public bool LimitedAccount { get; set; }
        public bool CompletedNewAccountOffer { get; set; }
    }
}