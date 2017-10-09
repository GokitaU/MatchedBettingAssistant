using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class OfferTypeLookup
    {
        private IOfferType offerType;

        public OfferTypeLookup(IOfferType offerType)
        {
            this.offerType = offerType;
        }

        public string Name => this.offerType.Name;

        internal IOfferType OfferType => this.offerType;
    }
}