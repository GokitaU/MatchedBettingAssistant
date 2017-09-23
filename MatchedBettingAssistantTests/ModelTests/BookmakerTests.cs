using System;
using MatchedBettingAssistant.Model;
using MatchedBettingAssistant.Model.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MatchedBettingAssistantTests.ModelTests
{
    [TestClass]
    public class BookmakerTests
    {
        private Bookmaker bookmaker;

        [TestInitialize]
        public void Setup()
        {
            this.bookmaker = new Bookmaker();
        }

        [TestMethod]
        public void Name_sets_correctly()
        {
            this.bookmaker.Name = "Test";

            Assert.AreEqual("Test", bookmaker.Name);
        }

        [TestMethod]
        public void Starting_balance_sets_correctly()
        {
            this.bookmaker.StartingBalance = 100;

            Assert.AreEqual(100, this.bookmaker.StartingBalance);
        }

        [TestMethod]
        public void Starting_balance_sets_initial_balance()
        {
            this.bookmaker.StartingBalance = 100;

            Assert.AreEqual(100, this.bookmaker.Balance);
        }

        [TestMethod]
        public void Deposit_adds_to_balance()
        {
            var transaction = new Mock<ITransaction>();
            transaction.Setup(x => x.Amount).Returns(100);
            bookmaker.AddTransaction(transaction.Object);

            Assert.AreEqual(100, this.bookmaker.Balance);
        }

        [TestMethod]
        public void Deposit_adds_to_non_zero_balance()
        {
            this.bookmaker.StartingBalance = 100;

            var transaction = new Mock<ITransaction>();
            transaction.Setup(x => x.Amount).Returns(100);
            bookmaker.AddTransaction(transaction.Object);

            Assert.AreEqual(200, this.bookmaker.Balance);
        }

        [TestMethod]
        public void Withdraw_removes_from_balance()
        {
            var transaction = new Mock<ITransaction>();
            transaction.Setup(x => x.Amount).Returns(-100);
            bookmaker.AddTransaction(transaction.Object);

            Assert.AreEqual(-100, this.bookmaker.Balance);
        }

        [TestMethod]
        public void Withdraw_removes_from_non_zero_balance()
        {
            this.bookmaker.StartingBalance = 200;

            var transaction = new Mock<ITransaction>();
            transaction.Setup(x => x.Amount).Returns(-100);
            bookmaker.AddTransaction(transaction.Object);

            Assert.AreEqual(100, this.bookmaker.Balance);
        }

        [TestMethod]
        public void Editing_starting_balance_adjusts_balance_correctly()
        {
            this.bookmaker.StartingBalance = 100;

            var transaction = new Mock<ITransaction>();
            transaction.Setup(x => x.Amount).Returns(100);
            bookmaker.AddTransaction(transaction.Object);

            this.bookmaker.StartingBalance = 200;

            Assert.AreEqual(300, this.bookmaker.Balance);
        }
    }
}
