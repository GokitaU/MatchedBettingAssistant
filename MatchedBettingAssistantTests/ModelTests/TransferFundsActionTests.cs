using System;
using MatchedBettingAssistant.Core;
using MatchedBettingAssistant.Model.Accounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MatchedBettingAssistant.Model.Tests.ModelTests
{
    [TestClass]
    public class TransferFundsActionTests
    {
        private Mock<IAccount> source;
        private Mock<IAccount> destination;
        private Mock<ITransactionRepository> repository;

        private const double tolerance = 0.001;

        [TestInitialize]
        public void Setup()
        {
            this.source = new Mock<IAccount>();
            this.destination = new Mock<IAccount>();
            this.repository = new Mock<ITransactionRepository>();
            this.repository.Setup(x => x.New()).Returns(new FundsTransaction());
        }

        [TestMethod]
        public void Transfer_removes_funds_from_source()
        {
            var withdrawal = new TransferFundsAccountAction(repository.Object)
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
            var withdrawal = new TransferFundsAccountAction(repository.Object)
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
