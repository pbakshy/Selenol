// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Selenol.Extensions;

namespace Selenol.SelectorAttributes
{
    /// <summary>The interseptor selector which selects <see cref="SelectorInterceptor"/> only where it needs.</summary>
    internal class InterseptorSelector : IInterceptorSelector
    {
        /// <summary>Select interseptors which must be applied to the method.</summary>
        /// <param name="type">The type.</param>
        /// <param name="method">The method.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>The interseptors.</returns>
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            if (method.IsGetter())
            {
                var propertyInfo = type.GetProperty(method);
                if (propertyInfo.IsPropertyWithSelectorAttribute())
                {
                    return interceptors.Where(x => x is SelectorInterceptor).ToArray();
                }
            }

            return interceptors.Where(x => !(x is SelectorInterceptor)).ToArray();
        }
    }
}