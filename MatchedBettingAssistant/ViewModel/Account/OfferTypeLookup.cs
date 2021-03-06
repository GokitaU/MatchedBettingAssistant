﻿using MatchedBettingAssistant.Core;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class OfferTypeLookup : Lookup<IOfferType>
    {
        public OfferTypeLookup(IOfferType offerType) : base(offerType)
        {
        }
    }
}