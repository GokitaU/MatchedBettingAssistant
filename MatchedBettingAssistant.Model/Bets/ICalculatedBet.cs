namespace MatchedBettingAssistant.Model.Bets
{
    public interface ICalculatedBet
    {
        double Stake { get; set; }
        double Odds { get; set; }
        void Lost();
        void Won();
    }
}