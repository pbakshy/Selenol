// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

using Selenol.Elements;
using Selenol.Validation.Element;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestElementDeclareRules
    {
        private static IEnumerable<Type> ElementTypes
        {
            get
            {
                var selenolAssembly = typeof(ButtonElement).Assembly;
                return selenolAssembly.GetTypes().Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(BaseHtmlElement)));
            }
        }

        [Test] 
        public void AllElementsHaveValidation()
        {
            foreach (var type in ElementTypes)
            {
                type.GetCustomAttributes(false).OfType<IElementValidator>().Should().NotBeEmpty("The element '{0}' should have at least one validation attribute.", type);
            }
        }

        [Test]
        public void AllElementsHaveProperConstructor()
        {
            foreach (var type in ElementTypes)
            {
                var constructor = type.GetConstructors().FirstOrDefault(x => x.GetParameters().Length == 1 && x.GetParameters()[0].ParameterType == typeof(IWebElement));
                constructor.Should().NotBeNull("The element '{0}' should have a constructor with one parameter of type IWebElement.", type);
            }
        }
    }
}