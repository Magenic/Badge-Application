using System.Linq;
using Magenic.BadgeApplication.Common.DTO;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface for use with awarding badges.
    /// </summary>
    public interface IAwardBadgesDAL
    {
        /// <summary>
        /// Returns a list of badges that an employee has been awarded in the past.
        /// </summary>
        /// <param name="employeeId">The id of the employee who's badges we are looking for.</param>
        /// <returns>Returns a list of badges that an employee has been awarded in the past.</returns>
        IList<BadgeAwardDTO> GetAwardedBadgesForUser(int employeeId);
        /// <summary>
        /// Returns a list of activities that an employee has submitted in the past
        /// that have the given activity id.
        /// </summary>
        /// <param name="employeeId">The id of the employee who's submitted activities we are looking for.</param>
        /// <param name="activityId">The id of the activity for the submitted activities we are looking for.</param>
        /// <returns>Returns a list of activities that an employee has submitted in the past
        /// that have the given activity id.</returns>
        IList<SubmittedActivityItemDTO> GetPreviousActivitiesForUser(int employeeId, int activityId);
        /// <summary>
        /// Saves a list of badges that have been earned.
        /// </summary>
        /// <param name="badges">The list of earned badges to save.</param>
        void SaveEarnedBadges(IQueryable<BadgeAwardDTO> badges);
    }
}
