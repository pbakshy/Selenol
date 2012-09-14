// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using NUnit.Framework;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestFormElement : BaseHtmlElementTest<FormElement>
    {
        [Test]
        public void Submit()
        {
            this.TypedElement.Submit();
            this.WebElement.AssertWasCalled(x => x.Submit());
        }

        protected override FormElement CreateElement()
        {
            return new FormElement(this.WebElement);
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("form");
        }
    }
}