using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class SportDto : ISport
    {
        private readonly Sport sport;

        private List<IMarket> markets;

        public SportDto(Sport sport)
        {
            this.sport = sport;

            this.markets = new List<IMarket>(this.sport.Markets.Select(x => new MarketDto(x)));
        }

        public int Id
        {
            get => this.sport.Id;
            set => this.sport.Id = value;
        }

        public string Name
        {
            get => this.sport.Name;
            set => this.sport.Name = value;
        }

        public IEnumerable<IMarket> Markets => this.markets;

        internal Sport Sport => this.sport;
    }
}