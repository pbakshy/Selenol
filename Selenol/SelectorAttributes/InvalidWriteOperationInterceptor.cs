// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using Castle.DynamicProxy;
using Selenol.Extensions;

namespace Selenol.SelectorAttributes
{
    /// <summary>The not supported interceptor which throws <see cref="NotSupportedException"/>.</summary>
    internal class InvalidWriteOperationInterceptor : IInterceptor
    {
        /// <summary>The intercept.</summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            var propertyInfo = invocation.TargetType.GetProperty(invocation.Method);
            throw new InvalidOperationException("Can not set value for the property '{0}' because it is used with selector attribute.".F(propertyInfo.Name));
        }
    }
}