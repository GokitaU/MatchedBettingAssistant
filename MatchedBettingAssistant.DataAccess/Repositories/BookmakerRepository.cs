﻿using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.Repositories
{
    public class BookmakerRepository : IBookmakerRepository
    {
        private readonly MatchedBettingAssistantDbContext dataContext;

        public BookmakerRepository(MatchedBettingAssistantDbContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<IBettingAccount> GetAccounts()
        {
            var accounts = this.dataContext.Accounts.OfType<DataModel.Bookmaker>();
            return new List<IBettingAccount>(accounts).Select(x => new Model.Accounts.Bookmaker(x));
        }

        public IBettingAccount New()
        {
            var newAccount = new DataModel.Bookmaker();
            this.dataContext.Accounts.Add(newAccount);

            return new Model.Accounts.Bookmaker(newAccount);
        }
    }
}