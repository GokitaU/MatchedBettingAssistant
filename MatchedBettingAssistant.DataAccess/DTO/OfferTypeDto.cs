using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class OfferTypeDto : IOfferType
    {
        private readonly OfferType offerType;

        public OfferTypeDto(OfferType offerType)
        {
            this.offerType = offerType;
        }

        public int Id
        {
            get => this.offerType.Id;
            set => this.offerType.Id = value;
        }

        public string Name
        {
            get => this.offerType.Name;
            set => this.offerType.Name = value;
        }

        internal OfferType OfferType => this.offerType;
    }
}