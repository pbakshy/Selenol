// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Threading;
using Selenol.Extensions;

namespace Selenol.Utils
{
    /// <summary>
    ///     The wait helper class which helps to wait some conditions for a period of time.
    /// </summary>
    internal static class Wait
    {
        private static readonly TimeSpan sleepTime = TimeSpan.FromMilliseconds(100);

        /// <summary>
        /// Repeats the action until it returns true during the period time.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="timeout">The timeout period.</param>
        /// <param name="waitedForPart">If specified adds part to exception by template "Waited for {0}.".</param>
        internal static void For(Func<bool> action, TimeSpan timeout, string waitedForPart = null)
        {
            var sleepedTime = TimeSpan.FromMilliseconds(0);
            while (!action() && sleepedTime < timeout)
            {
                Thread.Sleep(sleepTime);
                sleepedTime += sleepTime;
            }

            if (sleepedTime >= timeout)
            {
                var waitedForPartFull = string.IsNullOrEmpty(waitedForPart)
                                        ? string.Empty
                                        : " Waited for {0}.".F(waitedForPart);
                throw new TimeoutException("Timed out after {0}.{1}".FInv(timeout, waitedForPartFull));
            }
        }
    }
}