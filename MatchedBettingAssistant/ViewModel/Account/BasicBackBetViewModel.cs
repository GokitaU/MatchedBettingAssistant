using System;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Model;

namespace MatchedBettingAssistant.ViewModel.Account
{
    /// <summary>
    /// basic back bet
    /// </summary>
    public class BasicBackBetViewModel : ViewModelBase
    {
        private readonly SimpleBackBet backBet;

        public BasicBackBetViewModel(SimpleBackBet backBet)
        {
            this.backBet = backBet;
        }

        /// <summary>
        /// Gets or sets the date of the transaction
        /// </summary>
        public DateTime Date
        {
            get => this.backBet.Date;
            set
            {
                if (this.backBet.Date == value)
                    return;

                this.backBet.Date = value;

                RaisePropertyChanged(() => this.Date);
            }
        }

        /// <summary>
        /// Gets or sets the stake
        /// </summary>
        public double Stake
        {
            get => this.backBet.Stake;
            set
            {
                if (this.backBet.Stake == value)
                    return;

                this.backBet.Stake = value;

                RaisePropertyChanged(() => this.Stake);
            }
        }

        /// <summary>
        /// Gets the account name
        /// </summary>
        public string AccountName => this.backBet.Account?.Name;

        /// <summary>
        /// Gets or sets the return
        /// </summary>
        public double Returns
        {
            get => this.backBet.Returns;
            set
            {
                if (this.backBet.Returns == value)
                    return;

                this.backBet.Returns = value;

                RaisePropertyChanged(() => this.Returns);
            }
        }

        /// <summary>
        /// Commits the bet
        /// </summary>
        public void Commit()
        {
            this.backBet.Place();

            Messenger.Default.Send(new TransactionsUpdatedMessage());
        }
    }
}