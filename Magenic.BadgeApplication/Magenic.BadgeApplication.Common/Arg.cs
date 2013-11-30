using System;
using System.Linq.Expressions;

namespace Magenic.BadgeApplication.Common
{
    /// <summary>
    /// Static helper methods to validate input arguments
    /// </summary>
    public static class Arg
    {
        /// <summary>
        /// Checks the argument null.
        /// </summary>
        /// <param name="expressions">The expressions.</param>
        public static void IsNotNull(params Expression<Func<Object>>[] expressions)
        {
            if (expressions != null)
            {
                foreach (var expression in expressions)
                {
                    if (expression.Compile().Invoke() == null)
                    {
                        throw new ArgumentNullException((expression.Body as MemberExpression).Member.Name);
                    }
                }
            }
        }
    }
}
