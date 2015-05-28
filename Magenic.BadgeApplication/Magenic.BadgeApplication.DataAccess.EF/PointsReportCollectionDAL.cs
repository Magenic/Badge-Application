using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class PointsReportCollectionDAL : IPointsReportCollectionDAL
    {
        public async Task<IEnumerable<PointsReportItemDTO>> GetPointsReportItemsAsync(bool displayThreshold = false)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeAwardList = await (from ba in ctx.BadgeAwards
                                            join e in ctx.Employees on ba.EmployeeId equals e.EmployeeId
                                            where !ba.PaidOut
                                            group ba by new { e.EmployeeId, e.FirstName, e.LastName, e.ADName, e.Location, e.AwardPayoutThreshold } into g
                                            where !displayThreshold && g.Key.AwardPayoutThreshold <= g.Sum(t => t.AwardAmount) ||
                                                  displayThreshold && g.Sum(t => t.AwardAmount) > 0
                                            select new PointsReportItemDTO
                                            {
                                                EmployeeId = g.Key.EmployeeId,
                                                EmployeeFirstName = g.Key.FirstName,
                                                EmployeeLastName = g.Key.LastName,
                                                EmployeeADName = g.Key.ADName,
                                                EmployeeLocation = g.Key.Location,
                                                PaidOut = false,
                                                TotalPoints = g.Sum(t => t.AwardAmount),
                                                BadgeAwardIds = g.Select(t => t.BadgeAwardId).ToList()
                                            }).ToArrayAsync();
                return badgeAwardList;
            }
        }

        public IEnumerable<PointsReportItemDTO> Update(IEnumerable<PointsReportItemDTO> data)
        {
            var workingData = data.ToList();
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                ctx.Configuration.ValidateOnSaveEnabled = false;
                foreach (var pointsReportItem in workingData.Where(t => t.PaidOut))
                {
                    foreach (var id in pointsReportItem.BadgeAwardIds)
                    {
                        var badgeAward = LoadData(id, pointsReportItem);
                        ctx.BadgeAwards.Attach(badgeAward);
                        var objectState = ((IObjectContextAdapter)ctx).ObjectContext.ObjectStateManager;
                        objectState.GetObjectStateEntry(badgeAward).SetModifiedProperty("PaidCompletedById");
                        objectState.GetObjectStateEntry(badgeAward).SetModifiedProperty("PaidDate");
                        objectState.GetObjectStateEntry(badgeAward).SetModifiedProperty("PaidOut");
                    }
                }
                ctx.SaveChanges();
            }
            return workingData.Where(t => !t.PaidOut);
        }

        private BadgeAward LoadData(int id, PointsReportItemDTO pointsReportItem)
        {
            var badgeAward = new BadgeAward
            {
                BadgeAwardId = id,
                PaidCompletedById = pointsReportItem.PayoutById,
                PaidDate = pointsReportItem.PayoutDate,
                PaidOut = true
            };
            return badgeAward;
        }
    }
}
