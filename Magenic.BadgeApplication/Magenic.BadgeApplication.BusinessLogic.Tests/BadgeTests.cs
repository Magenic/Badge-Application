using System.Collections.Generic;
using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests
{
    [TestClass]
    public class BadgeTests
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "test")]
        [TestMethod]
        public async Task SetBadgeImage()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(typeof(BadgeEdit)).As(typeof(IBadgeEdit));
            builder.RegisterType(typeof (BadgeEditDAL)).As(typeof (IBadgeEditDAL));
            builder.RegisterGeneric(typeof(ObjectFactory<>)).As(typeof(IObjectFactory<>));
            IoC.Container = builder.Build();

            var badge = await BadgeEdit.GetBadgeEditByIdAsync(1);
            var image = new byte[] {0, 1, 2, 4};

            badge.SetBadgeImage(image);

            Assert.AreEqual(string.Empty, badge.ImagePath);
        }
    }

    public class BadgeEditDAL : IBadgeEditDAL
    {
        private string ImagePath = "http:\\\\MySite.com\\TestImage.jpg";

        public Task<BadgeEditDTO> GetBadgeByIdAsync(int badgeEditId)
        {
            var dto = new BadgeEditDTO
            {
                Id = 1,
                ImagePath = ImagePath
            };
            dto.BadgeActivities = new List<BadgeActivityEditDTO>();
            return Task.FromResult(dto);
        }

        public BadgeEditDTO Update(BadgeEditDTO data)
        {
            throw new NotImplementedException();
        }

        public BadgeEditDTO Insert(BadgeEditDTO data)
        {
            throw new NotImplementedException();
        }

        public void Delete(int badgeId)
        {
            throw new NotImplementedException();
        }

        public IList<BadgeEditDTO> GetPotentialBadgesForActivity(int activityId)
        {
            throw new NotImplementedException();
        }
    }
}
