namespace MatchedBettingAssistant.Model
{
    public interface ICalculatedBet
    {
        double Odds { get; set; }
        void Lost();
        void Won();
    }
}