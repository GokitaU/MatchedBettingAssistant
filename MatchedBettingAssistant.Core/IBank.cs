namespace MatchedBettingAssistant.Core
{
    public interface IBank : IAccount
    {
        double PointValue { get; set; }
    }
}