// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Selenol.SelectorAttributes;

namespace Selenol.Extensions
{
    /// <summary>The extensions for reflection types.</summary>
    internal static class ReflectionExtensions
    {
        /// <summary>Checks whether the method info is property getter.</summary>
        /// <param name="methodInfo">The method info.</param>
        /// <returns>True is method info is getter, otherwise false.</returns>
        public static bool IsGetter(this MethodInfo methodInfo)
        {
            return methodInfo.IsSpecialName && methodInfo.Name.StartsWith("get_", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Checks whether the method info is property setter.</summary>
        /// <param name="methodInfo">The method info.</param>
        /// <returns>True is method info is setter, otherwise false.</returns>
        public static bool IsSetter(this MethodInfo methodInfo)
        {
            return methodInfo.IsSpecialName && methodInfo.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Gets property info from specified type by getter or setter method info.</summary>
        /// <param name="rootType">The root type.</param>
        /// <param name="methodInfo">The getter of setter method Info.</param>
        /// <returns>The <see cref="PropertyInfo"/>.</returns>
        public static PropertyInfo GetProperty(this Type rootType, MethodInfo methodInfo)
        {
            return rootType.GetProperty(methodInfo.Name.Substring(4), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        /// <summary>Checks whether the property is auto property.</summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <returns>True if the property is auto property, otherwise false.</returns>
        public static bool IsAutoProperty(this PropertyInfo propertyInfo)
        {
            var getMethod = propertyInfo.GetGetMethod(true);
            if (getMethod == null)
            {
                return false;
            }

            var mightBe = getMethod.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any();
            if (!mightBe)
            {
                return false;
            }

            var maybe = propertyInfo.DeclaringType != null && propertyInfo.DeclaringType
                                                                          .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                                                                          .Where(f => f.Name.Contains(propertyInfo.Name))
                                                                          .Where(f => f.Name.Contains("BackingField"))
                                                                          .Any(
                                                                              f =>
                                                                              f.GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Any());
            return maybe;
        }

        /// <summary>Checks whether the property specified with <see cref="BaseSelectorAttribute"/>.</summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <returns>True if property specified with <see cref="BaseSelectorAttribute"/>, otherwise false.</returns>
        public static bool IsPropertyWithSelectorAttribute(this PropertyInfo propertyInfo)
        {
            return Attribute.GetCustomAttributes(propertyInfo).OfType<BaseSelectorAttribute>().Any();
        }
    }
}