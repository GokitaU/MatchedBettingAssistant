namespace MatchedBettingAssistant.Core
{
    public interface IBettingAccount : ITransactionAccount
    {
        double CommissionPercent { get; set; }

        bool IsExchange { get; set; }

        double Profit { get; }

        double PaybackPercent { get; set; }

        double PaybackDue { get; }
    }
}