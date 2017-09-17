using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using DevExpress.Mvvm;
using DevExpress.Xpf.Editors.Internal;
using MatchedBettingAssistant.Model;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class EditAccountViewModel : ViewModelBase
    {
        private readonly IAccount account;

        public IDialogService DepositDialogService {  get { return GetService<IDialogService>(); } }

        private DelegateCommand depositCommand;
        private DelegateCommand withdrawCommand;

        public EditAccountViewModel(IAccount account)
        {
            this.account = account;
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

        /// <summary>
        /// Gets the command for depositing funds to account
        /// </summary>
        public DelegateCommand DepositCommand => this.depositCommand ?? (this.depositCommand = new DelegateCommand(Deposit));

        /// <summary>
        /// Gets the command for withdrawing funds from account
        /// </summary>
        public DelegateCommand WithdrawCommand => this.withdrawCommand ?? (this.withdrawCommand = new DelegateCommand(Withdraw));

        /// <summary>
        /// Initiates the deposit action
        /// </summary>
        public void Deposit()
        {
            var action = new TransferFundsAccountAction()
            {
                Destination = this.account
            };

            var walletSetter = new DepositActionWalletSetter(action);

            TransferFunds(action, walletSetter);
        }

        /// <summary>
        /// Initiates the withdraw action
        /// </summary>
        public void Withdraw()
        {
            var action = new TransferFundsAccountAction()
            {
                Source = this.account
            };

            var walletSetter = new WithdrawActionWalletSetter(action);

            TransferFunds(action, walletSetter);
        }

        /// <summary>
        /// Creates dialog for transferring funds to or from a wallet
        /// </summary>
        /// <param name="action"></param>
        /// <param name="walletSetter"></param>
        private void TransferFunds(TransferFundsAccountAction action, ITransferActionWalletSetter walletSetter)
        {
            var wallets = new List<Wallet>()
            {
                new Wallet() {Name = "Skrill", StartingBalance = 100.00},
                new Wallet() {Name = "Credit Card", StartingBalance = 600.0}
            };

            var depositViewModel = new TransferFundsToAccountViewModel(action, wallets, walletSetter);

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

            var result = DepositDialogService.ShowDialog(
                dialogCommands: new List<UICommand>() { okCommand, cancelCommand },
                title: walletSetter.ActionDescription,
                viewModel: depositViewModel
            );

            if (result == okCommand)
            {
                RaisePropertyChanged(() => Balance);
            }
        }

    }
}
