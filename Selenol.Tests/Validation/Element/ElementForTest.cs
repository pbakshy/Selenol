// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using OpenQA.Selenium;

using Selenol.Elements;

namespace Selenol.Tests.Validation.Element
{
    [IgnoreValidationForTest]
    public class ElementForTest : BaseHtmlElement
    {
        public ElementForTest(IWebElement webElement)
            : base(webElement)
        {
        } 
    }
}