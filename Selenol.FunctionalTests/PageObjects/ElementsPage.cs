// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using Selenol.Elements;
using Selenol.Page;
using Selenol.SelectorAttributes;
using Selenol.Validation.Page;

namespace Selenol.FunctionalTests.PageObjects
{
    [Url("/elements.html")]
    public class ElementsPage : BasePage
    {
        [Id("select-1")]
        public virtual SelectElement FirstSelect { get; protected set; }

        [TagName("button")]
        public virtual ButtonElement SecondButton { get; protected set; }

        [Name("radio")]
        public virtual IEnumerable<RadioButtonElement> RadioButtons { get; protected set; }

        [Class("checkbox")]
        public virtual CheckboxElement SecondCheckbox { get; protected set; }
    }
}