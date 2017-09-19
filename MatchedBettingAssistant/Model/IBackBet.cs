namespace MatchedBettingAssistant.Model
{
    /// <summary>
    /// A standard back bet
    /// </summary>
    public interface IBackBet : IBet
    {
        bool StakeNotReturned { get; set; }

        bool IsBonus { get; set; }
    }
}