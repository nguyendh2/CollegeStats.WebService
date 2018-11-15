using System;
using Autofac;
using CollegeStats.BusinessLogic.Interface;
using NUnit.Framework;

namespace CollegeStats.BusinessLogic.Tests
{
    [TestFixture]
    public class TestAnnualCostService
    {
        private IAnnualCostService _service;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            WebService.AutoFacMapper.BeginLifeTimeScope();
            _service = WebService.AutoFacMapper.LifeTimeScope.Resolve<IAnnualCostService>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            WebService.AutoFacMapper.EndLifeTimeScope();
        }

        [TestCase("Johns Hopkins University", true, false, 70660)]
        [TestCase("Johnson & Wales University, Denver", true, false, 45420)]
        [TestCase("This should not be found", true, false, -1)]
        [TestCase(null, true, false, -1)]
        [Category("IntegrationTest")]
        public void TestGetAnnualCost(string collegeName, bool includeRoom, bool isOutOfState, double expectedResult)
        {
            //Arrange

            //Act
            var actualResult = _service.GetAnnualCost(collegeName, includeRoom, isOutOfState);

            //Assert
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            Assert.That(actualResult == expectedResult);
        }
    }
}
