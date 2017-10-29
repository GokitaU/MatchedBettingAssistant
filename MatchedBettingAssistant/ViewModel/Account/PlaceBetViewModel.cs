using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Bets;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BetLookup : ViewModelBase
    {
        public BetLookup(string name, Func<BetViewModel> createViewModel)
        {
            Name = name;
            CreateViewModel = createViewModel;
        }


        public string Name { get; }

        public Func<BetViewModel> CreateViewModel { get; }
    }

    public class PlaceBetViewModel : ViewModelBase
    {
        private readonly IBettingAccount account;
        private BetViewModel currentBet;
        private List<BetLookup> bets;
        private BetLookup selectedBet;

        private readonly IRepository repository;

        public PlaceBetViewModel(IBettingAccount account, IRepository repository)
        {
            this.account = account;
            this.repository = repository;
            CreateBets();
        }

        /// <summary>
        /// 
        /// </summary>
        public BetViewModel CurrentBet
        {
            get => this.currentBet;
            set
            {
                this.currentBet = value;
                RaisePropertyChanged(()=>this.CurrentBet);
            }
        }

        public double Returns => 1;

        public List<BetLookup> Bets => bets;

        public BetLookup SelectedBet
        {
            get => this.selectedBet;
            set
            {
                this.selectedBet = value;

                this.CurrentBet = this.selectedBet.CreateViewModel();

                RaisePropertyChanged(()=>SelectedBet);
            }
        }

        public void Commit()
        {
            this.CurrentBet.Commit(); 

            this.repository.TransactionRepository.AddDetail(this.CurrentBet.Detail);
        }

        private BetViewModel CreateSimpleBackBet()
        {
            var banks = this.repository.BankRepository.GetAccounts();
            var betTypes = this.repository.BetTypeRepository.GetBetTypes();
            var offerTypes = this.repository.OfferTypeRepository.GetOfferTypes();
            var sports = this.repository.SportRepository.GetSports().ToList();
            var markets = this.repository.MarketRepository.GetMarkets().ToList();

            var detail = this.repository.TransactionRepository.NewDetail();
            detail.CreateBackTransaction();
            var basicBet = new SimpleBet(detail)
            {
                Account = account,
                Date = DateTime.Today
            };
            return new BasicBetViewModel(basicBet, banks, betTypes, offerTypes, sports, markets);
        }

        private BetViewModel CreateSimpleMatchedBet()
        {
            var bookies = new List<IBettingAccount>() {this.account};
            var exchanges = this.repository.BookmakerRepository.GetAccounts().Where(x => x.IsExchange).ToList();
            var banks = this.repository.BankRepository.GetAccounts().ToList();
            var offerTypes = this.repository.OfferTypeRepository.GetOfferTypes().ToList();
            var betTypes = this.repository.BetTypeRepository.GetBetTypes().ToList();
            var sports = this.repository.SportRepository.GetSports().ToList();
            var markets = this.repository.MarketRepository.GetMarkets().ToList();

            var detail = this.repository.TransactionRepository.NewDetail();
            detail.CreateBackTransaction();
            detail.CreateLayTransaction();

            var simpleMatchedBet = new SimpleMatchedBet(detail)
            {
                BackAccount = this.account,
                LayAccount = exchanges.FirstOrDefault(),
                Date = DateTime.Today,
                BetType = betTypes.FirstOrDefault(),
                OfferType = offerTypes.FirstOrDefault(),
                Sport = sports.FirstOrDefault(),
                Market = markets.FirstOrDefault()
            };
            return new SimpleMatchedBetViewModel(simpleMatchedBet, bookies, exchanges, banks, betTypes, offerTypes, sports, markets);
        }

        private void CreateBets()
        {
            this.bets = new List<BetLookup>()
            {
                new BetLookup("Back Bet", CreateSimpleBackBet),
                new BetLookup("Matched Bet", CreateSimpleMatchedBet)
            };

            this.SelectedBet = this.bets.First();
        }
    }
}