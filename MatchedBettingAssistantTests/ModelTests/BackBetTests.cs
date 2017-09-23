using System;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Account;
using MatchedBettingAssistant.Model.Bets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MatchedBettingAssistantTests.ModelTests
{
    [TestClass]
    public class BackBetTests
    {
        private Mock<IBettingAccount> bookmaker;
        private CalculatedBackBet calculatedBackBet;
        private const double Tolerance = 0.01;

        [TestInitialize]
        public void Setup()
        {
            this.bookmaker = new Mock<IBettingAccount>();
            this.calculatedBackBet = new CalculatedBackBet
            {
                Account = this.bookmaker.Object,
                Stake = 10,
                Odds = 2.0
            };
        }

        [TestMethod]
        public void placing_bet_affects_account_funds()
        {
            this.calculatedBackBet.Place();

            this.bookmaker.Verify(x=>x.AddTransaction(It.Is<ITransaction>(t => Math.Abs(t.Amount - (-10.0)) < Tolerance)));
        }

        [TestMethod]
        public void bet_won_adds_winnings_to_account()
        {
            this.calculatedBackBet.Won();

            this.bookmaker.Verify(x => x.AddTransaction(It.Is<ITransaction>(t => Math.Abs(t.Amount - (20.0)) < Tolerance)));
        }

        [TestMethod]
        public void bet_won_affected_by_commission()
        {
            this.bookmaker.Setup(x => x.CommissionPercent).Returns(0.05);

            this.calculatedBackBet.Won();

            this.bookmaker.Verify(x => x.AddTransaction(It.Is<ITransaction>(t => Math.Abs(t.Amount - (19.50)) < Tolerance)));

        }


    }
}
