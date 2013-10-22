// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using NUnit.Framework;
using Rhino.Mocks;
using Selenol.Elements;

namespace Selenol.Tests.Elements
{
    [TestFixture]
    public class TestPasswordboxElement : BaseTextInputElementTest<PasswordboxElement>
    {
        protected override PasswordboxElement CreateElement()
        {
            return new PasswordboxElement(this.WebElement);
        }

        protected override void SetProperElementConditions()
        {
            base.SetProperElementConditions();
            this.WebElement.Stub(x => x.GetAttribute("type")).Return("password");
        }
    }
}