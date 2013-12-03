using System;
using System.Globalization;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Csla.Core;
using Magenic.BadgeApplication.Common.Interfaces;
using Csla.Rules;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.BusinessLogic.Tests
{
    [TestClass]
    public class DateOrderRuleTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            IoC.Container = HelperMethods.ContainerBulder.GetContainer().Build();
        }

        [TestMethod]
        public void AcceptsNullableDateTimes()
        {
            var mockStartDateProperty = new Mock<IPropertyInfo>();
            mockStartDateProperty.Setup(mp => mp.Type).Returns(typeof(DateTime?));
            mockStartDateProperty.Setup(mp => mp.Name).Returns("StartDate");
            var mockEndDateProperty = new Mock<IPropertyInfo>();
            mockEndDateProperty.Setup(mp => mp.Type).Returns(typeof(DateTime?));
            mockEndDateProperty.Setup(mp => mp.Name).Returns("EndDate");

            var newRule = new Rules.DateOrder(mockStartDateProperty.Object, mockEndDateProperty.Object);

            Assert.IsNotNull(newRule);
        }

        [TestMethod]
        public void AcceptsDateTimes()
        {
            var mockStartDateProperty = new Mock<IPropertyInfo>();
            mockStartDateProperty.Setup(mp => mp.Type).Returns(typeof(DateTime));
            mockStartDateProperty.Setup(mp => mp.Name).Returns("StartDate");
            var mockEndDateProperty = new Mock<IPropertyInfo>();
            mockEndDateProperty.Setup(mp => mp.Type).Returns(typeof(DateTime));
            mockEndDateProperty.Setup(mp => mp.Name).Returns("EndDate");

            var newRule = new Rules.DateOrder(mockStartDateProperty.Object, mockEndDateProperty.Object);

            Assert.IsNotNull(newRule);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newRule"), TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StartDateMustBeDateTime()
        {
            var mockStartDateProperty = new Mock<IPropertyInfo>();
            mockStartDateProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockStartDateProperty.Setup(mp => mp.Name).Returns("StartDate");
            var mockEndDateProperty = new Mock<IPropertyInfo>();
            mockEndDateProperty.Setup(mp => mp.Type).Returns(typeof(DateTime));
            mockEndDateProperty.Setup(mp => mp.Name).Returns("EndDate");

            try
            {
                var newRule = new Rules.DateOrder(mockStartDateProperty.Object, mockEndDateProperty.Object);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("startDateProperty must be a datetime or nullable datetime.", ex.Message);
                throw;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newRule"), TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EndDateMustBeDateTime()
        {
            var mockStartDateProperty = new Mock<IPropertyInfo>();
            mockStartDateProperty.Setup(mp => mp.Type).Returns(typeof(DateTime));
            mockStartDateProperty.Setup(mp => mp.Name).Returns("StartDate");
            var mockEndDateProperty = new Mock<IPropertyInfo>();
            mockEndDateProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockEndDateProperty.Setup(mp => mp.Name).Returns("EndDate");

            try
            {
                var newRule = new Rules.DateOrder(mockStartDateProperty.Object, mockEndDateProperty.Object);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("endDateProperty must be a datetime or nullable datetime.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        public void StartDateBeforeEndDate()
        {
            var mockStartDateProperty = new Mock<IPropertyInfo>();
            mockStartDateProperty.Setup(mp => mp.Type).Returns(typeof(DateTime));
            mockStartDateProperty.Setup(mp => mp.Name).Returns("StartDate");
            var mockEndDateProperty = new Mock<IPropertyInfo>();
            mockEndDateProperty.Setup(mp => mp.Type).Returns(typeof(DateTime));
            mockEndDateProperty.Setup(mp => mp.Name).Returns("EndDate");

            var newRule = new Rules.DateOrder(mockStartDateProperty.Object, mockEndDateProperty.Object);
            var targetObject = new Mock<IBadgeEdit>();
            var startDate = DateTime.Parse("1/1/2013", CultureInfo.CurrentCulture);
            var endDate = DateTime.Parse("12/31/2013", CultureInfo.CurrentCulture);

            var ruleContext = new RuleContext(null, newRule, targetObject.Object, new Dictionary<IPropertyInfo, object> { { mockStartDateProperty.Object, startDate }, { mockEndDateProperty.Object, endDate} });
            var ruleRunner = (IBusinessRule)newRule;

            ruleRunner.Execute(ruleContext);

            Assert.AreEqual(0, ruleContext.Results.Count);
        }

        [TestMethod]
        public void StartDateAfterEndDate()
        {
            var mockStartDateProperty = new Mock<IPropertyInfo>();
            mockStartDateProperty.Setup(mp => mp.Type).Returns(typeof(DateTime));
            mockStartDateProperty.Setup(mp => mp.Name).Returns("StartDate");
            var mockEndDateProperty = new Mock<IPropertyInfo>();
            mockEndDateProperty.Setup(mp => mp.Type).Returns(typeof(DateTime));
            mockEndDateProperty.Setup(mp => mp.Name).Returns("EndDate");

            var newRule = new Rules.DateOrder(mockStartDateProperty.Object, mockEndDateProperty.Object);
            var targetObject = new Mock<IBadgeEdit>();
            var startDate = DateTime.Parse("1/1/2014", CultureInfo.CurrentCulture);
            var endDate = DateTime.Parse("12/31/2013", CultureInfo.CurrentCulture);

            var ruleContext = new RuleContext(null, newRule, targetObject.Object, new Dictionary<IPropertyInfo, object> { { mockStartDateProperty.Object, startDate }, { mockEndDateProperty.Object, endDate } });
            var ruleRunner = (IBusinessRule)newRule;

            ruleRunner.Execute(ruleContext);

            Assert.AreEqual(1, ruleContext.Results.Count);
            Assert.AreEqual(RuleSeverity.Error, ruleContext.Results[0].Severity);
            Assert.IsFalse(ruleContext.Results[0].Success);
        }
    }
}
