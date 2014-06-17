using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Magenic.BadgeApplication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationSettings
    {
        /// <summary>
        /// Gets the badge award values from the configuration file.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<int> BadgeAwardValues()
        {
            var settingsString = ConfigurationManager.AppSettings["BadgeAwardValues"];
            return settingsString.Split(',').Select(s => Int32.Parse(s, CultureInfo.CurrentCulture));
        }
    }
}