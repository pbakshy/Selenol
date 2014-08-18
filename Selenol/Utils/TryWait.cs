// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Threading;

namespace Selenol.Utils
{
    /// <summary>
    ///     The wait helper class which helps to wait some conditions for a period of time. Does not throw after timeout.
    /// </summary>
    internal static class TryWait
    {
        private static readonly TimeSpan sleepTime = TimeSpan.FromMilliseconds(100);

        /// <summary>
        /// Repeats the action until it returns true during the period time. Does not throw after timeout.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="timeout">The timeout period.</param>
        /// <returns>The true if action returned true before timeout, otherwise false.</returns>
        internal static bool For(Func<bool> action, TimeSpan timeout)
        {
            var sleepedTime = TimeSpan.FromMilliseconds(0);
            while (!action() && sleepedTime < timeout)
            {
                Thread.Sleep(sleepTime);
                sleepedTime += sleepTime;
            }

            return sleepedTime < timeout;
        } 
    }
}