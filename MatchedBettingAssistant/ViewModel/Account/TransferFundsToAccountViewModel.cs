using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class TransferFundsToAccountViewModel : ViewModelBase
    {
        private readonly TransferFundsAccountAction action;

        /// <summary>
        /// The item representing the source of the funds
        /// </summary>
        private AccountLookupItem targetAccount;

        private readonly ITransferActionAccountSetter accountSetter;

        public TransferFundsToAccountViewModel(TransferFundsAccountAction action, IEnumerable<IAccount> accounts, ITransferActionAccountSetter accountSetter)
        {
            this.action = action;
            this.accountSetter = accountSetter;
            this.Accounts = new ObservableCollection<AccountLookupItem>(accounts.Select(x=> new AccountLookupItem(x)));
            this.Account = this.Accounts.FirstOrDefault();
            this.Amount = 10;
        }

        public DateTime TransactionDate
        {
            get { return this.action.Date; }
            set
            {
                this.action.Date = value;
                RaisePropertyChanged(()=>TransactionDate);
            }
        }

        public string ActionDescription => this.accountSetter.ActionDescription;

        /// <summary>
        /// List of accounts from which funds can be drawn
        /// </summary>
        public ObservableCollection<AccountLookupItem> Accounts { get; }

        /// <summary>
        /// Name of the account to which funds are deposited
        /// </summary>
        public string AccountName => accountSetter.AccountName;

        /// <summary>
        /// The account from which funds are drawn
        /// </summary>
        public AccountLookupItem Account
        {
            get => targetAccount;
            set
            {
                this.targetAccount = value;

                this.accountSetter.SetAccount(value.Account);

                RaisePropertyChanged(()=>this.Account);
            }
        }

        /// <summary>
        /// The amount to be deposited
        /// </summary>
        public double Amount
        {
            get => this.action.Amount;
            set
            {
                this.action.Amount = value;
                RaisePropertyChanged(()=>Amount);
            }
        }

        /// <summary>
        /// Commits the deposit
        /// </summary>
        public void Commit()
        {
            this.action.Apply();

            Messenger.Default.Send(new TransactionsUpdatedMessage());
        }

    }
}