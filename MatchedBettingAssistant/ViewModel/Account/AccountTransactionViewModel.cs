using System;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class AccountTransactionViewModel : ViewModelBase
    {
        private readonly ITransaction transaction;

        public AccountTransactionViewModel(ITransaction transaction)
        {
            this.transaction = transaction;
        }

        public DateTime TransactionDate => transaction.TransactionDate;

        public double Amount => transaction.Amount;

        public string Description => transaction.Description;

        public string BetType => transaction.Detail?.BetType;

        public string OfferType => transaction.Detail?.OfferType;

        public double Profit => transaction.Detail?.Profit ?? 0;
    }
}