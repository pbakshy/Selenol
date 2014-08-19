using System;
using System.Runtime.Serialization;

namespace Selenol
{
    /// <summary>Indicates that required element was not found.</summary>
    [Serializable]
    public class ElementNotFoundException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="ElementNotFoundException"/> class.</summary>
        /// <param name="message">The message.</param>
        public ElementNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ElementNotFoundException"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ElementNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ElementNotFoundException"/> class.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected ElementNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}