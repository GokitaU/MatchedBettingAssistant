using System.Collections.Generic;
using System.Windows;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class EditBookmakerViewModel : ViewModelBase
    {
        private readonly IBettingAccount account;
        private IRepository repository;

        public EditBookmakerViewModel(IBettingAccount account, IRepository repository)
        {
            this.account = account;
            this.repository = repository;
            this.BookmakerButtons = new BookmakerButtonsViewModel(this.account, this.repository)
            {
                BonusButtonVisibility = Visibility.Visible,
                BetButtonVisibility = Visibility.Visible
            };
            this.RegisterMessages();
        }

        public string Name
        {
            get => this.account.Name;
            set
            {
                this.account.Name = value;
                RaisePropertyChanged(() => this.Name);

                Messenger.Default.Send(new BookmakerNameChangedMessage(this.account));
            }
        }

        /// <summary>
        /// Gets or sets the starting balance
        /// </summary>
        public double StartingBalance
        {
            get => this.account.StartingBalance;
            set
            {
                this.account.StartingBalance = value;
                RaisePropertyChanged(() => this.StartingBalance);
            }
        }

        public bool IsExchange
        {
            get => this.account.IsExchange;
            set
            {
                this.account.IsExchange = value;
                RaisePropertyChanged(()=>this.IsExchange);
            }
        }

        public double CommissionPercent
        {
            get => this.account.CommissionPercent;
            set
            {
                this.account.CommissionPercent = value;
                RaisePropertyChanged(()=>this.CommissionPercent);
            }
        }

        public double Balance => this.account.Balance;

        public double Profit => this.account.Profit;

        public double PaybackDue => this.account.PaybackDue;

        public BookmakerButtonsViewModel BookmakerButtons { get; }

        private void RegisterMessages()
        {
            Messenger.Default.Register<TransactionsUpdatedMessage>(this, UpdateBalance);

        }

        private void UpdateBalance(TransactionsUpdatedMessage obj)
        {
            this.RaisePropertyChanged(() => this.Balance);
            this.RaisePropertyChanged(() => this.Profit);
            this.RaisePropertyChanged(() => this.PaybackDue);
        }
    }
}