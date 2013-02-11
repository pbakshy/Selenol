// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Selenol.Page
{
    /// <summary>Indicates that page does not initialized or initialized in a bad way.</summary>
    [Serializable]
    [SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly", Justification = "We do not use any remote calls.")]
    public class PageInitializationException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="PageInitializationException"/> class. Initializes a new instance of the<see cref="T:System.Exception"/>
        ///     class with a specified error message.</summary>
        /// <param name="message">The message that describes the error. </param>
        public PageInitializationException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="PageInitializationException"/> class.</summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public PageInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="PageInitializationException"/> class.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected PageInitializationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}