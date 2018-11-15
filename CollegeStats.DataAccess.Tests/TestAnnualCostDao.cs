using CollegeStats.DataAccess.Dao;
using CollegeStats.DataAccess.Interface;
using NUnit.Framework;
using System;

namespace CollegeStats.DataAccess.Tests
{
    [TestFixture]
    public class TestAnnualCostDao
    {
        private IAnnualCostDao _dao;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _dao = new AnnualCostDao();
        }
        [Test]
        [Category("IntegrationTest")]
        public void TestGetAll()
        {
            //Arrange

            //Act
            var result = _dao.GetAll();

            //Assert
            Assert.That(result != null);
            Assert.That(result.Count>0);
        }
        [TestCase("Johns Hopkins University")]
        [TestCase("Johnson & Wales University, Denver")]
        [Category("IntegrationTest")]
        public void TestGetAnnualCost(string collegeName)
        {
            //Arrange

            //Act
            var result = _dao.GetAnnualCost(collegeName);

            //Assert
            Assert.That(result != null);
            Assert.That(result.CollegeName.Trim().Equals(collegeName.Trim(),StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        [Category("IntegrationTest")]
        public void TestGetAnnualCost_HandlesNull()
        {
            //Arrange

            //Act
            var result = _dao.GetAnnualCost(null);

            //Assert
            Assert.That(result == null);
        }

        [Test]
        [Category("IntegrationTest")]
        public void TestGetAnnualCost_NotFound()
        {
            //Arrange

            //Act
            var result = _dao.GetAnnualCost("asdasdasd");

            //Assert
            Assert.That(result == null);
        }
    }
}
