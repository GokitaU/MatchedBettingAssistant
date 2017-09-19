namespace MatchedBettingAssistant.Model
{
    /// <summary>
    /// A laybet that calculates the liability from the odds
    /// </summary>
    public interface ICalculatedLayBet : ILayBet, ICalculatedBet
    {
        
    }
}