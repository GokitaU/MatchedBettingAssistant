namespace MatchedBettingAssistant.Model.Account
{
    public class Bookmaker : Account, IBettingAccount
    {
        public double CommissionPercent { get; set; }
        public bool IsExchange { get; set; }
    }
}