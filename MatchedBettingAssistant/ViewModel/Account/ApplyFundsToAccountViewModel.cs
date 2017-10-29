using System;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Core.Repositories;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Accounts;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class ApplyFundsToAccountViewModel : ViewModelBase
    {
        private readonly ITransactionRepository repository;

        private readonly ApplyFundsAccountAction action;

        public ApplyFundsToAccountViewModel(ApplyFundsAccountAction action, ITransactionRepository repository)
        {
            this.action = action;
            this.repository = repository;
        }

        public DateTime TransactionDate
        {
            get => this.action.Date;
            set
            {
                this.action.Date = value;
                RaisePropertyChanged(() => TransactionDate);
            }
        }

        public string AccountName => action.Destination?.Name;

        public string Description
        {
            get => action.Description;
            set
            {
                this.action.Description = value;
                RaisePropertyChanged(()=>Description);
            }
        }

        /// <summary>
        /// The amount to be added
        /// </summary>
        public double Amount
        {
            get => this.action.Amount;
            set
            {
                this.action.Amount = value;
                RaisePropertyChanged(() => Amount);
            }
        }

        public void Commit()
        {
            this.action.Apply();
            this.repository.AddDetail(this.action.Detail);

            Messenger.Default.Send(new TransactionsUpdatedMessage());
        }

    }
}