namespace MatchedBettingAssistant.Model
{
    public interface IBackBet : IBet
    {
        bool StakeNotReturned { get; set; }

        bool IsBonus { get; set; }
    }
}