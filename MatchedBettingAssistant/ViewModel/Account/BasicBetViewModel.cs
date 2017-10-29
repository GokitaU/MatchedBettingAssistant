using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Bets;

namespace MatchedBettingAssistant.ViewModel.Account
{
    /// <summary>
    /// basic back bet
    /// </summary>
    public class BasicBetViewModel : BetViewModel
    {
        private readonly SimpleBet bet;
        private BetTypeLookup betType;
        private OfferTypeLookup offerType;
        private SportLookup sportLookup;
        private MarketLookup marketLookup;
        private BankLookupItem bankLookup;

        public BasicBetViewModel(SimpleBet bet, 
                                 IEnumerable<IBank> banks,
                                 IEnumerable<IBetType> betTypes, 
                                 IEnumerable<IOfferType> offerTypes,
                                 IEnumerable<ISport> sports,
                                 IEnumerable<IMarket> markets)
        {
            this.bet = bet;
            this.Banks = banks.Select(x => new BankLookupItem(x));
            this.BetTypes = betTypes.Select(x => new BetTypeLookup(x));
            this.OfferTypes = offerTypes.Select(x => new OfferTypeLookup(x));
            this.Sports = sports.Select(x => new SportLookup(x));
            this.Markets = markets.Select(x => new MarketLookup(x));
        }

        /// <summary>
        /// Gets or sets the date of the transaction
        /// </summary>
        public DateTime Date
        {
            get => this.bet.Date;
            set
            {
                if (this.bet.Date == value)
                    return;

                this.bet.Date = value;

                RaisePropertiesChanged(() => this.Date, ()=> CanPlaceBet);
            }
        }

        public string Description
        {
            get => this.bet.Description;
            set
            {
                if (this.bet.Description == value)
                    return;

                this.bet.Description = value;

                RaisePropertyChanged(() => this.Description);
            }
        }

        public BankLookupItem Bank
        {
            get => this.bankLookup;
            set
            {
                this.bankLookup = value;

                this.bet.Bank = value?.Account;

                RaisePropertyChanged(()=>Bank);
            }
        }

        public BetTypeLookup BetType
        {
            get => this.betType;
            set
            {
                this.betType = value;
                this.bet.BetType = value?.Model;

                RaisePropertyChanged(() => this.BetType);
            }
        }

        public OfferTypeLookup OfferType
        {
            get => this.offerType;
            set
            {
                this.offerType = value;
                this.bet.OfferType = offerType?.Model;
                RaisePropertyChanged(()=>this.OfferType);
            }
        }

        public SportLookup Sport
        {
            get => this.sportLookup;
            set
            {
                this.sportLookup = value;
                this.bet.Sport = sportLookup?.Model;
                RaisePropertyChanged(() => this.Sport);
            }
        }
        public MarketLookup Market
        {
            get => this.marketLookup;
            set
            {
                this.marketLookup = value;
                this.bet.Market = marketLookup?.Model;
                RaisePropertyChanged(() => this.Market);
            }
        }

        /// <summary>
        /// Gets the account name
        /// </summary>
        public string AccountName => this.bet.Account?.Name;

        public bool CanPlaceBet => this.bet.Returns > 0;

        public IEnumerable<BetTypeLookup> BetTypes { get; }

        public IEnumerable<OfferTypeLookup> OfferTypes { get; }

        public IEnumerable<SportLookup> Sports { get; }

        public IEnumerable<MarketLookup> Markets { get; }

        public IEnumerable<BankLookupItem> Banks { get; }

        /// <summary>
        /// Gets or sets the return
        /// </summary>
        public double Returns
        {
            get => this.bet.Returns;
            set
            {
                if (this.bet.Returns == value)
                    return;

                this.bet.Returns = value;
                RaisePropertiesChanged(()=>this.Returns, ()=>this.CanPlaceBet);
            }
        }

        internal override ITransactionDetail Detail => this.bet.Detail;


        /// <summary>
        /// Commits the bet
        /// </summary>
        public override void Commit()
        {
            if (this.CanPlaceBet)
            {
                this.bet.Place();
                this.bet.Complete();
                Messenger.Default.Send(new TransactionsUpdatedMessage());
            }
        }

    }
}