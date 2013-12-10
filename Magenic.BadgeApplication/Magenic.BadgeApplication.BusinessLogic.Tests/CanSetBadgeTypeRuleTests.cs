using Csla.Core;
using Csla.Rules;
using Csla.Security;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Tests
{
    [TestClass]
    public class CanSetBadgeTypeRuleTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            IoC.Container = HelperMethods.ContainerBulder.GetContainer().Build();
        }

        [TestMethod]
        public void AcceptsIPropertyInfo()
        {
            var mockTypeProperty = new Mock<IPropertyInfo>();
            mockTypeProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeType));
            mockTypeProperty.Setup(mp => mp.Name).Returns("Type");

            var newRule = new Rules.CanSetBadgeType(AuthorizationActions.WriteProperty, mockTypeProperty.Object, Common.Enums.BadgeType.Corporate, "");

            Assert.IsNotNull(newRule);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "newRule"), TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StartDateMustBeIPropertyInfo()
        {
            var mockMemberInfoProperty = new Mock<IMemberInfo>();
            mockMemberInfoProperty.Setup(mp => mp.Name).Returns("Type");

            try
            {
                var newRule = new Rules.CanSetBadgeType(AuthorizationActions.WriteProperty, mockMemberInfoProperty.Object, Common.Enums.BadgeType.Corporate, "");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Parameter element must be of type IPropertyInfo.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        public void DifferentBadgeTypeAllowedForUnauthorizedUser()
        {
            var mockPrincipal = new Mock<ICslaPrincipal>();
            mockPrincipal.Setup(i => i.IsInRole(Common.Enums.PermissionType.Administrator.ToString())).Returns(false);
            Csla.ApplicationContext.User = mockPrincipal.Object;

            var mockTypeProperty = new Mock<IPropertyInfo>();
            mockTypeProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeType));
            mockTypeProperty.Setup(mp => mp.Name).Returns("Type");

            var newRule = new Rules.CanSetBadgeType(AuthorizationActions.WriteProperty, mockTypeProperty.Object, Common.Enums.BadgeType.Corporate, Common.Enums.Role.Administrator.ToString());
            var targetObject = new Mock<IBadgeEdit>();
            targetObject.Setup(to => to.Type).Returns(Common.Enums.BadgeType.Community);

            var ruleContext = new AuthorizationContext(newRule, targetObject.Object, typeof(IBadgeEdit));
            var ruleRunner = (IAuthorizationRule)newRule;

            ruleRunner.Execute(ruleContext);

            Assert.IsTrue(ruleContext.HasPermission);
        }

        [TestMethod]
        public void CorrectBadgeTypeNotAllowedForUnauthorizedUser()
        {
            var mockPrincipal = new Mock<ICslaPrincipal>();
            mockPrincipal.Setup(i => i.IsInRole(Common.Enums.PermissionType.Administrator.ToString())).Returns(false);
            Csla.ApplicationContext.User = mockPrincipal.Object;

            var mockTypeProperty = new Mock<IPropertyInfo>();
            mockTypeProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeType));
            mockTypeProperty.Setup(mp => mp.Name).Returns("Type");

            var newRule = new Rules.CanSetBadgeType(AuthorizationActions.WriteProperty, mockTypeProperty.Object, Common.Enums.BadgeType.Corporate, Common.Enums.Role.Administrator.ToString());
            var targetObject = new Mock<IBadgeEdit>();
            targetObject.Setup(to => to.Type).Returns(Common.Enums.BadgeType.Corporate);

            var ruleContext = new AuthorizationContext(newRule, targetObject.Object, typeof(IBadgeEdit));
            var ruleRunner = (IAuthorizationRule)newRule;

            ruleRunner.Execute(ruleContext);

            Assert.IsFalse(ruleContext.HasPermission);
        }

        [TestMethod]
        public void CorrectBadgeTypeAllowedForAuthorizedUser()
        {
            var mockPrincipal = new Mock<ICslaPrincipal>();
            mockPrincipal.Setup(i => i.IsInRole(Common.Enums.PermissionType.Administrator.ToString())).Returns(true);
            Csla.ApplicationContext.User = mockPrincipal.Object;

            var mockTypeProperty = new Mock<IPropertyInfo>();
            mockTypeProperty.Setup(mp => mp.Type).Returns(typeof(Common.Enums.BadgeType));
            mockTypeProperty.Setup(mp => mp.Name).Returns("Type");

            var newRule = new Rules.CanSetBadgeType(AuthorizationActions.WriteProperty, mockTypeProperty.Object, Common.Enums.BadgeType.Corporate, Common.Enums.Role.Administrator.ToString());
            var targetObject = new Mock<IBadgeEdit>();
            targetObject.Setup(to => to.Type).Returns(Common.Enums.BadgeType.Corporate);

            var ruleContext = new AuthorizationContext(newRule, targetObject.Object, typeof(IBadgeEdit));
            var ruleRunner = (IAuthorizationRule)newRule;

            ruleRunner.Execute(ruleContext);

            Assert.IsTrue(ruleContext.HasPermission);
        }
    }
}
