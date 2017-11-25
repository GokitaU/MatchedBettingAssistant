using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using DevExpress.Data.Helpers;
using DevExpress.Mvvm;
using DevExpress.Xpf.Editors.Internal;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Accounts;
using MatchedBettingAssistant.ViewModel.Messages;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public abstract class EditAccountViewModel : ViewModelBase, IAddsEntity, IRefreshable
    {
        private ITransactionAccount account;

        protected EditAccountViewModel(IRepository repository)
        {
            this.Repository = repository;

            this.RegisterMessages();
        }

        public IAccount Account
        {
            get => this.account;
            set
            {
                this.account = value as ITransactionAccount;
                RaisePropertyChanged(() => Account);
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name
        {
            get => this.account?.Name;
            set
            {
                this.account.Name = value;

                RaisePropertyChanged(()=>this.Name);

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
                RaisePropertyChanged(()=>this.StartingBalance);
                UpdateBalance();
            }
        }

        /// <summary>
        /// Gets the current balance
        /// </summary>
        public double Balance => this.account?.Balance ?? 0;

        public void Add()
        {
            New();

            Refresh();

            Messenger.Default.Send(new AccountAddedMessage(this.account));
        }

        protected abstract void New();

        protected IRepository Repository { get; }

        public virtual void Refresh()
        {
            this.RaisePropertyChanged(() => Name);
            this.RaisePropertyChanged(() => StartingBalance);
            this.RaisePropertyChanged(() => Balance);

        }

        protected void EntityPropertyChanged<T>(Expression<Func<T>> expression)
        {
            RaisePropertyChanged(expression);
            Messenger.Default.Send(new ModelSaveStatusChangedMessage());
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
        }
    }
}
