﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Account;
using MatchedBettingAssistant.Model.Bets;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BookmakerButtonsViewModel : ViewModelBase
    {
        private readonly IAccount account;

        public IDialogService TransferDialogService => ServiceContainer.GetService<IDialogService>("transferDialog");
        public IDialogService ApplyDialogService => ServiceContainer.GetService<IDialogService>("applyDialog");
        public IDialogService BetDialogService => ServiceContainer.GetService<IDialogService>("betDialog");

        private DelegateCommand depositCommand;
        private DelegateCommand withdrawCommand;
        private DelegateCommand bonusCommand;
        private DelegateCommand betCommand;
        private Visibility bonusButtonVisibility;
        private Visibility betButtonVisibility;

        public BookmakerButtonsViewModel(IAccount account)
        {
            this.account = account;
            this.BonusButtonVisibility = Visibility.Hidden;
            this.BetButtonVisibility = Visibility.Hidden;
        }

        /// <summary>
        /// Gets the command for depositing funds to account
        /// </summary>
        public DelegateCommand DepositCommand => this.depositCommand ?? (this.depositCommand = new DelegateCommand(Deposit));

        /// <summary>
        /// Gets the command for withdrawing funds from account
        /// </summary>
        public DelegateCommand WithdrawCommand => this.withdrawCommand ?? (this.withdrawCommand = new DelegateCommand(Withdraw));

        public DelegateCommand BonusCommand => this.bonusCommand ?? (this.bonusCommand = new DelegateCommand(Bonus));

        public DelegateCommand BetCommand => this.betCommand ?? (this.betCommand = new DelegateCommand(Bet));

        public Visibility BonusButtonVisibility
        {
            get => bonusButtonVisibility;
            set
            {
                bonusButtonVisibility = value;
                RaisePropertyChanged(()=>BonusButtonVisibility);
            }
        }

        public Visibility BetButtonVisibility
        {
            get => betButtonVisibility;
            set
            {
                betButtonVisibility = value; 
                RaisePropertyChanged(()=>BetButtonVisibility);
            }
        }

        /// <summary>
        /// Initiates the deposit action
        /// </summary>
        public void Deposit()
        {
            var action = new TransferFundsAccountAction()
            {
                Destination = this.account,
                Date = DateTime.Today
            };

            var walletSetter = new DepositActionWalletSetter(action);

            TransferFunds(action, walletSetter);
        }

        /// <summary>
        /// Initiates the withdraw action
        /// </summary>
        public void Withdraw()
        {
            var action = new TransferFundsAccountAction()
            {
                Source = this.account,
                Date = DateTime.Today
            };

            var walletSetter = new WithdrawActionWalletSetter(action);

            TransferFunds(action, walletSetter);
        }

        public void Bonus()
        {
            var action = new ApplyFundsAccountAction()
            {
                Destination = this.account,
                Date = DateTime.Today
            };

            var applyFunds = new ApplyFundsToAccountViewModel(action);

            var okCommand = new UICommand()
            {
                Caption = "Ok",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand(applyFunds.Commit)
            };

            var cancelCommand = new UICommand()
            {
                Id = MessageBoxResult.Cancel,
                Caption = "Cancel",
                IsCancel = true,
                IsDefault = false
            };

            ApplyDialogService.ShowDialog(
                new List<UICommand>() { okCommand, cancelCommand },
                "Adjust Funds",
                applyFunds);
        }

        public void Bet()
        {
            if (this.account is IBettingAccount bettingAccount)
            {
                var basicBet = new SimpleBet()
                {
                    Account = bettingAccount,
                    Date = DateTime.Today
                };

                var bet = new PlaceBetViewModel(bettingAccount);

                var okCommand = new UICommand()
                {
                    Caption = "Ok",
                    IsCancel = false,
                    IsDefault = true,
                    Command = new DelegateCommand<CancelEventArgs>(x=>bet.Commit(), x=> bet.Returns > 0)
                };

                var cancelCommand = new UICommand()
                {
                    Id = MessageBoxResult.Cancel,
                    Caption = "Cancel",
                    IsCancel = true,
                    IsDefault = false
                };

                BetDialogService.ShowDialog(
                    new List<UICommand>() {okCommand, cancelCommand},
                    "Basic Bet",
                    bet);
            }
        }

        /// <summary>
        /// Creates dialog for transferring funds to or from a wallet
        /// </summary>
        /// <param name="action"></param>
        /// <param name="walletSetter"></param>
        private void TransferFunds(TransferFundsAccountAction action, ITransferActionWalletSetter walletSetter)
        {
            var wallets = new List<Wallet>()
            { 
                new Wallet() {Name = "Skrill", StartingBalance = 100.00},
                new Wallet() {Name = "Credit Card", StartingBalance = 600.0}
            };

            var depositViewModel = new TransferFundsToAccountViewModel(action, wallets, walletSetter);

            var okCommand = new UICommand()
            {
                Caption = "Ok",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand(depositViewModel.Commit)
            };

            var cancelCommand = new UICommand()
            {
                Id = MessageBoxResult.Cancel,
                Caption = "Cancel",
                IsCancel = true,
                IsDefault = false
            };

            TransferDialogService.ShowDialog(
                new List<UICommand>() { okCommand, cancelCommand },
                walletSetter.ActionDescription,
                depositViewModel);


        }

        
    }
}