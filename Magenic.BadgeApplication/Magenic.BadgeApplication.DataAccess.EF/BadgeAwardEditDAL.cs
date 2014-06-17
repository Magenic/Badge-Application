using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Linq;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    /// <summary>
    /// 
    /// </summary>
    public class BadgeAwardEditDAL
        : IBadgeAwardEditDAL
    {
        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public BadgeAwardEditDTO Update(BadgeAwardEditDTO data)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var saveBadgeAward = ctx.BadgeAwards.Where(ba => ba.BadgeAwardId == data.Id).Single();
                saveBadgeAward.AwardAmount = data.AwardAmount;
                ctx.SaveChanges();
            }
            return data;
        }
    }
}
