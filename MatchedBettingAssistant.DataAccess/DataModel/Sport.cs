using System.Collections.Generic;

namespace MatchedBettingAssistant.DataAccess.DataModel
{
    public class Sport
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Market> Markets { get; set; }
    }
}