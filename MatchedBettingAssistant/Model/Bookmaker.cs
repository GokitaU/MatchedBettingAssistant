using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace MatchedBettingAssistant.Model
{
    public class Bookmaker : Account, IBettingAccount
    {
        public double CommissionPercent { get; set; }
        public bool IsExchange { get; set; }
    }
}