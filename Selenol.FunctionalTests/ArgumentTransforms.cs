// ﻿Copyright (c) Pavel Bakshy, Valeriy Ogiy. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Linq;

using TechTalk.SpecFlow;

namespace Selenol.FunctionalTests
{
    [Binding]
    public class ArgumentTransforms
    {
        [StepArgumentTransformation]
        public string[] StringCollectionTransform(string value)
        {
            return value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
        }
    }
}