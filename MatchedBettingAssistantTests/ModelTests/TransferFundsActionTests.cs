using System;
using MatchedBettingAssistant.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MatchedBettingAssistantTests.ModelTests
{
    [TestClass]
    public class TransferFundsActionTests
    {
        private Mock<IAccount> source;
        private Mock<IAccount> destination;
        private const double tolerance = 0.001;

        [TestInitialize]
        public void Setup()
        {
            this.source = new Mock<IAccount>();
            this.destination = new Mock<IAccount>();
        }

        [TestMethod]
        public void Transfer_removes_funds_from_source()
        {
            var withdrawal = new TransferFundsAccountAction
            {
                Source = source.Object,
                Destination = destination.Object,
                Amount = 100,
                Date = DateTime.Now
            };
            withdrawal.Apply();

            this.source.Verify(x=>x.AddTransaction(It.Is<ITransaction>(t => Math.Abs(t.Amount - (-100.0)) < tolerance)));
        }

        [TestMethod]
        public void Transfer_adds_funds_to_destination()
        {
            var withdrawal = new TransferFundsAccountAction
            {
                Source = source.Object,
                Destination = destination.Object,
                Amount = 100,
                Date = DateTime.Now
            };
            withdrawal.Apply();

            this.destination.Verify(x => x.AddTransaction(It.Is<ITransaction>(t => Math.Abs(t.Amount - 100.0) < tolerance)));
        }
    }
}
