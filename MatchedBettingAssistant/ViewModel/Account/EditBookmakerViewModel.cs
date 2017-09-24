﻿using System.Collections.Generic;
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
        private IEnumerable<IWallet> wallets;

        public EditBookmakerViewModel(IBettingAccount account, IEnumerable<IWallet> wallets)
        {
            this.account = account;
            this.wallets = wallets;
            this.BookmakerButtons = new BookmakerButtonsViewModel(this.account, this.wallets)
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

        public BookmakerButtonsViewModel BookmakerButtons { get; }

        private void RegisterMessages()
        {
            Messenger.Default.Register<TransactionsUpdatedMessage>(this, UpdateBalance);

        }

        private void UpdateBalance(TransactionsUpdatedMessage obj)
        {
            this.RaisePropertyChanged(() => this.Balance);
        }
    }
}