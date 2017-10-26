namespace MatchedBettingAssistant.DataAccess.DataModel
{
    public class OfferType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Whether this offer type counts as a bonus
        /// </summary>
        public bool IsBonusOffer { get; set; }

        /// <summary>
        /// Gets whether bets of this type should be
        /// excluded from payback
        /// </summary>
        public bool ExcludeFromPayback { get; set; }
    }
}