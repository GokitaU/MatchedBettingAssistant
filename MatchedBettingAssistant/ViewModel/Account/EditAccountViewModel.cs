using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using DevExpress.Mvvm;
using DevExpress.Xpf.Editors.Internal;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class EditAccountViewModel : ViewModelBase
    {
        private readonly ITransactionAccount account;
        private IRepository repository;

        public EditAccountViewModel(ITransactionAccount account, IRepository repository)
        {
            this.account = account;
            this.repository = repository;
            this.ActionButtons = new WalletButtonsViewModel(this.account, this.repository);

            this.RegisterMessages();
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name
        {
            get => this.account.Name;
            set
            {
                this.account.Name = value;
                RaisePropertyChanged(()=>this.Name);
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
                RaisePropertyChanged(()=>this.StartingBalance);
            }
        }

        /// <summary>
        /// Gets the current balance
        /// </summary>
        public double Balance => this.account.Balance;

        public WalletButtonsViewModel ActionButtons { get; }

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
