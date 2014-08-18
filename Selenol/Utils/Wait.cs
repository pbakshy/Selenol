// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using Selenol.Extensions;

namespace Selenol.Utils
{
    /// <summary>
    ///     The wait helper class which helps to wait some conditions for a period of time.
    /// </summary>
    internal static class Wait
    {
        /// <summary>
        /// Repeats the action until it returns true during the period time.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="timeout">The timeout period.</param>
        /// <param name="waitedForPart">If specified adds part to exception by template "Waited for {0}.".</param>
        internal static void For(Func<bool> action, TimeSpan timeout, string waitedForPart = null)
        {
            if (TryWait.For(action, timeout))
            {
                return;
            }

            var waitedForPartFull = string.IsNullOrEmpty(waitedForPart)
                                        ? string.Empty
                                        : " Waited for {0}.".F(waitedForPart);
            throw new TimeoutException("Timed out after {0}.{1}".FInv(timeout, waitedForPartFull));
        }
    }
}