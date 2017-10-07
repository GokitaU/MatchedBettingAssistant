using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.DataAccess.DataModel
{
    public class Bookmaker : Account, IBettingAccount
    {
        public double CommissionPercent { get; set; }
        public bool IsExchange { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double Profit => this.Transactions.Sum(x => x.Detail?.Profit ?? 0);
    }
}