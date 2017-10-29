using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Windows;
using DevExpress.Entity.Model;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Accounts;
using MatchedBettingAssistant.Model.Bets;
 
namespace MatchedBettingAssistant.ViewModel.Account
{
    public class BookmakerButtonsViewModel : ViewModelBase
    {
        private readonly ITransactionAccount account;
        private readonly IRepository repository;

        public IDialogService TransferDialogService => ServiceContainer.GetService<IDialogService>("transferDialog");
        public IDialogService ApplyDialogService => ServiceContainer.GetService<IDialogService>("applyDialog");
        public IDialogService BetDialogService => ServiceContainer.GetService<IDialogService>("betDialog");

        private DelegateCommand depositCommand;
        private DelegateCommand withdrawCommand;
        private DelegateCommand bonusCommand;
        private DelegateCommand betCommand;

        public BookmakerButtonsViewModel(ITransactionAccount account, IRepository repository)
        {
            this.account = account;
            this.repository = repository;
        }

        /// <summary>
        /// Gets the command for depositing funds to account
        /// </summary>
        public DelegateCommand ButtonOneCommand => this.depositCommand ?? (this.depositCommand = new DelegateCommand(Deposit, CanWithdrawAndDeposit));

        public string ButtonOneCaption => "Deposit";

        /// <summary>
        /// Gets the command for withdrawing funds from account
        /// </summary>
        public DelegateCommand ButtonTwoCommand => this.withdrawCommand ?? (this.withdrawCommand = new DelegateCommand(Withdraw, CanWithdrawAndDeposit));

        public string ButtonTwoCaption => "Withdraw";

        public DelegateCommand ButtonThreeCommand => this.bonusCommand ?? (this.bonusCommand = new DelegateCommand(Bonus));

        public string ButtonThreeCaption => "Bonus";

        public DelegateCommand ButtonFourCommand => this.betCommand ?? (this.betCommand = new DelegateCommand(Bet));

        public string ButtonFourCaption => "Bet";

        private bool CanWithdrawAndDeposit()
        {
            //need at least one wallet 
            return this.repository.WalletRepository.Count()> 0;
        }

        /// <summary>
        /// Initiates the deposit action
        /// </summary>
        public void Deposit()
        {
            var detail = this.repository.TransactionRepository.NewDetail();
            detail.CreateBackTransaction();
            detail.CreateLayTransaction();

            var action = new TransferFundsAccountAction(detail)
            {
                Destination = this.account,
                Date = DateTime.Today
            };

            var walletSetter = new DepositActionAccountSetter(action);

            TransferFunds(action, walletSetter);
        }

        /// <summary>
        /// Initiates the withdraw action
        /// </summary>
        public void Withdraw()
        {
            var detail = this.repository.TransactionRepository.NewDetail();
            detail.CreateBackTransaction();
            detail.CreateLayTransaction();

            var action = new TransferFundsAccountAction(detail)
            {
                Source = this.account,
                Date = DateTime.Today
            };

            var walletSetter = new WithdrawActionAccountSetter(action);

            TransferFunds(action, walletSetter);
        }

        public void Bonus()
        {
            var detail = this.repository.TransactionRepository.NewDetail();
            detail.CreateBackTransaction();

            var action = new ApplyFundsAccountAction(detail)
            {
                Destination = this.account,
                Date = DateTime.Today
            };

            var applyFunds = new ApplyFundsToAccountViewModel(action, this.repository.TransactionRepository);

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
                var bet = new PlaceBetViewModel(bettingAccount, this.repository);

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
        /// <param name="accountSetter"></param>
        private void TransferFunds(TransferFundsAccountAction action, ITransferActionAccountSetter accountSetter)
        {
            var wallets = this.repository.WalletRepository.GetWallets();
            var depositViewModel = new TransferFundsToAccountViewModel(action, wallets, accountSetter, this.repository.TransactionRepository);

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