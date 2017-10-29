using System;
using System.Linq;
using System.Threading;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class TransactionDetailDto : ITransactionDetail
    {
        private readonly DataModel.TransactionDetail detail;

        private IBetType betType;
        private IOfferType offerType;
        private ISport sport;
        private IMarket market;

        private ITransaction backTransaction;
        private ITransaction layTransaction;

        public int Id
        {
            get => this.detail.Id;
            set { this.detail.Id = value; }
        }

        public TransactionDetailDto(TransactionDetail detail)
        {
            this.detail = detail;

            if (this.detail.BetType != null)
            {
                this.betType = new BetTypeDto(detail.BetType);
            }

            if (this.detail.OfferType != null)
            {
                this.offerType = new OfferTypeDto(detail.OfferType);             
            }

            CreateTransactions();
        }

        public DateTime Date
        {
            get => this.detail.Date;
            set => this.detail.Date = value;
        }

        public string Description
        {
            get => this.detail.Description;
            set => this.detail.Description = value;
        }

        public double Profit
        {
            get => this.detail.Profit;
            set => this.detail.Profit = value;
        }

        public bool IsSettled
        {
            get => this.detail.IsSettled;
            set => this.detail.IsSettled = value;
        }

        public IBetType BetType
        {
            get => this.betType;
            set
            {
                if (value is BetTypeDto betTypeDto)
                {
                    this.detail.BetType = betTypeDto.BetType;
                }
                this.betType = value;
            }
        }

        public IOfferType OfferType
        {
            get => this.offerType;
            set
            {
                if (value is OfferTypeDto offerTypeDto)
                {
                    this.detail.OfferType = offerTypeDto.OfferType;
                }
                this.offerType = value;

            }
        }

        public ISport Sport
        {
            get => this.sport;
            set
            {
                if (value is SportDto sportDto)
                {
                    this.detail.Sport = sportDto.Sport;
                }
                this.sport = value;
            }
        }

        public IMarket Market
        {
            get => this.market;
            set
            {
                if (value is MarketDto marketDto)
                {
                    this.detail.Market = marketDto.Market;
                }
                this.market = value;
            }
        }

        public double PaybackPercent
        {
            get => this.detail.PaybackPercent;
            set => this.detail.PaybackPercent = value;
        }

        public double Payback => this.Profit * this.PaybackPercent;


        public void AddTransaction(ITransaction transaction)
        {
            if (transaction is TransactionDto tran)
            {
                this.detail.Transactions.Add(tran.Transaction);
            }
        }

        public ITransaction CreateTransaction()
        {
            var transaction = new Transaction();

            this.detail.Transactions.Add(transaction);

            return new TransactionDto(transaction);
        }

        public ITransaction CreateBackTransaction()
        {
            if (this.detail.Transactions.Count == 0)
            {
                var transaction = new Transaction();

                this.detail.Transactions.Add(transaction);

                this.backTransaction = new TransactionDto(transaction, this.TransactionDetail);
            }

            return this.BackTransaction;
        }

        public ITransaction CreateLayTransaction()
        {
            if (this.detail.Transactions.Count == 1)
            {
                var transaction = new Transaction();

                this.detail.Transactions.Add(transaction);

                this.layTransaction = new TransactionDto(transaction, this.TransactionDetail);
            }

            return LayTransaction;
        }

        public ITransaction BackTransaction => this.backTransaction;

        public ITransaction LayTransaction => this.layTransaction;

        internal TransactionDetail TransactionDetail => this.detail;

        private void CreateTransactions()
        {
            var count = this.detail.Transactions.Count;

            if (count > 0)
            {
                this.backTransaction = new TransactionDto(this.detail.Transactions.ElementAt(0));

                if (count == 2)
                {
                    this.layTransaction = new TransactionDto(this.detail.Transactions.ElementAt(1));
                }
            }

        }
    }
}