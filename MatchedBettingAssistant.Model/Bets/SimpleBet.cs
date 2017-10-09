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
        private ITransactionRepository transactionRepository;

        public SimpleBet(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

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


        public IBetType BetType { get; set; }


        public IOfferType OfferType { get; set; }

        /// <summary>
        /// Places the bet and creates the transaction
        /// </summary>
        public void Place()
        {
            if (this.Transaction == null)
            {
                var transaction = transactionRepository.New();
                transaction.TransactionDate = Date;
                transaction.Amount = this.Returns;
                transaction.Description = this.Description;

                this.Transaction = transaction;

                this.Account.AddTransaction(this.Transaction);
            }
        }

        public void Complete()
        {
            if (this.Transaction != null)
            {
                if (this.Transaction.Detail == null)
                {
                    var detail = this.transactionRepository.NewDetail();
                    detail.Profit = this.Returns;
                    detail.OfferType = this.OfferType;
                    detail.BetType = this.BetType;

                    this.Transaction.AddDetail(detail);
                }
            }
        }
    }
}