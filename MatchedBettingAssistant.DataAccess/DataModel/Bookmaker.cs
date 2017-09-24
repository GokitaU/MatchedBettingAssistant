using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.DataAccess.DataModel
{
    public class Bookmaker : Account, IBettingAccount
    {
        public double CommissionPercent { get; set; }
        public bool IsExchange { get; set; }
    }
}