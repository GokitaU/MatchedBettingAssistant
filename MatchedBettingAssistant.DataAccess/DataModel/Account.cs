using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.DataAccess.DataModel
{
    public abstract class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double StartingBalance { get; set; }

        public bool Deleted { get; set; }
    }
}