namespace MatchedBettingAssistant.Model
{
    public interface ILayBet : IBet
    {
        double Liability { get; }
    }
}