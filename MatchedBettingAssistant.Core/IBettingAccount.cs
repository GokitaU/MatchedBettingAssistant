namespace MatchedBettingAssistant.Core
{
    public interface IBettingAccount : IAccount
    {
        double CommissionPercent { get; set; }

        bool IsExchange { get; set; }

        double Profit { get; }
    }

    public interface IWallet : IAccount
    {
        
    }
}