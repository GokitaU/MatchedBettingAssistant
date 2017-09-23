using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchedBettingAssistant.Model.Account;

namespace MatchedBettingAssistant.Model.Project
{
    public class MatchedBettingAssistantProject
    {
        public IEnumerable<IAccount> Accounts { get; }
    }
}
