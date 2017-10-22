using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess
{
    public class MatchedBettingInitialiser : DropCreateDatabaseIfModelChanges<MatchedBettingAssistantDbContext>
    {
        protected override void Seed(MatchedBettingAssistantDbContext context)
        {
            var betTypes = CreateBetTypes();
            context.BetTypes.AddRange(betTypes);

            var offerTypes = CreateOfferTypes();
            context.OfferTypes.AddRange(offerTypes);

            var sports = CreateSports();
            context.Sports.AddRange(sports);

            var banks = CreateBanks();
            context.Accounts.AddRange(banks);

            
            var bookmakers = CreateBookmakers(betTypes, offerTypes, banks);
            context.Accounts.AddRange(bookmakers);

            var wallets = CreateWallets();
            context.Accounts.AddRange(wallets);


            context.SaveChanges();
            base.Seed(context);
        }

        private IEnumerable<BetType> CreateBetTypes()
        {
            var betTypes = new List<BetType>()
            {
                new BetType() {Name = "Sports Bet"},
                new BetType() {Name = "Accumulator"},
                new BetType() {Name = "Casino"},
                new BetType() {Name = "Bingo"},
            };

            return betTypes;
        }

        private IEnumerable<OfferType> CreateOfferTypes()
        {
            var offerTypes = new List<OfferType>()
            {
                new OfferType() { Name = "None", IsBonusOffer = false},
                new OfferType() { Name = "Qualifier", IsBonusOffer = true},
                new OfferType() { Name="Bonus", IsBonusOffer = true}
            };

            return offerTypes;
        }

        private IEnumerable<Sport> CreateSports()
        {
            var football = new Sport()
            {
                Name = "Football",
                Markets = new Collection<Market>()
                {
                    new Market() {Name = "Match Odds"},
                    new Market() {Name = "Correct Score"},
                    new Market() {Name = "First Scorer"},
                    new Market() {Name = "Other"}
                }
            };

            var sports = new List<Sport>()
            {
                new Sport() {Name = "None", Markets = new Collection<Market> { new Market(){Name = "None"}}},
                football,
                new Sport() {Name = "Horse Racing", Markets = new Collection<Market>() { new Market() {Name = "Winner" }}},
                new Sport() {Name = "Golf", Markets = new Collection<Market>() { new Market() {Name = "Winner" }}},
                new Sport() {Name = "American Sports", Markets = new Collection<Market>() { new Market() {Name = "Any" }}},
                new Sport() {Name = "Other", Markets = new Collection<Market>() { new Market() {Name = "Any" }}}
            };

            return sports;
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

        private IEnumerable<DataModel.Bank> CreateBanks()
        {
            var bank = new DataModel.Bank()
            {
                Name = "No Lay Accumulator",
                StartingBalance = 100.00
            };

            var banks = new List<DataModel.Bank>() {bank};

            return banks;
        }

        private IEnumerable<DataModel.Bookmaker> CreateBookmakers(
                                            IEnumerable<BetType> betTypes, 
                                            IEnumerable<OfferType> offerTypes, 
                                            IEnumerable<Bank> banks)
        {
            var bookmakers = new List<DataModel.Bookmaker>();

            var betfair = new DataModel.Bookmaker()
            {
                Name = "Betfair",
                StartingBalance = 525.00d,
                PayBackPercent = 0.00,
                IsExchange = true,
                CommissionPercent = 0.05
            };

            var blankDetail = new TransactionDetail()
            {
                Profit =  0.00,
            };
            var betfairTransaction = new Transaction()
            {
                TransactionDate = DateTime.Today,
                Amount = 100.00,
                Description = "Initial Deposit",
            };

            blankDetail.Transactions.Add(betfairTransaction);
            betfair.Transactions.Add(betfairTransaction);
            bookmakers.Add(betfair);

            var paddyPower = new DataModel.Bookmaker()
            {
                Name = "Paddy Power",
                StartingBalance = 77.98,
                PayBackPercent = 0.10,
                IsExchange = false,
                CommissionPercent = 0.0d
            };

            var betType = betTypes.First(x => x.Name == "Sports Bet");
            var offerType = offerTypes.FirstOrDefault(x => x.Name == "Bonus");

            var detail = new TransactionDetail()
            {
                Profit = 1.00,
                PaybackPercent = 0.10,
                BetType = betType,
                OfferType = offerType
            };

            var backTransaction = new Transaction()
            {
                TransactionDate = DateTime.Today,
                Amount = 29.00,
                Description = "Free Bet",
                Detail = detail
            };

            var layTransaction = new Transaction()
            {
                TransactionDate = DateTime.Today,
                Amount = -28.00,
                Description = "Paddy Power Free Bet",
                Detail = detail
            };

            detail.Transactions.Add(backTransaction);
            detail.Transactions.Add(layTransaction);
            betfair.Transactions.Add(layTransaction);
            paddyPower.Transactions.Add(backTransaction);

            bookmakers.Add(paddyPower);
            return bookmakers;
        }
    }
}