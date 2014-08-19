// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using FluentAssertions;

using NUnit.Framework;

using Rhino.Mocks;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestButtonElementHtmlButton : BaseButtonElementTest 
    {
        [Test]
        public void GetText()
        {
            this.WebElement.Stub(x => x.Text).Return("abc");
            this.TypedElement.Text.Should().Be("abc");
        }

        protected override void SetProperElementConditions()
        {
            this.WebElement.Stub(x => x.TagName).Return("button");
        }
    }
}