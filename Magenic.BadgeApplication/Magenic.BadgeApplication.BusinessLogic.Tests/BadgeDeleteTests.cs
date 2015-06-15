using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Csla.Security;
using Moq;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Common.DTO;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    /// <summary>
    /// Summary description for BadgeDeleteTests
    /// </summary>
    [TestClass]
    public class BadgeDeleteTests
    {
        private IContainer globalContainer;
        [TestInitialize]
        public void TestInit()
        {
            globalContainer = IoC.Container;
        }

        [TestCleanup]
        public void Teardown()
        {
            IoC.Container = globalContainer;
        }

        [TestMethod]
        public void DeleteBadgeAdmin()
        {
            var mockPrincipal = new Mock<ICslaPrincipal>();
            mockPrincipal.Setup(i => i.IsInRole(Common.Enums.PermissionType.Administrator.ToString())).Returns(true);
            Csla.ApplicationContext.User = mockPrincipal.Object;

            var builder = new ContainerBuilder();
            builder.RegisterType(typeof(LeaderboardItemDAL)).As(typeof(ILeaderboardItemDAL));
            builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));
            IoC.Container = builder.Build();

            var badgeDelete = BadgeDelete.DeleteBadgeAsync(1);
            badgeDelete.Wait();

            Assert.AreEqual(badgeDelete.Status, TaskStatus.RanToCompletion);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void DeleteBadgeNonAdmin()
        {
            var mockPrincipal = new Mock<ICslaPrincipal>();
            mockPrincipal.Setup(i => i.IsInRole(Common.Enums.PermissionType.Administrator.ToString())).Returns(false);
            Csla.ApplicationContext.User = mockPrincipal.Object;

            var builder = new ContainerBuilder();
            builder.RegisterType(typeof(LeaderboardItemDAL)).As(typeof(ILeaderboardItemDAL));
            builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));
            IoC.Container = builder.Build();

            try
            {
                var badgeDelete = BadgeDelete.DeleteBadgeAsync(1);
                badgeDelete.Wait();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.InnerException.Message, "User not authorized to execute object type BadgeDelete");
                throw;
            }
        }
    }

    public class LeaderboardItemDAL : ILeaderboardItemDAL
    {
        public Task<LeaderboardItemDTO> GetLeaderboardItemForUserNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public void Delete(int badgeAwardId)
        {
        }
    }
}
