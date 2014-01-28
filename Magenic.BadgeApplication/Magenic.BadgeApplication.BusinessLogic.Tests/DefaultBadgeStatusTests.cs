using Csla.Core;
using Csla.Rules;
using Magenic.BadgeApplication.BusinessLogic.Rules;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magenic.BadgeApplication.BusinessLogic.Tests
{
    [TestClass]
    public class DefaultBadgeStatusTests
    {
        [TestMethod]
        public void AcceptsCorrectParameters()
        {
            var mockTypeProperty = new Mock<IPropertyInfo>();
            mockTypeProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeType));
            mockTypeProperty.Setup(mp => mp.Name).Returns("BadgeType");
            var mockStatusProperty = new Mock<IPropertyInfo>();
            mockStatusProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeStatus));
            var mockApprovedByIdProperty = new Mock<IPropertyInfo>();
            mockApprovedByIdProperty.Setup(mp => mp.Type).Returns(typeof(int));

            var newRule = new DefaultBadgeStatus(mockTypeProperty.Object, mockStatusProperty.Object, mockApprovedByIdProperty.Object);

            Assert.IsNotNull(newRule);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newRule"), TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BadgeTypeParameterCorrectType()
        {
            var mockTypeProperty = new Mock<IPropertyInfo>();
            mockTypeProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockTypeProperty.Setup(mp => mp.Name).Returns("BadgeType");
            var mockStatusProperty = new Mock<IPropertyInfo>();
            mockStatusProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeStatus));
            var mockApprovedByIdProperty = new Mock<IPropertyInfo>();
            mockApprovedByIdProperty.Setup(mp => mp.Type).Returns(typeof(int));

            var newRule = new DefaultBadgeStatus(mockTypeProperty.Object, mockStatusProperty.Object, mockApprovedByIdProperty.Object);

            Assert.Fail("Rule Created when error should occur.");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newRule"), TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StatusParameterCorrectType()
        {
            var mockTypeProperty = new Mock<IPropertyInfo>();
            mockTypeProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeType));
            mockTypeProperty.Setup(mp => mp.Name).Returns("BadgeType");
            var mockStatusProperty = new Mock<IPropertyInfo>();
            mockStatusProperty.Setup(mp => mp.Type).Returns(typeof(int));
            var mockApprovedByIdProperty = new Mock<IPropertyInfo>();
            mockApprovedByIdProperty.Setup(mp => mp.Type).Returns(typeof(int));

            var newRule = new DefaultBadgeStatus(mockTypeProperty.Object, mockStatusProperty.Object, mockApprovedByIdProperty.Object);

            Assert.Fail("Rule Created when error should occur.");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newRule"), TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ApprovedByIdCorrectType()
        {
            var mockActivityIdProperty = new Mock<IPropertyInfo>();
            mockActivityIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockActivityIdProperty.Setup(mp => mp.Name).Returns("ActivityId");
            var mockStatusProperty = new Mock<IPropertyInfo>();
            mockStatusProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.ActivitySubmissionStatus));
            var mockApprovedByIdProperty = new Mock<IPropertyInfo>();
            mockApprovedByIdProperty.Setup(mp => mp.Type).Returns(typeof(string));

            var newRule = new DefaultActivityStatus(mockActivityIdProperty.Object, mockStatusProperty.Object, mockApprovedByIdProperty.Object);

            Assert.Fail("Rule Created when error should occur.");
        }

        [TestMethod]
        public void LeaveApprovedAlone()
        {
            var mockTypeProperty = new Mock<IPropertyInfo>();
            mockTypeProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeType));
            mockTypeProperty.Setup(mp => mp.Name).Returns("BadgeType");
            var mockStatusProperty = new Mock<IPropertyInfo>();
            mockStatusProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeStatus));
            mockStatusProperty.Setup(mp => mp.Name).Returns("BadgeStatus");
            var mockApprovedByIdProperty = new Mock<IPropertyInfo>();
            mockApprovedByIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockApprovedByIdProperty.Setup(mp => mp.Name).Returns("ApproveById");

            var newRule = new DefaultBadgeStatus(mockTypeProperty.Object, mockStatusProperty.Object, mockApprovedByIdProperty.Object);

            var targetObject = new Mock<IActivityEdit>();

            var ruleContext = new RuleContext(Complete, newRule, targetObject.Object, new Dictionary<IPropertyInfo, object> { { mockTypeProperty.Object, Common.Enums.BadgeType.Community }, { mockStatusProperty.Object, Common.Enums.ActivitySubmissionStatus.Approved }, { mockApprovedByIdProperty.Object, 54 } });

            var ruleRunner = (IBusinessRule)newRule;

            ruleRunner.Execute(ruleContext);

            Assert.IsNotNull(newRule);
            Assert.IsNull(ruleContext.OutputPropertyValues);
        }

        [TestMethod]
        public void RequiresApproval()
        {
            var mockTypeProperty = new Mock<IPropertyInfo>();
            mockTypeProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeType));
            mockTypeProperty.Setup(mp => mp.Name).Returns("BadgeType");
            var mockStatusProperty = new Mock<IPropertyInfo>();
            mockStatusProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeStatus));
            mockStatusProperty.Setup(mp => mp.Name).Returns("BadgeStatus");
            var mockApprovedByIdProperty = new Mock<IPropertyInfo>();
            mockApprovedByIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockApprovedByIdProperty.Setup(mp => mp.Name).Returns("ApproveById");

            var newRule = new DefaultBadgeStatus(mockTypeProperty.Object, mockStatusProperty.Object, mockApprovedByIdProperty.Object);

            var targetObject = new Mock<IActivityEdit>();

            var ruleContext = new RuleContext(Complete, newRule, targetObject.Object, new Dictionary<IPropertyInfo, object> { { mockTypeProperty.Object, Common.Enums.BadgeType.Community }, { mockStatusProperty.Object, Common.Enums.ActivitySubmissionStatus.Unset }, { mockApprovedByIdProperty.Object, 0 } });

            var ruleRunner = (IBusinessRule)newRule;

            ruleRunner.Execute(ruleContext);

            Assert.IsNotNull(newRule);
            Assert.IsTrue(ruleContext.OutputPropertyValues.Count == 1);
            Assert.IsNotNull(ruleContext.OutputPropertyValues.SingleOrDefault(o => o.Key.Name == mockStatusProperty.Object.Name));
            Assert.AreEqual(Common.Enums.BadgeStatus.AwaitingApproval, ruleContext.OutputPropertyValues.Single(o => o.Key.Name == mockStatusProperty.Object.Name).Value);
        }

        [TestMethod]
        public void DoesNotRequireApproval()
        {
            var mockTypeProperty = new Mock<IPropertyInfo>();
            mockTypeProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeType));
            mockTypeProperty.Setup(mp => mp.Name).Returns("BadgeType");
            var mockStatusProperty = new Mock<IPropertyInfo>();
            mockStatusProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeStatus));
            mockStatusProperty.Setup(mp => mp.Name).Returns("BadgeStatus");
            var mockApprovedByIdProperty = new Mock<IPropertyInfo>();
            mockApprovedByIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockApprovedByIdProperty.Setup(mp => mp.Name).Returns("ApproveById");

            var newRule = new DefaultBadgeStatus(mockTypeProperty.Object, mockStatusProperty.Object, mockApprovedByIdProperty.Object);

            var targetObject = new Mock<IActivityEdit>();

            var ruleContext = new RuleContext(Complete, newRule, targetObject.Object, new Dictionary<IPropertyInfo, object> { { mockTypeProperty.Object, Common.Enums.BadgeType.Corporate }, { mockStatusProperty.Object, Common.Enums.ActivitySubmissionStatus.Unset }, { mockApprovedByIdProperty.Object, 0 } });

            var ruleRunner = (IBusinessRule)newRule;

            ruleRunner.Execute(ruleContext);

            Assert.IsNotNull(newRule);
            Assert.IsTrue(ruleContext.OutputPropertyValues.Count == 1);
            Assert.IsNotNull(ruleContext.OutputPropertyValues.SingleOrDefault(o => o.Key.Name == mockStatusProperty.Object.Name));
            Assert.AreEqual(Common.Enums.BadgeStatus.Approved, ruleContext.OutputPropertyValues.Single(o => o.Key.Name == mockStatusProperty.Object.Name).Value);
        }

        internal static void Complete(RuleContext context)
        {

        }
    }
}
