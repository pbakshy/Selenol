// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using Selenol.Elements;
using Selenol.Page;
using Selenol.SelectorAttributes;
using Selenol.Validation.Page;

namespace Selenol.Sample
{
    [Url("/")]
    public class MainPage : BasePage
    {
        [Id("js-command-bar-field")]
        public virtual TextboxElement SearchBox { get; set; }
    }
}