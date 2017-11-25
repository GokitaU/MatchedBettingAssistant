using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using DevExpress.Mvvm;
using MatchedBettingAssistant.ViewModel.Messages;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountViewModel : ViewModelBase, IAddsEntity
    {
        private EditAccountViewModel editViewModel;
        private ViewModelBase transactionViewModel;

        public AccountViewModel()
        {
             RegisterMessages();
           
        }

        public AccountViewModel(EditAccountViewModel account, ViewModelBase transaction) : this()
        {
            this.EditViewModel = account;
            this.TransactionViewModel = transaction;
        }

        public IDialogService TransferDialogService => ServiceContainer.GetService<IDialogService>("TransferDialog");
        public IDialogService ApplyDialogService => ServiceContainer.GetService<IDialogService>("ApplyDialog");
        public IDialogService BetDialogService => ServiceContainer.GetService<IDialogService>("BetDialog");

        public EditAccountViewModel EditViewModel
        {
            get => editViewModel;
            set
            {
                editViewModel = value;
                RaisePropertyChanged(()=>EditViewModel);
            }
        }

        public ViewModelBase TransactionViewModel
        {
            get => transactionViewModel;
            set
            {
                transactionViewModel = value;
                RaisePropertyChanged(()=>TransactionViewModel);
            }
        }

        public void Add()
        {
            if (this.EditViewModel is IAddsEntity addable)
            {
                addable.Add();
            }
        }

        public void Unregister()
        {
            Messenger.Default.Unregister<SelectedAccountChangedMessage>(this, AccountChanged);
            Messenger.Default.Unregister<TransferFundsMessage>(this, TransferFunds);
            Messenger.Default.Unregister<PlaceBetMessage>(this, PlaceBet);
            Messenger.Default.Unregister<ApplyFundsMessage>(this, ApplyFunds);
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<SelectedAccountChangedMessage>(this, AccountChanged);
            Messenger.Default.Register<TransferFundsMessage>(this, TransferFunds);
            Messenger.Default.Register<PlaceBetMessage>(this, PlaceBet);
            Messenger.Default.Register<ApplyFundsMessage>(this, ApplyFunds);
        }

        private void AccountChanged(SelectedAccountChangedMessage msg)
        {
            if (this.EditViewModel != null)
            {
                this.EditViewModel.Account = msg.Account;
            }
        }

        private void TransferFunds(TransferFundsMessage msg)
        {
            var depositViewModel = msg.ViewModel;

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

            TransferDialogService.ShowDialog(
                new List<UICommand>() { okCommand, cancelCommand },
                depositViewModel.ActionDescription,
                depositViewModel);
        }

        private void PlaceBet(PlaceBetMessage message)
        {
            var bet = message.ViewModel;

            var okCommand = new UICommand()
            {
                Caption = "Ok",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand<CancelEventArgs>(x => bet.Commit(), x => bet.Returns > 0)
            };

            var cancelCommand = new UICommand()
            {
                Id = MessageBoxResult.Cancel,
                Caption = "Cancel",
                IsCancel = true,
                IsDefault = false
            };

            BetDialogService.ShowDialog(
                new List<UICommand>() { okCommand, cancelCommand },
                "Basic Bet",
                bet);
        }

        private void ApplyFunds(ApplyFundsMessage message)
        {
            var applyFunds = message.ViewModel;

            var okCommand = new UICommand()
            {
                Caption = "Ok",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand(applyFunds.Commit)
            };

            var cancelCommand = new UICommand()
            {
                Id = MessageBoxResult.Cancel,
                Caption = "Cancel",
                IsCancel = true,
                IsDefault = false
            };

            ApplyDialogService.ShowDialog(
                new List<UICommand>() { okCommand, cancelCommand },
                "Adjust Funds",
                applyFunds);
        }


    }
}