using Magenic.BadgeApplication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Exceptions
{
    /// <summary>
    /// This exception that is thrown when there are model state errors.
    /// <remarks>
    /// Based on content from this site http://erraticdev.blogspot.com/2010/11/handling-validation-errors-on-ajax.html
    /// </remarks>
    /// </summary>
    [Serializable]
    public class ModelStateException
        : Exception
    {
        /// <summary>
        /// Gets the model errors.
        /// </summary>
        public Dictionary<string, string> Errors { get; private set; }

        /// <summary>
        /// Gets a message that describes the current exception and is related to the first model state error.
        /// </summary>
        public override string Message
        {
            get
            {
                if (this.Errors.Count > 0)
                {
                    return this.Errors.First().Value;
                }

                return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateException"/> class.
        /// </summary>
        public ModelStateException()
            : base()
        {
            this.Errors = new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateException"/> class.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        public ModelStateException(ModelStateDictionary modelState)
            : this()
        {
            Arg.IsNotNull(() => modelState);

            //this.ModelState = modelState;
            if (!modelState.IsValid)
            {
                StringBuilder errors;
                foreach (KeyValuePair<string, ModelState> state in modelState)
                {
                    if (state.Value.Errors.Count > 0)
                    {
                        errors = new StringBuilder();
                        foreach (ModelError err in state.Value.Errors)
                        {
                            errors.AppendLine(err.ErrorMessage);
                        }

                        this.Errors.Add(state.Key, errors.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected ModelStateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            // deserialize
            this.Errors = info.GetValue("ModelStateException.Errors", typeof(Dictionary<string, string>)) as Dictionary<string, string>;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ModelStateException(string message)
            : base(message)
        {
            this.Errors = new Dictionary<string, string>();
            this.Errors.Add(string.Empty, message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelStateException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ModelStateException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.Errors = new Dictionary<string, string>();
            this.Errors.Add(string.Empty, message);
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic).
        /// </exception>
        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Arg.IsNotNull(() => info);
            Arg.IsNotNull(() => context);

            // serialize errors
            info.AddValue("ModelStateException.Errors", this.Errors, typeof(Dictionary<string, string>));
            base.GetObjectData(info, context);
        }
    }
}