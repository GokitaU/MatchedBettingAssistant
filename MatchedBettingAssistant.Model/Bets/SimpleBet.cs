using System;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.Model.Bets
{
    /// <summary>
    /// A simple back bet where the return is entered by the user
    /// </summary>
    public class SimpleBet : ISimpleBet
    {
        /// <summary>
        /// The betting account on which the bet was made
        /// </summary>
        public IBettingAccount Account { get; set; }

        /// <summary>
        /// The date of the bet
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The amount the bet returned
        /// </summary>
        public double Returns { get; set; }

        /// <summary>
        /// A description for this bet
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The transaction created by this bet
        /// </summary>
        public ITransaction Transaction { get; private set; }

        /// <summary>
        /// Places the bet and creates the transaction
        /// </summary>
        public void Place(bool calculateDetail = true)
        {
            if (this.Transaction == null)
            {
                this.Transaction = new FundsTransaction()
                {
                    TransactionDate = Date,
                    Amount = this.Returns,
                    Description = this.Description
                };

                this.Account.AddTransaction(this.Transaction);
            }
        }
    }
}