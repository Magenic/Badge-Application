using Autofac;
using Csla.Core;
using Csla.Rules;
using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.BusinessLogic.Rules;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests
{
    [TestClass]
    public class DefaultActivityStatusTests
    {
        protected const int ItemRequiresApproval = 5;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(typeof(ActivityEdit)).As(typeof(IActivityEdit));
            builder.RegisterType(typeof(ActivityEditDAL)).As(typeof(IActivityEditDAL));
            builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));
            IoC.Container = builder.Build();
        }

        [TestMethod]
        public void AcceptsCorrectParameters()
        {
            var mockActivityIdProperty = new Mock<IPropertyInfo>();
            mockActivityIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockActivityIdProperty.Setup(mp => mp.Name).Returns("ActivityId");
            var mockStatusProperty = new Mock<IPropertyInfo>();
            mockStatusProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.ActivitySubmissionStatus));
            var mockApprovedByIdProperty = new Mock<IPropertyInfo>();
            mockApprovedByIdProperty.Setup(mp => mp.Type).Returns(typeof(int));

            var newRule = new DefaultActivityStatus(mockActivityIdProperty.Object, mockStatusProperty.Object, mockApprovedByIdProperty.Object);

            Assert.IsNotNull(newRule);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newRule"), TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ActivityIdParameterCorrectType()
        {
            var mockActivityIdProperty = new Mock<IPropertyInfo>();
            mockActivityIdProperty.Setup(mp => mp.Type).Returns(typeof(string));
            mockActivityIdProperty.Setup(mp => mp.Name).Returns("ActivityId");
            var mockStatusProperty = new Mock<IPropertyInfo>();
            mockStatusProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.ActivitySubmissionStatus));
            var mockApprovedByIdProperty = new Mock<IPropertyInfo>();
            mockApprovedByIdProperty.Setup(mp => mp.Type).Returns(typeof(int));

            var newRule = new DefaultActivityStatus(mockActivityIdProperty.Object, mockStatusProperty.Object, mockApprovedByIdProperty.Object);

            Assert.Fail("Rule Created when error should occur.");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newRule"), TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StatusParameterCorrectType()
        {
            var mockActivityIdProperty = new Mock<IPropertyInfo>();
            mockActivityIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockActivityIdProperty.Setup(mp => mp.Name).Returns("ActivityId");
            var mockStatusProperty = new Mock<IPropertyInfo>();
            mockStatusProperty.Setup(mp => mp.Type).Returns(typeof(string));
            var mockApprovedByIdProperty = new Mock<IPropertyInfo>();
            mockApprovedByIdProperty.Setup(mp => mp.Type).Returns(typeof(int));

            var newRule = new DefaultActivityStatus(mockActivityIdProperty.Object, mockStatusProperty.Object, mockApprovedByIdProperty.Object);

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
            var mockActivityIdProperty = new Mock<IPropertyInfo>();
            mockActivityIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockActivityIdProperty.Setup(mp => mp.Name).Returns("ActivityId");
            var mockStatusProperty = new Mock<IPropertyInfo>();
            mockStatusProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.ActivitySubmissionStatus));
            mockStatusProperty.Setup(mp => mp.Name).Returns("Status");
            var mockApprovedByIdProperty = new Mock<IPropertyInfo>();
            mockApprovedByIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockApprovedByIdProperty.Setup(mp => mp.Name).Returns("ApprovedById");

            var newRule = new DefaultActivityStatus(mockActivityIdProperty.Object, mockStatusProperty.Object, mockApprovedByIdProperty.Object);


            var targetObject = new Mock<IActivityEdit>();

            var ruleContext = new RuleContext(Complete, newRule, targetObject.Object, new Dictionary<IPropertyInfo, object> { { mockActivityIdProperty.Object, 1 }, { mockStatusProperty.Object, Common.Enums.ActivitySubmissionStatus.Approved }, { mockApprovedByIdProperty.Object, 54 } });

            var ruleRunner = (IBusinessRule)newRule;

            ruleRunner.Execute(ruleContext);

            Assert.IsNotNull(newRule);
            Assert.IsNull(ruleContext.OutputPropertyValues);
        }

        [TestMethod]
        public void RequiresApproval()
        {
            var mockActivityIdProperty = new Mock<IPropertyInfo>();
            mockActivityIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockActivityIdProperty.Setup(mp => mp.Name).Returns("ActivityId");
            var mockStatusProperty = new Mock<IPropertyInfo>();
            mockStatusProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.ActivitySubmissionStatus));
            mockStatusProperty.Setup(mp => mp.Name).Returns("Status");
            var mockApprovedByIdProperty = new Mock<IPropertyInfo>();
            mockApprovedByIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockApprovedByIdProperty.Setup(mp => mp.Name).Returns("ApprovedById");

            var newRule = new DefaultActivityStatus(mockActivityIdProperty.Object, mockStatusProperty.Object, mockApprovedByIdProperty.Object);


            var targetObject = new Mock<IActivityEdit>();

            var ruleContext = new RuleContext(Complete, newRule, targetObject.Object, new Dictionary<IPropertyInfo, object> { { mockActivityIdProperty.Object, ItemRequiresApproval }, { mockStatusProperty.Object, Common.Enums.ActivitySubmissionStatus.Unset }, { mockApprovedByIdProperty.Object, 0 } });

            var ruleRunner = (IBusinessRule)newRule;

            ruleRunner.Execute(ruleContext);

            Assert.IsNotNull(newRule);
            Assert.IsTrue(ruleContext.OutputPropertyValues.Count == 1);
            Assert.IsNotNull(ruleContext.OutputPropertyValues.SingleOrDefault(o => o.Key.Name == mockStatusProperty.Object.Name));
            Assert.AreEqual(Common.Enums.ActivitySubmissionStatus.AwaitingApproval, ruleContext.OutputPropertyValues.Single(o => o.Key.Name == mockStatusProperty.Object.Name).Value);
        }

        [TestMethod]
        public void DoesNotRequireApproval()
        {
            var mockActivityIdProperty = new Mock<IPropertyInfo>();
            mockActivityIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockActivityIdProperty.Setup(mp => mp.Name).Returns("ActivityId");
            var mockStatusProperty = new Mock<IPropertyInfo>();
            mockStatusProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.ActivitySubmissionStatus));
            mockStatusProperty.Setup(mp => mp.Name).Returns("Status");
            var mockApprovedByIdProperty = new Mock<IPropertyInfo>();
            mockApprovedByIdProperty.Setup(mp => mp.Type).Returns(typeof(int));
            mockApprovedByIdProperty.Setup(mp => mp.Name).Returns("ApprovedById");

            var newRule = new DefaultActivityStatus(mockActivityIdProperty.Object, mockStatusProperty.Object, mockApprovedByIdProperty.Object);

            var targetObject = new Mock<IActivityEdit>();

            var ruleContext = new RuleContext(Complete, newRule, targetObject.Object, new Dictionary<IPropertyInfo, object> { { mockActivityIdProperty.Object, ItemRequiresApproval + 1 }, { mockStatusProperty.Object, Common.Enums.ActivitySubmissionStatus.Unset }, { mockApprovedByIdProperty.Object, 0 } });

            var ruleRunner = (IBusinessRule)newRule;

            ruleRunner.Execute(ruleContext);

            Assert.IsNotNull(newRule);
            Assert.IsTrue(ruleContext.OutputPropertyValues.Count == 1);
            Assert.IsNotNull(ruleContext.OutputPropertyValues.SingleOrDefault(o => o.Key.Name == mockStatusProperty.Object.Name));
            Assert.AreEqual(Common.Enums.ActivitySubmissionStatus.Approved, ruleContext.OutputPropertyValues.Single(o => o.Key.Name == mockStatusProperty.Object.Name).Value);
        }

        internal static void Complete(RuleContext context)
        {
            
        }

        internal class ActivityEditDAL : IActivityEditDAL
        {
            public Task<ActivityEditDTO> GetActivityByIdAsync(int activityEditId)
            {
                var dto = new ActivityEditDTO
                {
                    Id = activityEditId,
                    RequiresApproval = activityEditId == ItemRequiresApproval
                };
                return Task.FromResult(dto);
            }

            public ActivityEditDTO Update(ActivityEditDTO data)
            {
                throw new NotImplementedException();
            }

            public ActivityEditDTO Insert(ActivityEditDTO data)
            {
                throw new NotImplementedException();
            }

            public void Delete(int activityId)
            {
                throw new NotImplementedException();
            }

            public bool ActivityNameExists(int id, string name)
            {
                throw new NotImplementedException();
            }
        }

    }
}
