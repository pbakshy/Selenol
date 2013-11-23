// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Runtime.Serialization;

namespace Selenol
{
    /// <summary>Indicates that waiting operation finished with timeout.</summary>
    [Serializable]
    public class TimeoutException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="TimeoutException"/> class.</summary>
        /// <param name="message">The message.</param>
        public TimeoutException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TimeoutException"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public TimeoutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TimeoutException"/> class.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected TimeoutException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}