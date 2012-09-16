// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Globalization;

namespace Selenol.Extensions
{
    /// <summary>The string extensions.</summary>
    public static class StringExtensions
    {
        /// <summary>Extension method for <see cref="string.IsNullOrEmpty"/>.</summary>
        /// <param name="value">The value. </param>
        /// <returns>Returns true if string is null or empty otherwise false. </returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>Formats string with <see cref="CultureInfo.CurrentCulture"/>.</summary>
        /// <param name="format">The format. </param>
        /// <param name="parameters">The parameters. </param>
        /// <returns>The formatted string. </returns>
        public static string F(this string format, params object[] parameters)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            return string.Format(CultureInfo.CurrentCulture, format, parameters);
        }

        /// <summary>Formats string with <see cref="CultureInfo.InvariantCulture"/>.</summary>
        /// <param name="format">The format. </param>
        /// <param name="parameters">The parameters. </param>
        /// <returns>The formatted string. </returns>
        public static string FInv(this string format, params object[] parameters)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            return string.Format(CultureInfo.InvariantCulture, format, parameters);
        }
    }
}