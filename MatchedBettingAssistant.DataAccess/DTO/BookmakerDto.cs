﻿using System.Collections.Generic;
using System.Linq;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.DataAccess.DataModel;

namespace MatchedBettingAssistant.DataAccess.DTO
{ 

    public class BookmakerDto : TransactionAccountDto, IBettingAccount
    {
        private readonly DataModel.Bookmaker bookmaker;


        public BookmakerDto(Bookmaker bookmaker) : base(bookmaker)
        {
            this.bookmaker = bookmaker;

        }

        public double CommissionPercent
        {
            get => this.bookmaker.CommissionPercent;
            set => this.bookmaker.CommissionPercent = value;
        }

        public bool IsExchange
        {
            get => this.bookmaker.IsExchange;
            set => this.bookmaker.IsExchange = value;
        }

        public double PaybackPercent
        {
            get => this.bookmaker.PayBackPercent;
            set => this.bookmaker.PayBackPercent = value;
        }

        public bool LimitedAccount
        {
            get => this.bookmaker.LimitedAccount;
            set => this.bookmaker.LimitedAccount = value;
        }

        public bool CompletedNewAccountOffer
        {
            get => this.bookmaker.CompletedNewAccountOffer;
            set => this.bookmaker.CompletedNewAccountOffer = value;
        }

        public double Profit => this.bookmaker.Profit;

        public double AccountProfit => this.bookmaker.AccountProfit;

        public double PaybackDue => this.bookmaker.PaybackDue;
    }
}