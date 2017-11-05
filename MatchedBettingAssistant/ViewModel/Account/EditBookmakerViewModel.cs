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

    public class EditBookmakerViewModel : ViewModelBase, IAddsEntity, IRefreshable
    {
        private IBettingAccount account;
        private readonly IRepository repository;

        public EditBookmakerViewModel(IRepository repository)
        {
            this.repository = repository;
            this.RegisterMessages();
            this.ActionButtons = new BookmakerButtonsViewModel(this.account, this.repository);
        }

        public IAccount Account
        {
            get => this.account;
            set
            {
                this.account = value as IBettingAccount;
                RaisePropertyChanged(()=>Account);
            }
        }

        public string Name
        {
            get => this.account?.Name;
            set
            {
                this.account.Name = value;
                RaisePropertyChanged(() => this.Name);

                Messenger.Default.Send(new AccountNameChangedMessage(this.account));
            }
        }

        /// <summary>
        /// Gets or sets the starting balance
        /// </summary>
        public double StartingBalance
        {
            get => this.account?.StartingBalance ?? 0;
            set
            {
                this.account.StartingBalance = value;
                EntityPropertyChanged(() => this.StartingBalance);
                UpdateBalance();
            }
        }

        public bool IsExchange
        {
            get => this.account?.IsExchange ?? false;
            set
            {
                this.account.IsExchange = value;

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
            get => this.account?.CommissionPercent ?? 0;
            set
            {
                this.account.CommissionPercent = value;
                EntityPropertyChanged(()=>this.CommissionPercent);
            }
        }

        public double PaybackPercent
        {
            get => this.account?.PaybackPercent ?? 0;
            set
            {
                this.account.PaybackPercent = value;
                EntityPropertyChanged(()=>this.PaybackPercent);
                UpdateBalance();
            }
        }

        public bool LimitedAccount
        {
            get => this.account?.LimitedAccount ?? false;
            set
            {
                if (this.account.LimitedAccount == value)
                    return;

                this.account.LimitedAccount = value;

                RaisePropertyChanged(() => this.LimitedAccount);
            }
        }

        public bool CompletedNewAccountOffer
        {
            get => this.account?.CompletedNewAccountOffer ?? false;
            set
            {
                if (this.account.CompletedNewAccountOffer == value)
                    return;

                this.account.CompletedNewAccountOffer = value;

                RaisePropertyChanged(() => this.CompletedNewAccountOffer);
            }
        }

        public double Balance => this.account?.Balance ?? 0;

        public double AccountProfit => this.account?.AccountProfit ?? 0;

        public double Profit => this.account?.Profit ?? 0;

        public double PaybackDue => this.account?.PaybackDue ?? 0;

        public BookmakerButtonsViewModel ActionButtons { get; }

        public Visibility PaybackPercentVisibility
        {
            get => this.IsExchange ? Visibility.Hidden : Visibility.Visible;
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

        private void EntityPropertyChanged<T>(Expression<Func<T>> expression)
        {
            RaisePropertyChanged(expression);
            Messenger.Default.Send(new ModelSaveStatusChangedMessage());
        }

        public void Add()
        {
            this.Account = this.repository.BookmakerRepository.New();
            this.Name = "New Bookmaker";
            Refresh();

            Messenger.Default.Send(new AccountAddedMessage(this.account));
        }

        public void Refresh()
        {
            this.RaisePropertyChanged(() => Name);
            this.RaisePropertyChanged(() => IsExchange);
            this.RaisePropertyChanged(() => StartingBalance);
            this.RaisePropertyChanged(() => CommissionPercent);
            this.RaisePropertyChanged(() => LimitedAccount);
            this.RaisePropertyChanged(() => CompletedNewAccountOffer);
            this.RaisePropertyChanged(() => PaybackPercent);
            this.RaisePropertyChanged(() => Balance);
            this.RaisePropertyChanged(() => AccountProfit);
            this.RaisePropertyChanged(() => Profit);
            this.RaisePropertyChanged(() => PaybackDue);

        }
    }
}