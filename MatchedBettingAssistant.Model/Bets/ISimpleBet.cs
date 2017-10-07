namespace MatchedBettingAssistant.Model.Bets
{
    public interface ISimpleBet : IBet
    {
        new double Returns { get; set; }
    }
}