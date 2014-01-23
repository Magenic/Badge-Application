using Magenic.BadgeApplication.Common;
using System;
using System.ComponentModel;
using System.Linq;

namespace Magenic.BadgeApplication.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description from enum value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetDescriptionFromEnumValue(this Enum value)
        {
            Arg.IsNotNull(() => value);

            var attribute = value.GetType()
                        .GetField(value.ToString())
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .SingleOrDefault() as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}