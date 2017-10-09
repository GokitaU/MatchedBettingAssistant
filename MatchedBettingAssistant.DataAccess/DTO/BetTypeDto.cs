using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{
    public class BetTypeDto : IBetType
    {
        private readonly BetType betType;

        public BetTypeDto(BetType betType)
        {
            this.betType = betType;
        }

        public int Id
        {
            get => this.betType.Id;
            set => this.betType.Id = value;
        }

        public string Name
        {
            get => this.betType.Name;
            set => this.betType.Name = value;
        }

        internal BetType BetType => this.betType;
    }
}