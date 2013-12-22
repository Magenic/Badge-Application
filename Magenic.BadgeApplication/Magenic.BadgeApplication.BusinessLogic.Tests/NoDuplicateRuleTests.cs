using System;
using Magenic.BadgeApplication.BusinessLogic.Rules;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Csla.Core;
using Csla.Rules;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.BusinessLogic.Tests
{
    [TestClass]
    public class NoDuplicateRuleTests
    {
        [TestMethod]
        public void AcceptsString()
        {
            var mockProperty = new Mock<IPropertyInfo>();
            mockProperty.Setup(mp => mp.Type).Returns(typeof(string));
            mockProperty.Setup(mp => mp.Name).Returns("Name");

            var newRule = new NoDuplicates(mockProperty.Object, MockFactoryFoundNone);

            Assert.IsNotNull(newRule);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newRule")]
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MustBeString()
        {
            var mockProperty = new Mock<IPropertyInfo>();
            mockProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockProperty.Setup(mp => mp.Name).Returns("Name");

            try
            {
                var newRule = new NoDuplicates(mockProperty.Object, MockFactoryFoundNone);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("primaryProperty must be a string.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MustHaveFactory()
        {
            var mockProperty = new Mock<IPropertyInfo>();
            mockProperty.Setup(mp => mp.Type).Returns(typeof(string));
            mockProperty.Setup(mp => mp.Name).Returns("Name");

            try
            {
                var newRule = new NoDuplicates(mockProperty.Object, null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("duplicateCommand cannot be null.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        public void RegistrationNotFound()
        {
            var mockProperty = new Mock<IPropertyInfo>();
            mockProperty.Setup(mp => mp.Type).Returns(typeof(string));
            mockProperty.Setup(mp => mp.Name).Returns("Name");

            var newRule = new NoDuplicates(mockProperty.Object, MockFactoryFoundNone);

            var targetObject = new Mock<IBadgeEdit>();
            var ruleContext = new RuleContext(null, newRule, targetObject, new Dictionary<IPropertyInfo, object> { { mockProperty.Object, "FoundString" } });

            var ruleInterface = (IBusinessRule) newRule;

            ruleInterface.Execute(ruleContext);

            Assert.AreEqual(0, ruleContext.Results.Count);
        }

        //[Ignore]
        //[TestMethod]
        //public void RegistrationFound()
        //{
        //    var mockProperty = new Mock<IPropertyInfo>();
        //    mockProperty.Setup(mp => mp.Type).Returns(typeof(string));
        //    mockProperty.Setup(mp => mp.Name).Returns("Name");

        //    var newRule = new NoDuplicates(mockProperty.Object, MockFactoryFoundOne);

        //    var targetObject = new Mock<IBadgeEdit>();
        //    var ruleContext = new RuleContext(null, newRule, targetObject, new Dictionary<IPropertyInfo, object> { { mockProperty.Object, "NotFoundString" } });

        //    var ruleInterface = (IBusinessRule)newRule;

        //    ruleInterface.Execute(ruleContext);

        //    Assert.AreEqual(1, ruleContext.Results.Count);
        //    Assert.AreEqual(RuleSeverity.Error, ruleContext.Results[0].Severity);
        //    Assert.IsFalse(ruleContext.Results[0].Success);
        //}

        public bool MockFactoryFoundNone(string value)
        {
            return false;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
        public bool MockFactoryFoundOne(string value)
        {
            return true;
        }
    }
}
