using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
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
            SelectedActivityIds = new List<int>();
            this.Badge = ((BadgeEdit)BadgeEdit.CreateBadge());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeEditViewModel" /> class.
        /// </summary>
        /// <param name="allActivities">All activities.</param>
        public BadgeEditViewModel(IActivityCollection allActivities)
        {
            AllActivities = new MultiSelectList(allActivities, "Id", "Name");
            SelectedActivityIds = new List<int>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeEditViewModel" /> class.
        /// </summary>
        /// <param name="allActivities">All activities.</param>
        /// <param name="badgeActivities">The badge activities.</param>
        public BadgeEditViewModel(IActivityCollection allActivities, IBadgeActivityEditCollection badgeActivities)
        {
            SelectedActivityIds = badgeActivities.Select(bae => bae.ActivityId).ToList();
            var selectedValues = badgeActivities
                .Join(allActivities, bae => bae.ActivityId, ai => ai.Id, (bae, ai) => new { ai = ai })
                .Select(anon => anon.ai);

            AllActivities = new MultiSelectList(allActivities, "Id", "Name", selectedValues);
        }

        /// <summary>
        /// Gets or sets all activities.
        /// </summary>
        /// <value>
        /// All activities.
        /// </value>
        public MultiSelectList AllActivities { get; private set; }

        /// <summary>
        /// Gets or sets the selected activity ids.
        /// </summary>
        /// <value>
        /// The selected activity ids.
        /// </value>
        public List<int> SelectedActivityIds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [has permission].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [has permission]; otherwise, <c>false</c>.
        /// </value>
        public bool HasPermission { get; set; }

        /// <summary>
        /// Gets the default corporate badge URI.
        /// </summary>
        /// <value>
        /// The default corporate badge URI.
        /// </value>
        public Uri DefaultCorporateBadgeUri
        {
            get
            {
                var defaultCorporateBadgeUriString = ConfigurationManager.AppSettings["DefaultCorporateBadge"];
                if (!String.IsNullOrWhiteSpace(defaultCorporateBadgeUriString))
                {
                    return new Uri(defaultCorporateBadgeUriString);
                }

                var httpContextBase = DependencyResolver.Current.GetService<HttpContextBase>();
                return new Uri(httpContextBase.Request.Url, new Uri(Links.Content.Images.unknown_png, UriKind.Relative)); ;
            }
        }

        /// <summary>
        /// Gets the default communit badge URI.
        /// </summary>
        /// <value>
        /// The default communit badge URI.
        /// </value>
        public Uri DefaultCommunitBadgeUri
        {
            get
            {
                var httpContextBase = DependencyResolver.Current.GetService<HttpContextBase>();
                var defaultUri = httpContextBase.Server.MapPath(Links.Content.Images.unknown_png);
                var defaultCommunityBadgeUriString = ConfigurationManager.AppSettings["DefaultCommunityBadge"];
                if (!String.IsNullOrWhiteSpace(defaultCommunityBadgeUriString))
                {
                    return new Uri(defaultCommunityBadgeUriString);
                }

                return new Uri(defaultUri, UriKind.RelativeOrAbsolute);
            }
        }

        /// <summary>
        /// Gets or sets the badge.
        /// </summary>
        /// <value>
        /// The badge.
        /// </value>
        public BadgeEdit Badge { get; set; }
    }
}