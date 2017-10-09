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

        public BasicBetViewModel(SimpleBet bet, IEnumerable<IBetType> betTypes, IEnumerable<IOfferType> offerTypes)
        {
            this.bet = bet;
            this.BetTypes = betTypes.Select(x => new BetTypeLookup(x));
            this.OfferTypes = offerTypes.Select(x => new OfferTypeLookup(x));
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

        public BetTypeLookup BetType
        {
            get => this.betType;
            set
            {
                this.betType = value;
                this.bet.BetType = value?.BetType;

                RaisePropertyChanged(() => this.BetType);
            }
        }

        public OfferTypeLookup OfferType
        {
            get => this.offerType;
            set
            {
                this.offerType = value;
                this.bet.OfferType = offerType?.OfferType;
                RaisePropertyChanged(()=>this.OfferType);
            }
        }

        /// <summary>
        /// Gets the account name
        /// </summary>
        public string AccountName => this.bet.Account?.Name;

        public bool CanPlaceBet => this.bet.Returns > 0;

        public IEnumerable<BetTypeLookup> BetTypes { get; }

        public IEnumerable<OfferTypeLookup> OfferTypes { get; }

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


        /// <summary>
        /// Commits the bet
        /// </summary>
        public override void Commit()
        {
            if (this.CanPlaceBet)
            {
                this.bet.Place();

                Messenger.Default.Send(new TransactionsUpdatedMessage());
            }
        }

    }
}