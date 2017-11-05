using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using DevExpress.Mvvm;
using MatchedBettingAssistant.ViewModel.Messages;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountViewModel : ViewModelBase, IAddsEntity
    {
        private ViewModelBase editViewModel;
        private ViewModelBase transactionViewModel;

        public AccountViewModel()
        {
             RegisterMessages();
           
        }

        public AccountViewModel(ViewModelBase account, ViewModelBase transaction) : this()
        {
            this.EditViewModel = account;
            this.TransactionViewModel = transaction;
        }

        public IDialogService TransferDialogService => ServiceContainer.GetService<IDialogService>("TransferDialog");
        public IDialogService ApplyDialogService => ServiceContainer.GetService<IDialogService>("ApplyDialog");
        public IDialogService BetDialogService => ServiceContainer.GetService<IDialogService>("BetDialog");

        public ViewModelBase EditViewModel
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

        private void RegisterMessages()
        {
            Messenger.Default.Register<TransferFundsMessage>(this, TransferFunds);
            Messenger.Default.Register<PlaceBetMessage>(this, PlaceBet);
            Messenger.Default.Register<ApplyFundsMessage>(this, ApplyFunds);
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