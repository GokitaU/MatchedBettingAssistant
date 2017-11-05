using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Bets;
using MatchedBettingAssistant.ViewModel.Messages;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class SimpleMatchedBetViewModel : BetViewModel
    {
        private readonly SimpleMatchedBet matchedBet;
        private const double tolerance = 0.0001;
        private BookmakerLookupItem backAccount;
        private BookmakerLookupItem layAccount;
        private BetTypeLookup betType;
        private OfferTypeLookup offerType;
        private SportLookup sportLookup;
        private MarketLookup marketLookup;
        private BankLookupItem bankLookup;

        private ObservableCollection<BookmakerLookupItem> bookmakers;
        private ObservableCollection<BookmakerLookupItem> exchanges;
        private ObservableCollection<BetTypeLookup> betTypes;
        private ObservableCollection<OfferTypeLookup> offerTypes;
        private ObservableCollection<SportLookup> sports;
        private ObservableCollection<MarketLookup> markets;
        private ObservableCollection<BankLookupItem> banks;

        private DelegateCommand mugBetCommand;
        private DelegateCommand qualifyingBetCommand;
        private DelegateCommand bonusBetCommand;

        public SimpleMatchedBetViewModel(SimpleMatchedBet matchedBet, 
                                         IEnumerable<IBettingAccount> bookmakers, 
                                         IEnumerable<IBettingAccount> exchanges,
                                         IEnumerable<IBank> banks,
                                         IEnumerable<IBetType> betTypes,
                                         IEnumerable<IOfferType> offerTypes,
                                         IEnumerable<ISport> sports,
                                         IEnumerable<IMarket> markets)
        {
            this.matchedBet = matchedBet;
            CreateBookmakers(bookmakers);
            CreateExchanges(exchanges);
            CreateBanks(banks);
            CreateBetTypes(betTypes);
            CreateOfferTypes(offerTypes);
            CreateSports(sports);
            CreateMarkets(markets);
        }

        public DateTime Date
        {
            get => this.matchedBet.Date;
            set
            {
                if (this.matchedBet.Date == value)
                    return;

                this.matchedBet.Date = value;

                RaisePropertyChanged(() => this.Date);
            }
        }

        public double BackReturns
        {
            get => this.matchedBet.BackReturns;
            set
            {
                if (Math.Abs(this.matchedBet.BackReturns - value) < tolerance)
                    return;

                this.matchedBet.BackReturns = value;

                RaisePropertiesChanged(() => this.BackReturns, ()=> Profit);
            }
        }

        public double LayReturns
        {
            get => this.matchedBet.LayReturns;
            set
            {
                if (Math.Abs(this.matchedBet.LayReturns - value) < tolerance)
                    return;

                this.matchedBet.LayReturns = value;

                RaisePropertiesChanged(() => this.LayReturns, ()=> this.Profit);
            }
        }

        public double Profit
        {
            get => this.matchedBet.LayReturns + this.matchedBet.BackReturns;
        }

        public ObservableCollection<BookmakerLookupItem> Bookmakers
        {
            get => bookmakers;
            set
            {
                this.bookmakers = value;
                this.RaisePropertyChanged(()=>Bookmakers);
            }
        }

        public ObservableCollection<BookmakerLookupItem> Exchanges
        {
            get => exchanges;
            private set
            {
                this.exchanges = value;
                this.RaisePropertyChanged(()=>Exchanges);
            }
        }

        public ObservableCollection<BankLookupItem> Banks
        {
            get => banks;
            set
            {
                this.banks = value;
                RaisePropertyChanged(()=>Banks);
            }
        }

        public ObservableCollection<BetTypeLookup> BetTypes
        {
            get => betTypes;
            private set
            {
                this.betTypes = value;
                this.RaisePropertyChanged(()=>BetTypes);
            }
        }

        public ObservableCollection<OfferTypeLookup> OfferTypes
        {
            get => offerTypes;
            private set
            {
                this.offerTypes = value;
                this.RaisePropertyChanged(()=>OfferTypes);
            }
        }

        public ObservableCollection<SportLookup> Sports
        {
            get => sports;
            set
            {
                this.sports = value;
                RaisePropertyChanged(()=>Sports);
            }
        }

        public ObservableCollection<MarketLookup> Markets
        {
            get => markets;
            set
            {
                this.markets = value;
                RaisePropertyChanged(()=>Markets);
            }
        }

        public BankLookupItem Bank
        {
            get => this.bankLookup;
            set
            {
                this.bankLookup = value;
                this.matchedBet.Bank = value?.Account;

                RaisePropertyChanged(()=>Bank);
            }
        }

        public BookmakerLookupItem BackAccount
        {
            get { return backAccount; }
            set
            {
                backAccount = value;
                this.matchedBet.BackAccount = value?.Account as IBettingAccount;
                RaisePropertyChanged(()=>BackAccount);
            }
        }

        public BookmakerLookupItem LayAccount
        {
            get { return layAccount; }
            set
            {
                layAccount = value;
                this.matchedBet.LayAccount = value?.Account as IBettingAccount;
                RaisePropertyChanged(()=>LayAccount);
            }
        }

        public BetTypeLookup BetType
        {
            get => this.betType;
            set
            {
                this.betType = value;
                this.matchedBet.BetType = value?.Model;
                RaisePropertyChanged(()=>BetType);
            }
        }

        public OfferTypeLookup OfferType
        {
            get => this.offerType;
            set
            {
                this.offerType = value;
                this.matchedBet.OfferType = value?.Model;
                RaisePropertyChanged(()=>OfferType);
            }
        }

        public SportLookup Sport
        {
            get => this.sportLookup;
            set
            {
                this.sportLookup = value;
                this.matchedBet.Sport = value?.Model;
                RaisePropertyChanged(()=>this.Sport);
            }
        }

        public MarketLookup Market
        {
            get => this.marketLookup;
            set
            {
                this.marketLookup = value;
                this.matchedBet.Market = value?.Model;
                RaisePropertyChanged(()=>Market);
            }
        }
        public string Description
        {
            get => this.matchedBet.Description;
            set
            {
                if (this.matchedBet.Description == value)
                    return;

                this.matchedBet.Description = value;

                RaisePropertyChanged(() => this.Description);
            }
        }

        internal override ITransactionDetail Detail => this.matchedBet.Detail;

        public DelegateCommand MugBetCommand => this.mugBetCommand ?? (this.mugBetCommand = new DelegateCommand(MugBet));

        public DelegateCommand QualifyingBetCommand => this.qualifyingBetCommand ?? (this.qualifyingBetCommand = new DelegateCommand(QualifyingBet));

        public DelegateCommand BonusBetCommand => this.bonusBetCommand ?? (this.bonusBetCommand = new DelegateCommand(BonusBet));

        public override void Commit()
        {
            this.matchedBet.Place();

            Messenger.Default.Send(new TransactionsUpdatedMessage());
        }

        private void CreateExchanges(IEnumerable<IBettingAccount> exchanges)
        {
            this.Exchanges = new ObservableCollection<BookmakerLookupItem>(exchanges.Select(x => new BookmakerLookupItem(x)));

            this.LayAccount =
                this.Exchanges.FirstOrDefault(x => ReferenceEquals(x.Account, this.matchedBet.LayAccount));
        }

        private void CreateBookmakers(IEnumerable<IBettingAccount> bookmakers)
        {
            this.Bookmakers = new ObservableCollection<BookmakerLookupItem>(bookmakers.Select(x => new BookmakerLookupItem(x)));

            this.BackAccount = this.Bookmakers.FirstOrDefault(x => ReferenceEquals(x.Account, this.matchedBet.BackAccount));
        }

        private void CreateBanks(IEnumerable<IBank> banks)
        {
            this.Banks = new ObservableCollection<BankLookupItem>(banks.Select(x => new BankLookupItem(x)));

            this.Bank = this.Banks.FirstOrDefault(x => ReferenceEquals(x.Account, this.matchedBet.Bank));
        }

        private void CreateOfferTypes(IEnumerable<IOfferType> offerTypes)
        {
            this.OfferTypes = new ObservableCollection<OfferTypeLookup>(offerTypes.Select(x => new OfferTypeLookup(x)));

            this.OfferType = this.OfferTypes.FirstOrDefault(x => ReferenceEquals(x.Model, this.matchedBet.OfferType));
        }

        private void CreateBetTypes(IEnumerable<IBetType> betTypes)
        {
            this.BetTypes = new ObservableCollection<BetTypeLookup>(betTypes.Select(x => new BetTypeLookup(x)));

            this.BetType = this.BetTypes.FirstOrDefault(x => ReferenceEquals(x.Model, this.matchedBet.BetType));
        }

        private void CreateSports(IEnumerable<ISport> sports)
        {
            this.Sports = new ObservableCollection<SportLookup>(sports.Select(x => new SportLookup(x)));

            this.Sport = this.Sports.FirstOrDefault(x => ReferenceEquals(x.Model, this.matchedBet.Sport));
        }

        private void CreateMarkets(IEnumerable<IMarket> markets)
        {
            this.Markets = new ObservableCollection<MarketLookup>(markets.Select(x => new MarketLookup(x)));

            this.Market = this.Markets.FirstOrDefault(x => ReferenceEquals(x.Model, this.matchedBet.Market));

        }

        private void MugBet()
        {
            this.OfferType = this.OfferTypes.FirstOrDefault(x => x.Name == "None");
            this.Description = "Payback Bet";
        }

        private void QualifyingBet()
        {
            this.OfferType = this.OfferTypes.FirstOrDefault(x => x.Name == "Qualifier");
            this.Description = "Qualifier";
        }

        private void BonusBet()
        {
            this.OfferType = this.OfferTypes.FirstOrDefault(x => x.Name == "Bonus");
            this.Description = "Bonus";
        }
    }
}