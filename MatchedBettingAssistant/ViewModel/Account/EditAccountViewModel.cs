using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Model;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class EditAccountViewModel : ViewModelBase
    {
        private readonly IAccount account;

        public IDialogService DepositDialogService {  get { return GetService<IDialogService>(); } }

        private DelegateCommand depositCommand;

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

        public double Balance => this.account.Balance;

        public DelegateCommand DepositCommand
        {
            get { return this.depositCommand ?? (this.depositCommand = new DelegateCommand(Deposit)); }
        }


        /// <summary>
        /// Initiates the deposit action
        /// </summary>
        public void Deposit()
        {
            var wallets = new List<Wallet>()
            {
                new Wallet() {Name = "Skrill", StartingBalance = 100.00},
                new Wallet() {Name = "Credit Card", StartingBalance = 600.0}
            };

            var action = new TransferFundsAccountAction()
            {
                Destination = this.account
            };


            var depositViewModel = new DepositFundsToAccountViewModel(action, wallets);

            UICommand okCommand = new UICommand()
            {
                Caption = "Ok",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand(depositViewModel.Commit)
            };

            UICommand cancelCommand = new UICommand()
            {
                Id = MessageBoxResult.Cancel,
                Caption = "Cancel",
                IsCancel = true,
                IsDefault = false
            };

            var result = DepositDialogService.ShowDialog(
                dialogCommands: new List<UICommand>() {okCommand, cancelCommand},
                title: "Deposit",
                viewModel: depositViewModel
            );

            if (result == okCommand)
            {
                RaisePropertyChanged(()=>Balance);
            }
        }

        /// <summary>
        /// Initiates the withdraw action
        /// </summary>
        public void Withdraw()
        {
            
        }
    }
}
