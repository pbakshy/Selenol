// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Selenol.Page;

namespace Selenol.Validation.Page
{
    /// <summary>Indicates that page does not match restrictions.</summary>
    [Serializable]
    [SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly", Justification = "We do not use any remote calls.")]
    public class PageValidationException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="PageValidationException"/> class.</summary>
        /// <param name="message">The message that describes the error. </param>
        /// <param name="page">The page. </param>
        public PageValidationException(string message, BasePage page)
            : base(message)
        {
            this.Page = page;
        }

        /// <summary>Initializes a new instance of the <see cref="PageValidationException"/> class.</summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="page">The page. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public PageValidationException(string message, BasePage page, Exception innerException)
            : base(message, innerException)
        {
            this.Page = page;
        }

        /// <summary>Initializes a new instance of the <see cref="PageValidationException"/> class.</summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected PageValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>Gets the context page where exception was thrown.</summary>
        public BasePage Page { get; private set; }
    }
}