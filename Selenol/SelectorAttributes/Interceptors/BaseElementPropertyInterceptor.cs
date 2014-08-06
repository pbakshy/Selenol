// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using OpenQA.Selenium;
using Selenol.Extensions;

namespace Selenol.SelectorAttributes.Interceptors
{
    /// <summary>
    /// The base selector interceptor. It intercepts property getter and select elements by selector from <see cref="BaseSelectorAttribute"/>.
    /// Can cache the result if need.
    /// </summary>
    internal abstract class BaseElementPropertyInterceptor : IInterceptor
    {
        private IDictionary<PropertyInfo, object> propertyValueCache;

        /// <summary>The intercept.</summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            var methodInfo = invocation.Method;
            var propertyInfo = invocation.TargetType.GetProperty(methodInfo);
            var selectorAttribute = Attribute.GetCustomAttributes(propertyInfo).OfType<BaseSelectorAttribute>().First();

            var page = invocation.InvocationTarget;
            invocation.ReturnValue = this.UseCacheIfNeed(propertyInfo, page, selectorAttribute);
        }

        /// <summary>Selects value for the proxied property.</summary>
        /// <param name="propertyType">The property type.</param>
        /// <param name="context">The invocation context, can be page or control.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The property value.</returns>
        protected abstract object SelectPropertyValue(Type propertyType, object context, By selector);

        private object UseCacheIfNeed(PropertyInfo propertyInfo, object context, BaseSelectorAttribute selectorAttribute)
        {
            if (!selectorAttribute.CacheValue)
            {
                return this.SelectPropertyValue(propertyInfo.PropertyType, context, selectorAttribute.Selector);
            }

            if (this.propertyValueCache == null)
            {
                this.propertyValueCache = new Dictionary<PropertyInfo, object>();
            }

            if (!this.propertyValueCache.ContainsKey(propertyInfo))
            {
                this.propertyValueCache[propertyInfo] = this.SelectPropertyValue(propertyInfo.PropertyType, context, selectorAttribute.Selector);
            }

            return this.propertyValueCache[propertyInfo];
        }
    }
}