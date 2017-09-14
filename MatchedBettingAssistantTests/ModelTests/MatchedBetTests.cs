using System;
using MatchedBettingAssistant.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MatchedBettingAssistantTests.ModelTests
{
    [TestClass]
    public class MatchedBetTests
    {
        private Mock<IAccount> bookmaker;
        private Mock<IAccount> exchange;
        private Mock<IBackBet> backBet;
        private Mock<ILayBet> layBet;

        private MatchedBet matchedBet;
        private const double tolerance = 0.01;

        [TestInitialize]
        public void Setup()
        {
            this.bookmaker = new Mock<IAccount>();
            this.exchange = new Mock<IAccount>();
            this.backBet = new Mock<IBackBet>();
            this.layBet = new Mock<ILayBet>();

            this.backBet.Setup(x => x.Account).Returns(this.bookmaker.Object);
            this.backBet.Setup(x => x.Odds).Returns(3.0);
            this.backBet.Setup(x => x.Stake).Returns(10);
            this.backBet.Setup(x => x.Returns).Returns(20);

            this.layBet.Setup(x => x.Account).Returns(this.exchange.Object);
            this.layBet.Setup(x => x.Odds).Returns(3.1);

            this.exchange.Setup(x => x.CommissionPercent).Returns(0.05);
            this.matchedBet = new MatchedBet(this.backBet.Object, this.layBet.Object);
        }

        [TestMethod]
        public void matched_back_bet_returns_correctly()
        {
            Assert.AreEqual(20, this.matchedBet.BackReturn);
        }

        [TestMethod]
        public void matched_lay_stake_calculates_correctly()
        {
            this.matchedBet.Calculate();
            this.layBet.VerifySet(x => x.Stake = 9.84);
        }

        [TestMethod]
        public void matched_snr_lay_stake_calculates_correctly()
        {
            this.backBet.Setup(x => x.StakeNotReturned).Returns(true);
            this.matchedBet.Calculate();
            this.layBet.VerifySet(x => x.Stake = 6.56);
        }
    }
}
