namespace MatchedBettingAssistant.Model.Bets
{
    /// <summary>
    /// A laybet that calculates the liability from the odds
    /// </summary>
    public interface ICalculatedLayBet : IBet, ICalculatedBet
    {
        double Liability { get; }
    }
}