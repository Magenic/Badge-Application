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
        public void AcceptsValidParameters()
        {
            var mockProperty = new Mock<IPropertyInfo>();
            mockProperty.Setup(mp => mp.Type).Returns(typeof(string));
            mockProperty.Setup(mp => mp.Name).Returns("Name");
            var mockIdProperty = new Mock<IPropertyInfo>();
            mockIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockIdProperty.Setup(mp => mp.Name).Returns("Id");

            var newRule = new NoDuplicates(mockProperty.Object, mockIdProperty.Object, MockFoundNone);

            Assert.IsNotNull(newRule);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newRule")]
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParameterMustBeString()
        {
            var mockProperty = new Mock<IPropertyInfo>();
            mockProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockProperty.Setup(mp => mp.Name).Returns("Name");
            var mockIdProperty = new Mock<IPropertyInfo>();
            mockIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockIdProperty.Setup(mp => mp.Name).Returns("Id");

            var newRule = new NoDuplicates(mockProperty.Object, mockIdProperty.Object, MockFoundNone);
            Assert.Fail();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newRule")]
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParameterMustBeInteger()
        {
            var mockProperty = new Mock<IPropertyInfo>();
            mockProperty.Setup(mp => mp.Type).Returns(typeof(string));
            mockProperty.Setup(mp => mp.Name).Returns("Name");
            var mockIdProperty = new Mock<IPropertyInfo>();
            mockIdProperty.Setup(mp => mp.Type).Returns(typeof(string));
            mockIdProperty.Setup(mp => mp.Name).Returns("Id");

            var newRule = new NoDuplicates(mockProperty.Object, mockIdProperty.Object, MockFoundNone);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MustHaveFactory()
        {
            var mockProperty = new Mock<IPropertyInfo>();
            mockProperty.Setup(mp => mp.Type).Returns(typeof(string));
            mockProperty.Setup(mp => mp.Name).Returns("Name");
            var mockIdProperty = new Mock<IPropertyInfo>();
            mockIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockIdProperty.Setup(mp => mp.Name).Returns("Id");

            var newRule = new NoDuplicates(mockProperty.Object, mockIdProperty.Object, null);
            Assert.Fail();
        }

        [TestMethod]
        public void NoErrorIfNotFound()
        {
            var mockProperty = new Mock<IPropertyInfo>();
            mockProperty.Setup(mp => mp.Type).Returns(typeof(string));
            mockProperty.Setup(mp => mp.Name).Returns("Name");
            var mockIdProperty = new Mock<IPropertyInfo>();
            mockIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockIdProperty.Setup(mp => mp.Name).Returns("Id");

            var newRule = new NoDuplicates(mockProperty.Object, mockIdProperty.Object, MockFoundNone);

            var targetObject = new Mock<IActivityEdit>();
            targetObject.Setup(a => a.IsNew).Returns(true);
            var ruleContext = new RuleContext(null, newRule, targetObject.Object, new Dictionary<IPropertyInfo, object> { { mockProperty.Object, "FoundString" }, {mockIdProperty.Object, 1} });

            var ruleInterface = (IBusinessRule) newRule;

            ruleInterface.Execute(ruleContext);

            Assert.AreEqual(0, ruleContext.Results.Count);
        }

        [TestMethod]
        public void ErrorIfFound()
        {
            var mockProperty = new Mock<IPropertyInfo>();
            mockProperty.Setup(mp => mp.Type).Returns(typeof(string));
            mockProperty.Setup(mp => mp.Name).Returns("Name");
            var mockIdProperty = new Mock<IPropertyInfo>();
            mockIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockIdProperty.Setup(mp => mp.Name).Returns("Id");

            var newRule = new NoDuplicates(mockProperty.Object, mockIdProperty.Object, MockFoundOne);

            var targetObject = new Mock<IActivityEdit>();
            targetObject.Setup(a => a.IsNew).Returns(true);
            var ruleContext = new RuleContext(null, newRule, targetObject.Object, new Dictionary<IPropertyInfo, object> { { mockProperty.Object, "FoundString" }, { mockIdProperty.Object, 1 } });

            var ruleInterface = (IBusinessRule)newRule;

            ruleInterface.Execute(ruleContext);

            Assert.AreEqual(1, ruleContext.Results.Count);
        }

        public bool MockFoundNone(int id, string value)
        {
            return false;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "value")]
        public bool MockFoundOne(int id, string value)
        {
            return true;
        }
    }
}
