using System;

namespace Magenic.BadgeApplication.Common.Exceptions
{
    /// <summary>
    /// Exception type used to communicate that a record or object was not found
    /// </summary>
    public class NotFoundException : ApplicationException
    {
        /// <summary>
        /// Creates an instance of NotFoundException with the input message
        /// </summary>
        /// <param name="message"></param>
        public NotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates an instance of NotFoundException with the input message and inner exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public NotFoundException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
