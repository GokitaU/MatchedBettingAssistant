using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using DevExpress.Data.Helpers;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Accounts;
using MatchedBettingAssistant.ViewModel.Messages;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class EditBookmakerViewModel : EditAccountViewModel
    {
        private DelegateCommand depositCommand;
        private DelegateCommand withdrawCommand;
        private DelegateCommand bonusCommand;
        private DelegateCommand betCommand;

        public EditBookmakerViewModel(IRepository repository)
            : base(repository)
        { 
        }

        public bool IsExchange
        {
            get => this.BettingAccount?.IsExchange ?? false;
            set
            {
                this.BettingAccount.IsExchange = value;

                if (this.IsExchange)
                {
                    this.PaybackPercent = 0;
                }

                EntityPropertyChanged(()=>this.IsExchange);
                RaisePropertyChanged(()=> this.PaybackPercentVisibility);
            }
        }

        public double CommissionPercent
        {
            get => this.BettingAccount?.CommissionPercent ?? 0;
            set
            {
                this.BettingAccount.CommissionPercent = value;
                EntityPropertyChanged(()=>this.CommissionPercent);
            }
        }

        public double PaybackPercent
        {
            get => this.BettingAccount?.PaybackPercent ?? 0;
            set
            {
                this.BettingAccount.PaybackPercent = value;
                EntityPropertyChanged(()=>this.PaybackPercent);
                UpdateBalance();
            }
        }

        public bool LimitedAccount
        {
            get => this.BettingAccount?.LimitedAccount ?? false;
            set
            {
                if (this.BettingAccount.LimitedAccount == value)
                    return;

                this.BettingAccount.LimitedAccount = value;

                RaisePropertyChanged(() => this.LimitedAccount);
            }
        }

        public bool CompletedNewAccountOffer
        {
            get => this.BettingAccount?.CompletedNewAccountOffer ?? false;
            set
            {
                if (this.BettingAccount.CompletedNewAccountOffer == value)
                    return;

                this.BettingAccount.CompletedNewAccountOffer = value;

                RaisePropertyChanged(() => this.CompletedNewAccountOffer);
            }
        }

        public double AccountProfit => this.BettingAccount?.AccountProfit ?? 0;

        public double Profit => this.BettingAccount?.Profit ?? 0;

        public double PaybackDue => this.BettingAccount?.PaybackDue ?? 0;

        /// <summary>
        /// Gets the command for depositing funds to account
        /// </summary>
        public DelegateCommand DepositCommand => 
            this.depositCommand ?? (this.depositCommand = new DelegateCommand(Deposit, CanWithdrawAndDeposit));

        /// <summary>
        /// Gets the command for withdrawing funds from account
        /// </summary>
        public DelegateCommand WithdrawCommand => 
            this.withdrawCommand ?? (this.withdrawCommand = new DelegateCommand(Withdraw, CanWithdrawAndDeposit));

        public DelegateCommand BonusCommand => this.bonusCommand ?? (this.bonusCommand = new DelegateCommand(Bonus));

        public DelegateCommand BetCommand => this.betCommand ?? (this.betCommand = new DelegateCommand(Bet));


        public Visibility PaybackPercentVisibility => this.IsExchange ? Visibility.Hidden : Visibility.Visible;

        protected override void New()
        {
            this.Account = this.Repository.BookmakerRepository.New();
            this.Name = "New Bookmaker";
        }

        public override void Refresh()
        {
            this.RaisePropertyChanged(() => IsExchange);
            this.RaisePropertyChanged(() => CommissionPercent);
            this.RaisePropertyChanged(() => LimitedAccount);
            this.RaisePropertyChanged(() => CompletedNewAccountOffer);
            this.RaisePropertyChanged(() => PaybackPercent);
            this.RaisePropertyChanged(() => AccountProfit);
            this.RaisePropertyChanged(() => Profit);
            this.RaisePropertyChanged(() => PaybackDue);

            base.Refresh();

        }

        private IBettingAccount BettingAccount => this.Account as IBettingAccount;

        private bool CanWithdrawAndDeposit()
        {
            return true;
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<TransactionsUpdatedMessage>(this, UpdateBalance);

        }

        private void UpdateBalance(TransactionsUpdatedMessage obj)
        {
            UpdateBalance();
        }

        private void UpdateBalance()
        {
            this.RaisePropertyChanged(() => this.Balance);
            this.RaisePropertyChanged(() => this.Profit);
            this.RaisePropertyChanged(() => this.PaybackDue);
        }

        /// <summary>
        /// requests a deposit of funds
        /// </summary>
        private void Deposit()
        {

            TransferFunds((action) => new DepositActionAccountSetter(action));
        }

        /// <summary>
        /// requests a withdrawal of funds 
        /// </summary>
        private void Withdraw()
        {
            TransferFunds((action) => new WithdrawActionAccountSetter(action));
        }

        /// <summary>
        /// creates a message to request the transfer of funds
        /// </summary>
        /// <param name="setterGetter"></param>
        private void TransferFunds(Func<TransferFundsAccountAction, ITransferActionAccountSetter> setterGetter)
        {
            var detail = this.Repository.TransactionRepository.NewDetail();
            detail.CreateBackTransaction();
            detail.CreateLayTransaction();

            var action = new TransferFundsAccountAction(detail)
            {
                Destination = this.BettingAccount,
                Date = DateTime.Today
            };

            var accountSetter = setterGetter(action);

            var wallets = this.Repository.WalletRepository.GetWallets();
            var depositViewModel = new TransferFundsToAccountViewModel(action, wallets, accountSetter, this.Repository.TransactionRepository);

            Messenger.Default.Send(new TransferFundsMessage(depositViewModel));
        }

        private void Bonus()
        {
            var detail = this.Repository.TransactionRepository.NewDetail();
            detail.CreateBackTransaction();

            var action = new ApplyFundsAccountAction(detail)
            {
                Destination = this.BettingAccount,
                Date = DateTime.Today
            };

            var applyFunds = new ApplyFundsToAccountViewModel(action, this.Repository.TransactionRepository);

            Messenger.Default.Send(new ApplyFundsMessage(applyFunds));
        }

        private void Bet()
        {
            var bet = new PlaceBetViewModel(this.BettingAccount, this.Repository);

            Messenger.Default.Send(new PlaceBetMessage(bet));
        }
    }
}