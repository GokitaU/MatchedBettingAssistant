using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Bets;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class SimpleMatchedBetViewModel : BetViewModel
    {
        private readonly SimpleMatchedBet matchedBet;
        private const double tolerance = 0.0001;
        private AccountLookupItem backAccount;
        private AccountLookupItem layAccount;
        private BetTypeLookup betType;
        private OfferTypeLookup offerType;
        private ObservableCollection<AccountLookupItem> bookmakers;
        private ObservableCollection<AccountLookupItem> exchanges;
        private ObservableCollection<BetTypeLookup> betTypes;
        private ObservableCollection<OfferTypeLookup> offerTypes;

        public SimpleMatchedBetViewModel(SimpleMatchedBet matchedBet, 
                                         IEnumerable<IBettingAccount> bookmakers, 
                                         IEnumerable<IBettingAccount> exchanges,
                                         IEnumerable<IBetType> betTypes,
                                         IEnumerable<IOfferType> offerTypes)
        {
            this.matchedBet = matchedBet;
            CreateBookmakers(bookmakers);
            CreateExchanges(exchanges);
            CreateBetTypes(betTypes);
            CreateOfferTypes(offerTypes);
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

        public ObservableCollection<AccountLookupItem> Bookmakers
        {
            get => bookmakers;
            set
            {
                this.bookmakers = value;
                this.RaisePropertyChanged(()=>Bookmakers);
            }
        }

        public ObservableCollection<AccountLookupItem> Exchanges
        {
            get => exchanges;
            private set
            {
                this.exchanges = value;
                this.RaisePropertyChanged(()=>Exchanges);
            }
        }

        public ObservableCollection<BetTypeLookup> BetTypes
        {
            get { return betTypes; }
            private set
            {
                this.betTypes = value;
                this.RaisePropertyChanged(()=>BetTypes);
            }
        }

        public ObservableCollection<OfferTypeLookup> OfferTypes
        {
            get { return offerTypes; }
            private set
            {
                this.offerTypes = value;
                this.RaisePropertyChanged(()=>OfferTypes);
            }
        }

        public AccountLookupItem BackAccount
        {
            get { return backAccount; }
            set
            {
                backAccount = value;
                this.matchedBet.BackAccount = value?.Account as IBettingAccount;
                RaisePropertyChanged(()=>BackAccount);
            }
        }

        public AccountLookupItem LayAccount
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
                this.matchedBet.BetType = value.BetType;
                RaisePropertyChanged(()=>BetType);
            }
        }

        public OfferTypeLookup OfferType
        {
            get => this.offerType;
            set
            {
                this.offerType = value;
                this.matchedBet.OfferType = value.OfferType;
                RaisePropertyChanged(()=>OfferType);
            }
        }

        public override void Commit()
        {
            this.matchedBet.Place();

            Messenger.Default.Send(new TransactionsUpdatedMessage());
        }

        private void CreateExchanges(IEnumerable<IBettingAccount> exchanges)
        {
            this.Exchanges = new ObservableCollection<AccountLookupItem>(exchanges.Select(x => new AccountLookupItem(x)));

            this.LayAccount =
                this.Exchanges.FirstOrDefault(x => ReferenceEquals(x.Account, this.matchedBet.LayAccount));
        }

        private void CreateBookmakers(IEnumerable<IBettingAccount> bookmakers)
        {
            this.Bookmakers = new ObservableCollection<AccountLookupItem>(bookmakers.Select(x => new AccountLookupItem(x)));

            this.BackAccount = this.Bookmakers.FirstOrDefault(x => ReferenceEquals(x.Account, this.matchedBet.BackAccount));
        }


        private void CreateOfferTypes(IEnumerable<IOfferType> offerTypes)
        {
            this.OfferTypes = new ObservableCollection<OfferTypeLookup>(offerTypes.Select(x => new OfferTypeLookup(x)));

            this.OfferType = this.OfferTypes.FirstOrDefault(x => ReferenceEquals(x.OfferType, this.matchedBet.OfferType));
        }

        private void CreateBetTypes(IEnumerable<IBetType> betTypes)
        {
            this.BetTypes = new ObservableCollection<BetTypeLookup>(betTypes.Select(x => new BetTypeLookup(x)));

            this.BetType = this.BetTypes.FirstOrDefault(x => ReferenceEquals(x.BetType, this.matchedBet.BetType));
        }
    }
}