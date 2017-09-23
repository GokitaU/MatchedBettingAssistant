using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Account;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class TransferFundsToAccountViewModel : ViewModelBase
    {
        private readonly TransferFundsAccountAction action;

        /// <summary>
        /// The item representing the source of the funds
        /// </summary>
        private AccountLookupItem wallet;

        private readonly ITransferActionWalletSetter walletSetter;

        public TransferFundsToAccountViewModel(TransferFundsAccountAction action, IEnumerable<IAccount> wallets, ITransferActionWalletSetter walletSetter)
        {
            this.action = action;
            this.walletSetter = walletSetter;
            this.Wallets = new ObservableCollection<AccountLookupItem>(wallets.Select(x=> new AccountLookupItem(x)));
            this.Wallet = this.Wallets.FirstOrDefault();
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

        public string ActionDescription => this.walletSetter.ActionDescription;

        /// <summary>
        /// List of wallets from which funds can be drawn
        /// </summary>
        public ObservableCollection<AccountLookupItem> Wallets { get; }

        /// <summary>
        /// Name of the account to which funds are deposited
        /// </summary>
        public string AccountName => walletSetter.AccountName;

        /// <summary>
        /// The wallet from which funds are drawn
        /// </summary>
        public AccountLookupItem Wallet
        {
            get => wallet;
            set
            {
                this.wallet = value;

                this.walletSetter.SetWallet(value.Account);

                RaisePropertyChanged(()=>this.Wallet);
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