using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BadgeEditViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeEditViewModel"/> class.
        /// </summary>
        public BadgeEditViewModel()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeEditViewModel"/> class.
        /// </summary>
        /// <param name="allActivities">All activities.</param>
        public BadgeEditViewModel(IActivityCollection allActivities)
        {
            AllActivities = allActivities;
        }

        /// <summary>
        /// Gets or sets all activities.
        /// </summary>
        /// <value>
        /// All activities.
        /// </value>
        public IActivityCollection AllActivities { get; private set; }

        /// <summary>
        /// Gets or sets the award values possible.
        /// </summary>
        /// <value>
        /// The award values possible.
        /// </value>
        public IEnumerable<SelectListItem> AwardValuesPossible { get; set; }

        /// <summary>
        /// Gets or sets the badge.
        /// </summary>
        /// <value>
        /// The badge.
        /// </value>
        public IBadgeEdit Badge { get; set; }
    }
}