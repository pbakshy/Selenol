// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using Selenol.Elements;
using Selenol.Page;
using Selenol.SelectorAttributes;
using Selenol.Validation.Page;

namespace Selenol.Sample
{
    [Url("/search")]
    public class SearchPage : BasePage
    {
        [Id("search_form")]
        public virtual SearchFormControl SearchForm { get; set; }

        [Css("h3.repolist-name a")]
        public virtual IEnumerable<LinkElement> Results { get; set; } 
    }
}