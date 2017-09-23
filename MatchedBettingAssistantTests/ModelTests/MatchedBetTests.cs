using System;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Account;
using MatchedBettingAssistant.Model.Bets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MatchedBettingAssistantTests.ModelTests
{
    [TestClass]
    public class MatchedBetTests
    {
        private Mock<IBettingAccount> bookmaker;
        private Mock<IBettingAccount> exchange;
        private Mock<ICalculatedBackBet> backBet;
        private Mock<ICalculatedLayBet> layBet;

        private CalculatedMatchedBet calculatedMatchedBet;
        private const double tolerance = 0.01;

        [TestInitialize]
        public void Setup()
        {
            this.bookmaker = new Mock<IBettingAccount>();
            this.exchange = new Mock<IBettingAccount>();
            this.backBet = new Mock<ICalculatedBackBet>();
            this.layBet = new Mock<ICalculatedLayBet>();

            this.backBet.Setup(x => x.Account).Returns(this.bookmaker.Object);
            this.backBet.Setup(x => x.Odds).Returns(3.0);
            this.backBet.Setup(x => x.Stake).Returns(10);
            this.backBet.Setup(x => x.Returns).Returns(20);

            this.layBet.Setup(x => x.Account).Returns(this.exchange.Object);
            this.layBet.Setup(x => x.Odds).Returns(3.1);

            this.exchange.Setup(x => x.CommissionPercent).Returns(0.05);
            this.calculatedMatchedBet = new CalculatedMatchedBet(this.backBet.Object, this.layBet.Object);
        }

        [TestMethod]
        public void matched_back_bet_returns_correctly()
        {
            Assert.AreEqual(20, this.calculatedMatchedBet.BackReturn);
        }

        [TestMethod]
        public void matched_lay_stake_calculates_correctly()
        {
            this.calculatedMatchedBet.Calculate();
            this.layBet.VerifySet(x => x.Stake = 9.84);
        }

    }
}
