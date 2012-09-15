﻿// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestGenericContainerElement : BaseHtmlElementTest<GenericContainerElement>
    {
        [Test]
        public void GetText()
        {
            this.WebElement.Stub(x => x.Text).Return("abc");
            this.TypedElement.Text.Should().Be("abc");
        }

        protected override GenericContainerElement CreateElement()
        {
            return new GenericContainerElement(this.WebElement);
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("div");
        }

        protected override void SetWrongElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("input");
        }
    }
}