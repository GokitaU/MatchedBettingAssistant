using System.Collections.Generic;

namespace MatchedBettingAssistant.Core
{
    public interface IBank : IAccount
    {
        double PointValue { get; set; }

        IList<ITransactionDetail> Transactions { get;  }
    }
}