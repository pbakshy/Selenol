// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;

using Selenol.Elements;
using Selenol.Validation.Element;

namespace Selenol.Tests.Validation
{
    public class IgnoreValidationForTestAttribute : Attribute, IElementValidator
    {
        public bool Validate(BaseHtmlElement element)
        {
            return true;
        }

        public string GetErrorMessage(BaseHtmlElement element)
        {
            throw new NotSupportedException();
        }
    }
}