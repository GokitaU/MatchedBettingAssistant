using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class MarketDto : IMarket
    {
        private readonly Market market;

        public MarketDto(Market market)
        {
            this.market = market;
        }

        public int Id
        {
            get => this.market.Id;
            set => this.market.Id = value;
        }

        public string Name
        {
            get => this.market.Name;
            set => this.market.Name = value;
        }

        internal Market Market => this.market;
    }

    public class SportDto : ISport
    {
        private readonly Sport sport;

        private List<IMarket> markets;

        public SportDto(Sport sport)
        {
            this.sport = sport;

            this.markets = new List<IMarket>(this.sport.Markets.Select(x => new MarketDto(x)));
        }

        public int Id
        {
            get => this.sport.Id;
            set => this.sport.Id = value;
        }

        public string Name
        {
            get => this.sport.Name;
            set => this.sport.Name = value;
        }

        public IEnumerable<IMarket> Markets => this.markets;

        internal Sport Sport => this.sport;
    }

    public class TransactionDetailDto : ITransactionDetail
    {
        private readonly DataModel.TransactionDetail detail;

        private IBetType betType;
        private IOfferType offerType;
        private ISport sport;
        private IMarket market;

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
        }

        public double Profit
        {
            get => this.detail.Profit;
            set => this.detail.Profit = value;
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

        internal TransactionDetail TransactionDetail => this.detail;
    }
}