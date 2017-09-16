using System;

namespace MatchedBettingAssistant.Model
{
    public class MatchedBet
    {
        private readonly IBackBet backBet;

        private readonly ILayBet laybet;

        public MatchedBet()
        {
            this.backBet = new BackBet();
            this.laybet = new LayBet();
        }

        public MatchedBet(IBackBet backBet, ILayBet layBet)
        {
            this.backBet = backBet;
            this.laybet = layBet;
        }

        public IBettingAccount BackAccount
        {
            get => this.backBet.Account;
            set => this.backBet.Account = value;
        }

        public double BackStake
        {
            get => this.backBet.Stake;
            set => this.backBet.Stake = value;
        }

        public double BackOdds
        {
            get => this.backBet.Odds;
            set => this.backBet.Odds = value;
        }

        public double BackReturn => this.backBet.Returns;

        public bool StakeNotReturned
        {
            get => this.backBet.StakeNotReturned;
            set => this.backBet.StakeNotReturned = value;
        }

        public IBettingAccount LayAccount
        {
            get => this.laybet.Account;
            set => this.laybet.Account = value;
        }

        public double LayOdds
        {
            get => this.laybet.Odds;
            set => this.laybet.Odds = value;
        }

        public double LayCommission
        {
            get => this.laybet.Account.CommissionPercent;
            set => this.laybet.Account.CommissionPercent = value;
        }

        public double LayStake => this.laybet.Stake;

        public double LayLiability => this.laybet.Liability;

        public void Calculate()
        {
            this.laybet.Stake = CalculateLayStake();
        }

        public void Place()
        {
            this.Calculate();

            this.backBet.Place();
            this.laybet.Place();
        }

        public void Won()
        {
            this.backBet.Won();
            this.laybet.Lost();
        }

        public void Lost()
        {
            this.backBet.Lost();
            this.laybet.Won();
        }

        private double CalculateLayStake()
        {
            double result = 0.0d;

            if (this.StakeNotReturned)
            {
                result = this.BackReturn / (this.LayOdds - this.laybet.Account.CommissionPercent);
            }
            else
            {
                result = (this.BackReturn + this.BackStake) / (this.LayOdds - this.laybet.Account.CommissionPercent);
            }

            return Math.Round(result, 2);
        }
    }
}