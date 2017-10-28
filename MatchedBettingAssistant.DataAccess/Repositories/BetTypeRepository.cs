using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Core.Repositories;
using MatchedBettingAssistant.DataAccess.DataModel;
using MatchedBettingAssistant.DataAccess.DTO;

namespace MatchedBettingAssistant.DataAccess.Repositories
{
    public class BetTypeRepository : IBetTypeRepository
    {
        private readonly MatchedBettingAssistantDbContext dataContext;

        public BetTypeRepository(MatchedBettingAssistantDbContext dataContext)
        {
            this.dataContext = dataContext;
        }


        public IBetType GetBetType(int id)
        {
            var betType = this.dataContext.BetTypes.FirstOrDefault(x => x.Id == id);

            if (betType != null)
            {
                return new BetTypeDto(betType);
            }

            return null;
        }

        public IEnumerable<IBetType> GetBetTypes()
        {
            var betTypes = this.dataContext.BetTypes.ToList();

            return new List<IBetType>(betTypes.Select(x => new BetTypeDto(x)));
        }

        public IBetType New()
        {
            var betType = new BetType();
            this.dataContext.BetTypes.Add(betType);
            return new BetTypeDto(betType);
        }
    }
}