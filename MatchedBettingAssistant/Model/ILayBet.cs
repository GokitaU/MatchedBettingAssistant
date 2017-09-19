namespace MatchedBettingAssistant.Model
{
    /// <summary>
    /// A standard laybet
    /// </summary>
    public interface ILayBet : IBet
    {
        double Liability { get; }
    }
}