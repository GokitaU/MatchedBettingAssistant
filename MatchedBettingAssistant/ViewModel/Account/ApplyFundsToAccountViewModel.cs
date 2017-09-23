using System;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Account;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class ApplyFundsToAccountViewModel : ViewModelBase
    {
        private readonly ApplyFundsAccountAction action;

        public ApplyFundsToAccountViewModel(ApplyFundsAccountAction action)
        {
            this.action = action;
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

            Messenger.Default.Send(new TransactionsUpdatedMessage());
        }


    }
}