using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class TransactionDetailDto : ITransactionDetail
    {
        private readonly DataModel.TransactionDetail detail;

        private IBetType betType;
        private IOfferType offerType;

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
            get { return this.offerType; }
            set
            {
                if (value is OfferTypeDto offerTypeDto)
                {
                    this.detail.OfferType = offerTypeDto.OfferType;
                }
                this.offerType = value;

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