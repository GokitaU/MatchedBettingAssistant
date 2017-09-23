﻿using System;

namespace MatchedBettingAssistant.Model.Account
{
    public class FundsTransaction : ITransaction
    {
        public DateTime TransactionDate { get; set; }

        public double Amount { get; set; }
        public string Description { get; set; }
    }
}