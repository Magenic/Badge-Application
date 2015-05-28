using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magenic.BadgeApplication.Models
{
    public class BadgeAwardsModel
    {
        public IPointsReportCollection PointsReportCollection { get; set; }

        public IBadgeAwardEditCollection BadgeAwardEditCollection { get; set; }
    }
}