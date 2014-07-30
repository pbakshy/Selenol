// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using Selenol.Elements;
using Selenol.Page;
using Selenol.SelectorAttributes;
using Selenol.Validation.Page;

namespace Selenol.Sample
{
    [Url("/")]
    public class RepositoryPage : BasePage
    {
        [Css("h1 span.author")]
        public virtual ContainerElement Author { get; set; }

        [Css("h1 strong")]
        public virtual ContainerElement RepositoryName { get; set; }
    }
}