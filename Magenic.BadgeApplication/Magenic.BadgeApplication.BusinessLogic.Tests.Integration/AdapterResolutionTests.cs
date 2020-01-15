using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Processor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass, Ignore]
    public class AdapterResolutionTests
    {
        [TestMethod]
        public void TestAdapterResolution()
        {
            IEnumerable<IPublisher> publishers = IoC.Container.Resolve<IEnumerable<IPublisher>>();
            Assert.IsTrue(publishers.Count() == 1);
        }
    }

    public class TestYammerPublisher : IPublisher
    {
        public void Publish(Common.DTO.EarnedBadgeItemDTO earnedBadge)
        {
            
        }
    }   
}
