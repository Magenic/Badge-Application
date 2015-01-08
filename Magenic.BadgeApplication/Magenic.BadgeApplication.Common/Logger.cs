using System.Globalization;
using log4net;
using System;

namespace Magenic.BadgeApplication.Common
{
    /// <summary>
    /// Logging utility for the Badge Application.  This is intended to provide a single point of contact with the chosen logging
    /// framework for the application.  Currently the framework being used is log4Net
    /// </summary>
    public static class Logger
    {
        static Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        
        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <typeparam name="T">The type of the class logging the message</typeparam>
        /// <param name="message">The message to log</param>
        public static void Debug<T>(string message)
        {
            Debug<T>(message, null);
        }

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <typeparam name="T">The type of the class logging the message</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="ex">The exception to log</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static void Debug<T>(string message, Exception ex)
        {
            ILog logger = GetLogger<T>();

            logger.Debug(message, ex);
        }

        /// <summary>
        /// Logs an info message
        /// </summary>
        /// <typeparam name="T">The type of the class logging the message</typeparam>
        /// <param name="message">The message to log</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static void Info<T>(string message)
        {
            Info<T>(message, null);
        }

        /// <summary>
        /// Logs an info message
        /// </summary>
        /// <typeparam name="T">The type of the class logging the message</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="ex">The exception to log</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static void Info<T>(string message, Exception ex)
        {
            ILog logger = GetLogger<T>();

            logger.Info(message, ex);
        }

        /// <summary>
        /// Logs a formatted informational message 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">A formatted string</param>
        /// <param name="args">The argument array</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static void InfoFormat<T>(string message, params object[] args)
        { 
            Info<T>(string.Format(CultureInfo.CurrentCulture, message, args));
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <typeparam name="T">The type of the class logging the message</typeparam>
        /// <param name="message">The message to log</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static void Warn<T>(string message)
        {
            Warn<T>(message, null);
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <typeparam name="T">The type of the class logging the message</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="ex">The exception to log</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static void Warn<T>(string message, Exception ex)
        {
            ILog logger = GetLogger<T>();

            logger.Warn(message, ex);
        }

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <typeparam name="T">The type of the class logging the message</typeparam>
        /// <param name="message">The message to log</param>
        public static void Error<T>(string message)
        {
            Error<T>(message, null);
        }

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <typeparam name="T">The type of the class logging the message</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="ex">The exception to log</param>
        public static void Error<T>(string message, Exception ex)
        {
            ILog logger = GetLogger<T>();

            logger.Error(message, ex);
        }

        /// <summary>
        /// Logs a fatal error
        /// </summary>
        /// <typeparam name="T">The type of the class logging the message</typeparam>
        /// <param name="message">The message to log</param>
        public static void Fatal<T>(string message)
        {
            Fatal<T>(message, null);
        }

        /// <summary>
        /// Logs a fatal error
        /// </summary>
        /// <typeparam name="T">The type of the class logging the message</typeparam>
        /// <param name="message">The message to log</param>
        /// <param name="ex">The exception to log</param>
        public static void Fatal<T>(string message, Exception ex)
        {
            ILog logger = GetLogger<T>();

            logger.Fatal(message, ex);
        }

        /// <summary>
        /// Gets the Log4Net logger based on the input type
        /// </summary>
        /// <typeparam name="T">The type to use when fetching the logger</typeparam>
        /// <returns>The logger</returns>
        private static ILog GetLogger<T>()
        {
            return LogManager.GetLogger(typeof(T));
        }
    }
}