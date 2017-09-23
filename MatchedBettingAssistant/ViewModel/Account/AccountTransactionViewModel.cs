﻿using System;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Account;

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
    }
}