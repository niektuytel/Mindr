﻿using Mindr.Core.Enums;

namespace Mindr.Core.Models.HttpCollection
{
    public class HttpVariable: PostmanVariable
    {
        public VariablePosition Location { get; set; }
    }
}