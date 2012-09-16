// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using NUnit.Framework;

using Rhino.Mocks;

using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestContainerElement : BaseContainerElementTest<ContainerElement>
    {
        protected override ContainerElement CreateElement()
        {
            return new ContainerElement(this.WebElement);
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