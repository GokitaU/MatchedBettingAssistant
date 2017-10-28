using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Core.Repositories;
using MatchedBettingAssistant.DataAccess.DataModel;
using MatchedBettingAssistant.DataAccess.DTO;

namespace MatchedBettingAssistant.DataAccess.Repositories
{
    public class SportRepository : ISportRepository
    {
        private readonly MatchedBettingAssistantDbContext dataContext;

        public SportRepository(MatchedBettingAssistantDbContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public ISport GetSport(int id)
        {
            return new SportDto(this.dataContext.Sports.First(x => x.Id == id));
        }

        public IEnumerable<ISport> GetSports()
        {
            var sports = this.dataContext.Sports
                .Include(x => x.Markets).ToList();

            return new List<ISport>(sports.Select(x => new SportDto(x)));
        }

        public ISport New()
        {
            var sport = new Sport();

            return new SportDto(sport);
        }
    }
}