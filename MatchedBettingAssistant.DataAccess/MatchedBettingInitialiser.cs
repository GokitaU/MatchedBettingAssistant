﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess
{
    public class MatchedBettingInitialiser : DropCreateDatabaseIfModelChanges<MatchedBettingAssistantDbContext>
    {
        protected override void Seed(MatchedBettingAssistantDbContext context)
        {
            var bookmakers = CreateBookmakers();
            context.Accounts.AddRange(bookmakers);

            var wallets = CreateWallets();
            context.Accounts.AddRange(wallets);

            context.SaveChanges();
            base.Seed(context);
        }

        private IEnumerable<DataModel.Wallet> CreateWallets()
        {
            var skrill = new DataModel.Wallet()
            {
                Name = "Skrill",
                StartingBalance = 521.00d
            };

            var wallets = new List<DataModel.Wallet>() { skrill };

            return wallets;
        }

        private IEnumerable<DataModel.Bookmaker> CreateBookmakers()
        {
            var bookmakers = new List<DataModel.Bookmaker>();

            var betfair = new DataModel.Bookmaker()
            {
                Name = "Betfair",
                StartingBalance = 525.00d,
                IsExchange = true,
                CommissionPercent = 0.05
            };

            betfair.Transactions.Add(new Transaction()
            {
                TransactionDate = DateTime.Today,
                Amount = 100.00,
                Description = "Initial Deposit",
                Detail = new TransactionDetail { Profit = 0.00 }
            });

            bookmakers.Add(betfair);

            var paddyPower = new DataModel.Bookmaker()
            {
                Name = "Paddy Power",
                StartingBalance = 77.98,
                IsExchange = false,
                CommissionPercent = 0.0d
            };

            var transaction = new Transaction()
            {
                TransactionDate = DateTime.Today,
                Amount = 29.00,
                Description = "Free Bet",
                Detail = new TransactionDetail {  Profit = 22.00 }
            };

            paddyPower.Transactions.Add(transaction);

            bookmakers.Add(paddyPower);

            return bookmakers;
        }
    }
}