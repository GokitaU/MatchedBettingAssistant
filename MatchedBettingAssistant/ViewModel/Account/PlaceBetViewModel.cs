using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Bets;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class PlaceBetViewModel : ViewModelBase
    {
        private readonly IBettingAccount account;
        private BetViewModel currentBet;

        private readonly IRepository repository;

        public PlaceBetViewModel(IBettingAccount account, IRepository repository)
        {
            this.account = account;
            this.repository = repository;
            CreateSimpleMatchedBet();
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

        public double Returns
        {
            get { return 1; }
        }

        public void Commit()
        {
            this.CurrentBet.Commit(); 
        }

        private void CreateSimpleBackBet()
        {
            var betTypes = this.repository.BetTypeRepository.GetBetTypes();
            var offerTypes = this.repository.OfferTypeRepository.GetOfferTypes();

            var basicBet = new SimpleBet(this.repository.TransactionRepository)
            {
                Account = account,
                Date = DateTime.Today
            };
            this.CurrentBet = new BasicBetViewModel(basicBet, betTypes, offerTypes);
        }

        private void CreateSimpleMatchedBet()
        {


            var bookies = new List<IBettingAccount>() {this.account};
            var exchanges = this.repository.BookmakerRepository.GetAccounts().Where(x => x.IsExchange).ToList();
            var offerTypes = this.repository.OfferTypeRepository.GetOfferTypes().ToList();
            var betTypes = this.repository.BetTypeRepository.GetBetTypes().ToList();

            var simpleMatchedBet = new SimpleMatchedBet(this.repository.TransactionRepository)
            {
                BackAccount = this.account,
                LayAccount = exchanges.FirstOrDefault(),
                Date = DateTime.Today,
                BetType = betTypes.FirstOrDefault(),
                OfferType = offerTypes.FirstOrDefault()
            };
            this.CurrentBet = new SimpleMatchedBetViewModel(simpleMatchedBet, bookies, exchanges, betTypes, offerTypes);
        }
    }
}