using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Magenic.BadgeApplication.Yammer;
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
				Name = "testBadgeName2",
				ImagePath = "testImagePath2",
				Tagline = "testTagLine2"
			};

			YammerPublisher yp = new YammerPublisher();

			yp.Publish(dto);
			

		}
	}
}
