using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Model;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class DepositFundsToAccountViewModel : ViewModelBase
    {
        private readonly TransferFundsAccountAction action;

        /// <summary>
        /// The item representing the source of the funds
        /// </summary>
        private AccountLookupItem source;

        public DepositFundsToAccountViewModel(TransferFundsAccountAction action, IEnumerable<IAccount> wallets)
        {
            this.action = action;
            this.Wallets = new ObservableCollection<AccountLookupItem>(wallets.Select(x=> new AccountLookupItem(x)));
        }

        /// <summary>
        /// List of wallets from which funds can be drawn
        /// </summary>
        public ObservableCollection<AccountLookupItem> Wallets { get; }

        /// <summary>
        /// Name of the account to which funds are deposited
        /// </summary>
        public string AccountName => action.Destination?.Name;

        /// <summary>
        /// The wallet from which funds are drawn
        /// </summary>
        public AccountLookupItem Wallet
        {
            get => source;
            set
            {
                this.source = value;
                this.action.Source = this.source.Account;
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