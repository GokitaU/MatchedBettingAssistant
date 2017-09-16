using System;
using MatchedBettingAssistant.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MatchedBettingAssistantTests.ModelTests
{
    [TestClass]
    public class BackBetTests
    {
        private Mock<IBettingAccount> bookmaker;
        private BackBet backBet;
        private const double Tolerance = 0.01;

        [TestInitialize]
        public void Setup()
        {
            this.bookmaker = new Mock<IBettingAccount>();
            this.backBet = new BackBet
            {
                Account = this.bookmaker.Object,
                Stake = 10,
                Odds = 2.0
            };
        }

        [TestMethod]
        public void placing_bet_affects_account_funds()
        {
            this.backBet.Place();

            this.bookmaker.Verify(x=>x.AddTransaction(It.Is<ITransaction>(t => Math.Abs(t.Amount - (-10.0)) < Tolerance)));
        }

        [TestMethod]
        public void bet_won_adds_winnings_to_account()
        {
            this.backBet.Won();

            this.bookmaker.Verify(x => x.AddTransaction(It.Is<ITransaction>(t => Math.Abs(t.Amount - (20.0)) < Tolerance)));
        }

        [TestMethod]
        public void bet_won_affected_by_commission()
        {
            this.bookmaker.Setup(x => x.CommissionPercent).Returns(0.05);

            this.backBet.Won();

            this.bookmaker.Verify(x => x.AddTransaction(It.Is<ITransaction>(t => Math.Abs(t.Amount - (19.50)) < Tolerance)));

        }

        [TestMethod]
        public void bet_won_but_stake_not_returned()
        {
            this.backBet.StakeNotReturned = true;

            this.backBet.Won();

            this.bookmaker.Verify(x => x.AddTransaction(It.Is<ITransaction>(t => Math.Abs(t.Amount - (10.0)) < Tolerance)));

        }

        [TestMethod]
        public void bonus_bet_does_not_affect_funds_on_place()
        {
            this.backBet.IsBonus = true;

            this.backBet.Place();

            this.bookmaker.Verify(x => x.AddTransaction(It.IsAny<ITransaction>()), Times.Never);
        }
    }
}
