using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Magenic.BadgeApplication.Extensions
{
    /// <summary>
    /// Types of string-literal enclosures for most programming languages.
    /// </summary>
    public enum StringEnclosureType {
        /// <summary>
        /// Indicates that a string literal is or will be enclosed in Single Quotes
        /// </summary>
        SingleQuotes,
        /// <summary>
        /// Indicates that a string literal is or will be enclosed in Double Quotes
        /// </summary>
        DoubleQuotes
    }

    /// <summary>
    /// 
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Gets the file name from path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string GetFileNameFromPath(this string path)
        {
            if (!String.IsNullOrWhiteSpace(path))
            {
                var uri = new Uri(path);
                return uri.AbsolutePath.Split('/').Last();
            }

            return "unknown.png";
        }

        private static Regex jsonEscRegDQ = new Regex(@"[\""\\\/\b\f\n\r\t]", RegexOptions.Compiled);
        private static Regex jsonEscRegSQ = new Regex(@"[\'\\\/\b\f\n\r\t]", RegexOptions.Compiled);
        private static string escMatchEval(Match m) {
            switch (m.Value) {
                case "\b": return @"\b";
                case "\f": return @"\f";
                case "\n": return @"\n";
                case "\r": return @"\r";
                case "\t": return @"\t";
                default: return @"\" + m.Value;
            }
        }
        /// <summary>
        /// Returns the string with any special characters transformed to their appropriate JSON escape sequences.
        /// </summary>
        /// <param name="str">The string to escape</param>
        /// <param name="enclosureType">The type of string-literal enclosure that will be used to enclose the string in JSON or JavaScript code (single or double quotes).
        /// Of the two, only the specified type will be escaped.</param>
        /// <returns></returns>
        public static string JSONEsc(this string str, StringEnclosureType enclosureType) {
            if (enclosureType == StringEnclosureType.DoubleQuotes)
                return jsonEscRegDQ.Replace(str, escMatchEval);
            return jsonEscRegSQ.Replace(str, escMatchEval);
        }
    }
}