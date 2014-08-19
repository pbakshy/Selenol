// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Runtime.Serialization;

namespace Selenol.Elements
{
    /// <summary>The invalid value exception.</summary>
    [Serializable]
    public class InvalidValueException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="InvalidValueException"/> class.</summary>
        /// <param name="message">The message.</param>
        public InvalidValueException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="InvalidValueException"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InvalidValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="InvalidValueException"/> class.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected InvalidValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}