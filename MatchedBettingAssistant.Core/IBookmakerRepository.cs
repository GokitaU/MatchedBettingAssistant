using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchedBettingAssistant.Core
{
    public interface IBookmakerRepository
    {
        IEnumerable<IBettingAccount> GetAccounts();

        IBettingAccount New();
    }
}
