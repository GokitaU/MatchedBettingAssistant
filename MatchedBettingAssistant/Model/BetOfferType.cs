namespace MatchedBettingAssistant.Model
{
    /// <summary>
    /// Bet Type represents the type of bet being placed in terms
    /// of offers. 
    /// 
    /// Standard Bet
    /// Bonus Qualifier
    /// Bonus
    /// </summary>
    public class BetOfferType
    {
        /// <summary>
        /// Gets identifier for bet type
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets description for bet type
        /// </summary>
        public string Description { get; set; }
    }
}