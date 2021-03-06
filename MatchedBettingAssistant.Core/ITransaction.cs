﻿using System;

namespace MatchedBettingAssistant.Core
{
    public interface ITransaction
    {
        DateTime TransactionDate { get; set; }

        double Amount { get; set; }

        string Description { get; set; }

        ITransactionDetailDisplay Detail { get; }

        /// <summary>
        /// Transaction is a bet and affects bookmaker profit/loss
        /// </summary>
        bool IncludeInProfit { get; set; }
    }
}