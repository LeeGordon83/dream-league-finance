﻿using LG.DLFinance.DAL;
using LG.DLFinance.Models;
using LG.DLFinance.SL;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LG.DLFinance.Tests.SL
{
    [TestFixture]
    [Category("Transaction Service")]
    class GeneralServiceTests
    {
        Mock<DLFinanceContext> context;
        Mock<DbSet<Manager>> manager;
        Mock<DbSet<Fees>> fee;
        Mock<DbSet<Prizes>> prize;
        Mock<DbSet<Week>> week;
        Mock<DbSet<Jackpot>> jackpot;
        Mock<DbSet<Transaction>> transaction;
        List<Manager> managerData;
        List<Emails> emailsData;
        List<Fees> feesData;
        List<Prizes> prizesData;
        List<Week> weeksData;
        List<Jackpot> jackpotsData;
        List<Transaction> transactionsData;
        GeneralService generalService;

        [SetUp]

        public void Setup()
        {


            var email1 = new Emails { EmailsId = Guid.Parse("1843FA04-B9C5-414E-9C63-293C0B5563A5"), EmailAddress = "test@test.co.uk", Primary = true };
            var email2 = new Emails { EmailsId = Guid.Parse("1843FA04-B9C5-414E-9C63-293C0B5563A8"), EmailAddress = "test@test.co.uk", Primary = false };

            emailsData = new List<Emails>
            {
                email1, email2
            };

            var manager1 = new Manager { ManagerId = Guid.Parse("1843FA04-B9C5-414E-9C63-293C0B5563A2"), Active = true, Balance = 5, Email = emailsData, ManagerName = "Tommy" };
            var manager2 = new Manager { ManagerId = Guid.Parse("1843FA04-B9C5-414E-9C63-293C0B5563B2"), Active = true, Balance = 60, Email = emailsData, ManagerName = "Lee" };
            var manager3 = new Manager { ManagerId = Guid.Parse("88FBED22-6E8A-4FC1-8174-13DAAC74FAEA"), Active = true, Balance = 0, Email = emailsData, ManagerName = "Scott" };

            managerData = new List<Manager>
            {
                manager1, manager2, manager3
            };

            var fee1 = new Fees { FeesId = Guid.Parse("5A145DC5-3009-444C-8BEF-71116181A91B"), FeeAmount = 10, FeeType = "test" };
            var fee2 = new Fees { FeesId = Guid.Parse("5A145DC5-3009-444C-8BEF-71116181A91C"), FeeAmount = 10, FeeType = "test1" };

            feesData = new List<Fees>
            {
                fee1, fee2
            };

            var prize1 = new Prizes { PrizesId = Guid.Parse("F10D915F-6F08-4743-94A6-D693BA9DEFA1"), CupPrize = false, LeaguePrize = true, PrizeAmount = 50, PrizeType = "Jackpot" };
            var prize2 = new Prizes { PrizesId = Guid.Parse("F10D915F-6F08-4743-94A6-D693BA9DEFA2"), CupPrize = false, LeaguePrize = false, PrizeAmount = 6, PrizeType = "Weekly" };
            var prize3 = new Prizes { PrizesId = Guid.Parse("F10D915F-6F08-4743-94A6-D693BA9DEFA3"), CupPrize = true, LeaguePrize = false, PrizeAmount = 20, PrizeType = "Cup Winner" };
            var prize4 = new Prizes { PrizesId = Guid.Parse("F10D915F-6F08-4743-94A6-D693BA9DEFA4"), CupPrize = false, LeaguePrize = false, PrizeAmount = 5, PrizeType = "Fiver" };

            prizesData = new List<Prizes>
            {
                prize1, prize2, prize3, prize4
            };

            var week1 = new Week { WeekNo = 1, WeekStartDate = DateTime.UtcNow, WeekCompleted = true };
            var week2 = new Week { WeekNo = 2, WeekStartDate = DateTime.UtcNow.AddDays(7), WeekCompleted = false };

            weeksData = new List<Week>
            {
                week1, week2
            };

            var jackpot1 = new Jackpot { JackpotId = 1, Active = true, JackpotStartWk = 1, JackpotEndWk = 2, JackpotValue = 4 };

            jackpotsData = new List<Jackpot>
            {
                jackpot1
            };

            var transaction2 = new Transaction { TransactionId = Guid.Parse("7193B211-B330-4E94-8A44-25FBB5FF35DA"), Manager = manager1, ManagerId = Guid.Parse("1843FA04-B9C5-414E-9C63-293C0B5563A2"), TransactionDate = DateTime.UtcNow, TransactionType = "Weekly", Value = 6, Week = week1, WeekId = 1 };
            var transaction1 = new Transaction { TransactionId = Guid.Parse("7193B211-B330-4E94-8A44-25FBB5FF35DB"), Manager = manager1, ManagerId = Guid.Parse("1843FA04-B9C5-414E-9C63-293C0B5563A2"), TransactionDate = DateTime.UtcNow, TransactionType = "Fiver", Value = 5, Week = week2, WeekId = 2 };


            transactionsData = new List<Transaction>
            {
                transaction2, transaction1
            };

            //Create mocks for teams repository and setup mocked behaviour to mimic EF using test data above


            manager = new Mock<DbSet<Manager>>().SetupData(managerData);
            fee = new Mock<DbSet<Fees>>().SetupData(feesData);
            prize = new Mock<DbSet<Prizes>>().SetupData(prizesData);
            context = new Mock<DLFinanceContext>();
            week = new Mock<DbSet<Week>>().SetupData(weeksData);
            jackpot = new Mock<DbSet<Jackpot>>().SetupData(jackpotsData);
            transaction = new Mock<DbSet<Transaction>>().SetupData(transactionsData);
            context.Setup(x => x.Managers).Returns(manager.Object);
            context.Setup(x => x.Fees).Returns(fee.Object);
            context.Setup(x => x.Prizes).Returns(prize.Object);
            context.Setup(x => x.Week).Returns(week.Object);
            context.Setup(x => x.Jackpot).Returns(jackpot.Object);
            context.Setup(x => x.Transaction).Returns(transaction.Object);
            generalService = new GeneralService(context.Object);

        }

        [Test]
        public void testWeek_returns_last_open_week()
        {
            //Arrange
            //Already Done in Setup

            //Act
            var result = generalService.GetWeek(false);

            //Assert
            Assert.AreEqual(2, result.WeekNo);
        }

        [Test]
        public void testWeek_returns_closed_week()
        {
            //Arrange
            //Already Done in Setup

            //Act
            var result = generalService.GetWeek(true);

            //Assert
            Assert.AreEqual(1, result.WeekNo);
        }

        [Test]
        public void testMonth_returns_MonthList()
        {
            //Arrange
            //Already Done in Setup

            //Act
            var result = generalService.GetMonths();

            //Assert
            Assert.AreEqual(1, result.Count);
        }
    }
}
