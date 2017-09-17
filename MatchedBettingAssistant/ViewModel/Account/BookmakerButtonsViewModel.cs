﻿using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Model;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BookmakerButtonsViewModel : ViewModelBase
    {
        private readonly IAccount account;

        public IDialogService DepositDialogService => GetService<IDialogService>();

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

        }

        public void Bet()
        {

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

            var result = DepositDialogService.ShowDialog(
                dialogCommands: new List<UICommand>() { okCommand, cancelCommand },
                title: walletSetter.ActionDescription,
                viewModel: depositViewModel
            );

        }

    }
}