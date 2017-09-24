namespace MatchedBettingAssistant.Core
{
    public interface IBettingAccount : IAccount
    {
        double CommissionPercent { get; set; }

        bool IsExchange { get; set; }
    }

    public interface IWallet : IAccount
    {
        
    }
}