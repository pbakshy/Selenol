// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Runtime.Serialization;

namespace Selenol.Validation
{
    /// <summary>The validation absence exception.</summary>
    [Serializable]
    public class ValidationAbsenceException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="ValidationAbsenceException"/> class.</summary>
        /// <param name="message">The message.</param>
        public ValidationAbsenceException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ValidationAbsenceException"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ValidationAbsenceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ValidationAbsenceException"/> class.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected ValidationAbsenceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}