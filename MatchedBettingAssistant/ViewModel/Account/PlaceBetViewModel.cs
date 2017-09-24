using System;
using System.Collections.Generic;
using DevExpress.Mvvm;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Bets;

namespace MatchedBettingAssistant.ViewModel.Account
{
    public class PlaceBetViewModel : ViewModelBase
    {
        private IBettingAccount account;
        private BetViewModel currentBet;

        public PlaceBetViewModel(IBettingAccount account)
        {
            this.account = account;

            CreateSimpleMatchedBet();
        }

        /// <summary>
        /// 
        /// </summary>
        public BetViewModel CurrentBet
        {
            get => this.currentBet;
            set
            {
                this.currentBet = value;
                RaisePropertyChanged(()=>this.CurrentBet);
            }
        }

        public double Returns
        {
            get { return 1; }
        }

        public void Commit()
        {
            this.CurrentBet.Commit(); 
        }

        private void CreateSimpleBackBet()
        {
            var basicBet = new SimpleBet()
            {
                Account = account,
                Date = DateTime.Today
            };
            this.CurrentBet = new BasicBetViewModel(basicBet);
        }

        private void CreateSimpleMatchedBet()
        {
            var simpleMatchedBet = new SimpleMatchedBet()
            {
                BackAccount = this.account,
                Date = DateTime.Today
            };

            var bookies = new List<IBettingAccount>() {this.account};
            this.CurrentBet = new SimpleMatchedBetViewModel(simpleMatchedBet, bookies, null);
        }
    }
}