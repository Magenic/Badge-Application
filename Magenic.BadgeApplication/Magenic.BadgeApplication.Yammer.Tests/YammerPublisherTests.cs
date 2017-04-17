using Microsoft.VisualStudio.TestTools.UnitTesting;
using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Yammer.Tests
{
    [TestClass]
    public class YammerPublisherTests
    {
        [TestMethod]
        public void PublishHarness()
        {
            var dto = new EarnedBadgeItemDTO
            {
                EmployeeADName = "billzi",
                Name = "testBadgeName3",
                ImagePath = "testImagePath3",
                Tagline = "testTagLine3"
            };

            var yp = new YammerPublisher();

            yp.Publish( dto );
        }
    }
}
