using System;
using DevExpress.Mvvm;
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

        public BasicBetViewModel(SimpleBet bet)
        {
            this.bet = bet;
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

        /// <summary>
        /// Gets the account name
        /// </summary>
        public string AccountName => this.bet.Account?.Name;

        public bool CanPlaceBet => this.bet.Returns > 0;

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