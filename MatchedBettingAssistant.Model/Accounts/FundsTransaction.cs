﻿using System;
using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.Model.Accounts
{
    public class FundsTransaction : ITransaction
    {
        public DateTime TransactionDate { get; set; }

        public double Amount { get; set; }
        public string Description { get; set; }
    }
}