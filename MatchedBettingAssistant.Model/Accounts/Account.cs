using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Accounts
{
    /// <summary>
    /// Basic account
    /// </summary>
    public abstract class Account : IAccount
    {
        private readonly IAccount baseAccount;

        protected Account(IAccount baseAccount)
        {
            this.baseAccount = baseAccount;
        }

        /// <summary>
        /// Identifier for this account
        /// </summary>
        public int Id
        {
            get => this.baseAccount.Id;
            set => this.baseAccount.Id = value;
        }

        /// <summary>
        /// Gets or sets the name of the bookmaker
        /// </summary>
        public string Name
        {
            get => this.baseAccount.Name;
            set => this.baseAccount.Name = value;
        }


        /// <summary>
        /// Gets or sets the starting balance at this bookmaker
        /// </summary>
        public double StartingBalance
        {
            get => this.baseAccount.StartingBalance;
            set => this.baseAccount.StartingBalance = value;
        }

        public abstract double Balance { get; }
    }
}