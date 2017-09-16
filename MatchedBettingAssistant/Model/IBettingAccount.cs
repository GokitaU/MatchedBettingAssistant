namespace MatchedBettingAssistant.Model
{
    public interface IBettingAccount : IAccount
    {
        double CommissionPercent { get; set; }
    }
}