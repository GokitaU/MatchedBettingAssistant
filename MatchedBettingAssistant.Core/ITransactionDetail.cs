﻿namespace MatchedBettingAssistant.Core
{
    public interface ITransactionDetail
    {
        double Profit { get; set; }

        IBetType BetType { get; set; }

        IOfferType OfferType { get; set; }
    }
}