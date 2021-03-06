﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.ViewModel.Messages;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountTransactionListViewModel : ViewModelBase
    {
        private readonly ITransactionAccount account;

        private bool isBet;

        private AccountTransactionViewModel selectedItem;

        private DelegateCommand deleteCommand;

        public AccountTransactionListViewModel(ITransactionAccount account, bool isBet)
        {
            this.account = account;
            this.isBet = isBet;
            CreateTransactions();
            this.RegisterMessages();
        }

        public ObservableCollection<AccountTransactionViewModel> Transactions { get; private set; }

        public bool IsBet => this.isBet;

        public AccountTransactionViewModel SelectedItem
        {
            get => this.selectedItem;
            set
            {
                this.selectedItem = value;
                RaisePropertyChanged(()=>this.SelectedItem);
            }
        }

        public int DescriptionWidth => this.isBet ? 100 : 200;
        public DelegateCommand DeleteCommand => this.deleteCommand ?? (this.deleteCommand = new DelegateCommand(Delete));

        private void RegisterMessages()
        {
            Messenger.Default.Register<TransactionsUpdatedMessage>(this, RefreshTransactions);
        }

        private void RefreshTransactions(TransactionsUpdatedMessage message)
        {
            CreateTransactions();
        }

        private void CreateTransactions()
        {
            if (this.account.Transactions != null)
            {
                var transactions = this.account.Transactions.Select(x => new AccountTransactionViewModel(x));
                this.Transactions = new ObservableCollection<AccountTransactionViewModel>(transactions);
                this.SelectedItem = this.Transactions.FirstOrDefault();
                RaisePropertyChanged(()=>Transactions);
            }
        }

        private void Delete()
        {
            if (this.SelectedItem != null)
            {
                
            }
        }
    }
}