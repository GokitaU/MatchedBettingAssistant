using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class WalletButtonsViewModel : ViewModelBase
    {
        private readonly ITransactionAccount account;
        private readonly IRepository repository;

        public WalletButtonsViewModel(ITransactionAccount account, IRepository repository)
        {
            this.account = account;
            this.repository = repository;
        }

        public IDialogService TransferDialogService => ServiceContainer.GetService<IDialogService>("transferDialog");
        public IDialogService ApplyDialogService => ServiceContainer.GetService<IDialogService>("applyDialog");
        public IDialogService BetDialogService => ServiceContainer.GetService<IDialogService>("betDialog");

        private DelegateCommand depositCommand;
        private DelegateCommand withdrawCommand;
        private DelegateCommand transferCommand;
        private DelegateCommand addFundsCommand;

        public DelegateCommand ButtonOneCommand => this.depositCommand ?? (this.depositCommand = new DelegateCommand(Deposit, CanWithdrawAndDeposit));

        public string ButtonOneCaption { get; } = "Deposit";

        public DelegateCommand ButtonTwoCommand
        {
            get => this.withdrawCommand ?? (this.withdrawCommand = new DelegateCommand(Withdraw, CanWithdrawAndDeposit));
        }

        public string ButtonTwoCaption { get; } = "Withdraw";

        public DelegateCommand ButtonThreeCommand
        {
            get => this.addFundsCommand ?? (this.addFundsCommand = new DelegateCommand(AddFunds));
        }

        public string ButtonThreeCaption { get; } = "Add";

        public DelegateCommand ButtonFourCommand
        {
            get => this.transferCommand ?? (this.transferCommand = new DelegateCommand(Transfer, CanTransfer));
        }

        public string ButtonFourCaption { get; } = "Transfer";

        private bool CanWithdrawAndDeposit()
        {
            //need at least one wallet 
            return this.repository.BookmakerRepository.Count() > 0;
        }

        private bool CanTransfer()
        {
            return this.repository.WalletRepository.Count(this.account.Id) > 0;
        }

        /// <summary>
        /// Deposit funds from a betting account
        /// </summary>
        private void Deposit()
        {
            var action = new TransferFundsAccountAction(this.repository.TransactionRepository)
            {
                Destination = this.account,
                Date = DateTime.Today
            };

            var accountSetter = new DepositActionAccountSetter(action);
            var accounts = this.repository.BookmakerRepository.GetAccounts();
            TransferFunds(action, accountSetter, accounts);
        }

        /// <summary>
        /// Withdraw funds to a betting account
        /// </summary>
        private void Withdraw()
        {
            var action = new TransferFundsAccountAction(this.repository.TransactionRepository)
            {
                Source = this.account,
                Date = DateTime.Today
            };

            var accountSetter = new WithdrawActionAccountSetter(action);
            var accounts = this.repository.BookmakerRepository.GetAccounts();
            TransferFunds(action, accountSetter, accounts);
        }

        /// <summary>
        /// Add or remove funds (no account)
        /// </summary>
        private void AddFunds()
        {
            var action = new ApplyFundsAccountAction(this.repository.TransactionRepository)
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

        /// <summary>
        /// Transfer from another wallet
        /// </summary>
        private void Transfer()
        {
            var action = new TransferFundsAccountAction(this.repository.TransactionRepository)
            {
                Destination = this.account,
                Date = DateTime.Today
            };

            var accountSetter = new DepositActionAccountSetter(action);
            var accounts = this.repository.WalletRepository.GetWallets();
            TransferFunds(action, accountSetter, accounts);
        }

        private void TransferFunds(TransferFundsAccountAction action, ITransferActionAccountSetter accountSetter, IEnumerable<ITransactionAccount> accounts)
        {
            var depositViewModel = new TransferFundsToAccountViewModel(action, accounts, accountSetter);

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
                accountSetter.ActionDescription,
                depositViewModel);
        }
    }
}