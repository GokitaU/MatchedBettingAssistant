using System;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Core.Repositories;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.Model.Bets
{
    /// <summary>
    /// A simple back bet where the return is entered by the user
    /// </summary>
    public class SimpleBet : ISimpleBet
    {
        private ITransactionDetail detail;

        public SimpleBet(ITransactionDetail detail)
        {
            this.detail = detail;
        }

        public ITransactionDetail Detail => this.detail;

        /// <summary>
        /// The betting account on which the bet was made
        /// </summary>
        public IBettingAccount Account { get; set; }


        /// <summary>
        /// The date of the bet
        /// </summary>
        public DateTime Date
        {
            get => this.detail.Date;
            set
            {
                this.detail.Date = value;

                if (this.detail.BackTransaction != null)
                {
                    this.detail.BackTransaction.TransactionDate = value;
                }
            }
        }

        /// <summary>
        /// The amount the bet returned
        /// </summary>
        public double Returns
        {
            get => this.detail.BackTransaction?.Amount ?? 0;
            set
            {
                if (this.detail.BackTransaction != null)
                {
                    this.detail.BackTransaction.Amount = value;
                    this.detail.Profit = value;
                }                
            }
        }

        /// <summary>
        /// A description for this bet
        /// </summary>
        public string Description
        {
            get => this.detail.Description;
            set
            {
                this.detail.Description = value;

                if (this.detail.BackTransaction != null)
                {
                    this.detail.BackTransaction.Description = value;
                }
            }
        }

        /// <summary>
        /// The transaction created by this bet
        /// </summary>
        public ITransaction Transaction { get; private set; }

        public IBank Bank { get; set; }

        public IBetType BetType
        {
            get => this.detail.BetType;
            set => this.detail.BetType = value;
        }

        public IOfferType OfferType
        {
            get => this.detail.OfferType;
            set => this.detail.OfferType = value;
        }

        public ISport Sport
        {
            get => this.detail.Sport;
            set => this.detail.Sport = value;
        }

        public IMarket Market
        {
            get => this.detail.Market;
            set => this.detail.Market = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSettled
        {
            get => this.detail.IsSettled;
            set => this.detail.IsSettled = value;
        }

        /// <summary>
        /// Places the bet and creates the transaction
        /// </summary>
        public void Place()
        {
            if (string.IsNullOrEmpty(this.Description))
            {
                this.Description = $"Bet at {this.Account?.Name}";
            }
            this.detail.BackTransaction.IncludeInProfit = true;

            this.Account?.AddTransaction(this.detail.BackTransaction);

            this.Bank?.AddTransaction(detail);
        }

        public void Complete()
        {
            //if (this.Transaction != null)
            //{
            //    if (this.Transaction.Detail == null)
            //    {
            //        var detail = this.transactionRepository.NewDetail();
            //        SetDetailProperties(detail);
            //        detail.AddTransaction(this.Transaction);

            //        Bank?.AddTransaction(detail);
            //    }
            //    else
            //    {
            //        SetDetailProperties(this.Transaction.Detail);
            //    }
            //}
        }

        private void SetDetailProperties(ITransactionDetail detail)
        {
            detail.Date = this.Date;
            detail.Profit = this.Returns;
            detail.OfferType = this.OfferType;
            detail.BetType = this.BetType;
            detail.PaybackPercent = this.Account.PaybackPercent;
            detail.Sport = this.Sport;
            detail.Market = this.Market;
            detail.IsSettled = this.IsSettled;
        }
    }
}